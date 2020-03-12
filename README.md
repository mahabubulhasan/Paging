# Paging
Basic pagination library for asp.net

NuGet Url https://www.nuget.org/packages/Uzzal.Paging

## Installation
```
Install-Package Uzzal.Paging -Version 1.0.0
```

After successful installation follow these three steps

> Step 1: In Controller
```C#
public IActionResult Index(int? page)
{
    var itemsPerPage = 10;
    var pagedList = PagedList<string>.Build(GetExampleList(), page ?? 1, itemsPerPage);

    return View(pagedList);
}

private List<string> GetExampleList()
{
    var list = new List<string>();
    for(int i=0; i< 300; i++)
    {
        list.Add($"Item: {i}");
    }
    return list;
}
```
> Step 2: Add these lines into your `View/_ViewImports.cshtml` file
```
@using Paging
@addTagHelper *, Paging
```

> Step 3: In Razor view file
```C#
@model PagedList<string>

<page-links 
    paging-context="@Model.GetContext()"
    asp-route-param1="value1"
    asp-route-param2="value2"
    controller="Home"
    action="Index">
</page-links>
```

