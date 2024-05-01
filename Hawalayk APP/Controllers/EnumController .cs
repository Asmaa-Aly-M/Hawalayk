//using Hawalayk_APP.Enums;
//using Microsoft.AspNetCore.Mvc;

//namespace Hawalayk_APP.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EnumController : ControllerBase
//    {
//        [HttpGet("ReportedIssueValues")]
//        public IActionResult GetReportedIssueValues()
//        {
//            // Get all values of the ReportedIssue enum
//            Array enumValues = Enum.GetValues(typeof(ReportedIssue));
//            List<string> enumStringValues = enumValues
//                .Cast<ReportedIssue>()
//                .Select(e => e.ToString())
//                .ToList();

//            return Ok(enumStringValues);
//        }
//    }

//}
using Hawalayk_APP.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Reflection;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnumController : ControllerBase
    {
        [HttpGet("ReportedIssueValues")]
        public ActionResult<IEnumerable<string>> getArabicValuesOfReportedIssue()
        {
            List<string> arabicDescriptionsOfReportedIssue = getArabicValues<ReportedIssue>();
            return Ok(arabicDescriptionsOfReportedIssue);
        }

        //private static List<string> GetArabicValues<T>()
        //{
        //    List<string> descriptions = new List<string>();
        //    Type enumType = typeof(T);

        //    if (enumType.IsEnum)
        //    {
        //        FieldInfo[] fields = enumType.GetFields();
        //        foreach (FieldInfo field in fields)
        //        {
        //            if (field.FieldType == enumType)
        //            {
        //                DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
        //                string description = (attributes.Length > 0) ? attributes[0].Description : field.Name;
        //                descriptions.Add(description);
        //            }
        //        }
        //    }

        //    return descriptions;
        //}


        [HttpGet("PostStatusValues")]
        public ActionResult<IEnumerable<string>> getArabicValuesOfPostStatusValues()
        {
            List<string> arabicDescriptionsOfReportedIssue = getArabicValues<PostStatus>();
            return Ok(arabicDescriptionsOfReportedIssue);
        }



        [HttpGet("CraftsNameValues")]
        public ActionResult<IEnumerable<string>> getArabicValuesOfCraftName()
        {
            List<string> arabicValues = getArabicValues<CraftName>();
            return Ok(arabicValues);
        }

        private static List<string> getArabicValues<T>()
        {
            List<string> descriptions = new List<string>();
            Type enumType = typeof(T);

            if (enumType.IsEnum)
            {
                FieldInfo[] fields = enumType.GetFields();
                foreach (FieldInfo field in fields)
                {
                    if (field.FieldType == enumType)
                    {
                        DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        string description = (attributes.Length > 0) ? attributes[0].Description : field.Name;
                        descriptions.Add(description);
                    }
                }
            }

            return descriptions;
        }


        //[HttpPost("arEnum")]
        //public ActionResult<ReportedIssue?> ConvertArabicToEnum(string arabicString)
        //{
        //    ReportedIssue? enumValue = ConvertToEnum<ReportedIssue>(arabicString);
        //    if (enumValue.HasValue)
        //        return Ok(enumValue);
        //    else
        //        return BadRequest("Invalid Arabic string.");
        //}

        //private static T? ConvertToEnum<T>(string arabicString) where T : struct
        //{
        //    Type enumType = typeof(T);

        //    if (enumType.IsEnum)
        //    {
        //        foreach (FieldInfo field in enumType.GetFields())
        //        {
        //            if (Attribute.GetCustomAttribute(field,
        //                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
        //            {
        //                if (attribute.Description == arabicString)
        //                {
        //                    return (T)field.GetValue(null);
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}
    }
}

