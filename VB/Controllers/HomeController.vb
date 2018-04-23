Imports Microsoft.VisualBasic
Imports System.Web.Mvc
Imports E4425.Models
Imports DevExpress.Web.Mvc
Imports System.Linq
Namespace E4425.Controllers
	Public Class HomeController
		Inherits Controller
		Private entity As New WorldCitiesEntities()
		Public Function Index() As ActionResult
			Return View(entity.Customers.ToList())
		End Function
		Public Function GridViewPartial() As ActionResult
			Return PartialView(entity.Customers.ToList())
		End Function
		Public Function GridViewEditPartial(ByVal modifiedValues As MVCxGridViewBatchUpdateValues(Of Customer, Integer)) As ActionResult
			For Each customerInfo In modifiedValues.Insert
				If modifiedValues.IsValid(customerInfo) Then
					entity.Customers.Add(customerInfo)
				End If
			Next customerInfo
			For Each customerInfo In modifiedValues.Update
				If modifiedValues.IsValid(customerInfo) Then
					entity.Customers.Attach(customerInfo)
					Dim entry = entity.Entry(customerInfo)
					entry.Property(Function(e) e.CityId).IsModified = True
					entry.Property(Function(e) e.CountryId).IsModified = True
					entry.Property(Function(e) e.CustomerName).IsModified = True
				End If
			Next customerInfo
			For Each customerID In modifiedValues.DeleteKeys
				entity.Customers.Remove(entity.Customers.Find(customerID))
            Next customerID
            'uncomment the next line to enable database updates
            'entity.SaveChanges()
			Return PartialView("GridViewPartial", entity.Customers.ToList())
		End Function
		Public Function ComboBoxCountryPartial() As ActionResult
			Return GridViewExtension.GetComboBoxCallbackResult(ComboBoxPropertiesProvider.Current.CountryComboBoxProperties)
		End Function
		Public Function ComboBoxCityPartial() As ActionResult
			Return GridViewExtension.GetComboBoxCallbackResult(ComboBoxPropertiesProvider.Current.CityComboBoxProperties)
		End Function
	End Class

End Namespace