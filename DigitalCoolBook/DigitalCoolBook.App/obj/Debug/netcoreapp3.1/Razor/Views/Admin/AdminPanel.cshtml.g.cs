#pragma checksum "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\AdminPanel.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fceaffe142b1ca8ea7f10e828cb2693372611c68"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_AdminPanel), @"mvc.1.0.view", @"/Views/Admin/AdminPanel.cshtml")]
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
#line 1 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\_ViewImports.cshtml"
using DigitalCoolBook.App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\_ViewImports.cshtml"
using DigitalCoolBook.App.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fceaffe142b1ca8ea7f10e828cb2693372611c68", @"/Views/Admin/AdminPanel.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6e381d074b162879d66b39334af76ae0439e473", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_AdminPanel : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\AdminPanel.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container"">
    <div class=""row"">
        <div class=""col-lg"">
            <div class=""list-group w-70 "">
                <a class=""list-group-item list-group-item-dark"">Редактиране на потребители</a>
                <a href=""/Teacher/EditTeachers"" class=""list-group-item list-group-item-action list-group-item-primary"">Учителски акаунт</a>
                <a href=""/Student/EditStudents"" class=""list-group-item list-group-item-action list-group-item-primary"">Ученически акаунт</a>
            </div>
        </div>
");
            WriteLiteral("    </div>\r\n</div>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
