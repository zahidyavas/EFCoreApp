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

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            _context.Ogrenciler.Add(model); // Bu satır, Ogrenci modelini Ogrenciler DbSet'ine ekler. Bu, veritabanına yeni bir öğrenci kaydı eklemek istediğimizde kullanılır. Ancak, bu değişiklik henüz veritabanına yansımamıştır. Değişikliklerin veritabanına kaydedilmesi için SaveChangesAsync() metodunu çağırmamız gerekmektedir.

            await _context.SaveChangesAsync(); // Bu satır, Ogrenci modelini veritabanına kaydeder. SaveChangesAsync() metodu, yapılan değişiklikleri veritabanına yansıtır ve işlemin tamamlanmasını bekler. Bu sayede, yeni eklenen öğrenci kaydı veritabanında kalıcı hale gelir.
            //return View();
            return RedirectToAction("Index"); // Bu satır, Create işlemi tamamlandıktan sonra kullanıcıyı HomeController'ın Index action'ına yönlendirir. Yani, yeni bir öğrenci kaydı oluşturulduktan sonra kullanıcı ana sayfaya geri döner. RedirectToAction() metodu, belirtilen controller ve action'a yönlendirme yapar.
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ogr = await _context.Ogrenciler.FindAsync(id);
            if (ogr == null)
            {
                return NotFound();
            }
            return View(ogr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Bu satır, Cross-Site Request Forgery (CSRF) saldırılarına karşı koruma sağlar. ValidateAntiForgeryToken attribute'u, form gönderimlerinde bir anti-forgery token'ı doğrular. Bu token, formun gerçekten uygulamanız tarafından gönderildiğini ve kötü niyetli bir üçüncü taraf tarafından oluşturulmadığını doğrulamak için kullanılır. Bu sayede, CSRF saldırılarına karşı ek bir güvenlik katmanı sağlar.
        public async Task<IActionResult> Edit(int? id, Ogrenci model)
        {
            if (id != model.OgrenciId)
            {
                return NotFound();

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model); // Bu satır, Ogrenci modelini veritabanında güncellemek için kullanılır. Update() metodu, modelde yapılan değişiklikleri izler ve bu değişiklikleri veritabanına yansıtmak için SaveChangesAsync() metodunu çağırmamız gerekmektedir.
                    await _context.SaveChangesAsync(); // Bu satır, Ogrenci modelindeki değişiklikleri veritabanına kaydeder. SaveChangesAsync() metodu, yapılan değişiklikleri veritabanına yansıtır ve işlemin tamamlanmasını bekler. Bu sayede, güncellenen öğrenci kaydı veritabanında kalıcı hale gelir.
                }
                catch (Exception)
                {
                    if (!_context.Ogrenciler.Any(o => o.OgrenciId == model.OgrenciId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; // Bu satır, eğer OgrenciId'ye sahip bir öğrenci kaydı veritabanında bulunamazsa NotFound() döndürür. Ancak, eğer öğrenci kaydı mevcutsa ve başka bir hata oluşursa, bu hatayı yeniden fırlatır (throw). Bu sayede, beklenmeyen hatalar için uygun bir hata yönetimi sağlanır.
                    }
                }
                return RedirectToAction("Index");
            }

            return View(model); // Bu satır, Edit işlemi tamamlandıktan sonra kullanıcıyı aynı Edit view'ına yönlendirir. Ancak, bu durumda model geçerli değilse (ModelState.IsValid false ise), kullanıcıya aynı view'ı geri döndürür ve modeldeki hataları göstermek için kullanılır. Eğer model geçerliyse, veritabanında gerekli güncellemeleri yaparak kullanıcıyı başka bir sayfaya yönlendirmek daha uygun olabilir.
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogr = await _context.Ogrenciler.FindAsync(id);

            if (ogr == null)
            {
                return NotFound();
            }

            return View(ogr);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id)
        {
            var ogr = await _context.Ogrenciler.FindAsync(id);

            if(ogr == null)
            {
                return NotFound();
            }

            _context.Ogrenciler.Remove(ogr); // Bu satır, Ogrenci modelini veritabanından silmek için kullanılır. Remove() metodu, belirtilen modeli DbSet'ten kaldırır ve bu değişiklikleri veritabanına yansıtmak için SaveChangesAsync() metodunu çağırmamız gerekmektedir.

            await _context.SaveChangesAsync(); // Bu satır, Ogrenci modelini veritabanından siler. SaveChangesAsync() metodu, yapılan değişiklikleri veritabanına yansıtır ve işlemin tamamlanmasını bekler. Bu sayede, silinen öğrenci kaydı veritabanında kalıcı olarak kaldırılır.

            return RedirectToAction("Index");
        }
    }
}

// Bu, OgrenciController sınıfının bir yapıcı (constructor) metodudur. Bu yapıcı, DataContext türünde bir parametre alır ve bu parametreyi _context adlı özel bir alana atar. Bu sayede, OgrenciController sınıfı içinde _context değişkeni üzerinden veritabanı işlemleri gerçekleştirebiliriz. Dependency Injection (Bağımlılık Enjeksiyonu) kullanarak, DataContext nesnesi otomatik olarak sağlanır ve böylece kodun test edilebilirliği ve bakımı kolaylaşır.

// (ModelState.IsValid) : // Bu satır, modelin geçerli olup olmadığını kontrol eder. ModelState.IsValid, model doğrulama kurallarına göre modelin geçerli olup olmadığını belirler. Eğer model geçerliyse (ModelState.IsValid true ise), bu blok içindeki kod çalışır. Aksi takdirde, model geçersizse (ModelState.IsValid false ise), bu blok atlanır ve kullanıcıya aynı view geri döndürülür, böylece modeldeki hatalar gösterilir.