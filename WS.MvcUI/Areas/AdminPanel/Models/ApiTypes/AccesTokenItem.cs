namespace WS.MvcUI.Areas.AdminPanel.Models.ApiTypes
{
    public class AccesTokenItem
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public List<string> Claims { get; set; }

    }
}
