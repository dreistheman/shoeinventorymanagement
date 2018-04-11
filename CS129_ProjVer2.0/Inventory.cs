using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS129_ProjVer2._0
{
     public class Inventory
    {                                               // Private Class Data Design Pattern
        private int idNo, sSize, quantity;
        private string description, brand, color;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public int SSize
        {
            get { return sSize; }
            set { sSize = value; }
        }

        public int IdNo
        {
            get { return idNo; }
            set { idNo = value; }
        }
        
        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public Inventory()
        {

        }






    }
}
