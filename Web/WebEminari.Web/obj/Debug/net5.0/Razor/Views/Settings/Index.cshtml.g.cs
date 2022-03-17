#pragma checksum "D:\Projects\WebEminars\Web\WebEminari.Web\Views\Settings\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "998ba986a5df2551b46d5db20cc9f5de0389715f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Settings_Index), @"mvc.1.0.view", @"/Views/Settings/Index.cshtml")]
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
#line 1 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\_ViewImports.cshtml"
using WebEminari.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\_ViewImports.cshtml"
using WebEminari.Web.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\_ViewImports.cshtml"
using WebEminari.Web.ViewModels.WebEminars;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\_ViewImports.cshtml"
using WebEminari.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\_ViewImports.cshtml"
using WebEminari.Data.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\_ViewImports.cshtml"
using WebEminari.Web.ViewModels.Reports.WebEminars;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\_ViewImports.cshtml"
using WebEminari.Web.ViewModels.Reports.Comments;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\_ViewImports.cshtml"
using WebEminari.Web.ViewModels.Administration;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\_ViewImports.cshtml"
using WebEminari.Web.ViewModels.Administration.Report;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\_ViewImports.cshtml"
using WebEminari.Web.ViewModels.Organizations;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\_ViewImports.cshtml"
using WebEminari.Web.ViewModels.Comments;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\_ViewImports.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"998ba986a5df2551b46d5db20cc9f5de0389715f", @"/Views/Settings/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"edf2788d1feffe69274e31855e28ee954f5c7826", @"/Views/_ViewImports.cshtml")]
    public class Views_Settings_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebEminari.Web.ViewModels.Settings.SettingsListViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "InsertSetting", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\Settings\Index.cshtml"
  
    this.ViewData["Title"] = "Settings";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 7 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\Settings\Index.cshtml"
Write(this.ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n");
#nullable restore
#line 9 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\Settings\Index.cshtml"
 foreach (var setting in this.Model.Settings)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>");
#nullable restore
#line 11 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\Settings\Index.cshtml"
    Write(setting.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(": ");
#nullable restore
#line 11 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\Settings\Index.cshtml"
                 Write(setting.NameAndValue);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
#nullable restore
#line 12 "D:\Projects\WebEminars\Web\WebEminari.Web\Views\Settings\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "998ba986a5df2551b46d5db20cc9f5de0389715f7020", async() => {
                WriteLiteral("Insert new");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebEminari.Web.ViewModels.Settings.SettingsListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
