#pragma checksum "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\Admin\AdminContact.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f721c934d1aaa1ab5c6a3d6e3782795a623472b2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_AdminContact), @"mvc.1.0.view", @"/Views/Admin/AdminContact.cshtml")]
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
#nullable restore
#line 4 "C:\Users\AW\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\Web\DigitalCoolBook.Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f721c934d1aaa1ab5c6a3d6e3782795a623472b2", @"/Views/Admin/AdminContact.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"852bf3ae06bcef806392b5f03b26926dea393dba", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_AdminContact : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-contact contact_form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("contact_process.php"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("contactForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("novalidate", new global::Microsoft.AspNetCore.Html.HtmlString("novalidate"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<!-- bradcam_area_start -->
<div class=""bradcam_area breadcam_bg overlay2"">
    <h3>contact us</h3>
</div>
<!-- bradcam_area_end -->
<!-- ================ contact section start ================= -->
<section class=""contact-section"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-12"">
                <h2 class=""contact-title"">Get in Touch</h2>
            </div>
            <div class=""col-lg-8"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f721c934d1aaa1ab5c6a3d6e3782795a623472b26033", async() => {
                WriteLiteral(@"
                    <div class=""row"">
                        <div class=""col-12"">
                            <div class=""form-group"">
                                <textarea class=""form-control w-100"" name=""message"" id=""message"" cols=""30"" rows=""9"" onfocus=""this.placeholder = ''"" onblur=""this.placeholder = 'Enter Message'"" placeholder="" Name""></textarea>
                            </div>
                        </div>
                        <div class=""col-sm-6"">
                            <div class=""form-group"">
                                <input class=""form-control valid"" name=""name"" id=""name"" type=""text"" onfocus=""this.placeholder = ''"" onblur=""this.placeholder = 'Enter your name'"" placeholder=""Enter your name"">
                            </div>
                        </div>
                        <div class=""col-sm-6"">
                            <div class=""form-group"">
                                <input class=""form-control valid"" name=""email"" id=""email"" type=""email"" onfoc");
                WriteLiteral(@"us=""this.placeholder = ''"" onblur=""this.placeholder = 'Enter email address'"" placeholder=""Email"">
                            </div>
                        </div>
                        <div class=""col-12"">
                            <div class=""form-group"">
                                <input class=""form-control"" name=""subject"" id=""subject"" type=""text"" onfocus=""this.placeholder = ''"" onblur=""this.placeholder = 'Enter Subject'"" placeholder=""Enter Subject"">
                            </div>
                        </div>
                    </div>
                    <div class=""form-group mt-3"">
                        <button type=""submit"" class=""button button-contactForm boxed-btn"">Send</button>
                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
            <div class=""col-lg-3 offset-lg-1"">
                <div class=""media contact-info"">
                    <span class=""contact-info__icon""><i class=""ti-home""></i></span>
                    <div class=""media-body"">
                        <h3>Buttonwood, California.</h3>
                        <p>Rosemead, CA 91770</p>
                    </div>
                </div>
                <div class=""media contact-info"">
                    <span class=""contact-info__icon""><i class=""ti-tablet""></i></span>
                    <div class=""media-body"">
                        <h3>+1 253 565 2365</h3>
                        <p>Mon to Fri 9am to 6pm</p>
                    </div>
                </div>
                <div class=""media contact-info"">
                    <span class=""contact-info__icon""><i class=""ti-email""></i></span>
                    <div class=""media-body"">
                        <h3>support@colorlib.com</h3>
                        <p>Send us y");
            WriteLiteral("our query anytime!</p>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n<!-- ================ contact section end ================= -->\r\n");
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
