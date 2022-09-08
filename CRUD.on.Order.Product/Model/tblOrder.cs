namespace CRUD.on.Order.Product.Model
{
    public class tblOrder
    {
        /*	oId INT IDENTITY(1,1) PRIMARY KEY,
	        custName VARCHAR(250),
	        address VARCHAR(500),
	        ordDate DATE,
	        grandTotal NUMERIC(10,2)    */

        public int oId { get; set; }
        public string? custName { get; set; }
        public string? address { get; set; }
        public DateTime ordDate { get; set; }
        public double grandTotal { get; set; }
        public List<tblProduct> prod { get; set; } = new List<tblProduct>();
    }
}
