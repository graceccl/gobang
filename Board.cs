using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace gobang
{
    class Board
    {
        private static readonly Point NO_MATCH_NODE = new Point(-1, -1);
        private static readonly int OFFSET = 75;
        private static readonly int NODE_RADIUS = 10;
        private static readonly int NODE_DISTANCE = 75;

        private Piece[,] pieces = new Piece[9, 9];
        
        public string checkLine(int x, int y)
        {
            Point nodeId = findTheClosetNode(x, y);
            int nHorizon = 0; int nVertical = 0; int nRight = 0; int nLeft = 0;
            for (int i = -4; i <= 4; i++)
            {
                //Horizon
                if (nodeId.X + i >= 0 && nodeId.X + i < 9 && pieces[nodeId.X + i, nodeId.Y] != null)
                {
                    if (pieces[nodeId.X, nodeId.Y].Name == pieces[nodeId.X + i, nodeId.Y].Name)
                    {
                        nHorizon++;
                        if (nHorizon >= 5)
                            return pieces[nodeId.X, nodeId.Y].Name + " win!!";
                    }
                    else
                        nHorizon = 0;
                }
                ///Vertical
                if (nodeId.Y + i >= 0 && nodeId.Y + i < 9 && pieces[nodeId.X, nodeId.Y + i] != null)
                {
                    if (pieces[nodeId.X, nodeId.Y].Name == pieces[nodeId.X, nodeId.Y + i].Name)
                    {
                        nVertical++;
                        if (nVertical >= 5)
                            return pieces[nodeId.X, nodeId.Y].Name + " win!!";
                    }
                    else
                        nVertical = 0;
                }
                //Right
                if (nodeId.X + i >= 0 && nodeId.X + i < 9 && nodeId.Y + i >= 0 && nodeId.Y + i < 9&&pieces[nodeId.X + i, nodeId.Y + i] != null)
                {
                    if (pieces[nodeId.X, nodeId.Y].Name == pieces[nodeId.X + i, nodeId.Y + i].Name)
                    {
                        nRight++;
                        if (nRight >= 5)
                            return pieces[nodeId.X, nodeId.Y].Name + " win!!";
                    }
                    else
                        nRight = 0;
                }
                //Left
                if (nodeId.X + i >= 0 && nodeId.X + i < 9 && nodeId.Y - i >= 0 && nodeId.Y - i < 9&&pieces[nodeId.X + i, nodeId.Y - i] != null)
                {
                    if (pieces[nodeId.X, nodeId.Y].Name == pieces[nodeId.X + i, nodeId.Y - i].Name)
                    {
                        nLeft++;
                        if (nLeft >= 5)
                            return pieces[nodeId.X, nodeId.Y].Name + " win!!";
                    }
                    else
                        nLeft = 0;
                }
            }
            return null;
        }

        public bool CanBePlaced(int x, int y)
        {
            // 找出最近的節點(Node)
            Point nodeId = findTheClosetNode(x, y);

            // 如果沒有的話，回傳 false
            if (nodeId == NO_MATCH_NODE)
                return false;

            //TODO: 如果有的話，檢查是否已經有棋子存在
            if (pieces[nodeId.X, nodeId.Y] != null)
                return false;

            return true;
        }

        public Piece PlaceAPiece(int x, int y, PieceType type)
        {
            // 找出最近的節點(Node)
            Point nodeId = findTheClosetNode(x, y);

            // 如果沒有的話，回傳 false
            if (nodeId == NO_MATCH_NODE)
                return null;

            //TODO: 如果有的話，檢查是否已經有棋子存在
            if (pieces[nodeId.X, nodeId.Y] != null)
                return null;

            //TODO: 根據type產生對應的棋子
            Point formPos = convertToFormPosition(nodeId);
            if (type == PieceType.BLACK)
            {
                pieces[nodeId.X, nodeId.Y] = new BlackPiece(formPos.X, formPos.Y);
                pieces[nodeId.X, nodeId.Y].Name = "Black";
            }
            else if (type == PieceType.WHITE)
            {
                pieces[nodeId.X, nodeId.Y] = new WhitePiece(formPos.X, formPos.Y);
                pieces[nodeId.X, nodeId.Y].Name = "White";
            }

                return pieces[nodeId.X, nodeId.Y];
        }

        private Point convertToFormPosition(Point nodeId)
        {
            Point formPosition = new Point();
            formPosition.X = nodeId.X * NODE_DISTANCE + OFFSET;
            formPosition.Y = nodeId.Y * NODE_DISTANCE + OFFSET;
            return formPosition;
        }

        private Point findTheClosetNode(int x, int y)//private 命名開頭是小寫， public則是大寫
        {
            int nodeIdX = findTheClosetNode(x);
            if (nodeIdX == -1)
                return NO_MATCH_NODE;

            int nodeIdY = findTheClosetNode(y);
            if (nodeIdY == -1)
                return NO_MATCH_NODE;

            return new Point(nodeIdX, nodeIdY);
        }

        private int findTheClosetNode(int pos)
        {
            if (pos < OFFSET - NODE_RADIUS)
                return -1;

            pos -= OFFSET;
            int quotient = pos / NODE_DISTANCE;
            int remainder = pos % NODE_DISTANCE;

            if (remainder <= NODE_RADIUS)
                return quotient;
            else if (remainder >= NODE_DISTANCE - NODE_RADIUS)
                return quotient + 1;
            else
                return -1;
        }
    }
}
