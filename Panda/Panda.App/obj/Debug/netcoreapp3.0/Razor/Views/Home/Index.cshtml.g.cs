#pragma checksum "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "887d0e0f51d2034f03912f5abfc91a4b2a350a37"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Panda.App.Views.Home.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace Panda.App.Views.Home
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
#line 1 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\_ViewImports.cshtml"
using Panda.App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\_ViewImports.cshtml"
using Panda.App.Areas.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"887d0e0f51d2034f03912f5abfc91a4b2a350a37", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"02779047203a464cd39e4aae994c6be804f843a5", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 7 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Home\Index.cshtml"
 if (!this.User.Identity.IsAuthenticated)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""jumbotron mt-3 bg-panda"">
        <h1 class=""text-white"">Welcome to PANDA Delivery Services.</h1>
        <hr class=""bg-white hr-2"" />
        <h3 class=""text-white""><a href=""/Identity/Account/Login"">Login</a> if you have an account.</h3>
        <h3 class=""text-white""><a href=""/Identity/Account/Register"">Register</a> if you don't.</h3>
    </div>
");
#nullable restore
#line 15 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Home\Index.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h1 class=\"text-center\">Hello, ");
#nullable restore
#line 18 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Home\Index.cshtml"
                              Write(this.User.Identity.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"!</h1>
    <hr class=""hr-2 bg-panda"">
    <div class=""d-flex justify-content-between"">
        <div class=""w-25 bg-white"">
            <h2 class=""text-center"">Pending</h2>
            <div class=""border-panda p-3"">
                <div class=""p-2 d-flex justify-content-around"">
                    <h4 class=""w-75"">IPhone XS Case</h4>
                    <a");
            BeginWriteAttribute("href", " href=\"", 947, "\"", 954, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn bg-panda text-white w-25\">Details</a>\r\n                </div>\r\n                <div class=\"p-2 d-flex justify-content-around\">\r\n                    <h4 class=\"w-75\">TV table</h4>\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 1169, "\"", 1176, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn bg-panda text-white w-25\">Details</a>\r\n                </div>\r\n                <div class=\"p-2 d-flex justify-content-around\">\r\n                    <h4 class=\"w-75\">Chushkopek</h4>\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 1393, "\"", 1400, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn bg-panda text-white w-25\">Details</a>\r\n                </div>\r\n                <div class=\"p-2 d-flex justify-content-around\">\r\n                    <h4 class=\"w-75\">Office Chair</h4>\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 1619, "\"", 1626, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn bg-panda text-white w-25"">Details</a>
                </div>
            </div>
        </div>
        <div class=""w-25 bg-white"">
            <h2 class=""text-center"">Shipped</h2>
            <div class=""border-panda p-3"">
                <div class=""p-2 d-flex justify-content-around"">
                    <h4 class=""w-75"">1959 Irish Bourbon</h4>
                    <a");
            BeginWriteAttribute("href", " href=\"", 2018, "\"", 2025, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn bg-panda text-white w-25"">Details</a>
                </div>
            </div>
        </div>
        <div class=""w-25 bg-white"">
            <h2 class=""text-center"">Delivered</h2>
            <div class=""border-panda p-3"">
                <div class=""p-2 d-flex justify-content-around"">
                    <h4 class=""w-75"">Dog Toy</h4>
                    <a");
            BeginWriteAttribute("href", " href=\"", 2408, "\"", 2415, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn bg-panda text-white w-25\">Acquire</a>\r\n                </div>\r\n                <div class=\"p-2 d-flex justify-content-around\">\r\n                    <h4 class=\"w-75\">Mineral Water</h4>\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 2635, "\"", 2642, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn bg-panda text-white w-25\">Acquire</a>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 65 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Home\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
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
