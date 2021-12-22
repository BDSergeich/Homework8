using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repeat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            # region Задание 3.Проверка повторов

            //Что нужно сделать
            //Пользователь вводит число.Число сохраняется в HashSet коллекцию.
            //Если такое число уже присутствует в коллекции, то пользователю на экран
            //выводится сообщение, что число уже вводилось ранее. Если числа нет,
            //то появляется сообщение о том, что число успешно сохранено. 

            //Советы и рекомендации
            //Для добавление числа в HashSet используйте метод Add.

            //Что оценивается
            //В программе в качестве коллекции используется HashSet.
            #endregion

            HashSet<int> set = new HashSet<int>();
            string num;
            do
            {
                Console.WriteLine("Введите число (пустая строка - закончить):");
                num = Console.ReadLine();
                if (num != string.Empty)
                {
                    bool flag = set.Add(int.Parse(num));
                    if (!flag) Console.WriteLine("Такое число уже есть!");
                }
            } while (num != string.Empty);

        }
    }
}
