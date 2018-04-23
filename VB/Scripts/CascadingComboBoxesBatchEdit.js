var currentEditableVisibleIndex;
var preventEndEditOnLostFocus = false;
var lastCountryID;
var setValueFlag;
function CountriesCombo_SelectedIndexChanged(s, e) {
    var currentValue = s.GetValue();
    if (lastCountryID == currentValue) {
        if (CityID.GetSelectedIndex() < 0)
            CityID.SetSelectedIndex(0);
        return;
    }
    lastCountryID = currentValue;
    CityID.PerformCallback();
}
function CitiesCombo_BeginCallback(s, e) {
    e.customArgs['CountryID'] = lastCountryID;
}
function IntializeGlobalVariables(grid) {
    lastCountryID = -1;
    currentEditableVisibleIndex = -1;
    setValueFlag = -1;
}
function OnInit(s, e) {
    IntializeGlobalVariables(s);
}
function OnEndCallback(s, e) {
    IntializeGlobalVariables(s);
}
function CitiesCombo_EndCallback(s, e) {
    
    if (setValueFlag == -1) {
        if (s.GetSelectedIndex() == -1) {
            s.SetSelectedIndex(0);
        }
    }
    else if (setValueFlag > -1) {
        CityID.SetSelectedItem(CityID.FindItemByValue(setValueFlag));
        setValueFlag = -1;
    }
}
function OnBatchEditStartEditing(s, e) {
    currentEditableVisibleIndex = e.visibleIndex;
    var currentCountryID = grid.batchEditApi.GetCellValue(currentEditableVisibleIndex, "CountryID");
    var cityIDColumn = s.GetColumnByField("CityID");
    if (!e.rowValues.hasOwnProperty(cityIDColumn.index))
        return;
    var cellInfo = e.rowValues[cityIDColumn.index];
    if (lastCountryID == currentCountryID)
        if (CityID.FindItemByValue(cellInfo.value) != null)
            CityID.SetValue(cellInfo.value);
        else
            RefreshData(cellInfo);
    else {
        if (currentCountryID == null) {
            CityID.SetSelectedIndex(-1);
            return;
        }
        lastCountryID = currentCountryID;
        RefreshData(cellInfo);
    }
}
function RefreshData(cellInfo) {
    setValueFlag = cellInfo.value;
    CityID.PerformCallback();
}
function OnBatchEditEndEditing(s, e) {
    currentEditableVisibleIndex = -1;
    var cityIDColumn = s.GetColumnByField("CityID");
    if (!e.rowValues.hasOwnProperty(cityIDColumn.index))
        return;
    $("form").valid();
    var cellInfo = e.rowValues[cityIDColumn.index];
    if (CityID.GetSelectedIndex() > -1 || cellInfo.text != CityID.GetText()) {
        cellInfo.value = CityID.GetValue();
        cellInfo.text = CityID.GetText();
        CityID.SetValue(null);
    }
}
function OnBatchEditRowValidating(s, e) {
    var cityIDColumn = s.GetColumnByField("CityID");
    var cellValidationInfo = e.validationInfo[cityIDColumn.index];
    if (!cellValidationInfo) return;
    var value = cellValidationInfo.value;
    cellValidationInfo.isValid = CityID.GetIsValid();
    cellValidationInfo.errorText = CityID.GetErrorText();
}
function CitiesCombo_KeyDown(s, e) {
    var tabKey = 9;
    var enterKey = 13;
    var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    if (keyCode !== tabKey && keyCode !== enterKey) return;
    var moveActionName = e.htmlEvent.shiftKey ? "MoveFocusBackward" : "MoveFocusForward";
    if (grid.batchEditApi[moveActionName]()) {
        ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
        preventEndEditOnLostFocus = true;
    }
}
function CitiesCombo_LostFocus(s, e) {
    if (!preventEndEditOnLostFocus)
        grid.batchEditApi.EndEdit();
    preventEndEditOnLostFocus = false;
}