using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Exercise_005_InfoSystem
{
    struct Company
    {
        private const string NoNameDepartment = "Нет департамента";
        private const string DefaultDate = "01.01.2000";

        /// <summary>
        /// Список департаментов (словарь)
        /// </summary>
        private List<Department> Departments;

        /// <summary>
        /// Список сотрудников
        /// </summary>
        private List<Employee> Employees;

        public string Name { get; set; }

        public Company(string name)
        {
            this.Name = name;
            this.Departments = new List<Department>();
            this.Employees = new List<Employee>();
            AddDepartment(NoNameDepartment);
        }


        /// <summary>
        /// Определение формата сериализованных данных в файле
        /// запуск соответствующего импорта
        /// </summary>
        /// <returns></returns>
        public bool Import(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = sr.ReadLine();
                    char symbol = line[0];
                    if (symbol == '[' || symbol == '{') return ImportJSON(path);
                    else if (symbol == '<') return ImportXML(path);
                    else return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region Сруктура данных
        // Company(name)
        //   Department
        //      Name
        //      CreationDate
        //   Employee
        //      FirstName
        //      LastName
        //      DepartmentName
        //      Age
        //      Salary
        //      NumProj
        #endregion
        /// <summary>
        /// Импорт из файла в формате XML
        /// </summary>
        /// <returns></returns>
        private bool ImportXML(string path)
        {
            try
            {
                string xml = File.ReadAllText(path);
                // Компания
                string newCompanyName = XDocument.Parse(xml).Element("Company").Attribute("CompanyName").Value;
                if (ChangeCompanyName(newCompanyName))
                {
                    this.Name = newCompanyName;
                }

                // Департаменты
                var xDep = XDocument.Parse(xml)
                                    .Descendants("Company")
                                    .Descendants("Departments")
                                    .Descendants("Department")
                                    .ToList();
                
                int depId = Departments[0].GetLastId();
                int depIdFinaly;
                foreach (var itemDep in xDep)
                {
                    if (AddDepartment(itemDep.Attribute("Name").Value, itemDep.Attribute("CreationDate").Value)) 
                    { 
                        depId++;
                        depIdFinaly = depId;
                    }
                    else
                    {
                        depIdFinaly = Departments.Find(d => d.Name == itemDep.Attribute("Name").Value).Id;
                    }
                    // Сотрудники
                    var xEmpList = itemDep.Descendants("Employees")
                                          .Descendants("Employee")
                                          .ToList();
                    foreach (var itemEmp in xEmpList)
                    {
                        AddEmployee(itemEmp.Attribute("FirstName").Value,
                                    itemEmp.Attribute("LastName").Value,
                                    itemEmp.Attribute("Age").Value,
                                    depIdFinaly,
                                    itemEmp.Attribute("Salary").Value,
                                    itemEmp.Attribute("NumProj").Value);
                    }


                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Импорт из файла в формате JSON
        /// </summary>
        /// <returns></returns>
        private bool ImportJSON(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                
                string newCompanyName = JObject.Parse(json)["CompanyName"].ToString();
                if (ChangeCompanyName(newCompanyName))
                {
                    this.Name = newCompanyName;
                }

                // Департаменты
                var jDepartments = JObject.Parse(json)["Departments"].ToArray();
                
                int depId = Departments[0].GetLastId();
                int depIdFinaly;
                foreach (var item in jDepartments)
                {
                    if (AddDepartment(item["Name"].ToString(), item["CreationDate"].ToString()))
                    {
                        depId++;
                        depIdFinaly = depId;
                    }
                    else
                    {
                        depIdFinaly = Departments.Find(d => d.Name == item["Name"].ToString()).Id;
                    }

                    // Сотрудники
                    foreach (var emp in item["Employees"].ToArray())
                    {
                        AddEmployee(emp["FirsName"].ToString(),
                                    emp["LastName"].ToString(),
                                    emp["Age"].ToString(),
                                    depIdFinaly,
                                    emp["Salary"].ToString(),
                                    emp["NumProj"].ToString());
                    }
                }

                return true;
            } 
            catch (Exception ex)
            {
                return false;
            }
            
        }


        /// <summary>
        /// Экспорт данныйх в XML
        /// </summary>
        /// <returns></returns>
        public bool ExportXML(string path)
        {
            // Департаменты
            XElement xDepList = new XElement("Departments");
            foreach (var itemDep in Departments)
            {
                XAttribute xName = new XAttribute("Name", itemDep.Name);
                XAttribute xDate = new XAttribute("CreationDate", itemDep.CreationDate);
                XElement xDepartment = new XElement("Department");

                // Сотрудники
                XElement xEmpList = new XElement("Employees");
                foreach (var itemEmp in Employees)
                {
                    if (itemDep.Id != itemEmp.DepartemntId) continue;
                    XAttribute xFN = new XAttribute("FirstName", itemEmp.FirstName);
                    XAttribute xLN = new XAttribute("LastName", itemEmp.LastName);
                    XAttribute xAge = new XAttribute("Age", itemEmp.Age);
                    XAttribute xSalary = new XAttribute("Salary", itemEmp.Salary);
                    XAttribute xNumProj = new XAttribute("NumProj", itemEmp.NumProj);
                    XElement xEmployee = new XElement("Employee");
                    xEmployee.Add(xFN, xLN, xAge, xSalary, xNumProj);
                    xEmpList.Add(xEmployee);
                }

                xDepartment.Add(xName, xDate, xEmpList);
                xDepList.Add(xDepartment);
            }
            
            XElement xCompany = new XElement("Company");
            XAttribute xCompanyName = new XAttribute("CompanyName", Name);
            xCompany.Add(xCompanyName, xDepList);
            XDocument xDocument = new XDocument();
            xDocument.Add(xCompany);
            
            try
            {
                xDocument.Save(path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Экспорт данных в JSON
        /// </summary>
        /// <returns></returns>
        public bool ExportJSON(string path)
        {
            JObject jCompany = new JObject();
            jCompany["CompanyName"] = Name;
            // Департаменты
            JArray jDepartments = new JArray();
            foreach (var itemDep in Departments)
            {
                //if (itemDep.Id == 1) continue;
                JObject jDep = new JObject();
                jDep["Name"] = itemDep.Name;
                jDep["CreationDate"] = itemDep.CreationDate;

                // Сотрудники
                JArray jEmployees = new JArray();
                foreach (var itemEmp in Employees)
                {
                    // Если не департамент не сотрудника
                    if (itemDep.Id != itemEmp.DepartemntId) continue;

                    JObject jEmp = new JObject();
                    jEmp["FirsName"] = itemEmp.FirstName;
                    jEmp["LastName"] = itemEmp.LastName;
                    jEmp["Age"] = itemEmp.Age;
                    jEmp["Salary"] = itemEmp.Salary;
                    jEmp["NumProj"] = itemEmp.NumProj;
                    jEmployees.Add(jEmp);
                }
                jDep["Employees"] = jEmployees;


                jDepartments.Add(jDep);
            }
            jCompany["Departments"] = jDepartments;
            
            string json = jCompany.ToString();

            try
            {
                File.WriteAllText(path, json);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Добавление департамента.
        /// </summary>
        /// <param name="name">Название департамента</param>
        /// <param name="dateTime">Дата создания</param>
        /// <returns>
        /// false - такой департамент уже существует.
        /// </returns>
        public bool AddDepartment(string name, string dateTime = DefaultDate)
        {
            if (Departments.Any(d => d.Name == name)) return false;
            Departments.Add(new Department(name, DateTime.Parse(dateTime)));
            return true;
        }

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="depId">Id департамента</param>
        /// <param name="salary">Зраплата</param>
        /// <param name="numProj">Количество проектов</param>
        /// <returns>
        /// 0 - такой сотрудник уже существует, 
        /// 1 - сотрудник добавлен
        /// 2 - Указанного департамента не существует. Сотрудник добавлен в "Нет департамента".
        /// 3 - В указанном департаменте превышен лимит сотрудников. Сотрудник добавлен в "Нет департамента".
        /// 4 - Невозможно добавить сотрудника без департамента. Лимит превышен.
        /// </returns>
        public byte AddEmployee(string firstName, string lastName, string age, int depId, string salary, string numProj)
        {
            byte result = 0;

            foreach (Employee item in Employees)
            {
                if (item.FirstName.Equals(firstName) && item.LastName.Equals(lastName) && item.Age.Equals(int.Parse(age))) return 0;
            }

            // Если департамент существует, проверим можно в него добавить сотрудника или нельзя
            // Если департамента не существует проверим можно добавить сотрудника без департамента
            
            if (Departments.Any(d => d.Id == depId))
            {
                Department tempDep = Departments.Find(d => d.Id == depId);
                int index = Departments.IndexOf(tempDep);
                if (tempDep.AddEmployee())
                {
                    Departments[index] = tempDep;
                    result = 1;
                }
                else
                {
                    tempDep = Departments[0]; 
                    if (!tempDep.AddEmployee()) return 4;
                    index = Departments.IndexOf(tempDep);
                    Departments[index] = tempDep;
                    result = 3;
                }
            }
            else
            {

                Department tempDep = Departments[0]; 
                int index = Departments.IndexOf(tempDep);
                if (!tempDep.AddEmployee()) return 4;
                Departments[index] = tempDep;
                result = 2;
            }
            Employees.Add(new Employee(firstName, lastName, int.Parse(age), depId, int.Parse(salary), int.Parse(numProj)));
            return result;
        }

        /// <summary>
        /// Удаление департамента
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        /// false - нельзя удалить несуществующий департамент или департамент с сотрудниками
        /// </returns>
        public bool RemoveDepartment(int depId)
        {
            // Нельзя удалить департамент в котором есть сотрудники, или котороного не существует
            if (!Departments.Any(d => d.Id == depId) || Departments.Find(d => d.Id == depId).EmployeesCount() != 0) return false;

            Departments.Remove(Departments.Find(d => d.Id == depId));
            return true;
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Число удаленных элементов</returns>
        public int RemoveEmployee(int id)
        {
            int depId = 0;
            foreach (Employee item in Employees)
            {
                if (item.Id == id)
                {
                    depId = item.DepartemntId;
                    if (item.GetLastId() == id) item.CorrectCount();
                }
            }

            if (depId == 0) return 0;

            Department tempDep = Departments.Find(d => d.Id == depId);
            int index = Departments.IndexOf(tempDep);
            tempDep.RemoveEmployee();
            Departments[index] = tempDep;
            
            return Employees.RemoveAll(x => x.Id == id);
        }

        /// <summary>
        /// Изменение департамента
        /// </summary>
        /// <param name="depId">id</param>
        /// <param name="newName">Новое название</param>
        /// <param name="newDate">Новая дата создания</param>
        /// <returns>
        /// 0 - департамент с названием <name> не существует
        /// 1 - департамент с названием <newName> уже существует
        /// 2 - департамент успешно переименован
        /// </returns>
        public byte EditDepartment(int depId, string newName, string newDate)
        {
            if (!Departments.Any(d => d.Id == depId)) return 0;
            if (Departments.Any(d => d.Name == newName)) return 1;


            Department tempDep = Departments.Find(d => d.Id == depId);
            int index = Departments.IndexOf(tempDep);
            if (newName != string.Empty)
            {
                tempDep.Name = newName;
            }
            if (newDate != string.Empty) tempDep.CreationDate = DateTime.Parse(newDate);
            Departments[index] = tempDep;

            return 2;
        }

        /// <summary>
        /// Изменение полей сотрудника. Если поля пустые, то изменения не вносятся
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <param name="newFN">Новое имя</param>
        /// <param name="newLN">Новая фамилия</param>
        /// <param name="newAge">Новый возраст</param>
        /// <param name="newDepId">Id нового департамента</param>
        /// <param name="newSalary">Новая зарплата</param>
        /// <param name="newNumProj">Новое количество проектов</param>
        /// <returns>true - успешно, false - сотрудник с указанным id не найден или id пустой</returns>
        public bool EditEmployee(int id, string newFN, string newLN, string newAge, int newDepId, string newSalary, string newNumProj)
        {
            // вернем false, если такого id нет
            if (!Employees.Any(e => e.Id == id)) return false;
            
            Employee tempEmp = Employees.Find(e => e.Id == id);
            int indexEmp = Employees.IndexOf(tempEmp);
            Department tempDep = Departments.Find(d => d.Id == tempEmp.DepartemntId);
            int indexDep = Departments.IndexOf(tempDep);

            if (newFN != string.Empty) tempEmp.FirstName = newFN;
            if (newLN != string.Empty) tempEmp.LastName = newLN;
            if (newAge != string.Empty) tempEmp.Age = int.Parse(newAge);
            if (newDepId != 0)
            {
                tempDep.RemoveEmployee();
                Departments[indexDep] = tempDep;
                
                tempDep = Departments.Find(d => d.Id == newDepId);
                indexDep = Departments.IndexOf(tempDep);
                if (indexDep == -1) return false;
                tempDep.AddEmployee();
                Departments[indexDep] = tempDep;
                tempEmp.DepartemntId = newDepId;
            }
            if (newSalary != string.Empty) tempEmp.Salary = int.Parse(newSalary);
            if (newNumProj != string.Empty) tempEmp.NumProj = int.Parse(newNumProj);
            Employees[indexEmp] = tempEmp;
            return true;
        }

        /// <summary>
        /// Сортировка
        /// </summary>
        /// <param name="sortCase">Случай сортировки по полю(ям)
        /// FieldTOSort.ID - по id
        /// FieldTOSort.AGE - по возрасту
        /// FieldTOSort.AGE_SALARY - по возрасту и зарплате
        /// FieldTOSort.DEPART_AGE_SALARY - по возрасту и зарплате в рамках одного департамента
        /// </param>
        public void SortEmployees(int sortCase = FieldTOSort.ID)
        {
            switch (sortCase)
            {
                case FieldTOSort.ID:
                    Employees = Employees.OrderBy(i => i.Id).ToList<Employee>();
                    break;

                case FieldTOSort.AGE:
                    Employees = Employees.OrderBy(i => i.Age).ToList<Employee>();
                    break;

                case FieldTOSort.AGE_SALARY:
                    Employees = Employees.OrderBy(i => i.Age).ThenBy(i => i.Salary).ToList<Employee>();
                    break;

                case FieldTOSort.DEPART_AGE_SALARY:
                    Employees = Employees.OrderBy(i => i.DepartemntId).ThenBy(i => i.Age).ThenBy(i => i.Salary).ToList<Employee>();
                    break;
            }
        }

        /// <summary>
        /// Вывод данных в консоль
        /// </summary>
        public void PrintAll()
        
        {
            // Вывод списка департаментов
            PrintDepartmentsList();

            // Ввод списка сотрудниковs
            PrintEmployeesList();
        }


        public void PrintEmployeesList()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Сотрудники компании:");
            Console.WriteLine("     №            Имя        Фамилия     Возраст        Департамент     Оплата труда    Количество проектов");
            foreach (var item in Employees)
            {
                Console.WriteLine("{0,6}{1,15}{2,15}{3,12}{4,19}{5,17}{6,23}",
                    item.Id, item.FirstName, item.LastName, item.Age, GetNameDepById(item.DepartemntId), item.Salary, item.NumProj);
            }
        }

        public void PrintDepartmentsList()
        {
            // Вывод списка департаментов
            Console.WriteLine("Департаменты:");
            Console.WriteLine("    id          Название            Дата создания        Количество сотрудников");
            foreach (var item in Departments)
            {
                Console.WriteLine("{0,6}{1,18}{2,25}{3,30}", item.Id, item.Name, item.CreationDate.ToString("d"), item.EmployeesCount());
            }
        }

        public string GetNameDepById(int id)
        {
            return Departments.Find(d => d.Id == id).Name;
        }


        public bool ChangeCompanyName(string name)
        {
            if (this.Name == name) return false;
            Console.Write("Изменить название компании {0} на {1}? (y/n): ", this.Name, name);
            string key = Console.ReadLine();
            if (key.ToLower() == "y") return true;
            return false;
        }
    }
}
