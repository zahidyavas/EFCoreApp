using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Data
{
    public class DataContext : DbContext // Bu sınıf, uygulamanın veritabanı bağlamını temsil eder ve Entity Framework Core kullanarak veritabanıyla etkileşim kurmamızı sağlar.
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) // Bu constructor, DbContextOptions parametresi alır ve base sınıfın constructor'ına ileterek veritabanı bağlantı ayarlarını yapılandırır.
        {
            
        }
        public DbSet<Kurs> Kurslar { get; set; } // // DbSet, veritabanındaki Kurs tablosunu temsil eder ve bu tabloya erişim sağlar.
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<KursKayit> KursKayitları { get; set; }
        // Normal şartlarda oluşturduğumuz classlara ingilizce isimler vermemiz gerekir, daha sonrasında ise DbSet'lerimize ise bu yapancı adların s takısını halini yazmamız
        // gerekirdi ancak biz Türkçe isimler verdiğimiz için DbSet'lerimize de Türkçe isimler verdik. Bu şekilde de çalışır. Örneğin DbSet<Product> Products { get; set; } şeklinde de yazabilirdik.
    }
}
