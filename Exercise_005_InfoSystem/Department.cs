using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_005_InfoSystem
{
    struct Department
    {
        // У каждого департамента есть поля: наименование, дата создания,
        // количество сотрудников числящихся в нём 
        // (можно добавить свои пожелания)
        
        private const int MaxEmloyees = 1_000_000;
        
        public string DepartmentName { get; set; }
        public DateTime CreationDate { get; set; }

        private int employeesCount;
        public int EmployeesCount { get { return employeesCount; } }

        /// <summary>
        /// Список сотрудников компании
        /// </summary>
        private List<Employee> employees;

        #region Конструктор
        public Department(string departmentName, DateTime creationDate)
        {
            this.DepartmentName = departmentName;
            this.CreationDate = creationDate;
            this.employeesCount = 0;
            this.employees = new List<Employee>();
        }
        #endregion
        /// <summary>
        /// Добавление сотрудника в департамент
        /// </summary>
        /// <param name="emp">Сотрудник</param>
        /// <returns></returns>
        public bool AddEmployee(Employee emp)
        {
            if (employeesCount >= MaxEmloyees) return false;
            this.employees.Add(emp);
            this.employeesCount = employees.Count;
            return true;
        }
        /// <summary>
        /// Удаление сотрудника из департамента
        /// </summary>
        /// <param name="emp">Сотрудник</param>
        /// <returns></returns>
        public bool RemoveEmployee(Employee emp)
        {
            try
            {
                employees.Remove(emp);
                this.employeesCount = employees.Count;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
