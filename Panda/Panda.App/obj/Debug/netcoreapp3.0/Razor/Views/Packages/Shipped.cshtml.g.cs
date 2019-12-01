#pragma checksum "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Packages\Shipped.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4aec511fdb789508f2295b3c64c5f8039f65e5e9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Panda.App.Views.Packages.Views_Packages_Shipped), @"mvc.1.0.view", @"/Views/Packages/Shipped.cshtml")]
namespace Panda.App.Views.Packages
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4aec511fdb789508f2295b3c64c5f8039f65e5e9", @"/Views/Packages/Shipped.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"02779047203a464cd39e4aae994c6be804f843a5", @"/Views/_ViewImports.cshtml")]
    public class Views_Packages_Shipped : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Panda.App.Models.Package.PackageShippedViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Packages\Shipped.cshtml"
  
    ViewData["Title"] = "Shipped";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1 class=""text-center"">Shipped</h1>
<hr class=""hr-2 bg-panda"">
<div class=""d-flex justify-content-between"">
    <table class=""table table-hover table-bordered"">
        <thead>
            <tr class=""row"">
                <th scope=""col"" class=""col-lg-1 d-flex justify-content-center""><h3>#</h3></th>
                <th scope=""col"" class=""col-lg-4 d-flex justify-content-center""><h3>Description</h3></th>
                <th scope=""col"" class=""col-lg-1 d-flex justify-content-center""><h3>Weight</h3></th>
                <th scope=""col"" class=""col-lg-3 d-flex justify-content-center""><h3>Estimated Delivery Date</h3></th>
                <th scope=""col"" class=""col-lg-2 d-flex justify-content-center""><h3>Recipient</h3></th>
                <th scope=""col"" class=""col-lg-1 d-flex justify-content-center""><h3>Actions</h3></th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 23 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Packages\Shipped.cshtml"
             for (int i = 0; i < Model.Count(); i++)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr class=\"row\">\r\n                    <th scope=\"row\" class=\"col-lg-1 d-flex justify-content-center\"><h5>");
#nullable restore
#line 26 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Packages\Shipped.cshtml"
                                                                                   Write(i + 1);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5></th>\r\n                    <td class=\"col-lg-4 d-flex justify-content-center\"><h5>");
#nullable restore
#line 27 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Packages\Shipped.cshtml"
                                                                      Write(Model[i].Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5></td>\r\n                    <td class=\"col-lg-1 d-flex justify-content-center\"><h5>");
#nullable restore
#line 28 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Packages\Shipped.cshtml"
                                                                      Write(Model[i].Weight.ToString("F2"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" KG</h5></td>\r\n                    <td class=\"col-lg-3 d-flex justify-content-center\"><h5>");
#nullable restore
#line 29 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Packages\Shipped.cshtml"
                                                                      Write(Model[i].DeliveryDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5></td>\r\n                    <td class=\"col-lg-2 d-flex justify-content-center\"><h5>");
#nullable restore
#line 30 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Packages\Shipped.cshtml"
                                                                      Write(Model[i].Recipient);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5></td>\r\n                    <td class=\"col-lg-1 d-flex justify-content-center\">\r\n                        <a");
            BeginWriteAttribute("href", " href=\"", 1804, "\"", 1841, 2);
            WriteAttributeValue("", 1811, "/Packages/Deliver/", 1811, 18, true);
#nullable restore
#line 32 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Packages\Shipped.cshtml"
WriteAttributeValue("", 1829, Model[i].Id, 1829, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn bg-panda text-white\">Deliver</a>\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 35 "C:\Users\danig\Documents\GitHub\CSharp ASP.NET Core\Panda\Panda.App\Views\Packages\Shipped.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Panda.App.Models.Package.PackageShippedViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591