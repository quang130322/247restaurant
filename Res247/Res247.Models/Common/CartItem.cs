using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res247.Models.Common
{
    class CartItem
    {
        public Food Food { get; set; }
        public int Quantity { get; set; }
        public int DonGia { get; set; }
        public int ThanhTien
        {
            get
            {
                return Quantity * DonGia;
            }
        }
    }
}
