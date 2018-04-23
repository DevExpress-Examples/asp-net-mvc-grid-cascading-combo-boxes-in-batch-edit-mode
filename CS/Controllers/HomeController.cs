using System.Web.Mvc;
using E4425.Models;
using DevExpress.Web.Mvc;

namespace E4425.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View(DataProvider.GetCustomers());
        }
        public ActionResult GridViewPartial() {
            return PartialView(DataProvider.GetCustomers());
        }
        public ActionResult GridViewEditPartial(MVCxGridViewBatchUpdateValues<Customer, int> modifiedValues) {
            foreach(var customerInfo in modifiedValues.Insert) {
                if(modifiedValues.IsValid(customerInfo))
                    DataProvider.InsertCustomer(customerInfo);
            }
            foreach(var customerInfo in modifiedValues.Update) {
                if(modifiedValues.IsValid(customerInfo))
                    DataProvider.UpdateCustomer(customerInfo);
            }
            foreach(var customerID in modifiedValues.DeleteKeys) {
                DataProvider.DeleteCustomer(customerID);
            }
            return PartialView("GridViewPartial", DataProvider.GetCustomers());
        }

        public ActionResult ComboBoxCityPartial() {
            int countryID = (Request.Params["CountryID"] != null) ? int.Parse(Request.Params["CountryID"]) : -1;
            ViewData["cities"] = DataProvider.GetCities(countryID);
            return PartialView(DataProvider.GetCustomers());
        }
    }
}