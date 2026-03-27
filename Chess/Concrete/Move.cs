using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BekoS.Chess;

public class Move : IMove, IEquatable<Move>
{
    public Board Board { get; }

    public Square From { get; }
    public Square To { get; }

    public bool IsLegalNow => 
        From.Piece is not null 
        && Board.Turn == From.Piece.Color 
        && From.Piece.GetLegalMoves().Any(m => m == this);

    public Move(Square from, Square to)
    {
        ArgumentNullException.ThrowIfNull(from);
        ArgumentNullException.ThrowIfNull(to);

        Board = from.Board;
        From = from;
        To = to;
    }

    public bool TryPlay(out PlayedMove? playedMove) => Board.TryPlay(this, out playedMove);

    public static bool operator ==(Move obj1, Move obj2)
    {
        if (ReferenceEquals(obj1, obj2)) return true;

        if (obj1 is null) return false;
        if (obj2 is null) return false;

        return obj1.Equals(obj2);
    }
    public static bool operator !=(Move obj1, Move obj2) => !(obj1 == obj2);

    public bool Equals(Move? other)
    {
        if (other is null) return false;

        if (ReferenceEquals(this, other)) return true;

        return From == other.From && To == other.To;
    }
    public override bool Equals(object? obj) => obj is Move && Equals(obj as Move);

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = From.GetHashCode();
            hashCode = (hashCode * 397) ^ To.GetHashCode();

            return hashCode;
        }
    }

}
