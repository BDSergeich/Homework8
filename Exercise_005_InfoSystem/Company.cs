using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Exercise_005_InfoSystem
{
    struct Company
    {
        private const string NoName = "Нет";
        private const string DefaultDate = "01.01.2000";

        /// <summary>
        /// Список департаментов
        /// Key<string> - Название департамента (Department.Name) в нижнем регистре
        /// </summary>
        private Dictionary<string, Department> Departments;
        
        /// <summary>
        /// Список сотрудников
        /// Key<string> - уникальная строка из имени, фамилии и возраста в нижнем регистре
        /// </summary>
        private Dictionary<string, Employee> Employees;

        private string Path;

        public Company(string path)
        {
            this.Path = path;
            this.Departments = new Dictionary<string, Department>();
            this.Employees = new Dictionary<string, Employee>();
            AddDepartment(NoName);
        }


        /// <summary>
        /// Определение формата сериализованных данных в файле
        /// запуск соответствующего импорта
        /// </summary>
        /// <returns></returns>
        public bool Import()
        {
            try
            {
                using (StreamReader sr = new StreamReader(this.Path))
                {
                    string line = sr.ReadLine();
                    char symbol = line[0];
                    if (symbol == '[' || symbol == '{') return ImportJSON();
                    else if (symbol == '<') return ImportXML();
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
        //      DepartmentName
        //      CreationDate
        //   Employee
        //      FirstName
        //      LastName
        //      Department
        //      Age
        //      Salary
        //      NumProj
        #endregion
        private bool ImportXML()
        {
            return true;
        }
        private bool ExportXML()
        {
            return true;
        }
        private bool ImportJSON()
        {
            return true;
        }
        private bool ExportJSON()
        {
            return true;
        }
        /// <summary>
        /// Добавление департамента
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dateTime"></param>
        /// <returns>
        /// true - добавление успешно
        /// false - такой департамент уже существует
        /// </returns>
        public bool AddDepartment(string name, string dateTime = DefaultDate)
        {
            if (Departments.ContainsKey(name.ToLower())) return false;
            
            Departments.Add(name.ToLower(), new Department(name, DateTime.Parse(dateTime)));
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
        /// 1 - сотрудник добавлен,
        /// 2 - сотрудник добавлен в автоматически созданный департамент,
        /// 3 - В указанном департаменте превышен лимит сотрудников. Сотрудник добавлен в "Нет департамента".
        /// 4 - Невозможно добавить сотрудника без департамента. Лимит превышен
        /// </returns>
        public byte AddEmployee(string firstName, string lastName, int age, string depName, int salary, int numProj)
        {
            byte result;
            string key = (firstName + lastName + age).ToLower();
            // Проверка на наличие такого сотрудника в компании
            if (Employees.ContainsKey(key)) return 0;

            // Если указанного департамента не существует, мы его создадим автоматически
            // и предупредим об этом пользователя
            if (AddDepartment(depName)) 
                result = 2;
            else if(Departments[depName.ToLower()].AddEmployee())
            {
                result = 1;
            }
            else
            {
                depName = NoName;
                result = 3;
                if (!Departments[depName.ToLower()].AddEmployee()) return 4;
            }
            Employees.Add(key, new Employee(firstName, lastName, age, depName, salary, numProj));
            return result;
        }

        /// <summary>
        /// Удаление департамента
        /// </summary>
        /// <param name="name">Имя департамента</param>
        /// <returns>
        /// false если такого департамента не существует или в нем есть сотрудники.
        /// </returns>
        public bool RemoveDepartment(string name)
        {
            name = name.ToLower();
            // Нельзя удалить департамент в котором есть сотрудники,
            // Или которого не существует
            if (!Departments.ContainsKey(name) || Departments[name].EmployeesCount != 0)
                return false;

            return Departments.Remove(name);
        }

        public bool RenameDepartment(string oldName, string newName)
        {
            // Нельзя переименовать департамент, которого не существует.
            if (!Departments.ContainsKey(oldName.ToLower())) return false;

            foreach (var item in Employees)
            {

            }

        }


    }
}
