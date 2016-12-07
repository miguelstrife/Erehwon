using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErehwonMvc.Models
{
    [MetadataType(typeof(PlotMetaData))]
    public partial class Plot
    {

    }

    public class PlotMetaData
    {

        public int PlotID { get; set; }

        [Required]
        [DisplayName("Plot Name")]
        public string PlotName { get; set; }

        public string PlotDescription { get; set; }
        [Required]
        [DisplayName("Plot Category")]
        public int PlotCategoryID { get; set; }

        [Required]
        [DisplayName("Number of Hectares")]
        [Range(0.0, 500.0)]
        public double TotalHectares { get; set; }

        [Required]
        [DisplayName("Order Number")]
        public int OrderID { get; set; }
    }
}