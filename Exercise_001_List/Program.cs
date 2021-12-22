using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_001_List
{
    internal class Program
    {
        
        /// <summary>
        /// Заполняет список числами о 0 до 100
        /// </summary>
        /// <param name="list"></param>
        static void FillList(List<int> list)
        {
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
                list.Add(rnd.Next(0, 101));
        }

        /// <summary>
        /// Вывод списка на экран
        /// </summary>
        /// <param name="list"></param>
        static void PrintList(List<int> list)
        {
            foreach (int i in list)
                Console.Write($"{i}  ");
        }

        /// <summary>
        /// Удаление элемента списка > 20 и < 50
        /// </summary>
        /// <param name="list"></param>
        static void DeleteItem(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] > 25 && list[i] < 50) list.RemoveAt(i);
            }
        }


        static void Main(string[] args)
        {
            #region Задание 1.Работа с листом
            //Что нужно сделать
            //Создайте лист целых чисел.
            //Заполните лист 100 случайными числами в диапазоне от 0 до 100.
            //Выведите коллекцию чисел на экран.
            //Удалите из листа числа, которые больше 25, но меньше 50.
            //Снова выведите числа на экран.

            //Рекомендация
            //Сделайте отдельные методы для заполнения, удаления и вывода на экран.

            //Что оценивается
            //В программе используется три отдельных метода. 
            //В качестве хранилища данных используется List.
            #endregion

            List<int> list = new List<int>();

            FillList(list);
            PrintList(list);
            Console.WriteLine("\n\n");
            Console.ReadKey();
            DeleteItem(list);
            PrintList(list);
            Console.ReadKey();
        }
    }
}
