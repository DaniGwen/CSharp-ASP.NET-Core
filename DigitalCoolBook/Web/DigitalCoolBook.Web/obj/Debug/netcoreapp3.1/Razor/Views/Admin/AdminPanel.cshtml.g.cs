#pragma checksum "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Admin\AdminPanel.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "85237fdd6487dcf331578d934fa4af1dd23b693a"
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
#line 1 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\_ViewImports.cshtml"
using DigitalCoolBook.App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\_ViewImports.cshtml"
using DigitalCoolBook.App.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"85237fdd6487dcf331578d934fa4af1dd23b693a", @"/Views/Admin/AdminPanel.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6e381d074b162879d66b39334af76ae0439e473", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_AdminPanel : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Admin\AdminPanel.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "_LayoutMenus";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<style>
    .backgroundOrange{
        background: #ffb74d;
    }
</style>
<div class=""container-fluid mt-4"">
    <div class=""row"">
        <div class=""col"">
            <div class=""list-group"">
                <a class=""list-group-item backgroundOrange"">Регистрации</a>
                <a href=""/Teacher/EditTeachers"" class=""list-group-item list-group-item-action list-group-item-light"">Учителски акаунти</a>
                <a href=""/Student/EditStudents"" class=""list-group-item list-group-item-action list-group-item-light"">Ученически акаунти</a>
                <a href=""/Admin/EditParalelos"" class=""list-group-item list-group-item-action list-group-item-light"">Добавяне на Паралелки</a>
            </div>
            <div class=""mt-2 mb-2"">
                <a class=""list-group-item backgroundOrange"">Добавяне материали</a>
                <a href=""/Subject/AddLesson"" class=""list-group-item list-group-item-action list-group-item-light"">Добавяне на уроци</a>
                <a href=""/Test/CreateTest");
            WriteLiteral(@"Admin"" class=""list-group-item list-group-item-action list-group-item-light"">Добавяне на Тест</a>
                <a href=""/Test/TestsPreview"" class=""list-group-item list-group-item-action list-group-item-light"">Преглед на тестове</a>
            </div>
        </div>
    </div>
</div>


");
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