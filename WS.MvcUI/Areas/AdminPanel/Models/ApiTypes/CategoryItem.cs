namespace WS.MvcUI.Areas.AdminPanel.Models.ApiTypes
{
    public class CategoryItem
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public string Description{ get; set; }

        public string Base64Picture { get; set; }
        public string PicturePath { get; set; }


    }
}
