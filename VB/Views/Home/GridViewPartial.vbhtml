@Html.DevExpress().GridView(Sub(settings)
                                settings.Name = "grid"
                                settings.KeyFieldName = "CustomerID"
                                settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "GridViewPartial"}
                                settings.Width = System.Web.UI.WebControls.Unit.Pixel(500)
                                settings.SettingsEditing.Mode = GridViewEditingMode.Batch
                                settings.SettingsEditing.BatchEditSettings.EditMode = GridViewBatchEditMode.Row
                                settings.SettingsEditing.BatchUpdateRouteValues = New With {.Controller = "Home", .Action = "GridViewEditPartial"}
                                settings.CommandColumn.Visible = True
                                settings.CommandColumn.ShowNewButtonInHeader = True
                                settings.CommandColumn.ShowDeleteButton = True
                                settings.ClientSideEvents.BatchEditEndEditing = "OnBatchEditEndEditing"
                                settings.ClientSideEvents.BatchEditStartEditing = "OnBatchEditStartEditing"
                                settings.ClientSideEvents.BatchEditRowValidating = "OnBatchEditRowValidating"
                                settings.ClientSideEvents.Init = "OnInit"
                                settings.ClientSideEvents.EndCallback = "OnEndCallback"
                                settings.Columns.Add("CustomerID").Visible = False
                                settings.Columns.Add("CustomerName")
                                settings.SettingsEditing.ShowModelErrorsForEditors = True
                                settings.Columns.Add(Sub(columnCountry)
                                                         columnCountry.Caption = "Country"
                                                         columnCountry.FieldName = "CountryID"
                                                         columnCountry.ColumnType = MVCxGridViewColumnType.ComboBox
                                                         Dim propertiesComboBox As ComboBoxProperties = TryCast(columnCountry.PropertiesEdit, ComboBoxProperties)
                                                         propertiesComboBox.DataSource = E4425.Models.DataProvider.GetCountries()
                                                         propertiesComboBox.TextField = "CountryName"
                                                         propertiesComboBox.ValueField = "CountryID"
                                                         propertiesComboBox.ValueType = GetType(Integer)
                                                         propertiesComboBox.EnableSynchronization = DefaultBoolean.False
                                                         propertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.None
                                                         propertiesComboBox.ClientSideEvents.SelectedIndexChanged = "CountriesCombo_SelectedIndexChanged"
                                                     End Sub)
                                settings.CellEditorInitialize = Sub(s, e)
                                                                    Dim editor As ASPxEdit = e.Editor
                                                                    editor.ValidationSettings.Display = Display.None
                                                                End Sub
                                settings.Columns.Add(Sub(columnCity)
                                                         columnCity.Caption = "City"
                                                         columnCity.FieldName = "CityID"
                                                         columnCity.ColumnType = MVCxGridViewColumnType.ComboBox
                                                         Dim propertiesComboBox As ComboBoxProperties = TryCast(columnCity.PropertiesEdit, ComboBoxProperties)
                                                         propertiesComboBox.DataSource = E4425.Models.DataProvider.GetCities()
                                                         propertiesComboBox.TextField = "CityName"
                                                         propertiesComboBox.ValueField = "CityID"
                                                         propertiesComboBox.ValueType = GetType(Integer)
                                                         columnCity.SetEditItemTemplateContent(Sub(c)
                                                                                                   Dim countryID = c.Grid.GetRowValues(0, "CountryID")
                                                                                                   Dim cityID = c.Grid.GetRowValues(0, c.Column.FieldName)
                                                                                                   ViewData("CityID") = cityID
                                                                                                   Dim cities = If((cityID Is Nothing), E4425.Models.DataProvider.GetCities(), E4425.Models.DataProvider.GetCities(CInt(Fix(countryID))))
                                                                                                   ViewData("cities") = cities
                                                                                                   Html.RenderPartial("ComboBoxCityPartial")
                                                                                               End Sub)
                                                     End Sub)
                            End Sub).Bind(Model).GetHtml()