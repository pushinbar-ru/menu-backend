namespace Pushinbar.API.Options
{
    public class KonturMarketOptions : IOptions
    {
        public string OptionsTitle => "KonturMarket";
        public string ApiKey { get; set; }
        public string ShopId { get; set; }
    }
}