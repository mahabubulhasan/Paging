using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace Uzzal.Paging.TagHelpers
{
    public class PageLinksTagHelper : TagHelper
    {
        private const string RouteAttributeName = "asp-route";
        private const string RouteValuesDictionaryName = "asp-all-route-data";
        private const string RouteValuesPrefix = "asp-route-";
        private IDictionary<string, string> _routeValues;
        private readonly IHtmlGenerator htmlGenerator;

        public PageLinksTagHelper(IHtmlGenerator htmlGenerator)
        {
            this.htmlGenerator = htmlGenerator;
        }

        [ViewContext]
        public ViewContext ViewContext { set; get; }

        [HtmlAttributeName(RouteAttributeName)]
        public string Route { get; set; }

        [HtmlAttributeName(RouteValuesDictionaryName, DictionaryAttributePrefix = RouteValuesPrefix)]
        public IDictionary<string, string> RouteValues
        {
            get
            {
                if (_routeValues == null)
                {
                    _routeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }

                return _routeValues;
            }
            set
            {
                _routeValues = value;
            }
        }

        public string Action { get; set; }
        public string Controller { get; set; }
        public string SpacerText { get; set; } = "...";
        public string SpacerStyle { get; set; } = "p-0 mr-1 btn btn-default btn-sm";
        public string DefaultStyle { get; set; } = "mr-1 btn btn-outline-primary btn-default btn-sm";
        public string ActiveStyle { get; set; } = "font-weight-bold";

        public PagingContext PagingContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var builder = new HtmlContentBuilder();

            builder.AppendHtml(Anchor("Previous", (PagingContext.PageIndex - 1).ToString(), !PagingContext.HasPrevious));
            builder = GenerateSpan(builder);
            builder.AppendHtml(Anchor("Next", (PagingContext.PageIndex + 1).ToString(), !PagingContext.HasNext));


            output.TagName = "div";
            output.Content.SetHtmlContent(builder);
        }

        private HtmlContentBuilder GenerateSpan(HtmlContentBuilder html)
        {
            int lastIndex = 0;
            foreach (var i in PagingContext.PageLinks.GetSpanLinks())
            {
                if (i - lastIndex > 1)
                {
                    html.AppendHtml($"<a class=\"disabled {SpacerStyle}\">{SpacerText}</>");
                }
                lastIndex = i;
                var style = (PagingContext.PageIndex == i) ? $" disabled {ActiveStyle}" : "";
                var anchor = Anchor(i.ToString(), i.ToString(), false, style);
                html.AppendHtml(anchor);
            }

            return html;
        }

        private TagBuilder Anchor(string linkText, string page, bool disable = false, string style = "")
        {
            RouteValueDictionary routeValues = new RouteValueDictionary(_routeValues)
            {
                { "page", page }
            };

            var disabledStyle = disable ? " disabled" : "";

            return htmlGenerator.GenerateActionLink(
                ViewContext,
                linkText: linkText,
                actionName: Action,
                controllerName: Controller,
                fragment: null,
                hostname: null,
                htmlAttributes: new Dictionary<string, object> { { "class", $"{DefaultStyle}{disabledStyle}{style}" } },
                protocol: null,
                routeValues: routeValues
            );
        }
    }
}
