using System.Linq;

namespace Grasews.Infra.CrossCutting.Helpers
{
    public static class UrlHelper
    {
        public static string ExtractOntologyUrlFromUrl(string url)
        {
            return url.Replace("#", string.Empty).Replace(ExtractTermIdentifierFromUrl(url), string.Empty);
        }

        public static string ExtractTermIdentifierFromUrl(string url)
        {
            string anchorValue = null;

            var strippedUrl = url.Split('#');

            if (strippedUrl.Length > 1)
            {
                anchorValue = strippedUrl[1];
            }
            else
            {
                anchorValue = url.Split('/').LastOrDefault(x => !string.IsNullOrWhiteSpace(x));
            }

            return anchorValue;
        }

        public static string ExtractOntologyNameFromUrl(string url)
        {
            var strippedUrl = url.Split('#');

            if (strippedUrl.Length > 1)
            {
                url = strippedUrl[0];
            }

            url = url.Substring(url.LastIndexOf('/') + 1);

            var index = url.IndexOf('.');

            if (index != -1)
            {
                url = url.Remove(index);
            }

            return url;
        }
    }
}