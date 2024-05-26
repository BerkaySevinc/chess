using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BekoS.Chess;


public enum PieceColor
{
    White,
    Black
}

public enum SquareColor
{
    White,
    Black
}

public enum MoveType
{
    Move,
    Take,
    EnPassant,
    LongCastle,
    ShortCastle
}

public enum Theme
{
    Neo
}
