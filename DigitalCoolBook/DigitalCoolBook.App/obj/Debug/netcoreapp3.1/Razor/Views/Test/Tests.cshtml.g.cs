#pragma checksum "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Test\Tests.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a5eb39e41f7c457cdfea9258eec1faf4b7217279"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Test_Tests), @"mvc.1.0.view", @"/Views/Test/Tests.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\_ViewImports.cshtml"
using DigitalCoolBook.App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\_ViewImports.cshtml"
using DigitalCoolBook.App.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a5eb39e41f7c457cdfea9258eec1faf4b7217279", @"/Views/Test/Tests.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6e381d074b162879d66b39334af76ae0439e473", @"/Views/_ViewImports.cshtml")]
    public class Views_Test_Tests : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DigitalCoolBook.App.Models.TestviewModels.TestsNamesViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Test\Tests.cshtml"
  
    ViewData["Title"] = "Tests";
    Layout = "_LayoutMenus";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container-fluid w-75\">\r\n    <h1 class=\"text-center\">Тестове</h1>\r\n    <div class=\"row\">\r\n        ");
#nullable restore
#line 11 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Test\Tests.cshtml"
   Write(Html.DisplayNameFor(model => model.TestName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n\r\n");
#nullable restore
#line 14 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Test\Tests.cshtml"
     foreach (var test in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"row\">\r\n            <p class=\"col-form-label-lg\">");
#nullable restore
#line 17 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Test\Tests.cshtml"
                                    Write(test.TestName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            <div class=\"col d-flex justify-content-end\">\r\n");
#nullable restore
#line 19 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Test\Tests.cshtml"
                 if (test.IsExpired)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <p class=\"col-form-label-lg text-danger ml-3\">Теста е изтекъл</p>\r\n");
#nullable restore
#line 22 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Test\Tests.cshtml"
                }
                else if (test.Timer == 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"col-form-label-lg ml-3\">\r\n                        ");
#nullable restore
#line 26 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Test\Tests.cshtml"
                   Write(Html.ActionLink("Задаване на таймер", "SetTestTimer", "Test", new { testId = test.TestId }, new { @class = "btn btn-outline-info" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n");
#nullable restore
#line 28 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Test\Tests.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"col-form-label-lg ml-3\">\r\n                        ");
#nullable restore
#line 32 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Test\Tests.cshtml"
                   Write(Html.ActionLink("Старт", "StartTest", "Test", new { id = test.TestId }, new { @class = "btn btn-outline-success" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n");
#nullable restore
#line 34 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Test\Tests.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n\r\n        </div>\r\n");
#nullable restore
#line 38 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Test\Tests.cshtml"

    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DigitalCoolBook.App.Models.TestviewModels.TestsNamesViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
