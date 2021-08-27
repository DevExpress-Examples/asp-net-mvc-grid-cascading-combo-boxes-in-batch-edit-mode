<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128549455/14.1.7%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T155879)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [HomeController.cs](./CS/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/Controllers/HomeController.vb))
* [Model.cs](./CS/Models/Model.cs) (VB: [Model.vb](./VB/Models/Model.vb))
* [CascadingComboBoxesBatchEdit.js](./CS/Scripts/CascadingComboBoxesBatchEdit.js) (VB: [CascadingComboBoxesBatchEdit.js](./VB/Scripts/CascadingComboBoxesBatchEdit.js))
* [ComboBoxCityPartial.cshtml](./CS/Views/Home/ComboBoxCityPartial.cshtml)
* [GridViewPartial.cshtml](./CS/Views/Home/GridViewPartial.cshtml)
* [Index.cshtml](./CS/Views/Home/Index.cshtml)
<!-- default file list end -->
# GridView - A simple implementation of cascading comboboxes in Batch Edit mode
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t155879)**
<!-- run online end -->


<br>
<p>In this example, the combo box in the City column (the City combo box) is populated dynamically with city names via callbacks, based on the value selected in the combo box in the Country column (the Country combo box).  <br>You can find detailed steps by clicking below the "Show Implementation Details" link .<br><br><strong>Web Forms: </strong><br><a href="https://www.devexpress.com/Support/Center/p/T124512">ASPxGridView - How to implement cascading comboboxes in Batch Edit mode</a></p>


<h3>Description</h3>

The concept of cascading combo boxes requires sending a callback to the server to get data for the second editor based on the first editor's selected value.&nbsp;<br>In the meantime, it's impossible to send callbacks for built-in editors and it's necessary to use the EditItemTemplate (see&nbsp;<a href="https://www.devexpress.com/Support/Center/Question/Details/S173460">ASPxGridView - Batch Edit - Support a scenario when GridViewComboBoxColumn is used in callback mode</a>).<br>A basic scenario of this approach requires the following steps:<br>1) Use the client-side&nbsp;<a href="https://documentation.devexpress.com/AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditStartEditingtopic.aspx">MVCxClientGridView.BatchEditStartEditing</a>&nbsp; and&nbsp;<a href="https://documentation.devexpress.com/AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditEndEditingtopic.aspx">MVCxClientGridView.BatchEditEndEditing</a>&nbsp; events&nbsp; to provide the template combo box&nbsp;with values.<br>2) Handle the&nbsp;<a href="https://documentation.devexpress.com/AspNet/DevExpressWebASPxEditorsScriptsASPxClientComboBox_SelectedIndexChangedtopic.aspx">SelectedIndexChanged</a>&nbsp;event to send callbacks if it's required.<br>3) Handle the client&nbsp;<a href="https://documentation.devexpress.com/AspNet/DevExpressWebMVCScriptsMVCxClientComboBox_BeginCallbacktopic.aspx">BeginCallback</a>&nbsp; event to pass parameters on the server.<br>4) Handle the&nbsp;<a href="https://documentation.devexpress.com/AspNet/DevExpressWebASPxEditorsScriptsASPxClientComboBox_EndCallbacktopic.aspx">ASPxClientComboBox.EndCallback</a>&nbsp;event for the second editor to apply the selected item after a callback.<br>5) Handle the&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxClassesScriptsASPxClientControl_Inittopic">MVCxClientGridView.Init</a>&nbsp; and&nbsp;<a href="https://documentation.devexpress.com/AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_EndCallbacktopic.aspx">MVCxClientGridView.EndCallback</a>&nbsp;events&nbsp;to initialize and reset global variables responsible for data, providing logic&nbsp;after the grid is refreshed.&nbsp;<br>6) Use the ideas from&nbsp;<a data-ticket="T115130">GridView - Batch Editing - A simple implementation of an EditItem template</a>&nbsp; to emulate the behavior of standard grid editors&nbsp;when an end-user uses a keyboard or mouse.
<p>The attached example illustrates how to implement all these steps.&nbsp;</p>

<br/>


