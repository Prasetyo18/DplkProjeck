using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace DPLKCORE.Framework
{
    public class AssetManager
    {
        public List<string> _scriptUrls { get; set; }
        public List<string> _cssUrls { get; set; }

        public void SetScripts(List<string> scriptUrls)
        {
            _scriptUrls = scriptUrls;
        }

        public void SetCss(List<string> cssUrls)
        {
            _cssUrls = cssUrls;
        }

        public void RegisterScript(HtmlHead header, Func<string,string> urlResolver)
        {
            foreach (var url in _scriptUrls)
            {
                var resolvedUrl = urlResolver(url);
                var script = new HtmlGenericControl("script");
                script.Attributes["src"] = resolvedUrl;
                script.Attributes["type"] = "text/javascript";
                header.Controls.Add(script);
            }

        }

        public void RegisterCss(HtmlHead header, Func<string, string> urlResolver)
        {
            foreach (var url in _cssUrls)
            {
                var resolvedUrl = urlResolver(url);
                var link = new HtmlLink();
                link.Attributes["rel"] = "stylesheet";
                link.Attributes["href"] = resolvedUrl;
                header.Controls.Add(link);
            }

        }

    }
}