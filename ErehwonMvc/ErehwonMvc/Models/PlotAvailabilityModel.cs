using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErehwonMvc.Models
{
    public class PlotAvailabilityModel
    {
        public int PlotCategoryId { get; set; }
        public string PlotDescription { get; set; }
        public double PurchasedHectares { get; set; }
        public double TotalHectares { get; set; }
    }
}