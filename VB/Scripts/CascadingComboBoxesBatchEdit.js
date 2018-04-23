var curentEditingIndex;
var lastCountry = null;
var isCustomCascadingCallback = false;
function OnBatchEditStartEditing(s, e) {
    curentEditingIndex = e.visibleIndex;
    var currentCountry = grid.batchEditApi.GetCellValue(curentEditingIndex, "CountryId");
    if (currentCountry != lastCountry && e.focusedColumn.fieldName == "CityId" && currentCountry != null) {
        lastCountry = currentCountry;
        grid.GetEditor("CityId").PerformCallback();        
    }
}
function CountriesCombo_SelectedIndexChanged(s, e) {
    lastCountry = s.GetValue();
    isCustomCascadingCallback = true;
    grid.GetEditor("CityId").PerformCallback();
}
function CitiesCombo_BeginCallback(s, e) {
    e.customArgs['CountryId'] = lastCountry;
}
function CitiesCombo_EndCallback(s, e) {
    if (isCustomCascadingCallback) {
        if (s.GetItemCount() > 0)
            grid.batchEditApi.SetCellValue(curentEditingIndex, "CityId", s.GetItem(0).value);
        isCustomCascadingCallback = false;
    }
}
