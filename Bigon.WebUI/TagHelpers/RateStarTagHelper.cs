using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bigon.WebUI.TagHelpers
{
    [HtmlTargetElement("rate")]
    public class RateStarTagHelper : TagHelper
    {
        [HtmlAttributeName("value")]
        public double RateValue { get; set; }

        [HtmlAttributeName("product-id")]
        public int ProductId { get; set; }

        [HtmlAttributeName("readonly")]
        public bool Readonly { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.TagMode = TagMode.StartTagAndEndTag;

            string additionalClass = RateValue switch
            {
                var v when v >= 4.8D => "rate-5",
                var v when v > 4D => "rate-half5",
                var v when v >= 3.8D => "rate-4",
                var v when v > 3D => "rate-half4",
                var v when v >= 2.8D => "rate-3",
                var v when v > 2D => "rate-half3",
                var v when v >= 1.8D => "rate-2",
                var v when v > 1D => "rate-half2",
                var v when v >= 0.8D => "rate-1",
                var v when v > 0 => "rate-half1",
                _ => ""
            };

            output.Attributes.Add("class", $"rate {additionalClass}");

            if (this.Readonly)
            {
                output.Content.SetHtmlContent(@"<li></li><li></li><li></li><li></li><li></li>");
            }
            else
            {
                output.Content.SetHtmlContent(@$"<li data-rate='1' data-product-id='{this.ProductId}'></li>
                                             <li data-rate='2' data-product-id='{this.ProductId}'></li>
                                             <li data-rate='3' data-product-id='{this.ProductId}'></li>
                                             <li data-rate='4' data-product-id='{this.ProductId}'></li>
                                             <li data-rate='5' data-product-id='{this.ProductId}'></li>");
            }
        }
    }
}
