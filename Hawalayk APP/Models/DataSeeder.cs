using Hawalayk_APP.Context;

namespace Hawalayk_APP.Models
{
    public class DataSeeder
    {
        public void SeedGovernoratesData(ApplicationDbContext context)
        {
            using (context)
            {
                context.Database.EnsureCreated();
                if (context.governorates.Any())
                {
                    return;
                }

                context.governorates.AddRange(new List<Governorate>
                {
                    new Governorate { id = 1, governorate_name_ar = "القاهرة", governorate_name_en = "Cairo" },
                    new Governorate { id = 2, governorate_name_ar = "الجيزة", governorate_name_en = "Giza" },
                    new Governorate { id = 3, governorate_name_ar = "الأسكندرية", governorate_name_en = "Alexandria" },
                    new Governorate { id = 4, governorate_name_ar = "الدقهلية", governorate_name_en = "Dakahlia" },
                    new Governorate { id = 5, governorate_name_ar = "البحر الأحمر", governorate_name_en = "Red Sea" },
                    new Governorate { id = 6, governorate_name_ar = "البحيرة", governorate_name_en = "Beheira" },
                    new Governorate { id = 7, governorate_name_ar = "الفيوم", governorate_name_en = "Fayoum" },
                    new Governorate { id = 8, governorate_name_ar = "الغربية", governorate_name_en = "Gharbiya" },
                    new Governorate { id = 9, governorate_name_ar = "الإسماعلية", governorate_name_en = "Ismailia" },
                    new Governorate { id = 10, governorate_name_ar = "المنوفية", governorate_name_en = "Menofia" },
                    new Governorate { id = 11, governorate_name_ar = "المنيا", governorate_name_en = "Minya" },
                    new Governorate { id = 12, governorate_name_ar = "القليوبية", governorate_name_en = "Qaliubiya" },
                    new Governorate { id = 13, governorate_name_ar = "الوادي الجديد", governorate_name_en = "New Valley" },
                    new Governorate { id = 14, governorate_name_ar = "السويس", governorate_name_en = "Suez" },
                    new Governorate { id = 15, governorate_name_ar = "اسوان", governorate_name_en = "Aswan" },
                    new Governorate { id = 16, governorate_name_ar = "اسيوط", governorate_name_en =  "Assiut" },
                    new Governorate { id = 17, governorate_name_ar = "بني سويف", governorate_name_en = "Beni Suef" },
                    new Governorate { id = 18, governorate_name_ar = "بورسعيد", governorate_name_en = "Port Said" },
                    new Governorate { id = 19, governorate_name_ar = "دمياط", governorate_name_en = "Damietta" },
                    new Governorate { id = 20, governorate_name_ar = "الشرقية", governorate_name_en = "Sharkia" },
                    new Governorate { id = 21, governorate_name_ar = "جنوب سيناء", governorate_name_en = "South Sinai" },
                    new Governorate { id = 22, governorate_name_ar = "كفر الشيخ", governorate_name_en = "Kafr Al sheikh" },
                    new Governorate { id = 23, governorate_name_ar = "مطروح", governorate_name_en = "Matrouh" },
                    new Governorate { id = 24, governorate_name_ar = "الأقصر", governorate_name_en = "Luxor" },
                    new Governorate { id = 25, governorate_name_ar = "قنا", governorate_name_en = "Qena" },
                    new Governorate { id = 26, governorate_name_ar = "شمال سيناء", governorate_name_en = "North Sinai" },
                    new Governorate { id = 27, governorate_name_ar = "سوهاج", governorate_name_en = "Sohag" }
                });

                context.SaveChanges();
            }
        }
        public void SeedCitiesData(ApplicationDbContext context)
        {
            using (context)
            {
                context.Database.EnsureCreated();
                if (context.cities.Any())
                {
                    return;
                }

                int id = 1;
                context.cities.AddRange(new List<City>
                {
                    // Cities for Cairo (governorate_id = 1)
                    new City { id = id++, governorate_id = 1, city_name_ar = "15 مايو", city_name_en = "15 May" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الازبكية", city_name_en = "Al Azbakeyah" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "البساتين", city_name_en = "Al Basatin" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "التبين", city_name_en = "Tebin" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الخليفة", city_name_en = "El-Khalifa" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الدراسة", city_name_en = "El darrasa" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الدرب الاحمر", city_name_en = "Aldarb Alahmar" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الزاوية الحمراء", city_name_en = "Zawya al-Hamra" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الزيتون", city_name_en = "El-Zaytoun" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الساحل", city_name_en = "Sahel" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "السلام", city_name_en = "El Salam" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "السيدة زينب", city_name_en = "Sayeda Zeinab" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الشرابية", city_name_en = "El Sharabeya" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "مدينة الشروق", city_name_en = "Shorouk" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الظاهر", city_name_en = "El Daher" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "العتبة", city_name_en = "Ataba" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "القاهرة الجديدة", city_name_en = "New Cairo" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "المرج", city_name_en = "El Marg" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "عزبة النخل", city_name_en = "Ezbet el Nakhl" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "المطرية", city_name_en = "Matareya" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "المعادى", city_name_en = "Maadi" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "المعصرة", city_name_en = "Maasara" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "المقطم", city_name_en = "Mokattam" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "المنيل", city_name_en = "Manyal" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الموسكى", city_name_en = "Mosky" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "النزهة", city_name_en = "Nozha" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الوايلى", city_name_en = "Waily" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "باب الشعرية", city_name_en = "Bab al-Shereia" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "بولاق", city_name_en = "Bolaq" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "جاردن سيتى", city_name_en = "Garden City" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "حدائق القبة", city_name_en = "Hadayek El-Kobba" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "حلوان", city_name_en = "Helwan" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "دار السلام", city_name_en = "Dar Al Salam" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "شبرا", city_name_en = "Shubra" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "طره", city_name_en = "Tura" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "عابدين", city_name_en = "Abdeen" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "عباسية", city_name_en = "Abaseya" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "عين شمس", city_name_en = "Ain Shams" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "مدينة نصر", city_name_en = "Nasr City" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "مصر الجديدة", city_name_en = "New Heliopolis" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "مصر القديمة", city_name_en = "Masr Al Qadima" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "منشية ناصر", city_name_en = "Mansheya Nasir" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "مدينة بدر", city_name_en = "Badr City" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "مدينة العبور", city_name_en = "Obour City" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "وسط البلد", city_name_en = "Cairo Downtown" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الزمالك", city_name_en = "Zamalek" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "قصر النيل", city_name_en = "Kasr El Nile" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الرحاب", city_name_en = "Rehab" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "القطامية", city_name_en = "Katameya" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "مدينتي", city_name_en = "Madinty" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "روض الفرج", city_name_en = "Rod Alfarag" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "شيراتون", city_name_en = "Sheraton" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الجمالية", city_name_en = "El-Gamaleya" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "العاشر من رمضان", city_name_en = "10th of Ramadan City" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "الحلمية", city_name_en = "Helmeyat Alzaytoun" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "النزهة الجديدة", city_name_en = "New Nozha" },
                    new City { id = id++, governorate_id = 1, city_name_ar = "العاصمة الإدارية", city_name_en = "Capital New" }
                });
            }

        }
    }
}
