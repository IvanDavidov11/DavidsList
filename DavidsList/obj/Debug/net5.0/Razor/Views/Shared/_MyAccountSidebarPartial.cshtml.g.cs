#pragma checksum "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\Shared\_MyAccountSidebarPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "11ba14d3c4aaedfb7f4eef56310ed239026c2138"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__MyAccountSidebarPartial), @"mvc.1.0.view", @"/Views/Shared/_MyAccountSidebarPartial.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"11ba14d3c4aaedfb7f4eef56310ed239026c2138", @"/Views/Shared/_MyAccountSidebarPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ad576ca9cfcfc963f8ebc96878382875907a1adb", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__MyAccountSidebarPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<nav id=""sidebar"">
   <ul class=""list-unstyled components"">
        <li id=""aDets"" class=""active"">
            <a  href=""/MyProfile"">Account Details</a>
        </li>
        <li id =""upDets"">
            <a  href=""/MyProfile/UpdateDetails"">Update Account Details</a>
        </li>
        <li id=""genPref"">
            <a  href=""/MyProfile/Preferences"">Preferences</a>
        </li>
    </ul>
</nav>");
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
