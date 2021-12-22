using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_005_InfoSystem
{
    struct Company
    {

        /// <summary>
        /// Список департаментов
        /// </summary>
        private List<Department> departments;

        private string Path;

        public Company(string path)
        {
            this.Path = path;
            this.departments = new List<Department>();
        }
    }
}
