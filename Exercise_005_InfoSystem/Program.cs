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

            company.AddEmployee("asd", "asd", "123", "asd", "123", "123");
            
            do
            {
                Console.Clear();
                Console.WriteLine("--==СТРУКТУРА КОМПАНИИ {0}==--", company.Name.ToUpper());
                Console.WriteLine();

                Console.WriteLine("Выберите дальнейшее действие:");
                Console.WriteLine("1 - Создать новую структуру вручную");
                Console.WriteLine("2 - Редактировать данные");
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
                Console.WriteLine("3 - Завершить ввод");
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
                        return;
                }
            } while (true);
        }

        static void Import(ref Company company)
        {
            Console.Clear();
            Console.WriteLine("-=Загрузка данных из файла=-");
            Console.WriteLine();
            
            if (company.Import(GetPath())) Console.WriteLine("Данные успешно загружены!");
            else Console.WriteLine("Ошибка загрузки данных!");
            Console.ReadKey();
        }

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

        static void Print(ref Company company)
        {
            Console.Clear();
            company.PrintAll();
            Console.ReadKey();
        }

        /// <summary>
        /// Добавление департамента вручную
        /// </summary>
        /// <param name="company"></param>
        static void AddOrEditDepartment(ref Company company, bool isNew)
        {
            Console.Clear();
            string oldName = string.Empty;
            if (isNew)
            {
                Console.WriteLine("-=Добавление департамента=-");
            }
            else
            {
                Console.WriteLine("-=Редактирование департамента=-");
                Console.WriteLine("Введите название департамента для редактирования");
                oldName = Console.ReadLine();
            }

            Console.WriteLine("Введите название департамента:");

            string name = Console.ReadLine();
            if (name == string.Empty && isNew) name = "Отдел" + DateTime.Now.ToString("Hmmss");
                
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
                byte result = company.EditDepartment(oldName, name, date);
                
                switch (result)
                {
                    case 0:
                        Console.WriteLine($"Департамент с названием <{oldName}> не существует!");
                        break;
                    case 1:
                        Console.WriteLine($"Департамент с название <{name}> уже существует!");
                        break;
                    case 2:
                        Console.WriteLine("Успешно!");
                        break;
                }
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Добавление нового или редактирование существующего сотрудника
        /// </summary>
        /// <param name="company"></param>
        static void AddOrEditEmployee(ref Company company, bool isNew)
        {
            Console.Clear();
            string postfix = DateTime.Now.ToString("Hmmss");
            int id = -1;
            Console.Clear();
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
            if (firstName == string.Empty && isNew) firstName = "Имя" + postfix;

            Console.WriteLine("Введите фамилию сотрудника:");
            string lastName = Console.ReadLine();
            if (lastName == string.Empty && isNew) lastName = "Фамилия" + postfix;

            Console.WriteLine("Введите возраст сотрудника:");
            string age = Console.ReadLine();
            int temp;
            if (!int.TryParse(age, out temp) && isNew) age = "0";


            Console.WriteLine();
            company.PrintDepartmentsList();
            Console.WriteLine();
            Console.WriteLine("Введите имя департамента, в котором числится сотрудник:");
            string department = Console.ReadLine();

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
                if (!flag) Console.WriteLine($"Сотрудник с id = {id} не найден!");
            }
            Console.ReadKey();
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
