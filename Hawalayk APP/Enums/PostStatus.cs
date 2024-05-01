using System.ComponentModel;

namespace Hawalayk_APP.Enums
{
    public enum PostStatus
    {
        [Description("معرض")]
        Gallery,

        [Description("معرض الأعمال")]
        Portfolio,

        [Description("كلاهما")]
        Both
    }

}
