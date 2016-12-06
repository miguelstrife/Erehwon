using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErehwonMvc.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public List<PlotDetailModel> Plots { get; set; }
    }
}