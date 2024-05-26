using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BekoS.Chess;

public class PlayedMove : IMove, IEquatable<PlayedMove>
{
    public Board Board { get; }

    public Square From { get; }
    public Square To { get; }

    public Piece Piece { get; }

    public MoveType? Type { get; }


    public PlayedMove(Square from, Square to, Piece piece)
    {
        ArgumentNullException.ThrowIfNull(from);
        ArgumentNullException.ThrowIfNull(to);
        ArgumentNullException.ThrowIfNull(piece);

        Board = from.Board;

        From = from;
        To = to;

        Piece = piece;

        var (fromLetterIndex, _) = from.GetCoordinates();
        var (toLetterIndex, _) = to.GetCoordinates();

        Type =
            To.Piece is not null
            ? MoveType.Take
                : Piece is Pawn && fromLetterIndex != toLetterIndex
                ? MoveType.EnPassant
                    : Piece is King && toLetterIndex == fromLetterIndex - 2
                    ? MoveType.LongCastle
                        : Piece is King && toLetterIndex == fromLetterIndex + 2
                        ? MoveType.ShortCastle
                            : MoveType.Move;
    }


    public static bool operator ==(PlayedMove obj1, PlayedMove obj2)
    {
        if (ReferenceEquals(obj1, obj2)) return true;

        if (obj1 is null) return false;
        if (obj2 is null) return false;

        return obj1.Equals(obj2);
    }
    public static bool operator !=(PlayedMove left, PlayedMove right) => !(left == right);

    public bool Equals(PlayedMove? other)
    {
        if (other is null) return false;

        if (ReferenceEquals(this, other)) return true;

        return From == other.From && To == other.To && Piece == other.Piece;
    }
    public override bool Equals(object? obj) => obj is PlayedMove && Equals(obj as PlayedMove);

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = From.GetHashCode();
            hashCode = (hashCode * 397) ^ To.GetHashCode();
            hashCode = (hashCode * 397) ^ Piece.GetHashCode();

            return hashCode;
        }
    }
}
