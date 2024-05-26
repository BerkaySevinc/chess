using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BekoS.Chess;

public class Bishop : Piece
{
    public Bishop(PieceColor color) : this(color, default) { }
    public Bishop(PieceColor color, Theme theme) : base(color, theme) { }


    protected override Dictionary<Theme, Dictionary<PieceColor, string>> Base64Bitmaps { get; } = Base64BitmapConst;

    private static readonly Dictionary<Theme, Dictionary<PieceColor, string>> Base64BitmapConst = new()
    {
        {
            Theme.Neo,
            new Dictionary<PieceColor, string>
            {
                {PieceColor.White, "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"},
                {PieceColor.Black, "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"}
            }
        }
    };

    public override Move[] GetLegalMoves()
    {
        ArgumentNullException.ThrowIfNull(Square);

        Board board = Square.Board;

        var (letterIndex, numberIndex) = Square.GetCoordinates();

        // Moves
        var legalSquares = new List<Square>();

        // Letter+ Number+
        for (int i = 1; ; i++)
        {
            Square? square = board[letterIndex + i, numberIndex + i];

            if (square is null) break;

            if (square.Piece is null || square.Piece.Color != Color)
                legalSquares.Add(square);

            if (square.Piece is not null) break;
        }
        // Letter- Number+
        for (int i = 1; ; i++)
        {
            Square? square = board[letterIndex - i, numberIndex + i];

            if (square is null) break;

            if (square.Piece is null || square.Piece.Color != Color)
                legalSquares.Add(square);

            if (square.Piece is not null) break;
        }
        // Letter+ Number-
        for (int i = 1; ; i++)
        {
            Square? square = board[letterIndex + i, numberIndex - i];

            if (square is null) break;

            if (square.Piece is null || square.Piece.Color != Color)
                legalSquares.Add(square);

            if (square.Piece is not null) break;
        }
        // Letter- Number-
        for (int i = 1; ; i++)
        {
            Square? square = board[letterIndex - i, numberIndex - i];

            if (square is null) break;

            if (square.Piece is null || square.Piece.Color != Color)
                legalSquares.Add(square);

            if (square.Piece is not null) break;
        }


        return legalSquares.Select(s => new Move(Square, s)).ToArray();
    }

}
