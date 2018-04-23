Imports Microsoft.VisualBasic
Imports System.Web.Mvc
Imports E4425.Models
Imports DevExpress.Web.Mvc

Namespace E4425.Controllers
	Public Class HomeController
		Inherits Controller
		Public Function Index() As ActionResult
			Return View(DataProvider.GetCustomers())
		End Function
		Public Function GridViewPartial() As ActionResult
			Return PartialView(DataProvider.GetCustomers())
		End Function
		Public Function GridViewEditPartial(ByVal modifiedValues As MVCxGridViewBatchUpdateValues(Of Customer, Integer)) As ActionResult
			For Each customerInfo In modifiedValues.Insert
				If modifiedValues.IsValid(customerInfo) Then
					DataProvider.InsertCustomer(customerInfo)
				End If
			Next customerInfo
			For Each customerInfo In modifiedValues.Update
				If modifiedValues.IsValid(customerInfo) Then
					DataProvider.UpdateCustomer(customerInfo)
				End If
			Next customerInfo
			For Each customerID In modifiedValues.DeleteKeys
				DataProvider.DeleteCustomer(customerID)
			Next customerID
			Return PartialView("GridViewPartial", DataProvider.GetCustomers())
		End Function

        Public Function ComboBoxCityPartial() As ActionResult
            Dim countryID As Integer = If((Request.Params("CountryID") IsNot Nothing), Integer.Parse(Request.Params("CountryID")), -1)
            ViewData("cities") = DataProvider.GetCities(countryID)
            Return PartialView(DataProvider.GetCustomers())
        End Function
	End Class
End Namespace