using System;
using System.Collections.Generic;

namespace webproject1920.ViewModels
{
    public class ShoppingCartVM
    {
        public List<CartVM> Cart { get; set; }

        public double TotalPrice { get; set; } = 0.00;
    }

    public class CartVM
    {
        public DateTime DateAdded { get; set; } = DateTime.Now;

        public int? TeamSubscription { get; set; }
        public int? ForTeamId { get; set; }

        public int? MatchTicket { get; set; }
        public int? ForMatchId { get; set; }

        public int Count { get; set; } = 1;
    }
}
