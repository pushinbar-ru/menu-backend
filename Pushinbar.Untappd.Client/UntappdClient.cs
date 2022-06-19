using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pushinbar.Untappd.Client
{
    public class UntappdClient
    {
        public static async Task<string> GetBeerInformationByUrlAsync(string url)
        {
            string? result = null;
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var reg = new Regex("<div class=\"beer-descrption-read-less\" style=\"display: none;\">(.*?)</div>");
            var pre = reg.Match(content);
            result = pre.Value;

            return result;
        }
    }
}