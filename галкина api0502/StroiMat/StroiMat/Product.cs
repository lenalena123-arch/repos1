using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace StroiMat
{
    public class Product
    {
        public string ProductArticle { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public string ProductPhoto { get; set; } // Путь к изображению
        public string ProductManufacturer { get; set; }
        public decimal ProductCost { get; set; }
        public int ProductDiscount { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductStatus { get; set; } // Статус товара
    }
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=stud-mssql.sttec.yar.ru,38325; user id=user226_db; password=user226; MultipleActiveResultSets=True; App=EntityFramework");
        }
    }
}
