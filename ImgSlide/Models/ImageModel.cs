using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImgSlide.Models
{
    public class ImageModel { 
        public string imageName { get; set; }
        public string subFolder { get; set; }
        public int SlideTime { get; set; }
    }
}