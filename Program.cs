namespace YouTubeEğitim
{
    class Program
    {
        static void Main(string[] args)
        {
            //Referans Tipleri 
            //Bellekte 2 alan vardır . Birisi stack diğer heap'dir.Stack'de sayısal veriler tutulur.
            //int sayi1 = 10;
            //int sayi2 = 20;
            //sayi1 = sayi2;
            //sayi2 = 100;

            //Arrayler referans tiptir.Referans tipler heap bölümünde tutulur

            //stack alanı  = heap alanı
            //int[] sayilar1 = new[] { 1, 2, 3 };
            //int[] sayilar2 = new[] {10,20,30 };
            //burada değer ataması değil referansını(adres) atama yaptık.sayilar1'in adresindekiler garbage collection ile temizlenir
            //sayilar1 = sayilar2;
            //sayilar2[0] = 1000;

            //Console.WriteLine(sayilar1[0]);

            //CreditManager creditManager = new CreditManager();
            //creditManager.Calculate();
            //creditManager.Save();




            //Customer customer = new Customer(); //örneğini oluşturma yada instance oluşturma, instance creation
            //customer.Id = 1;
            //customer.City = "Ankara";

            //CustomerManager customerManager = new CustomerManager(customer);
            //customerManager.Save();
            //customerManager.Delete();

            //Company company = new Company();
            //company.TaxNumber = "1000000";
            //company.Id = 200;
            //company.CompanyName = "Samsung";

            //CustomerManager customerManager1 = new CustomerManager(new Person());


            //Person person = new Person();
            //person.NationalIdentity = "123456789";


            //Console.ReadLine();

            //IoC Container => IoC (Inversion of Control) Container, yazılım geliştirme sürecinde kullanılan bir tasarım deseni ve bir tür yazılım bileşeni yönetimi aracıdır.
            //Bu containerlar, genellikle bağımlılık enjeksiyonu (dependency injection) tekniklerini kullanarak bileşenler arasındaki bağımlılıkları çözerler. Bu, kodun daha esnek, yeniden kullanılabilir ve test edilebilir olmasını sağlar.
            CustomerManager customerManager = new CustomerManager(new Customer(), new TeacherCreditManager());
            customerManager.GiveCredit();

            Console.ReadLine();

           
        }

        //Class => İçerisinde yapacağımız işlemleri tutan veya herhangi bir konuda bilgi tutan yapılardır
        //Bir class sadece bir class'ı inherit edebilir
        //Bir class birden fazla interface'i implemente edebilir. 
        //Bir class sadece bir abstract class inherit edebilir
        //Abstractlar interfacelerde asla newlenemez.İkiside referans tiplerin özelliklerinden yararlanır.


        class CreditManager
        {
            public void Calculate()
            {
                Console.WriteLine("Hesaplandı");
            }

            public void Save()
            {
                Console.WriteLine("Kredi verildi.");
            }
        }

        //INTERFACE => İş yapan sınıfların operasyonlarını  sadece imza seviyesinde yazarak yazılımda bağımlılığı korumak adına yapılan çalışmadır.
        //Interfaceler referans tiplerdir.İmplemente edilenlerin yerini tutabilir.
        //sonar qube => yazılan kodlarda ne kadar if olduğuna bakar zafiyetleri söyler
        //Interfacelerle methodun ne döndürdüğünü ismini ve varsa parametreleri yazarız.Buna da methodların imzası anlamına gelir
        //Eğer yeni classlar oluşturup bir interface'i bu class'da kullanacaksak o interface içindeki işlemler extend edilen class'da da olmalıdır.

        interface ICreditManager
        {
            void Calculate();
            void Save();
        }


        class TeacherCreditManager :BaseCreditManager ,ICreditManager
        {
            //Calculate operasyonu imza(interface) halinde olduğu için override(üstüne yazmak) yaptık.
            public override  void Calculate()
            {
                Console.WriteLine("Öğretmen kredisi hesaplandı");
            }

            public override void Save()
            {
                //Oluşturulan virtual ile istersen direk böyle çalıştırıp istersek işlem ekleyebiliyoruz.
                //base = inherit edilen sınıf
                base.Save();
            }
        }

        class MilitaryCreditManager : BaseCreditManager, ICreditManager
        {
            public override void Calculate()
            {
                Console.WriteLine("Asker kredisi hesaplandı");
            }

          
        }

        class CarCreditManager : BaseCreditManager, ICreditManager
        {
            public override void Calculate()
            {
                Console.WriteLine("Araba kredisi hesaplandı.");
            }
        }

        //Abstract Class => Bizim için ortak operasyonları tutar.Tamamlanmış olanları daha detaylı tamamlanmamış olanları sade şekilde tutar.

        //DRY => Do not repeat yourself

        abstract class BaseCreditManager : ICreditManager
        {
            public abstract void Calculate(); //ortak olmayan heryerde değişen

            //virtual => sanal
            public virtual void Save()
            {
                Console.WriteLine("Kaydedildi.");
            }
        }

        // Classlar özellik tutucu olarak da kullanılabilir
        //SOLID(YAZILIM GELİŞTİRME PRENSİPLERİ)
        //bir class içinde bilgi tutup aynı zamanda bir işlem(operasyon) eklenirse solid prensiplerine aykırı olur

        class Customer
        {
            public int Id { get; set; }
           
            public string City { get; set; }

            //Constructor Methot => Yapıcı metot
            public Customer()
            {
                Console.WriteLine("Müşteri Nesnesi Başlatıldı.");
            }
        }

        class Person : Customer
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string NationalIdentity { get; set; }
        }

        //Inheritance => Kalıtım(Miras) ":" işareti ile extends(inherit) olmuş olur
        //Ineritance ile extends ettiğimiz sınıftaki özellikleri alır ayrıca kendi sınıfı içinde ek özellikler tanımlanır.

        class Company : Customer
        {
            public string CompanyName {  get; set; }
            public string TaxNumber { get; set; }
        }




        //KATMANLI MİMARİLER'de herşey katman halinde tutulur. 
        class CustomerManager
        {
            //Burada private olarak _customer alanı oluşturuyoruz.Private olduğu için başında alt çizgi var bu class içindeki bütün metotlarda kullanılabilir.
            private Customer _customer;
            ICreditManager _creditManager;
            public CustomerManager(Customer customer, ICreditManager creditManager)
            {
                _customer = customer;
                _creditManager = creditManager;
            }
            public void Save()
            {
                Console.WriteLine("Müşteri Kaydedildi.");
            }

            public void Delete()
            {
                Console.WriteLine("Müşteri Silindi.");
            }

            public void GiveCredit()
            {
                _creditManager.Calculate();
                Console.WriteLine("Kredi Verildi.");
            }
        }


    }
}
