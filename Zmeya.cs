using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Zmeika
{
    class Zmeya
    {
        static public int pozition = 1;
        static public List<int> cords = new List<int>();
        static public int brosoc = 0;
        static public Point point = new Point();       
        public int BrosocCubica()
        {
            Random rand = new Random();
            brosoc = rand.Next(1, 7);
            return brosoc;
        }
        public void ryh()
        {
                int brosok = BrosocCubica();
            pozition += brosok;
            if (pozition >= 80) { cords.Add(80); }
            else
            {
                int len = Cell.pole[pozition].ryh();
                for (int i = 0; i < brosok; i++)
                {
                    cords.Add(pozition - i);
                }
                Zmeya.pozition = cords[0];
            }
        }
    }
}
