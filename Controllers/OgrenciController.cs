using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCoreApp.Data;
namespace EFCoreApp.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly DataContext _context;
        public OgrenciController(DataContext context) 
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var ogrenciler = await _context.Ogrenciler.ToListAsync(); // Bu satır, Ogrenciler DbSet'inden tüm öğrenci kayıtlarını ASENKRON olarak alır ve bir listeye dönüştürür. ToListAsync() metodu, veritabanından verileri çekmek için kullanılır ve işlemin tamamlanmasını bekler. Sonuç olarak, ogrenciler değişkeni, veritabanındaki tüm öğrenci kayıtlarını içeren bir liste olacaktır.
            return View(ogrenciler);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            _context.Ogrenciler.Add(model); // Bu satır, Ogrenci modelini Ogrenciler DbSet'ine ekler. Bu, veritabanına yeni bir öğrenci kaydı eklemek istediğimizde kullanılır. Ancak, bu değişiklik henüz veritabanına yansımamıştır. Değişikliklerin veritabanına kaydedilmesi için SaveChangesAsync() metodunu çağırmamız gerekmektedir.

            await _context.SaveChangesAsync(); // Bu satır, Ogrenci modelini veritabanına kaydeder. SaveChangesAsync() metodu, yapılan değişiklikleri veritabanına yansıtır ve işlemin tamamlanmasını bekler. Bu sayede, yeni eklenen öğrenci kaydı veritabanında kalıcı hale gelir.
            //return View();
            return RedirectToAction("Index"); // Bu satır, Create işlemi tamamlandıktan sonra kullanıcıyı HomeController'ın Index action'ına yönlendirir. Yani, yeni bir öğrenci kaydı oluşturulduktan sonra kullanıcı ana sayfaya geri döner. RedirectToAction() metodu, belirtilen controller ve action'a yönlendirme yapar.
        }
    }
}

// Bu, OgrenciController sınıfının bir yapıcı (constructor) metodudur. Bu yapıcı, DataContext türünde bir parametre alır ve bu parametreyi _context adlı özel bir alana atar. Bu sayede, OgrenciController sınıfı içinde _context değişkeni üzerinden veritabanı işlemleri gerçekleştirebiliriz. Dependency Injection (Bağımlılık Enjeksiyonu) kullanarak, DataContext nesnesi otomatik olarak sağlanır ve böylece kodun test edilebilirliği ve bakımı kolaylaşır.