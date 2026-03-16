using EFCoreApp.Data;
using EFCoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Controllers
{
    public class KursController : Controller
    {
        private readonly DataContext _context;
        public KursController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var kurslar = await _context.Kurslar.Include(k => k.Ogretmen).ToListAsync(); // Kurslar tablosundan tüm kursları asenkron olarak alır ve her kursun öğretmen bilgilerini de dahil eder
            return View(kurslar);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyadOgretmen"); // Ogretmenler tablosundan tüm öğretmenleri asenkron olarak alır ve bir SelectList'e dönüştürerek ViewBag'e atar
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Kurs model)
        {
            _context.Kurslar.Add(model); // Kurslar tablosuna yeni bir kurs ekliyoruz
            await _context.SaveChangesAsync(); // Değişiklikleri veritabanına kaydediyoruz
            return RedirectToAction("Index"); // İşlem tamamlandıktan sonra Index sayfasına yönlendiriyoruz
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context
                .Kurslar
                .Include(k => k.KursKayitlari) // Kurs kayitlarini da dahil ediyoruz
                .ThenInclude(k => k.Ogrenci) // Kurs kayitlarindaki ogrenci bilgilerini de dahil ediyoruz
                .Select(k => new KursViewModel
                {
                    KursId = k.KursId,
                    Baslik = k.Baslik,
                    OgretmenId = k.OgretmenId,
                    KursKayitlari = k.KursKayitlari
                })
                .FirstOrDefaultAsync(k => k.KursId == id); // Id'ye göre kursu buluyoruz

            if (kurs == null)
            {
                return NotFound();
            }

            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyadOgretmen"); // Ogretmenler tablosundan tüm öğretmenleri asenkron olarak alır ve bir SelectList'e dönüştürerek ViewBag'e atar
            return View(kurs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KursViewModel model)
        {
            if (id != model.KursId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(new Kurs() {KursId = model.KursId, Baslik = model.Baslik, OgretmenId = model.OgretmenId });
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!_context.Kurslar.Any(k => k.KursId == model.KursId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurslar.FindAsync(id);

            if (kurs == null)
            {
                return NotFound();
            }

            return View(kurs);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var kurs = await _context.Kurslar.FindAsync(id);

            if (kurs == null)
            {
                return NotFound();
            }

            _context.Kurslar.Remove(kurs);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
