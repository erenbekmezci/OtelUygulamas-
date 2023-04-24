using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public enum EnumPaymentType
    {
        CreditCard = 0,
        Eft = 1
    }

    public enum EnumOrderState
    {
        alındı = 0,
        hazırlanıyor = 1,
        tamamlandı = 2
    }
}
