#pragma checksum "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Test\GetTestDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "92e808ce2e91ce768878e3ab0d5ee825b0f83955"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Test_GetTestDetails), @"mvc.1.0.view", @"/Views/Test/GetTestDetails.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"92e808ce2e91ce768878e3ab0d5ee825b0f83955", @"/Views/Test/GetTestDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6e381d074b162879d66b39334af76ae0439e473", @"/Views/_ViewImports.cshtml")]
    public class Views_Test_GetTestDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DigitalCoolBook.App.Models.TestviewModels.TestDetailsViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Test\GetTestDetails.cshtml"
  
    ViewData["Title"] = "GetTestDetailsAsync";
    Layout = "~/Views/Shared/_LayoutMenus.cshtml";
    var indexQuestion = 1;
    var indexAnswer = 0;
    var letters = new string[] { "А", "Б", "В", "Г", "Д", "Е", "Ж", "З" };

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container-fluid w-75\">\r\n    <h2 class=\"text-center\">ТЕСТ</h2>\r\n    <br />\r\n    <h3 class=\"text-center\">");
#nullable restore
#line 14 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Test\GetTestDetails.cshtml"
                       Write(Model.TestName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n    <hr />\r\n    <div class=\"form-group\">\r\n");
#nullable restore
#line 17 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Test\GetTestDetails.cshtml"
         foreach (var question in Model.Questions)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <h4 class=\"font-weight-bold mt-2 mb-3\"><span class=\"mr-3 font-weight-bold\">");
#nullable restore
#line 19 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Test\GetTestDetails.cshtml"
                                                                                  Write(indexQuestion);

#line default
#line hidden
#nullable disable
            WriteLiteral(".</span>");
#nullable restore
#line 19 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Test\GetTestDetails.cshtml"
                                                                                                        Write(question.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n            <ul class=\"list-group\">\r\n");
#nullable restore
#line 21 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Test\GetTestDetails.cshtml"
                 foreach (var answer in question.Answers)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <li class=\"list-group-item\">\r\n                        <div class=\"row\">\r\n                            <label class=\"col-form-label\">");
#nullable restore
#line 25 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Test\GetTestDetails.cshtml"
                                                     Write(letters[indexAnswer]);

#line default
#line hidden
#nullable disable
            WriteLiteral(".</label><p class=\"ml-2 col-form-label\">");
#nullable restore
#line 25 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Test\GetTestDetails.cshtml"
                                                                                                                  Write(answer.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n                    </li>\r\n");
#nullable restore
#line 28 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Test\GetTestDetails.cshtml"

                    indexAnswer += 1;
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </ul>\r\n");
#nullable restore
#line 32 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Test\GetTestDetails.cshtml"

            indexQuestion += 1;
            indexAnswer = 0;
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n    <div class=\"d-flex justify-content-between\">\r\n        <a href=\"/Test/TestsPreview\">назад</a>\r\n        <a class=\"btn btn-outline-secondary mb-2\"");
            BeginWriteAttribute("href", " href=\"", 1404, "\"", 1439, 2);
            WriteAttributeValue("", 1411, "/Test/EditTest/", 1411, 15, true);
#nullable restore
#line 39 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Test\GetTestDetails.cshtml"
WriteAttributeValue("", 1426, Model.TestId, 1426, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">редактирай</a>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DigitalCoolBook.App.Models.TestviewModels.TestDetailsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591