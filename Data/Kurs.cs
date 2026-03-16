using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Data
{
    public class Kurs
    {
        [Display(Name = "Kurs ID")]
        public int KursId { get; set; } // Id dışında Class'ın adını da kullanarak Primary key oluşturabiliriz. [Key] attribute'u eklememize gerek kalmaz.
        [Display(Name = "Kurs Başlığı")]
        public string? Baslik { get; set; }

        public Ogretmen Ogretmen { get; set; } = null!; // Kurs sınıfında Ogretmen türünde bir property tanımlıyoruz. Bu, her kursun bir öğretmeni olduğunu belirtir. null! ifadesi, bu property'nin null olamayacağını ancak başlangıçta null olarak atanabileceğini belirtir.

        public int? OgretmenId { get; set; } // Bu property, Kurs sınıfında OgretmenId adında bir int türünde property tanımlıyoruz. Bu, her kursun bir öğretmenle ilişkilendirileceğini belirtir. OgretmenId, Kurs tablosunda bir foreign key olarak kullanılacaktır.

        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();

    }
}
