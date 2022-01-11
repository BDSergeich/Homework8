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
        
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        private int employeesCount;

        #region Конструктор
        public Department(string name, DateTime creationDate)
        {
            this.Name = name;
            this.CreationDate = creationDate;
            this.employeesCount = 100;
        }
        #endregion

        public int EmployeesCount()
        {
            return employeesCount;
        }

        /// <summary>
        /// Увеличивает счетчик количества сотрудников в департаменте.
        /// Eсли количество сотрудников превышает MaxEmloyees, возвращает false
        /// </summary>
        /// <returns></returns>
        public bool AddEmployee()
        {
            if (this.employeesCount == MaxEmloyees) return false;
            this.employeesCount++;
            return true;
        }
        /// <summary>
        /// Уменьшает счетчик количества сотрудников в департаменте.
        /// Если количество = 0, возвращает false.
        /// </summary>
        /// <returns></returns>
        public bool RemoveEmployee()
        {
            if (this.employeesCount == 0) return false;
            this.employeesCount--;
            return true;
        }

    }
}
