using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Лабораторная_работа_11
{
    enum GenderType
    {
        Male,
        Female
    }

    class Person
    {
        String ID { get; set; }
        String Fname { get; set; }
        String Lname { get; set; }
        String Email { get; set; }
        public GenderType Gender { get; set; }
        public float Salary { get; set; }
        public Boolean HasChildren { get; set; }

        public static Person Create(String str)
        {
            Person p = new Person();
            string[] e = str.Split(',');
            p.ID = e[0].Trim();
            p.Fname = e[1].Trim();
            p.Lname = e[2].Trim();
            p.Email = e[3].Trim();
            String tmp = e[4].Trim();
            if (tmp == "Male")
                p.Gender = GenderType.Male;
            else p.Gender = GenderType.Female;
            p.Salary = Convert.ToSingle(e[5].TrimStart('$').Replace('.', ','));
            p.HasChildren = Convert.ToBoolean(e[6].Trim());

            return p;
        }

        public override string ToString()
        {
            String s = string.Format(
                "************************************************\n" +
                "ID: {0}, Имя: {1}, Фамилия: {2}, ({3})\n" +
                "E-mail: {4}, 3/п: {5}, Дети: {6}",
                ID, Fname, Lname, GenderToStr(Gender), Email, Salary, HasChildrenToStr(HasChildren));

            return s;
        }

        private static String GenderToStr(GenderType g)
        {
            if (g == GenderType.Male) return "M";
            else return "Ж";
        }

        private static String HasChildrenToStr(Boolean g)
        {
            if (g == true) return "Есть";
            else return "Нет";
        }

    }

    class Lab_11_Ex
    {
        static void Main(string[] args)
        {
            StreamReader f_in = new StreamReader(@"C:\Users\Николай Мальцев\source\repos\Lab_11_Malzew\Data\Input.csv");
#if !DEBUG
            TextWriter save_out = Console.Out;
            var new_out = new StreamWriter(@"C:\Users\Николай Мальцев\source\repos\Lab_11_Malzew\Data\Result.txt");
            Console.SetOut(new_out);
#endif
            List<Person> all = new List<Person>();
            try
            {
                String line = f_in.ReadLine();
                while ((line = f_in.ReadLine()) != null)
                {
                    all.Add(Person.Create(line));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Console.WriteLine("Всего пользователей: {0}", all.Count);
            //foreach (var p in all)
            //Console.WriteLine(p);

            // Задача 1
            Console.WriteLine("*********** Задача 1 ************");
            int mCount = all.FindAll(p => p.Gender == GenderType.Male).Count();
            Console.WriteLine("Количество женщин: {0}", mCount);
            Console.WriteLine("Количество мужчин: {0}", all.Count - mCount);

            // Задача 2
            Console.WriteLine("\n\n*********** Задача 2 *************");
            float male_max;
            var temp_male_max = (from p in all
                                 where p.Gender == GenderType.Male
                                 select p.Salary);
            if (temp_male_max.Any()) // Проверяет есть ли в переменной что-либо
            {
                male_max = temp_male_max.Max();
            }
            else
            {
                male_max = 0;
            }
            Person rich_man = (from p in all
                               where (p.Gender == GenderType.Male) &&
                               (p.Salary == male_max)
                               select p).FirstOrDefault();
            Console.WriteLine(rich_man);

            float female_max;
            var temp_female_max = (from p in all
                                   where p.Gender == GenderType.Female
                                   select p.Salary);
            if (temp_female_max.Any())
            {
                female_max = temp_female_max.Max();
            }
            else
            {
                female_max = 0;
            }


            Person rich_woman = (from p in all
                                 where (p.Gender == GenderType.Female) &&
                                 (p.Salary == female_max)
                                 select p).FirstOrDefault();
            Console.WriteLine(rich_woman);

            // Задача 3
            Console.WriteLine("\n\n************ Задача 3 **************");
            int Count_Yes = 0, Count_No = 0;
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].HasChildren) Count_Yes++;
                else Count_No++;
            }
            Console.WriteLine("С детьми: {0}, без детей {1}",
                Count_Yes, Count_No);

            // Задача 4
            Console.WriteLine("\n\n************ Задача 4 **************");
            float salary_total = (from p in all
                                  select p.Salary).Sum();
            float m_haschildren = (from p in all
                                   where (p.Gender == GenderType.Male) && p.HasChildren
                                   select p.Salary).Sum();
            float f_haschildren = (from p in all
                                   where (p.Gender == GenderType.Female) && p.HasChildren
                                   select p.Salary).Sum();
            float m_nochildren = (from p in all
                                  where (p.Gender == GenderType.Male) && !p.HasChildren
                                  select p.Salary).Sum();
            float f_nochildren = (from p in all
                                  where (p.Gender == GenderType.Female) && !p.HasChildren
                                  select p.Salary).Sum();
            var f = System.Globalization.CultureInfo.GetCultureInfo("en-us");
            Console.WriteLine("Фонд з/п: {0}\n" +
                "Муж. + дети: {1},\t Жен. + дети: {2},\n" +
                "Муж. - дети: {3},\t Жен. - дети: {4}",
                salary_total.ToString("C3", f),
                m_haschildren.ToString("C", f),
                f_haschildren.ToString("C", f),
                m_nochildren.ToString("C", f),
                f_nochildren.ToString("C", f)
                );
#if !DEBUG
            Console.SetOut(save_out);
            new_out.Close();
#else
            Console.ReadKey();
#endif
        }
    }
}
