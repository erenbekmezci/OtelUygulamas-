namespace teknofest.Models
{
    public class OrderModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telefon { get; set; }


        public CartModel? CartModel { get; set; }
    }
}
