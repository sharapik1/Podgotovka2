using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podgotovka2
{
    public  class Product
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int ProductTypeID { get; set; }
        public string ArticleNumber { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int ProductionPersonCount { get; set; }
        public int ProductionWorkshopNumber { get; set; }
        public decimal MinCostForAgent { get; set; }
       
        public ProductType CurrentProductType { get; set; }


        public Uri ImagePreview
        {
            get
            {
                var imageName = Environment.CurrentDirectory + (Image ?? "");
                return System.IO.File.Exists(imageName) ? new Uri(imageName) : null;
            }
        }
        public string TypeAndName
        {
            get
            {
                return CurrentProductType + " | " + Title;
            }
        }
        




    }
}
