#pragma checksum "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Subject\RemoveLesson.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9d3a5870471153b8120d7eccbf1ef634faed057e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Subject_RemoveLesson), @"mvc.1.0.view", @"/Views/Subject/RemoveLesson.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9d3a5870471153b8120d7eccbf1ef634faed057e", @"/Views/Subject/RemoveLesson.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6e381d074b162879d66b39334af76ae0439e473", @"/Views/_ViewImports.cshtml")]
    public class Views_Subject_RemoveLesson : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DigitalCoolBook.App.Models.CategoryViewModels.LessonRemoveViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Admin/AdminPanel", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RemoveLesson", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Subject\RemoveLesson.cshtml"
  
    ViewData["Title"] = "RemoveLesson";
    Layout = "~/Views/Shared/_SubjectDetailLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Изтриване на урок</h1>\r\n\r\n<div>\r\n    <hr class=\"hr-orchid\" />\r\n    <div class=\"row container-fluid\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9d3a5870471153b8120d7eccbf1ef634faed057e5533", async() => {
                WriteLiteral("\r\n            <div class=\"form-group\">\r\n                <div class=\"col\">\r\n                    <label class=\"gj-font-size-16\">Tema</label>\r\n                    ");
#nullable restore
#line 17 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Subject\RemoveLesson.cshtml"
               Write(Html.DropDownListFor(model => model.CategoryId, new SelectList(Model.Categories, "Id", "Title"), "Тема..", new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    ");
#nullable restore
#line 18 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Subject\RemoveLesson.cshtml"
               Write(Html.ValidationMessageFor(m => m.CategoryId, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </div>\r\n            </div>\r\n            <hr class=\"hr-orchid\" />\r\n            <div class=\"form-group\">\r\n                <div class=\"col\">\r\n                    <label class=\"gj-font-size-16\">Урок</label>\r\n                    ");
#nullable restore
#line 25 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Subject\RemoveLesson.cshtml"
               Write(Html.DropDownListFor(model => model.LessonId, new SelectList(Model.Lessons, "Id", "Title"), "Урок..", new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    ");
#nullable restore
#line 26 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Subject\RemoveLesson.cshtml"
               Write(Html.ValidationMessageFor(m => m.CategoryId, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                </div>
            </div>
            <hr class=""hr-orchid"" />
            <div class=""row"">
                <div class=""col-md"">
                    <a class=""btn btn-danger"" href=""#"" id=""deleteLesson"">Изтрии урока</a>
                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9d3a5870471153b8120d7eccbf1ef634faed057e8023", async() => {
                    WriteLiteral("Назад");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                </div>\r\n            </div>\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
    </div>
</div>

<div id=""successMessage""></div>
<div id=""errorMessage"" title=""Грешка!""></div>

<script>
    var lessonDropDown = $('#LessonId'); // cache variable
    $('#CategoryId').change(function () {
        var categoryId = $(this).val();
        lessonDropDown.empty(); // clear existing options
        if (!CategoryId) {
            return; // exit if null
        }
        $.ajax({
            type: ""POST"",
            url: ""/Subject/GetLessons"",
            data: { categoryId: categoryId }, // sends subjectId to the action
            success: function (response) {
                lessonDropDown.append($('<option></option>').val('').text('Избери урок')); // add a default null option
                $.each(response, function (i, item) {
                    lessonDropDown.append($('<option></option>').val(item.id).text(item.title));
                });
            },
        });
    })
</script>
<script>
    $(function () {
        $(""#deleteLesson"").click(function () ");
            WriteLiteral(@"{
            var lessonId = $('#LessonId').val();
            $(""#successMessage"").dialog(
                {
                    resizable: false,
                    height: 200,
                    width: 300,
                    modal: true,
                    buttons: {
                        ""Изтриване"": function () {
                            $.ajax({
                                type: ""POST"",
                                url: ""/Subject/RemoveLesson"",
                                data: { lessonId: lessonId },
                                success: function () {
                                    $(""#successMessage"").dialog(
                                        {
                                            resizable: false,
                                            height: 200,
                                            width: 300,
                                            modal: true,
                                            buttons:
                    ");
            WriteLiteral(@"                        {
                                                ""Затвори"": function () {
                                                    window.location.href = ""/Subject/AddLesson"",
                                                        $(this).dialog(""close"");
                                                }
                                            }
                                        });
                                    $(""#successMessage"").html(""Урока е изтрит!"")
                                },
                                error: function (request, err, message) {
                                    $(""#errorMessage"").html(request.responseText);
                                    $(""#errorMessage"").dialog({
                                        maxHeight: 290,
                                        width: 600,
                                        modal: true,
                                        buttons:
                                        {
 ");
            WriteLiteral(@"                                           ""Затвори"": function () {
                                                $(this).dialog(""close"");
                                            }
                                        }
                                    });
                                }
                            })
                        },
                        ""Отказ"": function () {
                            $(this).dialog(""close"");
                        }
                    }
                })
            $(""#successMessage"").html(""Изтриване на Урока?"")
        })
    })
</script>
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DigitalCoolBook.App.Models.CategoryViewModels.LessonRemoveViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
