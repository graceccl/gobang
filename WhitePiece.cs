using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gobang
{
    class WhitePiece :Piece
    {
        public WhitePiece(int x, int y) : base(x, y)
        {
            this.Image = Properties.Resources.white;
        }
    }
}
