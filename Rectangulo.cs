using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graficacion
{
    class Rectangulo
    {
        private int x, y;

        public Rectangulo(int x, int y)
        {
            this.x = x;
            this.y = y;

        }
        public int GetX()
        {
            return x;
        }
        public void SetX(int x)
        {
            this.x = x;
        }
        public int GetY()
        {
            return y;
        }
        public void SetY(int y)
        {
            this.y = y;
        }


}
}
