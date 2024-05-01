using System.ComponentModel;
namespace Hawalayk_APP.Enums
{
    public enum CraftName
    {
        [Description("سباك")]
        plumber,

        [Description("نجار")]
        carpenter,

        [Description("نقاش")]
        painter,

        [Description("رسام حائط")]
        muralArtist,

        [Description("طباخ منزلي")]
        homeCook,

        [Description("كهربائي")]
        electrician,

        [Description("كهربائي أجهزة")]
        electricApplianceTechnicians,

        [Description("يدوي")]
        handmade,

        [Description("ممرض")]
        nurse,

        [Description("ميكانيكي")]
        mechanic,

        [Description("تركيب الزجاج")]
        glazier,

        [Description("عامل سيراميك")]
        ceramist,

        [Description("عامل نظافة")]
        cleaner,

        [Description("فني ماكينة خياطة")]
        sewingMachineMechanic,

        [Description("خياط")]
        tailor,

        [Description("مبيض محارة")]
        plasterer,

        [Description("لحام")]
        welder,

        [Description("حداد")]
        blacksmith,

        [Description("فني تكييف وتبريد")]
        HVACTechnician,

        [Description("حرفيين")]
        artisans,

        [Description("نجار مسلح")]
        reinforcementIronworker,

        [Description("توصيل")]
        delivery,

        [Description("تصوير")]
        photography
    }

    //[AttributeUsage(AttributeTargets.Field)]
    //public class DescriptionAttribute : Attribute
    //{
    //    public string Arabic { get; }

    //    public DescriptionAttribute(string arabic)
    //    {
    //        Arabic = arabic;
    //    }
    //}
}
