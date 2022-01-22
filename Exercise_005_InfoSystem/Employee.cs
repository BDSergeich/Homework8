using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_005_InfoSystem
{
    struct Employee
    {
        /// У каждого сотрудника есть поля: Фамилия, Имя, Возраст, департамент в котором он числится, 
        /// уникальный номер, размер оплаты труда, количество закрепленным за ним проектов.
        private static int count;
        private int id;
        
        public int Id 
        {
            get { return id; }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string DepartemntName { get; set; }
        public int DepartemntId { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public int NumProj { get; set; }

        public Employee(string FirstName, string LastName, int Age, int DepId, int Salary, int NumProj)
        {
            count++;
            this.id = count;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DepartemntId = DepId;
            //this.DepartemntName = DepName;
            this.Age = Age;
            this.Salary = Salary;
            this.NumProj = NumProj;
        }

        public int GetLastId()
        {
            return count;
        }

        public void CorrectCount()
        {
            count--;
        }

    }
}
