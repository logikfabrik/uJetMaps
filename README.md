# uJetMaps [![Build status](https://ci.appveyor.com/api/projects/status/a3ea0hgp2f56wf2h?svg=true)](https://ci.appveyor.com/project/logikfabrik/ujetmaps)
Umbraco Jet Maps (uJetMaps) is a geographical coordinates data type and property editor for Umbraco 7, built using uJet - a Code First approach to building MVC applications in Umbraco 7.

![Screenshot](https://raw.githubusercontent.com/logikfabrik/uJetMaps/master/assets/screenshot.gif)

### NuGet
```
PM> Install-Package uJetMaps
```

## How To
uJetMaps is easy to use. Add a reference to uJetMaps. Add a public property of type `GeoCoordinates` to one of your document or media types (using uJet). Fire up your application and a property editor for setting coordinates will now be available in the Umbraco back office.

**Model**
```csharp
namespace Example.Models
{
    using System.ComponentModel.DataAnnotations;
    using Logikfabrik.Umbraco.Jet;
    using Logikfabrik.Umbraco.Jet.Maps;

    [DocumentType(
        "Map page",
        Description = "Document type for map coordinates",
        AllowedAsRoot = true)]
    public class MapPage
    {
        [Display(
            Name = "Map", 
            Description = "The map coordinates")]
        public GeoCoordinates MapCoordinates { get; set; }
    }
}
```

**View**
```html
@model Example.Models.MapPage

<!DOCTYPE html>
<html>
<head>
    <title></title>
</head>
<body>
    <p>lat: @Model.MapCoordinates.Lat</p>
    <p>lng: @Model.MapCoordinates.Lng</p>
</body>
</html>
```

**Controller**
```csharp
namespace Example.Controllers
{
    using System.Web.Mvc;
    using Logikfabrik.Umbraco.Jet.Web.Mvc;
    using Models;

    public class MapPageController : JetController
    {
        public ActionResult Index(MapPage model)
        {
            return View(model);
        }
    }
}
```

## Contributions
uJetMaps is Open Source (MIT), and you’re welcome to contribute!

If you have a bug report, feature request, or suggestion, please open a new issue. To submit a patch, please send a pull request.