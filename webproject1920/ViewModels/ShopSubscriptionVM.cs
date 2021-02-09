using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;
using webproject1920.ViewModels;

namespace webproject1920.ViewModels
{
    public class ShopSubscriptionVM
    {
        public List<LocationVM> Locations { get; set; }
        public CompetitionVM Competition { get; set; }
        public TCLVM SelectedTcl { get; set; }
        public List<TCLVM> Tcl { get; set; }
        public TeamVM Team { get; set; }

    }
}
