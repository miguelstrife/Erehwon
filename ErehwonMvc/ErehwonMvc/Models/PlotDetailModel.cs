using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErehwonMvc.Models
{
    public class PlotDetailModel
    {
        public string PlotName { get; set; }
        public string PlotDescription { get; set; }
        public double Hectares { get; set; }
        public int PlotCategoryId { get; set; }
        public double PricePaidPerHectare { get; set; }

    }
}