using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrabTheScreen
{
    public class Auto
    {
        private String model;
        private String modelDescription;
        private String price;
        private String source;

        public Auto(String model, String modelDescription, String price, String source)
        {
            this.model = model;
            this.modelDescription = modelDescription;
            this.price = price;
            this.source = source;
        }

        public Auto()
        {

        }

        public void setModel(String model)
        {
            this.model = model;
        }

        public String getModel()
        {
            return this.model;
        }

        public void setModelDescription(String modelDescription)
        {
            this.modelDescription = modelDescription;
        }

        public String getModelDescription()
        {
            return this.modelDescription;
        }

        public void setPrice(String price)
        {
            this.price = price;
        }

        public String getPrice()
        {
            return this.price;
        }

        public void setSource(String source)
        {
            this.source = source;
        }

        public String getSource()
        {
            return this.source;
        }
    }


}
