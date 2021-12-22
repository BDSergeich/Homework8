using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_004_NoteBook
{
    struct Person
    {
        public string FullName { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
        public string MobilePhone { get; set; }
        public string FlatPhone { get; set; }

        public Person(string fullName, string street, string houseNumber, string flatNumbet, string mobilePhone, string flatPhone)
        {
            this.FullName = fullName;
            this.Street = street;
            this.HouseNumber = houseNumber;
            this.FlatNumber = flatNumbet;
            this.MobilePhone = mobilePhone;
            this.FlatPhone = flatPhone;
        }

    }
}
