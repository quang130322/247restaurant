using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res247.Models.Common
{
<<<<<<< HEAD
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
=======
    public class CartItem
    {
        public Food Food { get; set; }

        public int Quantity { get; set; }
>>>>>>> d56a301ef03e1f4789bbbbae208cefd9bba6909e
    }
}
