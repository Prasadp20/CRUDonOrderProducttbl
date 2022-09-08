using CRUD.on.Order.Product.Model;

namespace CRUD.on.Order.Product.DTO
{
    public class CreateOrderDto
    {
        public string? custName { get; set; }
        public string? address { get; set; }
        public DateTime ordDate { get; set; }
        public double grandTotal { get; set; }
        public List<tblProduct> prod { get; set; } = new List<tblProduct>();
    }
}
