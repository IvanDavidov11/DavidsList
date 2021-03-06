#pragma checksum "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e9753975814602421502ca34f64b0ce0b3da519b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MostPopularMovies_Index), @"mvc.1.0.view", @"/Views/MostPopularMovies/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e9753975814602421502ca34f64b0ce0b3da519b", @"/Views/MostPopularMovies/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ad576ca9cfcfc963f8ebc96878382875907a1adb", @"/Views/_ViewImports.cshtml")]
    public class Views_MostPopularMovies_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ButtonsPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div style=""background-color: darkslategray"" class=""jumbotron"">
    <h1 style=""text-align:center"" class=""display-4"">Top 10 Most Popular Movies!</h1>
    <p style=""text-align:center"" class=""lead"">Here you will find the most popular movies of the week.</p>
</div>

<ol class=""list"">
");
#nullable restore
#line 7 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
     foreach (var movie in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <li>\r\n        <div class=\"regular\">\r\n            <a");
            BeginWriteAttribute("href", " href=\"", 384, "\"", 423, 2);
            WriteAttributeValue("", 391, "MovieDetails?id=", 391, 16, true);
#nullable restore
#line 11 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
WriteAttributeValue("", 407, movie.MoviePath, 407, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                <img class=\"thumbnailPic\"");
            BeginWriteAttribute("src", " src=\"", 468, "\"", 487, 1);
#nullable restore
#line 12 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
WriteAttributeValue("", 474, movie.ImgUrl, 474, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            </a>\r\n            <h1>");
#nullable restore
#line 14 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
           Write(movie.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n            <h3>Relase Date: ");
#nullable restore
#line 15 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
                        Write(movie.Year);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e9753975814602421502ca34f64b0ce0b3da519b6210", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 16 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = movie.MoviePath;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </li>\r\n    <li>\r\n        <div class=\"regular\">\r\n            <a");
            BeginWriteAttribute("href", " href=\"", 746, "\"", 782, 2);
            WriteAttributeValue("", 753, "MovieDetails/", 753, 13, true);
#nullable restore
#line 21 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
WriteAttributeValue("", 766, movie.MoviePath, 766, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                <img class=\"thumbnailPic\"");
            BeginWriteAttribute("src", " src=\"", 827, "\"", 846, 1);
#nullable restore
#line 22 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
WriteAttributeValue("", 833, movie.ImgUrl, 833, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            </a>\r\n            <h1>");
#nullable restore
#line 24 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
           Write(movie.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n            <h3>Relase Date: ");
#nullable restore
#line 25 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
                        Write(movie.Year);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e9753975814602421502ca34f64b0ce0b3da519b9318", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 26 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = movie.MoviePath;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </li>\r\n    <li>\r\n        <div class=\"regular\">\r\n            <a");
            BeginWriteAttribute("href", " href=\"", 1105, "\"", 1141, 2);
            WriteAttributeValue("", 1112, "MovieDetails/", 1112, 13, true);
#nullable restore
#line 31 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
WriteAttributeValue("", 1125, movie.MoviePath, 1125, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                <img class=\"thumbnailPic\"");
            BeginWriteAttribute("src", " src=\"", 1186, "\"", 1205, 1);
#nullable restore
#line 32 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
WriteAttributeValue("", 1192, movie.ImgUrl, 1192, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            </a>\r\n            <h1>");
#nullable restore
#line 34 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
           Write(movie.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n            <h3>Relase Date: ");
#nullable restore
#line 35 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
                        Write(movie.Year);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e9753975814602421502ca34f64b0ce0b3da519b12436", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 36 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = movie.MoviePath;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </li>\r\n");
#nullable restore
#line 39 "C:\Users\andic\source\repos\DavidsList\DavidsList\Views\MostPopularMovies\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</ol>");
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
