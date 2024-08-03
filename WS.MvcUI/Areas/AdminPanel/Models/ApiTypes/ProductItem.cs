namespace WS.MvcUI.Areas.AdminPanel.Models.ApiTypes
{
    public class ProductItem
    {

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public short UnitStock { get; set; }

        public CategoryItem Category{ get; set; }


    }
}
