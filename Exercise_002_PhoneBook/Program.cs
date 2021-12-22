using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_002_PhoneBook
{
    internal class Program
    {
        /// <summary>
        /// добавление контакта в телефонную книгу
        /// </summary>
        /// <param name="dic"></param>
        static void AddUser(Dictionary<string, string> dic)
        {
            string phone;
            string user;
            do
            {
                Console.WriteLine("Номер телефона:");
                phone = Console.ReadLine();
                Console.WriteLine("Ф.И.О владельца:");
                user = Console.ReadLine();
                dic.Add(phone, user);

            } while (phone != string.Empty && user != string.Empty);
        }
        /// <summary>
        /// Возвращает true и имя абонента по номеру телефона, или false, если такого номера не зарегистрировано
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static bool GetUserName(Dictionary<string, string> dic, string key, out string value)
        {
            return dic.TryGetValue(key, out value);
        }


        static void Main(string[] args)
        {
            #region Задание 2.Телефонная книга
            //Что нужно сделать
            //Пользователю итеративно предлагается вводить в программу номера телефонов и ФИО их владельцев. 
            //В процессе ввода информация размещается в Dictionary, где ключом является номер телефона,
            //а значением — ФИО владельца. Таким образом, у одного владельца может быть несколько номеров
            //телефонов.Признаком того, что пользователь закончил вводить номера телефонов, является ввод пустой строки. 
            //Далее программа предлагает найти владельца по введенному номеру телефона.Пользователь вводит
            //номер телефона и ему выдаётся ФИО владельца.Если владельца по такому номеру телефона не
            //зарегистрировано, программа выводит на экран соответствующее сообщение.

            //Совет
            //Для того, чтобы находить значение в Dictionary, нужно использовать TryGetValue.

            //Что оценивается
            //Программа разделена на логические методы.
            //Для хранения элементов записной книжки используется Dictionary.
            #endregion

            Dictionary<string, string> phoneBook = new Dictionary<string, string>();

            Console.WriteLine("- = З А П И С Н А Я   К Н И Ж К А = -");
            Console.WriteLine("Введите номер телефона и ФИО владельца (пустая строка для завершения ввода):");
            AddUser(phoneBook);
            Console.WriteLine();
            Console.WriteLine("Найдем владельца по ФИО");
            string number;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Введите номер телефона (пустая строка для завершения):");
                number = Console.ReadLine();
                if (number != string.Empty)
                {
                    string name = string.Empty;
                    bool flag = GetUserName(phoneBook, number, out name);
                    if (!flag)
                    {
                        Console.WriteLine("Такой номер не зарегистрирован");
                        continue;
                    }
                    Console.WriteLine(name);
                }
            } while (number != string.Empty);
                
        }
    }
}
