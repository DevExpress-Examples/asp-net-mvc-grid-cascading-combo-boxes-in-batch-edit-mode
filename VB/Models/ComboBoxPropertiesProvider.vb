Imports Microsoft.VisualBasic
Imports DevExpress.Web
Imports DevExpress.Web.Mvc
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI.WebControls

Namespace E4425.Models
	Public Class ComboBoxPropertiesProvider
		Implements IDisposable

        Private Shared _current As ComboBoxPropertiesProvider
        Public Shared ReadOnly Property Current() As ComboBoxPropertiesProvider
            Get
                If _current Is Nothing Then
                    _current = New ComboBoxPropertiesProvider()
                End If
                Return _current
            End Get
        End Property

		Public ReadOnly Property EditableCountryID() As Integer
			Get
				Dim rawCountryId As String = HttpContext.Current.Request("CountryId")
				Return If(String.IsNullOrEmpty(rawCountryId), -1, Integer.Parse(rawCountryId))
			End Get
		End Property
        Private _cityComboBoxProperties As MVCxColumnComboBoxProperties
        Public ReadOnly Property CityComboBoxProperties() As MVCxColumnComboBoxProperties
            Get
                If _cityComboBoxProperties Is Nothing Then
                    _cityComboBoxProperties = CreateCityComboBoxProperties()
                End If
                Return _cityComboBoxProperties
            End Get
        End Property

        Private _countryComboBoxProperties As MVCxColumnComboBoxProperties
        Public ReadOnly Property CountryComboBoxProperties() As MVCxColumnComboBoxProperties
            Get
                If _countryComboBoxProperties Is Nothing Then
                    _countryComboBoxProperties = CreateCountryComboBox()
                End If
                Return _countryComboBoxProperties
            End Get
        End Property

        Private _worldCities As WorldCitiesEntities
        Protected ReadOnly Property WorldCities() As WorldCitiesEntities
            Get
                If _worldCities Is Nothing Then
                    _worldCities = New WorldCitiesEntities()
                End If
                Return _worldCities
            End Get
        End Property

        Protected Function CreateCountryComboBox() As MVCxColumnComboBoxProperties
            Dim cs As New MVCxColumnComboBoxProperties()
            cs.CallbackRouteValues = New With {Key .Controller = "Home", Key .Action = "ComboBoxCountryPartial"}
            cs.Width = Unit.Percentage(100)
            cs.TextField = "CountryName"
            cs.ValueField = "CountryId"
            cs.ValueType = GetType(Integer)
            cs.IncrementalFilteringDelay = 1000
            cs.IncrementalFilteringMode = IncrementalFilteringMode.Contains
            cs.FilterMinLength = 2
            cs.CallbackPageSize = 20
            cs.ClientSideEvents.SelectedIndexChanged = "CountriesCombo_SelectedIndexChanged"
            cs.BindList(WorldCities.Countries.ToList())
            Return cs
        End Function
        Protected Function CreateCityComboBoxProperties() As MVCxColumnComboBoxProperties
            Dim cs As New MVCxColumnComboBoxProperties()
            cs.CallbackRouteValues = New With {Key .Controller = "Home", Key .Action = "ComboBoxCityPartial"}
            cs.Width = Unit.Percentage(100)
            cs.CallbackPageSize = 20
            cs.TextField = "CityName"
            cs.ValueField = "CityId"
            cs.ValueType = GetType(Integer)
            cs.IncrementalFilteringDelay = 1000
            cs.IncrementalFilteringMode = IncrementalFilteringMode.Contains
            cs.FilterMinLength = 2
            cs.ValidationSettings.Display = Display.None
            cs.BindList(AddressOf OnItemsRequestedByFilterCondition, AddressOf OnItemRequestedByValue)
            cs.IncrementalFilteringMode = IncrementalFilteringMode.Contains
            cs.ClientSideEvents.BeginCallback = "CitiesCombo_BeginCallback"
            cs.ClientSideEvents.EndCallback = "CitiesCombo_EndCallback"
            Return cs
        End Function

        Protected Function OnItemRequestedByValue(ByVal e As ListEditItemRequestedByValueEventArgs) As Object
            Dim id As Integer
            If e.Value Is Nothing OrElse (Not Integer.TryParse(e.Value.ToString(), id)) Then
                Return Nothing
            End If
            Dim query = WorldCities.Cities.Where(Function(city) city.CityId = id)
            Return query.ToList()

        End Function
        Protected Function OnItemsRequestedByFilterCondition(ByVal e As ListEditItemsRequestedByFilterConditionEventArgs) As Object
            Dim query As IQueryable(Of City)
            Dim skip = e.BeginIndex
            Dim take = e.EndIndex - e.BeginIndex + 1
            If EditableCountryID > -1 Then
                query = WorldCities.Cities.Where(Function(city) city.CityName.Contains(e.Filter) AndAlso city.Country.CountryId = EditableCountryID).OrderBy(Function(city) city.CityId)
            Else
                query = WorldCities.Cities.Where(Function(city) city.CityName.Contains(e.Filter)).OrderBy(Function(city) city.CityId)
            End If
            query = query.Skip(skip).Take(take)
            Return query.ToList()
        End Function

#Region "IDisposable Members"
        Public Sub Dispose() Implements IDisposable.Dispose
            If Me._worldCities IsNot Nothing Then
                _worldCities.Dispose()
            End If
        End Sub
#End Region
	End Class
End Namespace