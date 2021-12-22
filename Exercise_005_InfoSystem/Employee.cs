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

        private string FirstName { get; set; }
        private string LastName { get; set; }
        private Department Dep { get; set; }
        private int Age { get; set; }
        private int Salary { get; set; }
        private int NumProj { get; set; }

        public Employee(string FirstName, string LastName, int Age, Department Dep, int Salary, int NumProj)
        {
            count++;
            this.id = count;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Dep = Dep;
            this.Age = Age;
            this.Salary = Salary;
            this.NumProj = NumProj;
        }

    }
}
