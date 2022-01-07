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
        /// </summary>
        private List<Department> Departments;
        
        /// <summary>
        /// Список сотрудников
        /// </summary>
        private List<Employee> Employees;

        private string Path;

        public Company(string path)
        {
            this.Path = path;
            this.Departments = new List<Department>();
            this.Employees = new List<Employee>();
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
            var dep = Departments.Where(d => d.Name.Equals(name));
            if (dep.Any()) return false;
            
            Departments.Add(new Department(name, DateTime.Parse(dateTime)));
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
        /// 2 - невозможно добавить сотрудника в департамент, т.к. превышен лимт
        /// </returns>
        public byte AddEmployee(string firstName, string lastName, int age, string depName, int salary, int numProj)
        {
            // Проверка на наличие такого сотрудника уже в компании
            var emp = Employees.Where(e => e.FirstName.Equals(firstName) && e.LastName.Equals(lastName) && e.Age.Equals(age));
            if (emp.Any()) return 0;
            
            // Если указанного департамента не существует, запихнем сотрудника в "Нет департамента"
            var dep = Departments.Where(d => d.Name.Equals(depName));
            if (dep.Any())
                dep = dep.Cast<Department>();
            else 
                dep = Departments.Where(d=>d.Name.Equals(NoName)).Cast<Department>();

            if (dep.First().AddEmployee())
            {
                Employees.Add(new Employee(firstName, lastName, age, dep.First().Name, salary, numProj));
                return 1;
            }
            else return 2;
            
        }
    }
}
