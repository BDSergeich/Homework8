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
        /// Key<string> - Название департамента (Department.Name) в нижнем регистре
        /// </summary>
        private Dictionary<string, Department> Departments;
        //private List<Department> Departments;

        /// <summary>
        /// Список сотрудников
        /// </summary>
        private List<Employee> Employees;

        public string Name { get; set; }

        public Company(string name)
        {
            this.Name = name;
            this.Departments = new Dictionary<string, Department>();
            //this.Departments = new List<Department>();
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
                this.Name = XDocument.Parse(xml).Element("Company").Attribute("CompanyName").Value;

                // Департаменты
                var xDep = XDocument.Parse(xml)
                                    .Descendants("Company")
                                    .Descendants("Departments")
                                    .Descendants("Department")
                                    .ToList();
                foreach (var item in xDep)
                {
                    AddDepartment(item.Attribute("Name").Value, item.Attribute("CreationDate").Value);
                }
                // Сотрудники
                var xEmp = XDocument.Parse(xml)
                                    .Descendants("Company")
                                    .Descendants("Employees")
                                    .Descendants("Employee")
                                    .ToList();
                foreach (var item in xEmp)
                {
                    AddEmployee(item.Attribute("FirstName").Value, 
                                item.Attribute("LastName").Value, 
                                item.Attribute("Age").Value, 
                                item.Attribute("DepartmentName").Value, 
                                item.Attribute("Salary").Value, 
                                item.Attribute("NumProj").Value);
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

                this.Name = JObject.Parse(json)["CompanyName"].ToString();

                // Департаменты
                var jDepartments = JObject.Parse(json)["Departments"].ToArray();
                foreach (var item in jDepartments)
                {
                    AddDepartment(item["Name"].ToString(), item["CreationDate"].ToString());
                }

                // Сотрудники
                var jEmployees = JObject.Parse(json)["Employees"].ToArray();
                foreach (var item in jEmployees)
                {
                    AddEmployee(item["FirsName"].ToString(),
                                item["LastName"].ToString(),
                                item["Age"].ToString(),
                                item["DepartemntName"].ToString(),
                                item["Salary"].ToString(),
                                item["NumProj"].ToString());
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
            foreach (var item in Departments)
            {
                if (item.Key == NoNameDepartment.ToLower()) continue;
                //if (item.Name == NoNameDepartment) continue;
                XAttribute xName = new XAttribute("Name", item.Value.Name);
                XAttribute xDate = new XAttribute("CreationDate", item.Value.CreationDate);
                //XAttribute xName = new XAttribute("Name", item.Name);
                //XAttribute xDate = new XAttribute("CreationDate", item.CreationDate);
                XElement xDepartment = new XElement("Department");
                xDepartment.Add(xName, xDate);
                xDepList.Add(xDepartment);
            }
            
            // Сотрудники
            XElement xEmpList = new XElement("Employees");
            foreach (var item in Employees)
            {
                XAttribute xFN = new XAttribute("FirstName", item.FirstName);
                XAttribute xLN = new XAttribute("LastName", item.LastName);
                XAttribute xAge = new XAttribute("Age", item.Age);
                XAttribute xDN = new XAttribute("DepartmentName", item.DepartemntName);
                XAttribute xSalary = new XAttribute("Salary", item.Salary);
                XAttribute xNumProj = new XAttribute("NumProj", item.NumProj);
                XElement xEmployee = new XElement("Employee");
                xEmployee.Add(xFN, xLN, xAge, xDN, xSalary, xNumProj);
                xEmpList.Add(xEmployee);
            }
            XElement xCompany = new XElement("Company");
            XAttribute xCompanyName = new XAttribute("CompanyName", Name);
            xCompany.Add(xCompanyName, xDepList, xEmpList);
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
            foreach (var item in Departments)
            {
                if (item.Key == NoNameDepartment.ToLower()) continue;
                //if (item.Name == NoNameDepartment.ToLower()) continue;
                JObject jDep = new JObject();
                jDep["Name"] = item.Value.Name;
                jDep["CreationDate"] = item.Value.CreationDate;
                //jDep["Name"] = item.Name;
                //jDep["CreationDate"] = item.CreationDate;

                jDepartments.Add(jDep);
            }
            jCompany["Departments"] = jDepartments;
            
            // Сотрудники
            JArray jEmployees = new JArray();
            foreach (var item in Employees)
            {
                JObject jEmp = new JObject();
                jEmp["FirsName"] = item.FirstName;
                jEmp["LastName"] = item.LastName;
                jEmp["Age"] = item.Age;
                jEmp["DepartemntName"] = item.DepartemntName;
                jEmp["Salary"] = item.Salary;
                jEmp["NumProj"] = item.NumProj;
                jEmployees.Add(jEmp);
            }

            jCompany["Employees"] = jEmployees;

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
            if (Departments.ContainsKey(name.ToLower())) return false;
            Departments.Add(name.ToLower(), new Department(name, DateTime.Parse(dateTime)));
            //if (Departments.Any(d => d.Name == name)) return false;
            //Departments.Add(new Department(name, DateTime.Parse(dateTime)));
            return true;
        }

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="depName">Название департамента</param>
        /// <param name="salary">Зраплата</param>
        /// <param name="numProj">Количество проектов</param>
        /// <returns>
        /// 0 - такой сотрудник уже существует, 
        /// 1 - сотрудник добавлен
        /// 2 - Указанного департамента не существует. Сотрудник добавлен в "Нет департамента".
        /// 3 - В указанном департаменте превышен лимит сотрудников. Сотрудник добавлен в "Нет департамента".
        /// 4 - Невозможно добавить сотрудника без департамента. Лимит превышен.
        /// </returns>
        public byte AddEmployee(string firstName, string lastName, string age, string depName, string salary, string numProj)
        {
            byte result = 0;

            foreach (Employee item in Employees)
            {
                if (item.FirstName.Equals(firstName) && item.LastName.Equals(lastName) && item.Age.Equals(int.Parse(age))) return 0;
            }

            // Если департамент существует, проверим можно в него добавить сотрудника или нельзя
            // Если департамента не существует проверим можно добавить сотрудника без департамента
            
            //if (Departments.Any(d => d.Name == depName))
            if (Departments.ContainsKey(depName.ToLower()))
            {
                //if (Departments.Find(d => d.Name == depName).AddEmployee())
                Department tempDep = Departments[depName.ToLower()];
                if (tempDep.AddEmployee())
                {
                    Departments[depName.ToLower()] = tempDep;
                    result = 1;
                }
                else
                {
                    depName = NoNameDepartment;
                    tempDep = Departments[depName.ToLower()];
                    if (!tempDep.AddEmployee()) return 4;
                    Departments[depName.ToLower()] = tempDep;
                    result = 3;
                }
            }
            else
            {
                
                depName = NoNameDepartment;
                Department tempDep = Departments[depName.ToLower()];
                //if (Departments.Find(d => d.Name == depName).AddEmployee())
                if (!tempDep.AddEmployee()) return 4;
                Departments[depName.ToLower()] = tempDep;
                result = 2;
            }
            Employees.Add(new Employee(firstName, lastName, int.Parse(age), depName, int.Parse(salary), int.Parse(numProj)));
            return result;
        }

        /// <summary>
        /// Удаление департамента
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        /// false - нельзя удалить несуществующий департамент или департамент с сотрудниками
        /// </returns>
        public bool RemoveDepartment(string name)
        {
            // Нельзя удалить департамент в котором есть сотрудники, или котороного не существует
            if (!Departments.ContainsKey(name.ToLower()) || Departments[name.ToLower()].EmployeesCount() != 0) return false;
            //if (!Departments.Any(d => d.Name == name) || Departments.Find(d => d.Name == name).EmployeesCount() != 0) return false;


            Departments.Remove(name.ToLower());
            //Departments.Remove(Departments.Find(d => d.Name == name));
            return true;
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Число удаленных элементов</returns>
        public int RemoveEmployee(int id)
        {
            string depName = string.Empty;
            foreach (Employee item in Employees)
            {
                if (item.Id == id) depName = item.DepartemntName;
            }

            if (depName == string.Empty) return 0;

            Department tempDep = Departments[depName.ToLower()];
            tempDep.RemoveEmployee();
            Departments[tempDep.Name.ToLower()] = tempDep;
            //Departments.Find(d => d.Name == depName).RemoveEmployee();
            return Employees.RemoveAll(x => x.Id == id);
        }

        /// <summary>
        /// Изменение департамента
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="newName">Новое название</param>
        /// <param name="newDate">Новая дата создания</param>
        /// <returns>
        /// 0 - департамент с названием <name> не существует
        /// 1 - департамент с названием <newName> уже существует
        /// 2 - департамент успешно переименован
        /// </returns>
        public byte EditDepartment(string name, string newName, string newDate)
        {
            if (!Departments.ContainsKey(name.ToLower())) return 0;
            if (Departments.ContainsKey(newName.ToLower())) return 1;
            //if (!Departments.Any(d => d.Name == name)) return 0;
            //if (Departments.Any(d => d.Name == newName)) return 1;

            //int cnt = 0;
            //bool flag = false;
            //while (cnt < Departments.Count)
            //{
            //    Department tempDep = Departments[cnt];
            //    if (tempDep.Name == name)
            //    {
            //        if (newName != string.Empty)
            //        {
            //            tempDep.Name = newName;
            //            flag = true;
            //        }
            //        if (newDate != string.Empty) tempDep.CreationDate = DateTime.Parse(newDate);
            //        Departments[cnt] = tempDep;
            //    }
            //    cnt++;
            //}

            Department tempDep = Departments[name.ToLower()];
            bool flag = false;
            if (newName != string.Empty)
            {
                tempDep.Name = newName;
                flag = true;
            }
            if (newDate != string.Empty) tempDep.CreationDate = DateTime.Parse(newDate);
            Departments.Remove(name.ToLower());
            Departments.Add(name.ToLower(), tempDep);


            // Если меняли название департамнта - поменяем у всех его сотрудников
            int cnt = 0;
            while (cnt < Employees.Count && flag)
            {
                Employee tempEmp = Employees[cnt];
                if (tempEmp.DepartemntName.Equals(name))
                {
                    tempEmp.DepartemntName = newName;
                    Employees[cnt] = tempEmp;
                }
                cnt++;
            } 
            return 2;


        }

        /// <summary>
        /// Изменение полей сотрудника. Если поля пустые, то изменения не вносятся
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <param name="newFN">Новое имя</param>
        /// <param name="newLN">Новая фамилия</param>
        /// <param name="newAge">Новый возраст</param>
        /// <param name="newDep">Новый департамент</param>
        /// <param name="newSalary">Новая зарплата</param>
        /// <param name="newNumProj">Новое количество проектов</param>
        /// <returns>true - успешно, false - сотрудник с указанным id не найден или id пустой</returns>
        public bool EditEmployee(int id, string newFN, string newLN, string newAge, string newDep, string newSalary, string newNumProj)
        {
            int cnt = 0;
            while (cnt < Employees.Count)
            {
                Employee tempEmp = Employees[cnt];
                Department tempDep = Departments[tempEmp.DepartemntName.ToLower()];
                if (tempEmp.Id == id)
                {
                    if (newFN != string.Empty) tempEmp.FirstName = newFN;
                    if (newLN != string.Empty) tempEmp.LastName = newLN;
                    if (newAge != string.Empty) tempEmp.Age = int.Parse(newAge);
                    if (newDep != string.Empty)
                    {
                        tempDep.RemoveEmployee();
                        Departments[tempEmp.DepartemntName.ToLower()] = tempDep;
                        tempDep = Departments[newDep.ToLower()];
                        tempDep.AddEmployee();
                        Departments[newDep.ToLower()] = tempDep;
                        tempEmp.DepartemntName = newDep;
                    }
                    if (newSalary != string.Empty) tempEmp.Salary = int.Parse(newSalary);
                    if (newNumProj != string.Empty) tempEmp.NumProj = int.Parse(newNumProj);
                    Employees[cnt] = tempEmp;
                    return true;
                }
                cnt++;
            }
            return false;
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
                    Employees = Employees.OrderBy(i => i.DepartemntName).ThenBy(i => i.Age).ThenBy(i => i.Salary).ToList<Employee>();
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
                    item.Id, item.FirstName, item.LastName, item.Age, item.DepartemntName, item.Salary, item.NumProj);
            }
        }

        public void PrintDepartmentsList()
        {
            // Вывод списка департаментов
            Console.WriteLine("Департаменты:");
            Console.WriteLine("          Название            Дата создания        Количество сотрудников");
            foreach (var item in Departments)
            {
                Console.WriteLine("{0,18}{1,25}{2,30}", item.Value.Name, item.Value.CreationDate.ToString("d"), item.Value.EmployeesCount());
            }
        }
    }
}
