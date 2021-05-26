<!-- default file list -->
*Files to look at*:

* [HomeController.cs](./CS/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/Controllers/HomeController.vb))
* [ComboBoxPropertiesProvider.cs](./CS/Models/ComboBoxPropertiesProvider.cs) (VB: [ComboBoxPropertiesProvider.vb](./VB/Models/ComboBoxPropertiesProvider.vb))
* [Customer.cs](./CS/Models/Customer.cs) (VB: [Customer.vb](./VB/Models/Customer.vb))
* [CascadingComboBoxesBatchEdit.js](./CS/Scripts/CascadingComboBoxesBatchEdit.js) (VB: [CascadingComboBoxesBatchEdit.js](./VB/Scripts/CascadingComboBoxesBatchEdit.js))
* [GridViewPartial.cshtml](./CS/Views/Home/GridViewPartial.cshtml)
* **[Index.cshtml](./CS/Views/Home/Index.cshtml)**
<!-- default file list end -->
# GridView - A simple implementation of cascading comboboxes in Batch Edit mode
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t155879/)**
<!-- run online end -->


<br>
<p>In this example, the combo box in the City column (the City combo box) is populated dynamically with city names via callbacks, based on the value selected in the combo box in the Country column (the Country combo box).  <br>You can find detailed steps by clicking below the "Show Implementation Details" link .<br><br><strong>Web Forms: </strong><br><a href="https://www.devexpress.com/Support/Center/p/T124512">ASPxGridView - How to implement cascading comboboxes in Batch Edit mode</a></p>


<h3>Description</h3>

<p><strong>In&nbsp;v16.1</strong>, we started supporting the callback mode for&nbsp;GridViewComboBoxColumn and the EditItemTemplate implementation is not necessary.&nbsp;The main steps are:<br>1) Use the client-side&nbsp;<a href="https://documentation.devexpress.com/AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditStartEditingtopic.aspx">ASPxClientGridView.BatchEditStartEditing</a>&nbsp;event to check the main combo box value and update the child combo box data (if it's necessary).<br>2)&nbsp;Handle the&nbsp;<a href="https://documentation.devexpress.com/AspNet/DevExpressWebASPxEditorsScriptsASPxClientComboBox_SelectedIndexChangedtopic.aspx">SelectedIndexChanged</a>&nbsp;event to send callbacks when a user changes a value in the main combo box.<br>3) Use the&nbsp;<a href="https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.MVCxGridViewColumn.EditorProperties">MVCxGridViewColumn.EditorProperties</a> method and the&nbsp;<a href="https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.MVCxColumnComboBoxProperties">MVCxColumnComboBoxProperties</a>&nbsp;class to create a combo box column using the new API.<br>4) Use the&nbsp;&nbsp;<a href="http://help.devexpress.com/#AspNet/DevExpressWebMvcGridExtensionBase_GetComboBoxCallbackResulttopic">GetComboBoxCallbackResult</a>&nbsp;method to handle a combo box callback on the server.<br>5)&nbsp;Handle the&nbsp;<a href="https://documentation.devexpress.com/AspNet/DevExpressWebASPxEditorsScriptsASPxClientComboBox_EndCallbacktopic.aspx">ASPxClientComboBox.EndCallback</a>&nbsp;event for the second editor to select an item after a custom callback.</p>

<br/>


