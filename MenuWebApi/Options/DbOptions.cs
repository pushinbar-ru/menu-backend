namespace Pushinbar.API.Options
{
    public class DbOptions : IOptions
    {
        public string OptionsTitle => "Db";
        public string ConnectionString { get; set; }
    }
}