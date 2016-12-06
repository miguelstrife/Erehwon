using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ErehwonMvc.Models;

namespace ErehwonMvc.ApiController
{
    public class PlotCategoryController : System.Web.Http.ApiController
    {
        // GET: api/PlotCategory
        public List<PlotCategory> Get()
        {
            var dc = new ErehwonDataContext();

            return dc.PlotCategories.ToList();
        }

        // GET: api/PlotCategory/5
        public List<PlotCategory> Get(int id)
        {
            var dc = new ErehwonDataContext();

            return dc.PlotCategories.Where(x => x.PlotCategoryID == id).ToList();
        }

    }
}
