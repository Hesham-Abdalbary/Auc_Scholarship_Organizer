using System.Web;
using System.Web.Mvc;

namespace Auc_Scholarship_Organizer
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
