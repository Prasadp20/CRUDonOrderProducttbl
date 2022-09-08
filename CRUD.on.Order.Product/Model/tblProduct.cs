namespace CRUD.on.Order.Product.Model
{
    public class tblProduct
    {
		/*	prodId INT IDENTITY(101,1),
			prodName VARCHAR(250),
			prodPrice NUMERIC(10,2),
			prodQty INT,
			totalAmount NUMERIC(10,2),
			ordId INT,	*/

		public int prodId { get; set; }
		public string? prodName { get; set; }
		public double prodPrice { get; set; }
		public int prodQty { get; set; }
		public double totalAmount { get; set; }
		public int oId { get; set; }

	}
}
