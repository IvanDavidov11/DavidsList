#pragma checksum "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "46554787bb9c8236f9289a40c5a2e19cd15e32a1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
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
#line 1 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\_ViewImports.cshtml"
using DavidsList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\_ViewImports.cshtml"
using DavidsList.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\_ViewImports.cshtml"
using DavidsList.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\_ViewImports.cshtml"
using DavidsList.Data.DbModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\_ViewImports.cshtml"
using DavidsList.Models.FormModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"46554787bb9c8236f9289a40c5a2e19cd15e32a1", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ad576ca9cfcfc963f8ebc96878382875907a1adb", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ErrorViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\Shared\Error.cshtml"
  
    ViewData["Title"] = "Error";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1 class=""text-danger"">Access denied.</h1>

<p>
    You cannot access this page without logging into your account first. Please <a href=""/User/Login""><b>log in here</b></a> or if you do not have an account, <a href=""/User/Login""><b>register here</b></a>...
</p>
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ErrorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
