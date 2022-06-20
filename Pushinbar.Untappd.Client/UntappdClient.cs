using System.ComponentModel;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pushinbar.Untappd.Client
{
    public class UntappdClient
    {
        private const string DescriptionPattern =
            "<div class=\"beer-descrption-read-less\" style=\"display: none;\">(.*?)<a href=\"#\" class=\"read-less track-click\" data-track=\"beer\" data-href=\":info/readless\">Show Less</a></div>";
        private const string AlcPattern = "<p class=\"abv\">(.*?)</p>";
        private const string SubcategoryPattern = "<p class=\"style\">(.*?)</p>";
        private const string IbuPattern = "<p class=\"ibu\">(.*?)</p>";
        private const string BreweryPattern = "<p class=\"brewery\"><a href=\"/onthebones\">(.*?)</a></p>";


        public static async Task<BeerInfo> GetBeerInformationByUrlAsync(string url)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var strContent = Regex.Replace(content, @"\n", "");

            var result = new BeerInfo()
            {
                UntappdUrl = url,
                Description = GetRegexValueFromContent(strContent, DescriptionPattern),
                Alc = GetRegexValueFromContent(strContent, AlcPattern),
                Subcategory = GetRegexValueFromContent(strContent, SubcategoryPattern),
                IBU = GetRegexValueFromContent(strContent, IbuPattern),
                Brewery = GetRegexValueFromContent(strContent, BreweryPattern)
            };

            return result;
        }

        private static string GetRegexValueFromContent(string content, string pattern)
        {
            var reg = new Regex(pattern);
            var pre = reg.Match(content);
            return pre.Groups.Count > 1 ? pre.Groups[1].Value : null;
        } 
    }
}