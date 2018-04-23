<!DOCTYPE HTML>
<html>
<head>
    <title>@ViewBag.Title</title>
    <link href="@Url.Content("~/Content/Site.css")" rel="stylesheet" type="text/css" />
    @Html.DevExpress().GetStyleSheets(New StyleSheet With { .ExtensionSuite = ExtensionSuite.GridView })
    @Html.DevExpress().GetScripts(New Script With { .ExtensionSuite = ExtensionSuite.GridView })
</head>
<body>
    @RenderBody()
</body>
</html>