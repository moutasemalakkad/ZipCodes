#pragma checksum "D:\AOngoingwork\makinc1\Zipcodes_ASPNET\Zipcodes_ASPNET\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a3db5a620618e58fa63f6e14122850f26f330a80"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "D:\AOngoingwork\makinc1\Zipcodes_ASPNET\Zipcodes_ASPNET\Views\_ViewImports.cshtml"
using Zipcodes_ASPNET;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\AOngoingwork\makinc1\Zipcodes_ASPNET\Zipcodes_ASPNET\Views\_ViewImports.cshtml"
using Zipcodes_ASPNET.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a3db5a620618e58fa63f6e14122850f26f330a80", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4dfa10e5a79859f31f1b67b5b201a9e99e954485", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\AOngoingwork\makinc1\Zipcodes_ASPNET\Zipcodes_ASPNET\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral(@"
<div class=""container"">

    <h2>Find nearest zipcodes</h2>

    <div class=""form-group"">
        <label for=""zipcode"">Zipcode:</label>
        <input type=""text"" class=""form-control"" id=""zipcode"" placeholder=""Enter zip code"" name=""zipcode"">
    </div>
    <div class=""form-group"">
        <label for=""pwd"">Radius In Miles:</label>
        <input type=""text"" class=""form-control"" id=""radiusInMiles"" placeholder=""Enter radius in miles"" name=""radiusInMiles"">
    </div>

    <button id=""clickFetchDataId"" class=""btn btn-primary"">Fetch ZipCodes</button>


    <table class=""table"" style=""padding-top: 50px; display: none;"" id=""tableNearestZipCodes"">
        <thead>
            <tr>
                <th>User zipcode</th>
                <th>Nearest zipcode</th>
                <th>Distance</th>
            </tr>
        </thead>
        <tbody id=""tableBodyId"">
        </tbody>
    </table>
</div>
");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"

    <script>

    $(document).ready(function () {
        $('#clickFetchDataId').click(function () {

            var zipCodes = $('#zipcode').val();
            var radiusInMiles = $('#radiusInMiles').val();
            GetNearestZipcodes(zipCodes, radiusInMiles);
        });
    });

    function GetNearestZipcodes(zipCode, radiusInMiles)
    {
        $.get('");
#nullable restore
#line 52 "D:\AOngoingwork\makinc1\Zipcodes_ASPNET\Zipcodes_ASPNET\Views\Home\Index.cshtml"
          Write(Url.Action("GetZipCodesWithInRangeRadius" , "Home"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"', { 'zipCode': zipCode, 'radiusInMiles': radiusInMiles }, function (data, status) {

            alert(""Data: "" + data + ""\nStatus: "" + status);
            if (data == undefined || data.length == 0) {
                alert('No Data found');
            }
            else {
                var htmlRow = '<tr>';
                htmlRow += '<td>' + data.zip1 + '</td>';
                htmlRow += '<td>' + data.zip2 + '</td>';
                htmlRow += '<td>' + data.mi_to_zcta5 + '</td>'
                htmlRow += '</tr>';

                $('#tableBodyId').html(htmlRow);
                $('#tableNearestZipCodes').fadeIn();
            }
        });
    }

    </script>
");
            }
            );
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