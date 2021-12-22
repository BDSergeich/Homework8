using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace Exercise_004_NoteBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Задание 4.Записная книжка
            //Что нужно сделать
            //Программа спрашивает у пользователя данные о контакте:
            //ФИО
            //Улица
            //Номер дома
            //Номер квартиры
            //Мобильный телефон
            //Домашний телефон

            //С помощью XElement создайте xml файл, в котором есть введенная информация. XML файл должен содержать следующую структуру:

            //< Person name =”ФИО человека” >
            //      < Address >
            //          < Street > Название улицы </ Street >
            //             < HouseNumber > Номер дома </ HouseNumber >
            //                < FlatNumber > Номер квартиры </ FlatNumber >
            //               </ Address >
            //               < Phones >
            //                   < MobilePhone > 89999999999999 </ MobilePhone >
            //                   < FlatPhone > 123 - 45 - 67 < FlatPhone >
            //               </ Phones >
            //           </ Person >

            //Советы и рекомендации
            //Заполняйте XElement в ходе заполнения данных. Изучите возможности XElement в документации Microsoft.

            //Что оценивается
            //Программа создаёт Xml файл, содержащий все данные о контакте.
            #endregion
            
            Person person = new Person();
            List<Person> listPersons = new List<Person>();
            Console.WriteLine("- = З А П И С Н А Я   К Н И Ж К А = -\n");
            Console.WriteLine();
            Console.WriteLine("Введите путь к фалу для сохранения данных:");
            string path = Console.ReadLine();

            Console.WriteLine("Заполните данные о контакте (путая строка - закончить ввод)");
            Console.WriteLine();
            string argument = string.Empty;
            do
            {
                Console.WriteLine("ФИО:");
                if (!ConsoleRead(ref argument)) break;
                person.FullName = argument;
                Console.WriteLine("Улица:");
                if (!ConsoleRead(ref argument)) break;
                person.Street = argument;
                Console.WriteLine("Номер дома:");
                if (!ConsoleRead(ref argument)) break;
                person.HouseNumber = argument;
                Console.WriteLine("Номер квартиры:");
                if (!ConsoleRead(ref argument)) break;
                person.FlatNumber = argument;
                Console.WriteLine("Мобильный телефон:");
                if (!ConsoleRead(ref argument)) break;
                person.MobilePhone = argument;
                Console.WriteLine("Домашний телефон:");
                if (!ConsoleRead(ref argument)) break;
                person.FlatPhone = argument;

                listPersons.Add(person);
            } while (true);
            Console.WriteLine();
            Console.WriteLine($"Вывод данных в файл {path}");
            
            if (SaveXML(listPersons, path)) Console.WriteLine("Успешно!");
            else Console.WriteLine("Не удалось сохранить данные в файл");

            Console.ReadKey();

        }

        static bool ConsoleRead(ref string arg)
        {
            arg = Console.ReadLine();
            return arg != string.Empty;
        }


        /// <summary>
        /// Сериализуем данные
        /// </summary>
        /// <param name="list"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        static bool SaveXML(List<Person> list, string path)
        {
            try
            {
                using (Stream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                {

                    foreach (Person elem in list)
                    {
                        XElement person = new XElement("Person");
                        XElement address = new XElement("Address");
                        XElement phones = new XElement("Phones");

                        XElement street = new XElement("Street", elem.Street);
                        XElement houseNumber = new XElement("HouseNumber", elem.HouseNumber);
                        XElement flatNumber = new XElement("FlatNumber", elem.FlatNumber);
                        XElement mobilePhone = new XElement("MobilePhone", elem.MobilePhone);
                        XElement flatPhone = new XElement("FlatPhone", elem.FlatPhone);

                        address.Add(street, houseNumber, flatNumber);
                        phones.Add(mobilePhone, flatPhone);

                        XAttribute xAtrPerson = new XAttribute("name", elem.FullName);
                        person.Add(address, phones, xAtrPerson);
                        
                        person.Save(fs);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
