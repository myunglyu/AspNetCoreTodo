using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.CodeAnalysis.Differencing;

namespace AspNetCoreTodoTagHelpers.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";    // Replaces <email> with <a> tag
        }
    }
    public class ListTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";    // Replaces <email> with <a> tag
        }
    }

    [HtmlTargetElement(Attributes ="edit-form")]
    public class EditFormTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
            {
                output.Attributes.Add("style", "display: none");
            }
    }

    [HtmlTargetElement(Attributes = "default-time")]
    public class EnterTimeTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            DateTime defaultTime = DateTime.Now.AddDays(3);
            output.Attributes.SetAttribute("value", defaultTime.ToString("yyyy-MM-dd hh:mm"));
        }
    }

    [HtmlTargetElement(Attributes = "due-warning")]
    public class DueWarningTagHelper : TagHelper{
        public DateTimeOffset DueWarning { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output){
            if (DueWarning <= DateTime.Now)
            {
                output.AddClass("bg-danger", HtmlEncoder.Default);
                output.AddClass("fw-bold", HtmlEncoder.Default);
            }
            else if (DueWarning <= DateTime.Now.AddDays(+1))
            {
                output.AddClass("bg-warning", HtmlEncoder.Default);
                output.AddClass("fw-bold", HtmlEncoder.Default);
            }
        }
    }
}