using AutoDealer.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;

namespace AutoDealer.Web.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {

        private readonly IUrlHelperFactory _urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            _urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public CarsListViewModel PageModel { get; set; }

        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder divBuilder = new TagBuilder("div");

            if (PageModel.PagingInfo.HasPreviousPage)
            {
                int refPage = PageModel.PagingInfo.CurrentPage - 1;
                string value = "<";

                TagBuilder prevBuilder = CreateLink(refPage, value);
                divBuilder.InnerHtml.AppendHtml(prevBuilder);
            }

            for (int i = 1; i <= PageModel.PagingInfo.TotalPages; i++)
            {
                TagBuilder aBuilder = CreateLink(i, i.ToString());
                divBuilder.InnerHtml.AppendHtml(aBuilder);
            }

            if (PageModel.PagingInfo.HasNextPage)
            {
                int refPage = PageModel.PagingInfo.CurrentPage + 1;
                string value = ">";

                TagBuilder nextBuilder = CreateLink(refPage, value);
                divBuilder.InnerHtml.AppendHtml(nextBuilder);
            }
            output.Content.AppendHtml(divBuilder.InnerHtml);
        }

        private TagBuilder CreateLink(int number, string value)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

            TagBuilder aBuilder = new TagBuilder("a");
            PageUrlValues["currentPage"] = number;
            //PageUrlValues["carFilter"] = PageModel.CarFilter;
            aBuilder.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            if (PageClassesEnabled)
            {
                aBuilder.AddCssClass(PageClass);
                aBuilder.AddCssClass(number == PageModel.PagingInfo.CurrentPage
                ? PageClassSelected : PageClassNormal);
            }
            
            string strId = "page_" + number;
            aBuilder.InnerHtml.Append(value);
            aBuilder.Attributes.Add("id", strId);

            return aBuilder;
        }
    }
}
