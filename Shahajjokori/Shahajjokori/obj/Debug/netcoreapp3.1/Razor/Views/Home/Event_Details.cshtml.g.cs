#pragma checksum "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9587a940742a604b4717b633e33a025b7f5d363e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Event_Details), @"mvc.1.0.view", @"/Views/Home/Event_Details.cshtml")]
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
#line 1 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\_ViewImports.cshtml"
using Shahajjokori;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\_ViewImports.cshtml"
using Shahajjokori.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9587a940742a604b4717b633e33a025b7f5d363e", @"/Views/Home/Event_Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"57ee162fda7950fa03bc844746397fe0c0469436", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Event_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Shahajjokori.Models.Event>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("height: 20vw; max-width: 100%;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("<!--<h1>");
#nullable restore
#line 8 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
   Write(ViewBag.event_id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>-->\r\n<div style=\"background-color:aliceblue\">\r\n    <div class=\"container\" style=\"padding-top: 5%;\">\r\n        <div class=\"row\">\r\n            <div class=\"col-md-12\">\r\n                <h3 style=\"text-decoration: underline;\">");
#nullable restore
#line 13 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                                                   Write(Html.DisplayFor(model => model.e_title));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
            </div>
        </div>
        <div class=""row"" style=""margin-top: 3%;"">
            <div class=""col-md-6"">
                <h5>Created by: <button class=""btn btn-link"" data-toggle=""modal"" data-target=""#exampleModal1"" style=""font-size: 20px; text-decoration: none; margin-bottom:1%;"">");
#nullable restore
#line 18 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                                                                                                                                                                           Write(ViewBag.org);

#line default
#line hidden
#nullable disable
            WriteLiteral("</button></h5>\r\n\r\n                <h5>Location: ");
#nullable restore
#line 20 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                         Write(Html.DisplayFor(model => model.e_location));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                <br>\r\n");
#nullable restore
#line 22 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                 if (ViewBag.event_cat == 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h5>Category: Education</h5>\r\n");
#nullable restore
#line 25 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                 if (ViewBag.event_cat == 1)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h5>Category: Education</h5>\r\n");
#nullable restore
#line 29 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                 if (ViewBag.event_cat == 2)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h5>Category: Emergency Aid</h5>\r\n");
#nullable restore
#line 33 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                 if (ViewBag.event_cat == 3)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h5>Category: Education</h5>\r\n");
#nullable restore
#line 37 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 38 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                 if (ViewBag.event_cat == 4)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h5>Category: Education</h5>\r\n");
#nullable restore
#line 41 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                 if (ViewBag.event_cat == 5)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h5>Category: Education</h5>\r\n");
#nullable restore
#line 45 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <br>\r\n                <h5>Opening: ");
#nullable restore
#line 47 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                        Write(Html.DisplayFor(model => model.e_opening_date));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                <br>\r\n                <h5>Closing: ");
#nullable restore
#line 49 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                        Write(Html.DisplayFor(model => model.e_closing_date));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                <br>\r\n                <h5>Expected: ");
#nullable restore
#line 51 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                         Write(ViewBag.exp_amount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Tk.</h5>\r\n                <br>\r\n                <h5>Raised: ");
#nullable restore
#line 53 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                       Write(ViewBag.raised_amount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Tk.</h5>\r\n                <div class=\"progress\" style=\"margin-top: 5%\">\r\n");
#nullable restore
#line 55 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                      
                        float raised = (float)ViewBag.raised_amount;
                        float exp = (float)ViewBag.exp_amount;
                        float percentage = (raised / exp) * 100;
                        string perc = percentage + "%";
                    

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"progress-bar bg-info\" role=\"progressbar\"");
            BeginWriteAttribute("aria-valuenow", " aria-valuenow=\"", 2517, "\"", 2555, 1);
#nullable restore
#line 61 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
WriteAttributeValue("", 2533, ViewBag.raised_amount, 2533, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" aria-valuemin=\"0\"");
            BeginWriteAttribute("aria-valuemax", " aria-valuemax=\"", 2574, "\"", 2609, 1);
#nullable restore
#line 61 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
WriteAttributeValue("", 2590, ViewBag.exp_amount, 2590, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("style", " style=\"", 2610, "\"", 2629, 2);
            WriteAttributeValue("", 2618, "width:", 2618, 6, true);
#nullable restore
#line 61 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
WriteAttributeValue("", 2624, perc, 2624, 5, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                        ");
#nullable restore
#line 62 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                   Write(ViewBag.raised_amount);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n                <br>\r\n\r\n            </div>\r\n            <div class=\"col-md-6\">\r\n");
#nullable restore
#line 69 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                  
                    string pic_name = ViewBag.event_pic;
                    string path = $"~/images/{pic_name}";
                

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "9587a940742a604b4717b633e33a025b7f5d363e13044", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 74 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
              WriteLiteral(path);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("src", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
#nullable restore
#line 74 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                <br>\r\n                <h5 style=\"margin-top: 3%;\">");
#nullable restore
#line 76 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                                       Write(Html.DisplayFor(model => model.e_donor_count));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" people have donated for this event. </h5>
                <br />
                <button type=""button"" class=""btn btn-outline-primary"" data-toggle=""modal"" data-target=""#exampleModal2"">Donate</button>
            </div>
        </div>
        <div class=""row"" style=""margin-top: 5%;"">
            <div class=""col-md-12"">
                <h3>About the Event: </h3>
                <p>");
#nullable restore
#line 84 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
              Write(Html.DisplayFor(model => model.e_details));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
            </div>
        </div>

    </div>
</div>

<!-- Modal -->
<div class=""modal fade"" id=""exampleModal1"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLabel"">");
#nullable restore
#line 96 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                                                          Write(ViewBag.org);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n\r\n            </div>\r\n            <div class=\"modal-body\">\r\n                ");
#nullable restore
#line 100 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
           Write(ViewBag.org_about);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n\r\n            <div class=\"modal-footer\">\r\n                Contact: ");
#nullable restore
#line 104 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                    Write(ViewBag.org_phone);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </div>
        </div>
    </div>
</div>

<!-- Modal -->
<div class=""modal fade"" id=""exampleModal2"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLabel"">");
#nullable restore
#line 115 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                                                          Write(ViewBag.org);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n\r\n            </div>\r\n            <div class=\"modal-body\" style=\"white-space:pre-line;\">\r\n                ");
#nullable restore
#line 119 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
           Write(ViewBag.event_trans);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n\r\n            <div class=\"modal-footer\">\r\n                Contact: ");
#nullable restore
#line 123 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Event_Details.cshtml"
                    Write(ViewBag.org_phone);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Shahajjokori.Models.Event> Html { get; private set; }
    }
}
#pragma warning restore 1591
