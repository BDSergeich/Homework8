using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_005_InfoSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Описание задачи
            /// Создать прототип информационной системы, в которой есть возможност работать со структурой организации
            /// В структуре присутствуют департаменты и сотрудники
            /// Каждый департамент может содержать не более 1_000_000 сотрудников.
            /// У каждого департамента есть поля: наименование, дата создания,
            /// количество сотрудников числящихся в нём 
            /// (можно добавить свои пожелания)
            /// 
            /// У каждого сотрудника есть поля: Фамилия, Имя, Возраст, департамент в котором он числится, 
            /// уникальный номер, размер оплаты труда, количество закрепленным за ним.
            ///
            /// В данной информаиционной системе должна быть возможность 
            /// - импорта и экспорта всей информации в xml и json
            /// Добавление, удаление, редактирование сотрудников и департаментов
            /// 
            /// * Реализовать возможность упорядочивания сотрудников в рамках одно департамента 
            /// по нескольким полям, например возрасту и оплате труда
            /// 
            ///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
            ///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
            ///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
            ///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
            ///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
            ///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
            ///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
            ///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
            ///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
            ///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
            /// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
            /// 
            /// 
            /// Упорядочивание по одному полю возраст
            /// 
            ///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
            ///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
            /// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
            ///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
            ///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
            ///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
            ///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
            ///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
            ///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
            ///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
            ///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
            /// 
            ///
            /// Упорядочивание по полям возраст и оплате труда
            /// 
            ///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
            /// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
            ///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
            ///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
            ///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
            ///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
            ///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
            ///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
            ///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
            ///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
            ///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
            /// 
            /// 
            /// Упорядочивание по полям возраст и оплате труда в рамках одного департамента
            /// 
            ///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
            ///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
            ///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
            ///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
            ///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
            ///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
            ///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
            ///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
            /// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
            ///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
            ///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
            /// 
            #endregion
            Console.Clear();
            Console.WriteLine("Введите Название компании:");
            
            Company company = new Company(Console.ReadLine());

            do
            {
                Console.Clear();
                Console.WriteLine("--==СТРУКТУРА КОМПАНИИ {0}==--", company.Name.ToUpper());
                Console.WriteLine();

                Console.WriteLine("Выберите дальнейшее действие:");
                Console.WriteLine("1 - Ручной ввод данных");
                Console.WriteLine("2 - Редактировать/удалить/отсортировать данные");
                Console.WriteLine("3 - Загрузить данные из файла (xml/json)");
                Console.WriteLine("4 - Сохранить данные в файл (xml/json)");
                Console.WriteLine("5 - Вывести данные в консоль");

                string key = Console.ReadLine();
                switch (key)
                {
                    case "1":
                        Add(ref company);
                        break;
                    case "2":
                        Edit(ref company);
                        break;
                    case "3":
                        Import(ref company);
                        break;
                    case "4":
                        Export(ref company);
                        break;
                    case "5":
                        Print(ref company);
                        break;
                }
            }while(true);
        }

        static void Add(ref Company company)
        {
            string key;
            do
            {
                Console.Clear();
                Console.WriteLine("-=Добавление данных=-");
                Console.WriteLine();
                Console.WriteLine("1 - Добавить департамент");
                Console.WriteLine("2 - Добавить сотрудника");
                Console.WriteLine("3 - Завершить ввод");
                key = Console.ReadLine();

                switch (key)
                {
                    case "1":
                        AddOrEditDepartment(ref company, true);
                        break;
                    case "2":
                        AddOrEditEmployee(ref company, true);
                        break;
                    case "3":
                        return;
                }
            } while (true);


        }

        static void Edit(ref Company company)
        {
            string key;
            do
            {
                Console.Clear();
                Console.WriteLine("-=Редактирование данных=-");
                Console.WriteLine();
                Console.WriteLine("1 - Редактировать департамент");
                Console.WriteLine("2 - Редактировать сотрудника");
                Console.WriteLine("3 - Удалить департамент");
                Console.WriteLine("4 - Удалить сотрудника");
                Console.WriteLine("5 - Сортировка списка сотрудников");
                Console.WriteLine("6 - Завершить ввод");
                key = Console.ReadLine();

                switch (key)
                {
                    case "1":
                        AddOrEditDepartment(ref company, false);
                        break;
                    case "2":
                        AddOrEditEmployee(ref company, false);
                        break;
                    case "3":
                        Remove(ref company, false);
                        break;
                    case "4":
                        Remove(ref company, true);
                        break;
                    case "5":
                        Sort(ref company);
                        break;
                    case "6":
                        return;

                }
            } while (true);
        }

        /// <summary>
        /// Загрузка данных из файла
        /// </summary>
        /// <param name="company"></param>
        static void Import(ref Company company)
        {
            Console.Clear();
            Console.WriteLine("-=Загрузка данных из файла=-");
            Console.WriteLine();
            
            if (company.Import(GetPath())) Console.WriteLine("Данные успешно загружены!");
            else Console.WriteLine("Ошибка загрузки данных!");
            Console.ReadKey();
        }

        /// <summary>
        /// Сохранение данных в файл
        /// </summary>
        /// <param name="company"></param>
        static void Export(ref Company company)
        {

            string key;
            bool flag = true;
            do
            {
                Console.Clear();
                Console.WriteLine("-=Сохранение данных в файл=-");
                Console.WriteLine();
                
                Console.WriteLine("В каком формате выполнить сохранение:");
                Console.WriteLine("1 - xml; 2 - json.");

                key = Console.ReadLine();
                switch (key)
                {
                    case "1":
                        if (company.ExportXML(GetPath())) Console.WriteLine("Успешно!");
                        flag = false;
                        break;
                    case "2":
                        if (company.ExportJSON(GetPath())) Console.WriteLine("Успешно!");
                        flag = false;
                        break;
                }
            } while (flag);


        }

        /// <summary>
        /// Вывод данных в консоль
        /// </summary>
        /// <param name="company"></param>
        static void Print(ref Company company)
        {
            Console.Clear();
            company.PrintAll();
            Console.ReadKey();
        }

        /// <summary>
        /// Добавление или редактирование департамента
        /// </summary>
        /// <param name="company">компания</param>
        /// <param name="isNew">true - новый, false - редактирование</param>
        static void AddOrEditDepartment(ref Company company, bool isNew)
        {
            Console.Clear();
            int depId = 0;
            if (isNew)
            {
                Console.WriteLine("-=Добавление департамента=-");
            }
            else
            {
                Console.WriteLine("-=Редактирование департамента=-");
                Console.WriteLine("Укажите id департамента для редактирования");
                while (!int.TryParse(Console.ReadLine(), out depId)) { }
            }

            Console.WriteLine("Введите новое название департамента:");

            string name = Console.ReadLine();
            if (name == string.Empty && isNew) name = "Отдел_" + DateTime.Now.ToString("Hmmss");
                
            Console.WriteLine("Укажите дату создания:");
                
            string date = Console.ReadLine();
            if (date == string.Empty && isNew) date = DateTime.Now.ToString();

            if (isNew)
            {
                bool result = company.AddDepartment(name, date);
                if (!result) Console.WriteLine("Департамент с таким названием уже существует");
            }
            else
            {
                byte result = company.EditDepartment(depId, name, date);
                
                switch (result)
                {
                    case 0:
                        Console.WriteLine($"Департамент с id <{depId}> не существует!");
                        break;
                    case 1:
                        Console.WriteLine($"Департамент с название <{name}> уже существует!");
                        break;
                    case 2:
                        Console.WriteLine("Успешно!");
                        break;
                }
            }
        }

        /// <summary>
        /// Добавление нового или редактирование существующего сотрулника
        /// </summary>
        /// <param name="company">компания</param>
        /// <param name="isNew">true - новый, false - редактирование</param>
        static void AddOrEditEmployee(ref Company company, bool isNew)
        {
            Console.Clear();
            string postfix = DateTime.Now.ToString("Hmmss");
            int id = -1;
            if (isNew)
            {
                Console.WriteLine("-=Добавление сотрудника=-");
            }
            else
            {
                Console.WriteLine("-=Редактирование сотрудника=-");
                Console.WriteLine("Укажите id сотрудника для редактирования:");
                if (!int.TryParse(Console.ReadLine(), out id)) return;
            }

            Console.WriteLine("Введите имя сотрудника:");
            string firstName = Console.ReadLine();
            if (firstName == string.Empty && isNew) firstName = "Имя_" + postfix;

            Console.WriteLine("Введите фамилию сотрудника:");
            string lastName = Console.ReadLine();
            if (lastName == string.Empty && isNew) lastName = "Фамилия_" + postfix;

            Console.WriteLine("Введите возраст сотрудника:");
            string age = Console.ReadLine();
            int temp;
            if (!int.TryParse(age, out temp) && isNew) age = "0";


            Console.WriteLine();
            company.PrintDepartmentsList();
            Console.WriteLine();
            Console.WriteLine("Укажите id департамента, в котором числится сотрудник:");
            int department;
            string userInput = Console.ReadLine();
            while (!int.TryParse(userInput, out department)) 
            {
                if (userInput == string.Empty)
                {
                    department = 1;
                    break;
                }
            }

            Console.WriteLine("Введите зарплату сотрудника:");
            string salary = Console.ReadLine();
            if (!int.TryParse(salary, out temp) && isNew) salary = "0";

            Console.WriteLine("Введите количество проектов, закрепленных за сотрудником:");
            string numProj = Console.ReadLine();
            if (!int.TryParse(numProj, out temp) && isNew) numProj = "0";

            if (isNew)
            {
                byte result = company.AddEmployee(firstName, lastName, age, department, salary, numProj);

                switch (result)
                {
                    case 0:
                        Console.WriteLine("Такой сотрудник уже существует");
                        break;
                    case 1:
                        Console.WriteLine("Успешно!");
                        break;
                    case 2:
                        Console.WriteLine("Указанного департамента не существект. Сотрудник добавлен без департамента");
                        break;
                    case 3:
                        Console.WriteLine("В указанном департаменте превышен лимит сотрудников. Сотрудник добавлен без департамента");
                        break;
                    case 4:
                        Console.WriteLine("Невозможно добавить сотрудника без департамента. Лимит превышен");
                        break;
                }
            }
            else
            {
                bool flag = company.EditEmployee(id, firstName, lastName, age, department, salary, numProj);
                if (!flag)
                {
                    Console.WriteLine("Ошибка!");
                    Console.WriteLine("Id сотрудника или Id департамента указаны неверно!");
                }
                else
                {
                    Console.WriteLine("Успешно!");
                }
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Удаление данных
        /// </summary>
        /// <param name="company">компания</param>
        /// <param name="isEmployee">true - сотрудник, false - департамент</param>
        static void Remove(ref Company company, bool isEmployee)
        {
            if (isEmployee)
            {
                Console.Clear();
                Console.WriteLine("-=Удаление сотрудника=-");
                Console.WriteLine("Введите id сотрудника для удаления:");
                int id;
                bool flag;
                do
                {
                    flag = int.TryParse(Console.ReadLine(), out id);
                } while (!flag);
                
                if (company.RemoveEmployee(id) > 0)
                {
                    Console.WriteLine("Успешно!");
                }
                else
                {
                    Console.WriteLine($"Удаление не удалось! Сотрудник с id = {id} не существует");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("-=Удаление департамента=-");
                Console.WriteLine();

                company.PrintDepartmentsList();
                Console.WriteLine();
                Console.WriteLine("Введите id департамента для удаления:");
                int id = 0;
                while (!int.TryParse(Console.ReadLine(), out id) && id < 2) { }
                if (company.RemoveDepartment(id)) 
                { 
                    Console.WriteLine("Успешно!"); 
                }
                else
                {
                    Console.WriteLine("Удаление не выполнено!");
                    Console.WriteLine("Нельзя удалить департамент, которого не существует,");
                    Console.WriteLine("или в котором числятся сотрудники.");
                }
                
            }
            Console.ReadKey(true);
        }

        /// <summary>
        /// Сортировка
        /// </summary>
        /// <param name="company"></param>
        static void Sort(ref Company company)
        {
            Console.Clear();
            Console.WriteLine("-=Сортировка списка сотрудников=-");
            Console.WriteLine("Выберите вариант сортировки:");
            Console.WriteLine("1 - Сортировка по id сотрудника");
            Console.WriteLine("2 - Сортировка по возрасту сотрудника");
            Console.WriteLine("3 - Сортировка по возрасту и зарплате сотрудника");
            Console.WriteLine("4 - Сортировка по возрасту и зарплате сотрудника в рамках одного департамента");
            Console.WriteLine("5 - Завершить");
            bool flag = false;
            do
            {
                string key = Console.ReadLine();

                switch (key)
                {
                    case "1":
                        company.SortEmployees();
                        flag = true;
                        Console.WriteLine("Успешно!");
                        break;
                    case "2":
                        company.SortEmployees(FieldTOSort.AGE);
                        flag = true;
                        Console.WriteLine("Успешно!");
                        break;
                    case "3":
                        company.SortEmployees(FieldTOSort.AGE_SALARY);
                        flag = true;
                        Console.WriteLine("Успешно!");
                        break;
                    case "4":
                        company.SortEmployees(FieldTOSort.DEPART_AGE_SALARY);
                        flag = true;
                        Console.WriteLine("Успешно!");
                        break;
                    case "5":
                        return;
                }
            }while (!flag);
        }

        /// <summary>
        /// Запрос пути с именем файла
        /// </summary>
        /// <returns></returns>
        static string GetPath()
        {
            Console.WriteLine("Укажите путь и имя файла: ");
            return Console.ReadLine();
        }

    }
}
