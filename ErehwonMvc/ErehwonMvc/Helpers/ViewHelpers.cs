using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErehwonMvc.Helpers
{
    public static class ViewHelpers
    {
        public static string GetPlotThumbnail(int plotCategoryId)
        {
            var imageDictionary = new Dictionary<int, string>()
            {
                {1, "block1seaside_th.jpg"},
                {2, "block2mountainlake_th.jpg"},
                {3, "block3farmlands_th.jpg"},
                {4, "block4arable_th.jpg"},
                {5, "block5industrial_th.jpg"},
            };

            string imgPath;

            imageDictionary.TryGetValue(plotCategoryId, out imgPath);
            if (!string.IsNullOrWhiteSpace(imgPath))
            {
                imgPath = @"/Images/" + imgPath;
            }
            return imgPath;
        }

        public static string GetPlotCarrousel(int plotCategoryId)
        {
            var imageDictionary = new Dictionary<int, string>()
            {
                {1, "block1seaside.jpg"},
                {2, "block2mountainlake.jpg"},
                {3, "block3farmlands.jpg"},
                {4, "block4arable.jpg"},
                {5, "block5industrial.jpg"},
            };

            string imgPath;

            imageDictionary.TryGetValue(plotCategoryId, out imgPath);
            if (!string.IsNullOrWhiteSpace(imgPath))
            {
                imgPath = @"/Images/" + imgPath;
            }
            return imgPath;
        }
    }
}