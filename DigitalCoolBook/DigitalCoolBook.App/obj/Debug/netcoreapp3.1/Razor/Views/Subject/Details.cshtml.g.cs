#pragma checksum "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Subject\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bd3b5324955dd9c8a43d188c105f1600d011ed8a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Subject_Details), @"mvc.1.0.view", @"/Views/Subject/Details.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bd3b5324955dd9c8a43d188c105f1600d011ed8a", @"/Views/Subject/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6e381d074b162879d66b39334af76ae0439e473", @"/Views/_ViewImports.cshtml")]
    public class Views_Subject_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DigitalCoolBook.App.Models.SubjectViewModels.SubjectViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("#"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("newsletter_form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("test-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("white-popup-block mfp-hide"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("test-form2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bd3b5324955dd9c8a43d188c105f1600d011ed8a5532", async() => {
                WriteLiteral(@"
     <!-- bradcam_area_start -->
     <div class=""courses_details_banner"">
         <div class=""container"">
             <div class=""row"">
                 <div class=""col-xl-6"">
                     <div class=""course_text"">
                            <h3>");
#nullable restore
#line 10 "C:\Users\deko\Documents\GitHub\CSharp-ASP.NET-Core\DigitalCoolBook\DigitalCoolBook.App\Views\Subject\Details.cshtml"
                           Write(Model.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h3>
                            <div class=""hours"">
                                <div class=""video"">
                                     <div class=""single_video"">
                                            <i class=""fa fa-clock-o""></i> <span>...</span>
                                     </div>
                                     <div class=""single_video"">
                                            <i class=""fa fa-play-circle-o""></i> <span>...</span>
                                     </div>
                                </div>
                            </div>
                     </div>
                 </div>
             </div>
         </div>
    </div>
    <!-- bradcam_area_end -->

    <div class=""courses_details_info"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-xl-7 col-lg-7"">
                    <div class=""single_courses"">
                        <h3>Описание</h3>
                        <p>.............</p>
                    <h3 c");
                WriteLiteral(@"lass=""second_title"">Course Outline</h3>
                    </div>
                    <div class=""outline_courses_info"">
                            <div id=""accordion"">
                                    <div class=""card"">
                                        <div class=""card-header"" id=""headingTwo"">
                                            <h5 class=""mb-0"">
                                                <button class=""btn btn-link collapsed"" data-toggle=""collapse"" data-target=""#collapseTwo"" aria-expanded=""false"" aria-controls=""collapseTwo"">
                                                    <i class=""flaticon-question""></i> Is WordPress hosting worth it?
                                                </button>
                                            </h5>
                                        </div>
                                        <div id=""collapseTwo"" class=""collapse"" aria-labelledby=""headingTwo"" data-parent=""#accordion"">
                                            <div class=""card");
                WriteLiteral(@"-body"">
                                                Our set he for firmament morning sixth subdue darkness creeping gathered divide our
                                                let god moving. Moving in fourth air night bring upon
                                            </div>
                                        </div>
                                    </div>
                                    <div class=""card"">
                                        <div class=""card-header"" id=""headingOne"">
                                            <h5 class=""mb-0"">
                                                <button class=""btn btn-link collapsed"" data-toggle=""collapse"" data-target=""#collapseOne"" aria-expanded=""false"" aria-controls=""collapseOne"">
                                                    <i class=""flaticon-question""></i>Basic Classes</span>
                                                </button>
                                            </h5>
                                        ");
                WriteLiteral("</div>\n                                        <div id=\"collapseOne\" class=\"collapse\" aria-labelledby=\"headingOne\" data-parent=\"#accordion\"");
                BeginWriteAttribute("style", " style=\"", 3557, "\"", 3565, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                                            <div class=""card-body"">
                                                Our set he for firmament morning sixth subdue darkness creeping gathered divide our
                                                let god moving. Moving in fourth air night bring upon
                                            </div>
                                        </div>
                                    </div>
                                    <div class=""card"">
                                        <div class=""card-header"" id=""headingThree"">
                                            <h5 class=""mb-0"">
                                                <button class=""btn btn-link collapsed"" data-toggle=""collapse"" data-target=""#collapseThree"" aria-expanded=""false"" aria-controls=""collapseThree"">
                                                    <i class=""flaticon-question""></i> Will you transfer my site?
                                                </button>
               ");
                WriteLiteral(@"                             </h5>
                                        </div>
                                        <div id=""collapseThree"" class=""collapse"" aria-labelledby=""headingThree"" data-parent=""#accordion"">
                                            <div class=""card-body"">
                                                Our set he for firmament morning sixth subdue darkness creeping gathered divide our
                                                let god moving. Moving in fourth air night bring upon
                                            </div>
                                        </div>
                                    </div>
                                    <div class=""card"">
                                        <div class=""card-header"" id=""heading_4"">
                                            <h5 class=""mb-0"">
                                                <button class=""btn btn-link collapsed"" data-toggle=""collapse"" data-target=""#collapse_4"" aria-expanded=""false"" aria-");
                WriteLiteral(@"controls=""collapse_4"">
                                                    <i class=""flaticon-question""></i> Why should I host with Hostza?
                                                </button>
                                            </h5>
                                        </div>
                                        <div id=""collapse_4"" class=""collapse"" aria-labelledby=""heading_4"" data-parent=""#accordion"">
                                            <div class=""card-body"">
                                                Our set he for firmament morning sixth subdue darkness creeping gathered divide our
                                                let god moving. Moving in fourth air night bring upon
                                            </div>
                                        </div>
                                    </div>
                                    <div class=""card"">
                                        <div class=""card-header"" id=""heading_5"">
                  ");
                WriteLiteral(@"                          <h5 class=""mb-0"">
                                                <button class=""btn btn-link collapsed"" data-toggle=""collapse"" data-target=""#collapse_5"" aria-expanded=""false"" aria-controls=""collapse_5"">
                                                    <i class=""flaticon-question""></i> How do I get started <span>with Shared
                                                        Hosting?</span>
                                                </button>
                                            </h5>
                                        </div>
                                        <div id=""collapse_5"" class=""collapse"" aria-labelledby=""heading_5"" data-parent=""#accordion"">
                                            <div class=""card-body"">
                                                Our set he for firmament morning sixth subdue darkness creeping gathered divide our
                                                let god moving. Moving in fourth air night bring upon
        ");
                WriteLiteral(@"                                    </div>
                                        </div>
                                    </div>
                                </div>
                    </div>
                </div>
                <div class=""col-xl-5 col-lg-5"">
                    <div class=""courses_sidebar"">
                        <div class=""video_thumb"">
                            <img src=""img/latest_blog/video.png""");
                BeginWriteAttribute("alt", " alt=\"", 8096, "\"", 8102, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                            <a class=""popup-video"" href=""https://www.youtube.com/watch?v=AjgD3CvWzS0"">
                                <i class=""fa fa-play""></i>
                            </a>
                        </div>
                        <div class=""author_info"">
                            <div class=""auhor_header"">
                                <div class=""thumb"">
                                        <img src=""img/latest_blog/author.png""");
                BeginWriteAttribute("alt", " alt=\"", 8565, "\"", 8571, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                                </div>
                                <div class=""name"">
                                    <h3>Macau Wilium</h3>
                                    <p>UI/UX Designer</p>
                                </div>
                            </div>
                            <p class=""text_info"">
                                Our set he for firmament morning sixth subdue darkness creeping gathered divide our let
                                god moving. Moving in fourth air night bring upon you’re it beast let you dominion
                                likeness open place day
                            </p>
                            <ul>
                                <li><a href=""#""> <i class=""fa fa-envelope""></i> </a></li>
                                <li><a href=""#""> <i class=""fa fa-twitter""></i> </a></li>
                                <li><a href=""#""> <i class=""ti-linkedin""></i> </a></li>
                            </ul>
                        </div>
    ");
                WriteLiteral(@"                    <a href=""#"" class=""boxed_btn"">Buy Course</a>
                        <div class=""feedback_info"">
                            <h3>Write your feedback</h3>
                            <p>Your rating</p>
                            <i class=""flaticon-mark-as-favorite-star""></i>
                            <i class=""flaticon-mark-as-favorite-star""></i>
                            <i class=""flaticon-mark-as-favorite-star""></i>
                            <i class=""flaticon-mark-as-favorite-star""></i>
                            <i class=""flaticon-mark-as-favorite-star""></i>
                            
                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bd3b5324955dd9c8a43d188c105f1600d011ed8a17189", async() => {
                    WriteLiteral("\n                                <textarea");
                    BeginWriteAttribute("name", " name=\"", 10304, "\"", 10311, 0);
                    EndWriteAttribute();
                    BeginWriteAttribute("id", " id=\"", 10312, "\"", 10317, 0);
                    EndWriteAttribute();
                    WriteLiteral(" cols=\"30\" rows=\"10\" placeholder=\"Write your feedback\"></textarea>\n                                <button type=\"submit\" class=\"boxed_btn\">Submit</button>\n                            ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
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
                </div>
            </div>
        </div>
    </div>




    <!-- testimonial_area_start -->
    <div class=""testimonial_area testimonial_bg_1 overlay"">
        <div class=""testmonial_active owl-carousel"">
            <div class=""single_testmoial"">
                <div class=""container"">
                    <div class=""row"">
                        <div class=""col-xl-12"">
                            <div class=""testmonial_text text-center"">
                                <div class=""author_img"">
                                    <img src=""img/testmonial/author_img.png""");
                BeginWriteAttribute("alt", " alt=\"", 11160, "\"", 11166, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                                </div>
                                <p>
                                    ""Working in conjunction with humanitarian aid <br> agencies we have supported
                                    programmes to <br>
                                    alleviate.
                                    human suffering.

                                </p>
                                <span>- Jquileen</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""single_testmoial"">
                <div class=""container"">
                    <div class=""row"">
                        <div class=""col-xl-12"">
                            <div class=""testmonial_text text-center"">
                                <div class=""author_img"">
                                    <img src=""img/testmonial/author_img.png""");
                BeginWriteAttribute("alt", " alt=\"", 12114, "\"", 12120, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                                </div>
                                <p>
                                    ""Working in conjunction with humanitarian aid <br> agencies we have supported
                                    programmes to <br>
                                    alleviate.
                                    human suffering.

                                </p>
                                <span>- Jquileen</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- testimonial_area_end -->

    <!-- our_courses_start -->
    <div class=""our_courses"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-xl-12"">
                    <div class=""section_title text-center mb-100"">
                        <h3>Our Course Speciality</h3>
                        <p>Your domain control panel is designed for ease-of-use and <br>
             ");
                WriteLiteral(@"               allows for all aspects of your domains.
                        </p>
                    </div>
                </div>
            </div>
            <div class=""row"">
                <div class=""col-xl-3 col-md-6 col-lg-6"">
                    <div class=""single_course text-center"">
                        <div class=""icon"">
                            <i class=""flaticon-art-and-design""></i>
                        </div>
                        <h3>Premium Quality</h3>
                        <p>
                            Your domain control panel is designed for ease-of-use <br> and <br>
                            allows for all aspects of
                        </p>
                    </div>
                </div>
                <div class=""col-xl-3 col-md-6 col-lg-6"">
                    <div class=""single_course text-center"">
                        <div class=""icon blue"">
                            <i class=""flaticon-business-and-finance""></i>
                        </div>
      ");
                WriteLiteral(@"                  <h3>Premium Quality</h3>
                        <p>
                            Your domain control panel is designed for ease-of-use <br> and <br>
                            allows for all aspects of
                        </p>
                    </div>
                </div>
                <div class=""col-xl-3 col-md-6 col-lg-6"">
                    <div class=""single_course text-center"">
                        <div class=""icon"">
                            <i class=""flaticon-premium""></i>
                        </div>
                        <h3>Premium Quality</h3>
                        <p>
                            Your domain control panel is designed for ease-of-use <br> and <br>
                            allows for all aspects of
                        </p>
                    </div>
                </div>
                <div class=""col-xl-3 col-md-6 col-lg-6"">
                    <div class=""single_course text-center"">
                        <div class=""icon gradient");
                WriteLiteral(@""">
                            <i class=""flaticon-crown""></i>
                        </div>
                        <h3>Premium Quality</h3>
                        <p>
                            Your domain control panel is designed for ease-of-use <br> and <br>
                            allows for all aspects of
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- our_courses_end -->

    <!-- subscribe_newsletter_Start -->
    <div class=""subscribe_newsletter"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-xl-6 col-lg-6"">
                    <div class=""newsletter_text"">
                        <h3>Subscribe Newsletter</h3>
                        <p>Your domain control panel is designed for ease-of-use and allows for all aspects of your</p>
                    </div>
                </div>
                <div class=""col-xl-5 offset-xl-1 col-lg-6"">
                    <div cl");
                WriteLiteral("ass=\"newsletter_form\">\n                        <h4>Your domain control panel is</h4>\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bd3b5324955dd9c8a43d188c105f1600d011ed8a25439", async() => {
                    WriteLiteral("\n                            <input type=\"text\" placeholder=\"Enter your mail\">\n                            <button type=\"submit\">Sign Up</button>\n                        ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n                    </div>\n                </div>\n            </div>\n        </div>\n    </div>\n    <!-- subscribe_newsletter_end -->\n\n\n\n    <!-- form itself end-->\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bd3b5324955dd9c8a43d188c105f1600d011ed8a27260", async() => {
                    WriteLiteral("\n        <div class=\"popup_box \">\n            <div class=\"popup_inner\">\n                <div class=\"logo text-center\">\n                    <a href=\"#\">\n                        <img src=\"img/form-logo.png\"");
                    BeginWriteAttribute("alt", " alt=\"", 16973, "\"", 16979, 0);
                    EndWriteAttribute();
                    WriteLiteral(">\n                    </a>\n                </div>\n                <h3>Sign in</h3>\n                ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bd3b5324955dd9c8a43d188c105f1600d011ed8a28009", async() => {
                        WriteLiteral(@"
                    <div class=""row"">
                        <div class=""col-xl-12 col-md-12"">
                            <input type=""email"" placeholder=""Enter email"">
                        </div>
                        <div class=""col-xl-12 col-md-12"">
                            <input type=""password"" placeholder=""Password"">
                        </div>
                        <div class=""col-xl-12"">
                            <button type=""submit"" class=""boxed_btn_orange"">Sign in</button>
                        </div>
                    </div>
                ");
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\n                <p class=\"doen_have_acc\">Don’t have an account? <a class=\"dont-hav-acc\" href=\"#test-form2\">Sign Up</a>\n                </p>\n            </div>\n        </div>\n    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n    <!-- form itself end -->\n\n    <!-- form itself end-->\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bd3b5324955dd9c8a43d188c105f1600d011ed8a31510", async() => {
                    WriteLiteral("\n        <div class=\"popup_box \">\n            <div class=\"popup_inner\">\n                <div class=\"logo text-center\">\n                    <a href=\"#\">\n                        <img src=\"img/form-logo.png\"");
                    BeginWriteAttribute("alt", " alt=\"", 18194, "\"", 18200, 0);
                    EndWriteAttribute();
                    WriteLiteral(">\n                    </a>\n                </div>\n                <h3>Resistration</h3>\n                ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bd3b5324955dd9c8a43d188c105f1600d011ed8a32264", async() => {
                        WriteLiteral(@"
                    <div class=""row"">
                        <div class=""col-xl-12 col-md-12"">
                            <input type=""email"" placeholder=""Enter email"">
                        </div>
                        <div class=""col-xl-12 col-md-12"">
                            <input type=""password"" placeholder=""Password"">
                        </div>
                        <div class=""col-xl-12 col-md-12"">
                            <input type=""Password"" placeholder=""Confirm password"">
                        </div>
                        <div class=""col-xl-12"">
                            <button type=""submit"" class=""boxed_btn_orange"">Sign Up</button>
                        </div>
                    </div>
                ");
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\n            </div>\n        </div>\n    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
    <!-- form itself end -->


    <!-- JS here -->
    <script src=""js/vendor/modernizr-3.5.0.min.js""></script>
    <script src=""js/vendor/jquery-1.12.4.min.js""></script>
    <script src=""js/popper.min.js""></script>
    <script src=""js/bootstrap.min.js""></script>
    <script src=""js/owl.carousel.min.js""></script>
    <script src=""js/isotope.pkgd.min.js""></script>
    <script src=""js/ajax-form.js""></script>
    <script src=""js/waypoints.min.js""></script>
    <script src=""js/jquery.counterup.min.js""></script>
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
    <script src=""js/contact.js""></script>");
                WriteLiteral("\n    <script src=\"js/jquery.ajaxchimp.min.js\"></script>\n    <script src=\"js/jquery.form.js\"></script>\n    <script src=\"js/jquery.validate.min.js\"></script>\n    <script src=\"js/mail-script.js\"></script>\n\n    <script src=\"js/main.js\"></script>\n");
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
            WriteLiteral("\n\n</html>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DigitalCoolBook.App.Models.SubjectViewModels.SubjectViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
