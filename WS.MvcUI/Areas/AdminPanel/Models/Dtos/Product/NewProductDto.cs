using WS.MvcUI.Areas.AdminPanel.Models.ApiTypes;

namespace WS.MvcUI.Areas.AdminPanel.Models.Dtos.Product
{
    public class NewProductDto
    {

  

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public short UnitStock { get; set; }

        public int CategoryId { get; set; }
    }
}
