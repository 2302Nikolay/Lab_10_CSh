using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lab_11_Individual
{
    // Категории товара
    enum CategoryOfProduct
    {
        A,
        B,
        C,
        D,
        No
    }

    // Компании-поставщики
    enum Company
    {
        No,
        Zoory,
        Waka_waka,
        Nestle_Std,
        Jaromye
    }

    class Product
    {
        // Поля
        String ID { get; set; }
        String Name { get; set; }
        public Company Company { get; set; }
        public float Price { get; set; }
        public float Count { get; set; }
        public CategoryOfProduct Category { get; set;}
        public float Discount { get; set; }

        // Метод для создания объекта продукт по данным из файла
        public static Product Create(String str)
        {
            Product pr = new Product();
            string[] e = str.Split(',');
            pr.ID = e[0].Trim();
            pr.Name= e[1].Trim();
            
            String tmp_company = e[2].Trim();
            if (tmp_company == "Zoory")
                pr.Company = Company.Zoory;
            else if (tmp_company == "Waka-waka")
                pr.Company = Company.Waka_waka;
            else if (tmp_company == "Nestle Std")
                pr.Company = Company.Nestle_Std;
            else if (tmp_company == "Jaromye")
                pr.Company = Company.Jaromye;
            else pr.Company = Company.No;

            pr.Price = Convert.ToSingle(e[3].TrimStart('$').Replace('.', ','));
            pr.Count = Convert.ToSingle(e[4]);
            
            String tmp_category = e[5].Trim();
            if (tmp_category == "A")
                pr.Category = CategoryOfProduct.A;
            else if (tmp_category == "B")
                pr.Category = CategoryOfProduct.B;
            else if (tmp_category == "C")
                pr.Category = CategoryOfProduct.C;
            else if (tmp_category == "D")
                pr.Category = CategoryOfProduct.D;
            else pr.Category = CategoryOfProduct.No;

            pr.Discount = Convert.ToSingle(e[6]);

            return pr;
        }

        // Перегрузка метода ToString() для корректного вывода в цикле foreach
        public override string ToString()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}", Name, Company, Price, Count, Category, Discount, ID);
            String s = string.Format(
                "****************\n" +
                "ID: {0}, Товар: {1}, Поставщик: {2}, Цена: {3}, Количество: {4}, Категория: {5}, Скидка: {6}% ",
                ID, Name, CompanyToString(Company), Price, Count, CategoryToString(Category), Discount);

            return s;
        }

        // Метод преобразования типа перечисления в строку 
        private static String CompanyToString(Company comp)
        {
            if (comp == Company.Nestle_Std) return "Nestle Std";
            else if (comp == Company.Zoory) return "Zoory";
            else if (comp == Company.Waka_waka) return "Waka-waka";
            else if (comp == Company.Jaromye) return "Jaromye";
            else return "No";
        }

        // Метод преобразования типа перечисления в строку 
        private static String CategoryToString(CategoryOfProduct cat)
        {
            if (cat == CategoryOfProduct.A) return "A";
            else if (cat == CategoryOfProduct.B) return "B";
            else if (cat == CategoryOfProduct.C) return "C";
            else if (cat == CategoryOfProduct.D) return "D";
            else return "No";
        }

    }

    class Lab_11_Ind
    {
        static void Main(string[] args)
        {
            StreamReader s_in = new StreamReader(@"C:\Users\Николай Мальцев\source\repos\Lab_11_Malzew\Data\lr11_11_2.csv");

#if !DEBUG
            
#endif
            List<Product> all = new List<Product>();
            try
            {
                String line = s_in.ReadLine();
                while ((line = s_in.ReadLine()) != null)
                {
                    all.Add(Product.Create(line));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Всего продуктов: {0}", all.Count);
            foreach (var pr in all)
               Console.WriteLine(pr);
            
        }
    }
}
