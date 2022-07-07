using System.Collections.Generic;
using System.Linq;

namespace Basket.Api.Entities
{
    public class ShoppingCard
    {
        public ShoppingCard()
        {

        }

        public ShoppingCard(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; set; }
        public List<ShoppingCardItem> ShoppingCardItems { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                if (ShoppingCardItems != null && ShoppingCardItems.Any())
                {
                    foreach (var shoppingCardItem in ShoppingCardItems)
                    {
                        totalPrice += shoppingCardItem.Price * shoppingCardItem.Quantity;
                    }
                }


                return totalPrice;
            }
        }
    }
}
