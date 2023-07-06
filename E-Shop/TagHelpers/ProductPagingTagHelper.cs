using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace E_Shop.TagHelpers
{
    [HtmlTargetElement("product2-list-pager")]
    public class ProductPagingTagHelper:TagHelper
    {

        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; }

        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }

        [HtmlAttributeName("current-category")]
        public int CurrentCategory { get; set; }

        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }

        //sıra taghelper ı oluşturmada:
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "div";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ul class='pagination pagination-lg'>");
            for (int i = 1; i <= PageCount; i++)
            {


                //stringBuilder.AppendFormat("<li class='{0}'>", i == CurrentPage ? "page-item active" : "page-item");
                var css = "";
                if (i == CurrentPage)
                {
                    css = "page-item active";
                }
                else
                {
                    css = "";
                }
                stringBuilder.AppendFormat("<li class='{0}'>", css);
                stringBuilder.AppendFormat("<a class='page-link' href='/home2/index?page={0}&category={1}'> {2}</a>", i, CurrentCategory, i);
                stringBuilder.Append("</li>");
            }
            //divin içeriği için html contenti oluştur.onu da stringBuilder dan al dedik.
            output.Content.SetHtmlContent(stringBuilder.ToString());

            base.Process(context, output);
        }


    }
}
