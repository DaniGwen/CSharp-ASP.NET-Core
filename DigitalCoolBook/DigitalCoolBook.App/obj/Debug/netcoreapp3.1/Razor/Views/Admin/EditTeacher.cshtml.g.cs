#pragma checksum "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6b38a32552b6f707d62fe988ea7ad4ddd12acb84"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_EditTeacher), @"mvc.1.0.view", @"/Views/Admin/EditTeacher.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b38a32552b6f707d62fe988ea7ad4ddd12acb84", @"/Views/Admin/EditTeacher.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4100cddf35ca3c9416065655d32b777c09b5f350", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_EditTeacher : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DigitalCoolBook.Models.Teacher>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Register", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RegisterTeacher", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-dark font-weight-bold"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
  
    ViewData["Title"] = "EditTeacher";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Изтриване или редактиране на Учителски профили</h1>\r\n\r\n<p>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6b38a32552b6f707d62fe988ea7ad4ddd12acb844594", async() => {
                WriteLiteral("Create New");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</p>\r\n<table class=\"table font-weight-bold\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 16 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayNameFor(model => model.TeacherId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 19 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 22 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 25 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayNameFor(model => model.DateOfBirth));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 28 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayNameFor(model => model.PlaceOfBirth));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 31 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayNameFor(model => model.Sex));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 34 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayNameFor(model => model.MobilePhone));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 37 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayNameFor(model => model.Telephone));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 40 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayNameFor(model => model.Password));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 46 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 49 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayFor(modelItem => item.TeacherId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 52 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 55 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayFor(modelItem => item.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 58 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayFor(modelItem => item.DateOfBirth));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 61 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayFor(modelItem => item.PlaceOfBirth));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 64 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayFor(modelItem => item.Sex));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 67 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayFor(modelItem => item.MobilePhone));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 70 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayFor(modelItem => item.Telephone));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 73 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.DisplayFor(modelItem => item.Password));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 76 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                ");
#nullable restore
#line 77 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                ");
#nullable restore
#line 78 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
           Write(Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 81 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Admin\EditTeacher.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DigitalCoolBook.Models.Teacher>> Html { get; private set; }
    }
}
#pragma warning restore 1591
