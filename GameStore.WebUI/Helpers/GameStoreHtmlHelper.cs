using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Helpers
{
    public static class GameStoreHtmlHelper
    {
        public static MvcHtmlString SelectListMultiple(this HtmlHelper _this, string name, IEnumerable<SelectListItem> items)
        {
            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("multiple", "multiple");
            select.MergeAttribute("name", name);
            foreach (var item in items)
            {
                var option = new TagBuilder("option");
                option.MergeAttribute("value", item.Value);
                if (item.Selected)
                    option.MergeAttribute("selected","selected");
                option.SetInnerText(item.Text);
                select.InnerHtml += option.ToString();
            }
            return new MvcHtmlString(select.ToString());
        }
    }
}