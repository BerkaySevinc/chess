using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BekoS.Chess;

public class Rook : Piece, IKeepEverPlayed
{
    public bool IsEverPlayed { get; set; }

    public Rook(PieceColor color) : this(color, default) { }
    public Rook(PieceColor color, Theme theme) : base(color, theme) { }


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

        // Number+
        for (int i = 1; ; i++)
        {
            Square? square = board[letterIndex, numberIndex + i];

            if (square is null) break;

            if (square.Piece is null || square.Piece.Color != Color)
                legalSquares.Add(square);

            if (square.Piece is not null) break;
        }
        // Number-
        for (int i = -1; ; i--)
        {
            Square? square = board[letterIndex, numberIndex + i];

            if (square is null) break;

            if (square.Piece is null || square.Piece.Color != Color)
                legalSquares.Add(square);

            if (square.Piece is not null) break;
        }
        // Letter+
        for (int i = 1; ; i++)
        {
            Square? square = board[letterIndex + i, numberIndex];

            if (square is null) break;

            if (square.Piece is null || square.Piece.Color != Color)
                legalSquares.Add(square);

            if (square.Piece is not null) break;
        }
        // Letter-
        for (int i = -1; ; i--)
        {
            Square? square = board[letterIndex + i, numberIndex];

            if (square is null) break;

            if (square.Piece is null || square.Piece.Color != Color)
                legalSquares.Add(square);

            if (square.Piece is not null) break;
        }


        return legalSquares.Select(s => new Move(Square, s)).ToArray();

    }
}
