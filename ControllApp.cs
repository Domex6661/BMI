using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMI
{
    /// <summary>
    /// Класс контроллер, входные параметры конструктора: путь к файлу
    /// </summary>
    public class ControlApp
    {
        private List<Users> _users = new List<Users>();
        private string _path;
        /// <summary>
        /// Объявление консруктора с аргументом
        /// </summary>
        /// <param name="path"></param>
        public ControlApp(string path)
        {
            _path = path;
        }
        /// <summary>
        /// Метод считывания данных пользователя с файла. Дессюрилизация из json в строку.
        /// </summary>
        private void GetUsers()
        {
            _users.Clear();

            using (StreamReader reader = new StreamReader(_path))
            {
                string line;
                bool flag = true;

                while (flag)
                {
                    line = reader.ReadLine();
                    flag = !string.IsNullOrWhiteSpace(line);

                    if (flag)
                    {
                        var user = JsonConvert.DeserializeObject<Users>(line);

                        if (_users.All(s => s.Surname != user.Surname) || _users.Count == 0)
                        {
                            _users.Add(user);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Вывод на консоль пояснительного текста
        /// </summary>
        private void PrintCommand()
        {
            Console.WriteLine("Для того чтобы вывести всех пользователей нажмите 1");
            Console.WriteLine("Для расчёта своего ИМТ нажмите 2");
            Console.WriteLine("Нажмите 3 для выхода");
        }
        /// <summary>
        /// Метод получения данных о пользователях, расчёт новых параметров ИМТ, вставка в конец файла.
        /// </summary>
        public void StartBMIConsole()
        {
            PrintCommand();

            while (true)
            {
                string key = Console.ReadLine();

                switch (key)
                {
                    case "1":
                        GetAllUsers();

                        break;
                    case "2":
                        AddNewData();

                        break;
                    case "3":
                        return;

                    default:
                        Console.WriteLine("Введена не верная команда, выберите из предложенных!");
                        PrintCommand();

                        break;
                }

            }

        }
        /// <summary>
        /// Вывод всех пользователей в консоль
        /// </summary>
        private void GetAllUsers()
        {
            GetUsers();
            StringBuilder builder = new StringBuilder();

            foreach (var item in _users)
            {
                builder.AppendFormat("{0}\n{1}\n{2}\n", item.Surname, item.IMT, item.dateTime);
                Console.WriteLine(builder);
                builder.Clear();
            }
        }
        /// <summary>
        /// Добавление нового пользователя в файл и расчёт ИМТ
        /// </summary>
        /// <exception cref="System.ArgumentException">При недопустимом входном аргументе</exception>
        ///<exception cref="System.FormatException">Не верный формат строки</exception>
        private void AddNewData()
        {
            var user = new Users();

            try
            {
                Console.WriteLine("Введите вашу фамилию: ");
                string surname = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(surname))
                {
                    throw new FormatException("Фамилия не была введена!");
                }

                user.Surname = surname;
                Console.WriteLine("Введите ваш вес: ");
                user.weigth = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите ваш рост в метрах через запятую(напрмиер 1,80) или в сантиметрах(например 180): ");
                user.heigth = convert(Console.ReadLine());
                user.Calculate();
                var json = JsonConvert.SerializeObject(user);
                writer(json);
                Console.WriteLine(user.IMT);

            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);

            }
        }
        /// <summary>
        /// Метод записи в файл
        /// </summary>
        /// <param name="json"></param>
        private void writer(string json)
        {
            using (StreamWriter stream = new StreamWriter(_path, true))
            {
                stream.WriteLine(json);
            }
        }
        /// <summary>
        /// Метод конвертации роста в см или в метрах.
        /// </summary>
        /// <param name="heigth"></param>
        /// <returns>Рост в метрах</returns>
        ///  <exception cref="System.ArgumentException">Недопустимый параметр</exception>
        private double convert(string heigth)
        {
            if (heigth.Contains(","))
            {
                double heigth_local;
                return double.TryParse(heigth, out heigth_local) == true
                    ? heigth_local
                    : throw new ArgumentException("Ошибка");

            }
            else
            {
                int heigth_local;
                return int.TryParse(heigth, out heigth_local) == true
                    ? heigth_local / 100.0
                    : throw new ArgumentException("Ошибка");

            }


        }
    }
}
