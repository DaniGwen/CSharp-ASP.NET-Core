#pragma checksum "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Shared\_Layout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "58e10bcf163d6b27db82af588ac9bd77fcc1e435"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Layout), @"mvc.1.0.view", @"/Views/Shared/_Layout.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"58e10bcf163d6b27db82af588ac9bd77fcc1e435", @"/Views/Shared/_Layout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4100cddf35ca3c9416065655d32b777c09b5f350", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__Layout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!doctype html>\r\n<html class=\"no-js\" lang=\"en\">\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "58e10bcf163d6b27db82af588ac9bd77fcc1e4353403", async() => {
                WriteLiteral("\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"x-ua-compatible\" content=\"ie=edge\">\r\n    <title>");
#nullable restore
#line 7 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Shared\_Layout.cshtml"
      Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" = DigitalCoolBook</title>\r\n    <meta name=\"description\"");
                BeginWriteAttribute("content", " content=\"", 231, "\"", 241, 0);
                EndWriteAttribute();
                WriteLiteral(@">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">

    <!-- <link rel=""manifest"" href=""site.webmanifest""> -->
    <link rel=""shortcut icon"" type=""image/x-icon"" href=""img/favicon.png"">
    <!-- Place favicon.ico in the root directory -->
    <!-- CSS here -->
    <link rel=""stylesheet"" href=""css/bootstrap.min.css"">
    <link rel=""stylesheet"" href=""css/owl.carousel.min.css"">
    <link rel=""stylesheet"" href=""css/magnific-popup.css"">
    <link rel=""stylesheet"" href=""css/font-awesome.min.css"">
    <link rel=""stylesheet"" href=""css/themify-icons.css"">
    <link rel=""stylesheet"" href=""css/nice-select.css"">
    <link rel=""stylesheet"" href=""css/flaticon.css"">
    <link rel=""stylesheet"" href=""css/gijgo.css"">
    <link rel=""stylesheet"" href=""css/animate.css"">
    <link rel=""stylesheet"" href=""css/slicknav.css"">
    <link rel=""stylesheet"" href=""css/style.css"">
    <!-- <link rel=""stylesheet"" href=""css/responsive.css""> -->
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "58e10bcf163d6b27db82af588ac9bd77fcc1e4355985", async() => {
                WriteLiteral(@"
    <header>
        <div class=""header-area "">
            <div id=""sticky-header"" class=""main-header-area"">
                <div class=""container-fluid p-0"">
                    <div class=""row align-items-center no-gutters"">
                        <div class=""col-xl-2 col-lg-2"">
                            <div class=""logo-img"">
                                <a href=""index.html"">
                                    <img src=""img/logo.png""");
                BeginWriteAttribute("alt", " alt=\"", 1681, "\"", 1687, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                                </a>
                            </div>
                        </div>
                        <div class=""col-xl-7 col-lg-7"">
                            <div class=""main-menu  d-none d-lg-block"">
                                <nav>
                                    <ul id=""navigation"">
                                        <li><a class=""active"" href=""index.html"">home</a></li>
                                        <li><a href=""Courses.html"">Courses</a></li>
                                        <li>
                                            <a href=""#"">pages <i class=""ti-angle-down""></i></a>
                                            <ul class=""submenu"">
                                                <li><a href=""course_details.html"">course details</a></li>
                                                <li><a href=""elements.html"">elements</a></li>
                                            </ul>
                                        </li>
");
                WriteLiteral(@"                                        <li><a href=""about.html"">About</a></li>
                                        <li>
                                            <a href=""#"">blog <i class=""ti-angle-down""></i></a>
                                            <ul class=""submenu"">
                                                <li><a href=""blog.html"">blog</a></li>
                                                <li><a href=""single-blog.html"">single-blog</a></li>
                                            </ul>
                                        </li>
                                        <li><a href=""contact.html"">Contact</a></li>
                                    </ul>
                                </nav>
                            </div>
                        </div>
                        <div class=""col-xl-3 col-lg-3 d-none d-lg-block"">
                            <div class=""log_chat_area d-flex align-items-center"">
                                <a href=""#test-form"" cl");
                WriteLiteral(@"ass=""login popup-with-form"">
                                    <i class=""flaticon-user""></i>
                                    <span>log in</span>
                                </a>
                                <div class=""live_chat_btn"">
                                    <a class=""boxed_btn_orange"" href=""#"">
                                        <i class=""fa fa-phone""></i>
                                        <span>+10 378 467 3672</span>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class=""col-12"">
                            <div class=""mobile_menu d-block d-lg-none""></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <div class=""slider_area "">
        <div class=""single_slider d-flex align-items-center justify-content-center slider_bg_1"">
            <div");
                WriteLiteral(@" class=""container"">
                <div class=""row align-items-center justify-content-center"">
                    <div class=""col-xl-6 col-md-6"">
                        <div class=""illastrator_png"">
                            <img src=""img/banner/edu_ilastration.png""");
                BeginWriteAttribute("alt", " alt=\"", 5035, "\"", 5041, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                        </div>
                    </div>
                    <div class=""col-xl-6 col-md-6"">
                        <div class=""slider_info"">
                            <h3>
                                Learn your <br>
                                Favorite Course <br>
                                From Online
                            </h3>
                            <a href=""#"" class=""boxed_btn"">Browse Our Courses</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""container"">
        <main role=""main"" class=""pb-3"">
            ");
#nullable restore
#line 114 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Shared\_Layout.cshtml"
       Write(RenderBody());

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
        </main>
    </div>

    <footer class=""footer footer_bg_1"">
        <div class=""footer_top"">
            <div class=""container"">
                <div class=""row"">
                    <div class=""col-xl-4 col-md-6 col-lg-4"">
                        <div class=""footer_widget"">
                            <div class=""footer_logo"">
                                <a href=""#"">
                                    <img src=""img/logo.png""");
                BeginWriteAttribute("alt", " alt=\"", 6190, "\"", 6196, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                                </a>
                            </div>
                            <p>
                                Firmament morning sixth subdue darkness creeping gathered divide our let god moving.
                                Moving in fourth air night bring upon it beast let you dominion likeness open place day
                                great.
                            </p>
                            <div class=""socail_links"">
                                <ul>
                                    <li>
                                        <a href=""#"">
                                            <i class=""ti-facebook""></i>
                                        </a>
                                    </li>
                                    <li>
                                        <a href=""#"">
                                            <i class=""ti-twitter-alt""></i>
                                        </a>
                               ");
                WriteLiteral(@"     </li>
                                    <li>
                                        <a href=""#"">
                                            <i class=""fa fa-instagram""></i>
                                        </a>
                                    </li>
                                    <li>
                                        <a href=""#"">
                                            <i class=""fa fa-youtube-play""></i>
                                        </a>
                                    </li>
                                </ul>
                            </div>

                        </div>
                    </div>
                    <div class=""col-xl-2 offset-xl-1 col-md-6 col-lg-3"">
                        <div class=""footer_widget"">
                            <h3 class=""footer_title"">
                                Courses
                            </h3>
                            <ul>
                                <li><a href=""#"">Wordpres");
                WriteLiteral(@"s</a></li>
                                <li><a href=""#""> Photoshop</a></li>
                                <li><a href=""#"">Illustrator</a></li>
                                <li><a href=""#"">Adobe XD</a></li>
                                <li><a href=""#"">UI/UX</a></li>
                            </ul>

                        </div>
                    </div>
                    <div class=""col-xl-2 col-md-6 col-lg-2"">
                        <div class=""footer_widget"">
                            <h3 class=""footer_title"">
                                Resourches
                            </h3>
                            <ul>
                                <li><a href=""#"">Free Adobe XD</a></li>
                                <li><a href=""#"">Tutorials</a></li>
                                <li><a href=""#"">About</a></li>
                                <li><a href=""#""> About</a></li>
                                <li><a href=""#""> Contact</a></li>
                          ");
                WriteLiteral(@"  </ul>
                        </div>
                    </div>
                    <div class=""col-xl-3 col-md-6 col-lg-3"">
                        <div class=""footer_widget"">
                            <h3 class=""footer_title"">
                                Address
                            </h3>
                            <p>
                                200, D-block, Green lane USA <br>
                                +10 367 467 8934 <br>
                                edumark@contact.com
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class=""copy-right_text"">
            <div class=""container"">
                <div class=""footer_border""></div>
                <div class=""row"">
                    <div class=""col-xl-12"">
                        <p class=""copy_right text-center"">
                            <!-- Link back to Colorlib can't be removed. Templat");
                WriteLiteral(@"e is licensed under CC BY 3.0. -->
                            Copyright &copy;
                            <script>document.write(new Date().getFullYear());</script> All rights reserved | This template is made with <i class=""fa fa-heart-o"" aria-hidden=""true""></i> by <a href=""https://colorlib.com"" target=""_blank"">Colorlib</a>
                            <!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. -->
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </footer>
    <script src=""js/vendor/modernizr-3.5.0.min.js""></script>
    <script src=""js/vendor/jquery-1.12.4.min.js""></script>
    <script src=""js/popper.min.js""></script>
    <script src=""js/bootstrap.min.js""></script>
    <script src=""js/owl.carousel.min.js""></script>
    <script src=""js/isotope.pkgd.min.js""></script>
    <script src=""js/ajax-form.js""></script>
    <script src=""js/waypoints.min.js""></script>
    <script src=""js/jquery.co");
                WriteLiteral(@"unterup.min.js""></script>
    <script src=""js/imagesloaded.pkgd.min.js""></script>
    <script src=""js/scrollIt.js""></script>
    <script src=""js/jquery.scrollUp.min.js""></script>
    <script src=""js/wow.min.js""></script>
    <script src=""js/nice-select.min.js""></script>
    <script src=""js/jquery.slicknav.min.js""></script>
    <script src=""js/jquery.magnific-popup.min.js""></script>
    <script src=""js/plugins.js""></script>
    <script src=""js/gijgo.min.js""></script>

    <!--contact js-->
    <script src=""js/contact.js""></script>
    <script src=""js/jquery.ajaxchimp.min.js""></script>
    <script src=""js/jquery.form.js""></script>
    <script src=""js/jquery.validate.min.js""></script>
    <script src=""js/mail-script.js""></script>

    <script src=""js/main.js""></script>
    ");
#nullable restore
#line 248 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Shared\_Layout.cshtml"
Write(RenderSection("Scripts", required: false));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n");
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