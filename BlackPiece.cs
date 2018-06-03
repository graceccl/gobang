using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gobang
{
    class BlackPiece : Piece
    {
        public BlackPiece(int x, int y) : base(x, y)//基底類別
        {
            this.Image = Properties.Resources.black;
        }
    }
}
