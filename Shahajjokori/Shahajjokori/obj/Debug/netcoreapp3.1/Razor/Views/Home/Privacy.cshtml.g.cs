#pragma checksum "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Privacy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1e0edf8bd581b94155b3a5930d61c0c49d190cd8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Privacy), @"mvc.1.0.view", @"/Views/Home/Privacy.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1e0edf8bd581b94155b3a5930d61c0c49d190cd8", @"/Views/Home/Privacy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"26a33c8e4edb9634ea2c8d4523954a7c7aba6d12", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Privacy : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Privacy.cshtml"
  
    ViewData["Title"] = "Privacy Policy";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>");
#nullable restore
#line 4 "E:\3.2\SD - 5\Project-SD\Shajjokori-SD\Shahajjokori\Shahajjokori\Views\Home\Privacy.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>

    <button type=""button"" onclick=""getLocation()"" style=""margin-top:5%"">Get Current Location</button>
    <p id=""demo""></p>

    <script src=""https://code.jquery.com/jquery-2.2.4.min.js""></script>
    <script>
        var x = document.getElementById(""demo"");
        
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition);
            } else {
                x.innerHTML = ""Geolocation is not supported by this browser."";
            }
        }

        function showPosition(position) {
            //x.innerHTML = ""Latitude: "" + position.coords.latitude +
            //    ""<br>Longitude: "" + position.coords.longitude;
            var locAPI = ""http://api.positionstack.com/v1/forward?access_key=2df731ae5c00f8b77f82f634ffb83a0f&query=""+position.coords.latitude+"",""+position.coords.longitude+"""";

            //var locAPI = ""http://maps.googleapis.com/maps/api/geocode/json?latlng="" + position.coords.latitude + "","" + po");
            WriteLiteral(@"sition.coords.longitude + ""&sensor=true"";
            x.innerHTML = locAPI;
        }
    </script>




<script type=""text/javascript"">
    function getLocationConstant() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(onGeoSuccess, onGeoError);
        } else {
            alert(""Your browser or device doesn't support Geolocation"");
        }
    }

    // If we have a successful location update
    function onGeoSuccess(event) {
        document.getElementById(""Latitude"").value = event.coords.latitude;
        document.getElementById(""Longitude"").value = event.coords.longitude;
        document.getElementById(""Position1"").value = event.coords.latitude + "", "" + event.coords.longitude;

    }

    // If something has gone wrong with the geolocation request
    function onGeoError(event) {
        alert(""Error code "" + event.code + "". "" + event.message);
    }
</script>");
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
