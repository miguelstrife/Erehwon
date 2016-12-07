using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ErehwonMvc.Enums;

namespace ErehwonMvc.Helpers
{
    public  class ViewHelpers
    {
        public  string GetPlotImage(int plotCategoryId, ImageType imageType)
        {
            var imageTypeDictionary = new Dictionary<ImageType, string>()
            {
                {ImageType.Thumbnail, "_th"},
                {ImageType.Carousel, "_cr"}
            };

            var imageDictionary = new Dictionary<int, string>()
            {
                {1, $"block1seaside{imageTypeDictionary[imageType]}.jpg"},
                {2, $"block2mountainlake{imageTypeDictionary[imageType]}.jpg"},
                {3, $"block3farmlands{imageTypeDictionary[imageType]}.jpg"},
                {4, $"block4arable{imageTypeDictionary[imageType]}.jpg"},
                {5, $"block5industrial{imageTypeDictionary[imageType]}.jpg"},
            };

            string imgPath;

            imageDictionary.TryGetValue(plotCategoryId, out imgPath);
            if (!string.IsNullOrWhiteSpace(imgPath))
            {
                imgPath = @"/Images/" + imgPath;
            }
            return imgPath;
        }

        //public  string GetPlotCarrousel(int plotCategoryId)
        //{
        //    var imageDictionary = new Dictionary<int, string>()
        //    {
        //        {1, "block1seaside.jpg"},
        //        {2, "block2mountainlake.jpg"},
        //        {3, "block3farmlands.jpg"},
        //        {4, "block4arable.jpg"},
        //        {5, "block5industrial.jpg"},
        //    };

        //    string imgPath;

        //    imageDictionary.TryGetValue(plotCategoryId, out imgPath);
        //    if (!string.IsNullOrWhiteSpace(imgPath))
        //    {
        //        imgPath = @"/Images/" + imgPath;
        //    }
        //    return imgPath;
        //}
    }
}