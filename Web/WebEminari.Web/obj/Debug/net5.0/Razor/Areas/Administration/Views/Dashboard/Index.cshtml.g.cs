#pragma checksum "C:\Users\icaka\source\WebEminar\Web\WebEminari.Web\Areas\Administration\Views\Dashboard\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a05f9aafcfcb5b3e0be99c72663dfce277af1f24"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Administration_Views_Dashboard_Index), @"mvc.1.0.view", @"/Areas/Administration/Views/Dashboard/Index.cshtml")]
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
#line 1 "C:\Users\icaka\source\WebEminar\Web\WebEminari.Web\Areas\Administration\Views\_ViewImports.cshtml"
using WebEminari.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\icaka\source\WebEminar\Web\WebEminari.Web\Areas\Administration\Views\_ViewImports.cshtml"
using WebEminari.Web.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a05f9aafcfcb5b3e0be99c72663dfce277af1f24", @"/Areas/Administration/Views/Dashboard/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01f3ef45302eb2447cc1ba2493ca640b9f359c9f", @"/Areas/Administration/Views/_ViewImports.cshtml")]
    public class Areas_Administration_Views_Dashboard_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebEminari.Web.ViewModels.Administration.Dashboard.IndexViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\icaka\source\WebEminar\Web\WebEminari.Web\Areas\Administration\Views\Dashboard\Index.cshtml"
  
    this.ViewData["Title"] = "Admin dashboard";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 6 "C:\Users\icaka\source\WebEminar\Web\WebEminari.Web\Areas\Administration\Views\Dashboard\Index.cshtml"
Write(this.ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\nSettings: ");
#nullable restore
#line 7 "C:\Users\icaka\source\WebEminar\Web\WebEminari.Web\Areas\Administration\Views\Dashboard\Index.cshtml"
     Write(this.Model.SettingsCount);

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebEminari.Web.ViewModels.Administration.Dashboard.IndexViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
