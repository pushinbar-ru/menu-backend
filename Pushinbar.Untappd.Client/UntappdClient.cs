using System;
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
        private const string BreweryPattern = "<p class=\"brewery\"><a href=\"(.*?)\">(.*?)</a></p>";
        private const string NamePattern = "<div class=\"name\"><h1>(.*?)</h1>";


        public static async Task<BeerInfo> GetBeerInformationByUrlAsync(string url)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var strContent = Regex.Replace(content, @"\n", "");
            var ibu = GetRegexValueFromContent(strContent, IbuPattern);
            ibu = ibu?.Replace("IBU", "")?.Trim();
            var alc = GetRegexValueFromContent(strContent, AlcPattern);
            alc = alc?.Replace("% ABV", "")?.Trim();
            var description = GetRegexValueFromContent(strContent, DescriptionPattern);
            description = description.Replace("<br />", "");

            var result = new BeerInfo()
            {
                UntappdUrl = url,
                Description = description,
                Name = GetRegexValueFromContent(strContent, NamePattern),
                Alc = alc,
                Subcategory = GetRegexValueFromContent(strContent, SubcategoryPattern),
                IBU = ibu is "No" ? "0" : ibu,
                Brewery = GetRegexValueFromContent(strContent, BreweryPattern, 2)
            };

            return result;
        }

        private static string GetRegexValueFromContent(string content, string pattern, int index = 1)
        {
            var reg = new Regex(pattern);
            var pre = reg.Match(content);
            return pre.Groups.Count > index ? pre.Groups[index].Value : null;
        } 
    }
}