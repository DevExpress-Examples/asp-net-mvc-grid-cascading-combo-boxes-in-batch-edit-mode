@modeltype List(Of E4425.Models.Customer)
@Html.DevExpress().ComboBoxFor(Function(item) item.First().CityID, Sub(settings)
                                                                       settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "ComboBoxCityPartial"}
                                                                       settings.Width = System.Web.UI.WebControls.Unit.Percentage(100)
                                                                       settings.Properties.TextField = "CityName"
                                                                       settings.Properties.ValueField = "CityID"
                                                                       settings.Properties.ValueType = GetType(Integer)
                                                                       settings.ShowModelErrors = True
                                                                       settings.Properties.ValidationSettings.Display = Display.None
                                                                       settings.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                                                       settings.Properties.ClientSideEvents.BeginCallback = "CitiesCombo_BeginCallback"
                                                                       settings.Properties.ClientSideEvents.EndCallback = "CitiesCombo_EndCallback"
                                                                       settings.Properties.ClientSideEvents.LostFocus = "CitiesCombo_LostFocus"
                                                                       settings.Properties.ClientSideEvents.KeyDown = "CitiesCombo_KeyDown"
                                                                   End Sub).BindList(ViewData("cities")).GetHtml()