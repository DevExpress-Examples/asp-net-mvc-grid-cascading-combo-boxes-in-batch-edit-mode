using System.Web.Mvc;
using E4425.Models;
using DevExpress.Web.Mvc;
using System.Linq;
namespace E4425.Controllers
{
    public class HomeController : Controller
    {
        WorldCitiesEntities entity = new WorldCitiesEntities();
        public ActionResult Index()
        {
            return View(entity.Customers.ToList());
        }
        public ActionResult GridViewPartial()
        {
            return PartialView(entity.Customers.ToList());
        }
        public ActionResult GridViewEditPartial(MVCxGridViewBatchUpdateValues<Customer, int> modifiedValues)
        {
            foreach (var customerInfo in modifiedValues.Insert)
            {
                if (modifiedValues.IsValid(customerInfo))
                    entity.Customers.Add(customerInfo);
            }
            foreach (var customerInfo in modifiedValues.Update)
            {
                if (modifiedValues.IsValid(customerInfo))
                {
                    entity.Customers.Attach(customerInfo);
                    var entry = entity.Entry(customerInfo);
                    entry.Property(e => e.CityId).IsModified = true;
                    entry.Property(e => e.CountryId).IsModified = true;
                    entry.Property(e => e.CustomerName).IsModified = true;
                }
            }
            foreach (var customerID in modifiedValues.DeleteKeys)
            {
                entity.Customers.Remove(entity.Customers.Find(customerID));
            }
            // uncomment the next line to enable database updates
            // entity.SaveChanges();
            return PartialView("GridViewPartial", entity.Customers.ToList());
        }
        public ActionResult ComboBoxCountryPartial()
        {
            return GridViewExtension.GetComboBoxCallbackResult(ComboBoxPropertiesProvider.Current.CountryComboBoxProperties);
        }
        public ActionResult ComboBoxCityPartial()
        {
            return GridViewExtension.GetComboBoxCallbackResult(ComboBoxPropertiesProvider.Current.CityComboBoxProperties);
        }
    }

}