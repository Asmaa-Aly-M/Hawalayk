using System.ComponentModel;

namespace Hawalayk_APP.Enums
{
    public enum ReportedIssue
    {
        [Description("خلل")]
        BUG,
        [Description("تعطل")]
        CRASH,
        [Description("أداء")]
        PERFORMANCE,
        [Description("واجهة المستخدم")]
        UI_UX,
        [Description("المصادقة عند تسجيل الدخول")]
        LOGIN_AUTHENTICATION,
        [Description("فقدان البيانات")]
        DATA_LOSS,
        [Description("اتصال الشبكة")]
        NETWORK_CONNECTION,
        [Description("طلب ميزة")]
        FEATURE_REQUEST,
        [Description("الأمان")]
        SECURITY,
        [Description("أخرى")]
        OTHER
    }
}
