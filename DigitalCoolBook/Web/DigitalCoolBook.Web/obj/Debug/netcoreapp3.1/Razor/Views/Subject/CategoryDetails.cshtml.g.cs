#pragma checksum "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a70085b2194d43c417aacdef32de9eaf0f3be4e9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Subject_CategoryDetails), @"mvc.1.0.view", @"/Views/Subject/CategoryDetails.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a70085b2194d43c417aacdef32de9eaf0f3be4e9", @"/Views/Subject/CategoryDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6e381d074b162879d66b39334af76ae0439e473", @"/Views/_ViewImports.cshtml")]
    public class Views_Subject_CategoryDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DigitalCoolBook.App.Models.CategoryViewModels.CategoryDetailsViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddLesson", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
  
    Layout = "_LayoutMenus";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container-fluid\">\r\n    <div class=\"col\">\r\n        <h2>");
#nullable restore
#line 9 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
       Write(ViewData["CategoryTitle"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n    </div>\r\n    <hr />\r\n    <div class=\"d-flex justify-content-center\">\r\n        <h2 class=\"mb-2 my-border col-sm-auto text-white\">Теми</h2>\r\n    </div>\r\n    <div>\r\n");
#nullable restore
#line 16 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
         foreach (var lesson in Model.Lessons)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"row\">\r\n                <div class=\" col-sm-7 d-flex justify-content-center\">\r\n");
#nullable restore
#line 20 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                     if (lesson.IsUnlocked)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <a");
            BeginWriteAttribute("href", " href=\"", 639, "\"", 685, 2);
            WriteAttributeValue("", 646, "/Subject/LessonDetails/", 646, 23, true);
#nullable restore
#line 22 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
WriteAttributeValue("", 669, lesson.LessonId, 669, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\" btn-outline-info btn-lg mb-2\">");
#nullable restore
#line 22 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                                                                                                           Write(lesson.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
#nullable restore
#line 23 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <a href=\"#\" class=\" btn-outline-secondary btn-lg mb-2 disabled\" aria-disabled=\"true\">");
#nullable restore
#line 26 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                                                                                                        Write(lesson.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a>
                        <small class=""text-danger form-group align-content-sm-start"" style=""border-bottom:solid;border-bottom-color: red;border-width: 1px;margin: 12px;"">
                            Заключен.<span class=""fa fa-lock""></span>
                        </small>
");
#nullable restore
#line 30 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n                <div>\r\n");
#nullable restore
#line 33 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                     if (this.User.IsInRole("Admin"))
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"form-group\">\r\n                            <button class=\"btn btn-outline-info w-100\">\r\n                                ");
#nullable restore
#line 37 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                           Write(Html.ActionLink("Промяна", "Edit", new { id = lesson.LessonId }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </button>\r\n                            <a class=\"btnDelete btn btn-outline-info w-100 mt-2\"");
            BeginWriteAttribute("id", " id=\"", 1710, "\"", 1731, 1);
#nullable restore
#line 39 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
WriteAttributeValue("", 1715, lesson.LessonId, 1715, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" href=\"#\">Изтрии</a>\r\n                        </div>\r\n");
#nullable restore
#line 41 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n");
#nullable restore
#line 43 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                 if (this.User.IsInRole("Student"))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"col-sm-4 d-flex justify-content-end\">\r\n                        <p class=\"text-info\">Точки:</p>\r\n");
#nullable restore
#line 47 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                         if (lesson.Score == 0)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p class=\"text-info font-weight-bold ml-2\">");
#nullable restore
#line 49 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                                                                  Write(lesson.Score);

#line default
#line hidden
#nullable disable
            WriteLiteral("%</p>\r\n");
#nullable restore
#line 50 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                        }
                        else if (lesson.Score < 70)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p class=\"text-danger font-weight-bold\">");
#nullable restore
#line 53 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                                                               Write(lesson.Score);

#line default
#line hidden
#nullable disable
            WriteLiteral("%</p>\r\n");
#nullable restore
#line 54 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                        }
                        else if (lesson.Score >= 70)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p class=\"text-success font-weight-bold\">");
#nullable restore
#line 57 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                                                                Write(lesson.Score);

#line default
#line hidden
#nullable disable
            WriteLiteral("%</p>\r\n");
#nullable restore
#line 58 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n");
#nullable restore
#line 60 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n");
#nullable restore
#line 62 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n    <hr />\r\n    <div class=\"mt-4 ml-4 mb-2 row form-group\">\r\n        <a");
            BeginWriteAttribute("href", " href=\"", 2780, "\"", 2850, 1);
#nullable restore
#line 66 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
WriteAttributeValue("", 2787, Url.Action("Categories", "Subject",new {Id = Model.SubjectId}), 2787, 63, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Назад</a>\r\n    </div>\r\n\r\n");
#nullable restore
#line 69 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
     if (this.User.IsInRole("Admin"))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p class=\"btn btn-outline-info\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a70085b2194d43c417aacdef32de9eaf0f3be4e913821", async() => {
                WriteLiteral("Създаване");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </p>\r\n");
#nullable restore
#line 74 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Subject\CategoryDetails.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>

<div id=""dialog-confirm"" title=""Изтриване на урок"">
    <p>
        <span class=""ui-icon ui-icon-alert"" style=""float:left; margin:12px 12px 20px 0;""></span>
        Информацията ще бъде изтрита. Продължи?
    </p>
</div>

<div id=""successMessage""></div>

<div style=""display: none; "" id=""errorMessageDialog"" title=""Грешка!"">
    <p style=""max-height:280px; overflow:auto;"" id=""errorMessage"">
        <span class=""ui-icon ui-icon-alert"" style=""float:left; margin:12px 12px 20px 0;""></span>
    </p>
</div>

<script>
    $(function () {
        $(""#dialog-confirm"").hide();
        $("".btnDelete"").click(function () {
            $(""#dialog-confirm"").dialog({
                resizable: true,
                height: 250,
                width: 450,
                modal: true,
                buttons:
                {
                    ""изтриване"": function () {
                        var categoryId = $("".btnDelete"").attr(""id"");
                        $.ajax({
                ");
            WriteLiteral(@"            url: ""/Subject/DeleteLesson/"" + categoryId,
                            type: ""POST"",
                            success: function () {
                                $(""#successMessage"").dialog({
                                    resizable: false,
                                    height: 200,
                                    width: 300,
                                    modal: true,
                                    buttons: {
                                        ""Затвори"": function () {
                                            $(this).dialog(""close"");
                                        }
                                    }
                                });
                                $(""#successMessage"").html(""Успешно изтриване!"");
                            },
                            error: function (request, err, message) {
                                $(""#errorMessage"").html(request.statusText);
                                $(""#err");
            WriteLiteral(@"orMessageDialog"").dialog({
                                    maxHeight: 290,
                                    width: 600,
                                    modal: true,
                                    buttons: {
                                        ""Затвори"": function () {
                                            $(this).dialog(""close"");
                                        }
                                    }
                                });
                                //In release version use this
                                //$(""#errorMessage"").html(""Потребителя не  може да бъде изтрит"");
                            }
                        });
                        $(this).dialog(""close"");
                    },
                    ""назад"": function () {
                        $(this).dialog(""close"");
                    }
                }
            });
        });
    });
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DigitalCoolBook.App.Models.CategoryViewModels.CategoryDetailsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591