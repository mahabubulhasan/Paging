# Paging
Basic pagination library for asp.net core

![Screenshot of Paging](https://raw.githubusercontent.com/mahabubulhasan/Paging/master/screenshot/paging.png)

NuGet Url https://www.nuget.org/packages/Uzzal.Paging

## Installation
```
Install-Package Uzzal.Paging -Version 1.0.2
```

After successful installation follow these three steps

> Step 1: In Controller
```C#
public IActionResult Index(int? page)
{
    var itemsPerPage = 10;
    var list = GetCollection(); // returns ICollection<string>
    
    var pagedList = PagedList<string>.Build(list, page ?? 1, itemsPerPage);
    return View(pagedList);
}

private ICollection<string> GetCollection()
{
    ...
}
```

### OR

```C#
public async IActionResult Index(int? page)
{
    var itemsPerPage = 10;
    var list = GetRows(); // returns IQueryable<TEntity>
    
    var pagedList = await PagedList<TEntity>.BuildAsync(list, page ?? 1, itemsPerPage);
    return View(pagedList);
}

private IQueryable<TEntity> GetRows() 
{
    ....
}
```
> Step 2: Add these lines into your `View/_ViewImports.cshtml` file
```
@using Uzzal.Paging
@addTagHelper *, Uzzal.Paging
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
**NOTE:** Paging depends on bootstrap 4 for styling.

> Additionally some supported optional attributes are followings:

| Attribute Name | Default Value |
|----------------|---------------|
| `controller`      |  Current controller  |
| `default-style`   | `mr-1 btn btn-outline-primary btn-sm` |
| `active-style`    | `mr-1 btn btn-primary btn-sm` |
| `spacer-text`     | `...` |
| `spacer-style`    | `p-0 mr-1 btn btn-default btn-sm` |




