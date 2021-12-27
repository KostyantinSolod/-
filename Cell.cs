using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zmeika
{
    class Cell
    {
        public static Cell[] pole = new Cell[81];
        public int ryh()
        {
            int len = 0;
            if (this is Drabina) { len = (this as Drabina).ryh(); }
            else if (this is ZmeyaCell) { len = (this as ZmeyaCell).ryh(); }
            return len;
        }
        internal int len;
        public int ryh(int start) { return (-1); }
        public Cell() { }
        static public void Inicialization()
        {

            ((pole[2] = new Drabina()) as Drabina).generate(new int[] { 19, 22 });
            ((pole[4] = new Drabina()) as Drabina).generate(new int[] { 17, 24, 37 });
            ((pole[10] = new Drabina()) as Drabina).generate(new int[] { 11, 30 });
            ((pole[13] = new Drabina()) as Drabina).generate(new int[] { 28, 33 });
            ((pole[39] = new Drabina()) as Drabina).generate(new int[] { 42, 59, 62 });
            ((pole[51] = new Drabina()) as Drabina).generate(new int[] { 70, 71 });
            ((pole[58] = new Drabina()) as Drabina).generate(new int[] { 63, 78 });
            ((pole[77] = new ZmeyaCell()) as ZmeyaCell).generate(new int[] {77, 64, 65, 56 });
            ((pole[73] = new ZmeyaCell()) as ZmeyaCell).generate(new int[] {73, 68, 67, 54 });
            ((pole[61] = new ZmeyaCell()) as ZmeyaCell).generate(new int[] {61, 60, 41, 40 });
            ((pole[52] = new ZmeyaCell()) as ZmeyaCell).generate(new int[] {52, 49, 50, 31 });
            ((pole[47] = new ZmeyaCell()) as ZmeyaCell).generate(new int[] {47, 34, 35, 26 });
            ((pole[32] = new ZmeyaCell()) as ZmeyaCell).generate(new int[] {32, 29, 12, 9 });
            ((pole[25] = new ZmeyaCell()) as ZmeyaCell).generate(new int[] {25, 16, 15, 6 });
            for (int i = 0; i < pole.Length; i++)
            {
                if (pole[i] == null) { pole[i] = new Cell(); }
            }
        }
        static public void Write()
        {
            var q = pole;
            for (int i = 7; i >= 0; i--)
            {
                int start = i % 2 == 0 ? 1 : 10;
                int stop = i % 2 == 0 ? 11 : 0;
                int step = i % 2 == 0 ? 1 : -1;
                for (int j = start; ; j += step)
                {
                    if (j == stop) { break; }
                    if (pole[i * 10 + j] is ZmeyaCell) { Console.Write("-1" + "\t"); }
                    if (pole[i * 10 + j] is Drabina) { Console.Write("+1" + "\t"); }
                    else { Console.Write(0 + "\t"); }
                }
                Console.WriteLine();
            }
        }
    }
    class Drabina : Cell
    {
         public int[] cords;
        public void generate(int[] road)
        {
            var q = pole;
            cords = road;
            this.len = cords.Length - 1;
        }

        new public int ryh()
        {
            int len = this.len;
            for (int i = 0; i < len + 1; i++)
            {
                Zmeya.cords.Add( this.cords[len - i]);
            }
            return cords.Length;
        }
    }
    class ZmeyaCell : Cell
    {
        public int[] cords = new int[0];
        int down;
        public void generate(int[] road)
        {
            for (int i = road.Length - 1; i >= 0; i--)
            {
                int[] newroad = road;
                Array.Reverse(newroad);
                Array.Resize(ref newroad, road.Length - i);
                ((pole[road[road.Length - i - 1]]=new ZmeyaCell()) as ZmeyaCell).cords = newroad;
                down = this.cords.Length;
                Array.Reverse(road);
            }
        }
        new public int ryh()
        {
            int len = this.len;
            for (int i = 0; i < this.cords.Length; i++)
            {
                Zmeya.cords.Add (this.cords[i]);
            }
            return cords.Length == 1 || cords.Length == 0 ? len : cords.Length - 1;
        }
    }

}
