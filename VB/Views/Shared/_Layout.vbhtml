<!DOCTYPE HTML>
<html>
<head>
    <title>@ViewBag.Title</title>
    <link href="@Url.Content("~/Content/Site.css")" rel="stylesheet" type="text/css" />
    @Html.DevExpress().GetStyleSheets(new StyleSheet With { .ExtensionSuite = ExtensionSuite.Editors },
        new StyleSheet With { .ExtensionSuite = ExtensionSuite.GridView })
    @Html.DevExpress().GetScripts(new Script With { .ExtensionSuite = ExtensionSuite.Editors },
        new Script With { .ExtensionSuite = ExtensionSuite.GridView })
</head>
<body>
    @RenderBody()
</body>
</html>
