#pragma checksum "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Teacher\GradeDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8573d99e4318f87840ec90289c2e7acf3ba157e2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Teacher_GradeDetails), @"mvc.1.0.view", @"/Views/Teacher/GradeDetails.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8573d99e4318f87840ec90289c2e7acf3ba157e2", @"/Views/Teacher/GradeDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6e381d074b162879d66b39334af76ae0439e473", @"/Views/_ViewImports.cshtml")]
    public class Views_Teacher_GradeDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DigitalCoolBook.App.Models.GradesViewModels.GradeDetailViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Teacher\GradeDetails.cshtml"
  
    ViewData["Title"] = "GradeDetails";
    var paraleloName = ViewData["paraleloName"];
    Layout = "_LayoutMenus";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>");
#nullable restore
#line 9 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Teacher\GradeDetails.cshtml"
Write(paraleloName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" клас</h2>\r\n\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 15 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Teacher\GradeDetails.cshtml"
           Write(Html.DisplayName("Ученици"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 21 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Teacher\GradeDetails.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 25 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Teacher\GradeDetails.cshtml"
               Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td class=\"btn btn-secondary btn-sm\">\r\n                    ");
#nullable restore
#line 28 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Teacher\GradeDetails.cshtml"
               Write(Html.ActionLink("Информация", "Details", "Student", new { id = item.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 31 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Teacher\GradeDetails.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n<a class=\"btn btn-outline-info\" href=\"/Teacher/ChooseGrade\">Назад</a>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DigitalCoolBook.App.Models.GradesViewModels.GradeDetailViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
