using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMI
{
    /// <summary>
    /// Пользователя 
    /// </summary>
    public class Users
    {
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Индекс массы тела пользователя
        /// </summary>
        public string IMT { get; set; }
        /// <summary>
        /// Рост пользователя
        /// </summary>
        public double heigth { get; set; }
        /// <summary>
        /// Вес пользователя
        /// </summary>
        public double weigth { get; set; }
        /// <summary>
        /// Дата и время показаний
        /// </summary>
        public DateTime dateTime { get; set; }

        /// <summary>
        /// Конструктор модели с аргументами, входные параметры рост и вес
        /// </summary>
        /// <param name="heigth"></param>
        /// <param name="weigth"></param>
        public Users(double heigth, double weigth)
        {
            this.heigth = heigth;
            this.weigth = weigth;
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Users()
        {

        }
        /// <summary>
        /// Метод вычисления индекса массы тела.
        /// </summary>
        public void Calculate()
        {
            if (heigth <= 0 || weigth <= 0)
            {
                throw new ArgumentException("Ошибка, рост или вес не корректны!");
            }

            double index = weigth / Math.Pow(heigth, 2);

            IMT = (index < 23)
                ? "ИМТ = " + index + " Маленький"
                : (index >= 23 && index < 30)
                    ? "ИМТ = " + index + " Нормальный"
                    : "ИМТ = " + index + " Большой";

            dateTime = DateTime.Now;
        }
    }
}
