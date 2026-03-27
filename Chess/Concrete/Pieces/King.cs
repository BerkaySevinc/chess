using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BekoS.Chess;

public class King : Piece, IKeepEverPlayed
{
    public bool IsEverPlayed { get; set; }
    public bool IsChecked { get; set; }

    public King(PieceColor color) : this(color, default) { }
    public King(PieceColor color, Theme theme) : base(color, theme) { }


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
        var squares = board.Squares;

        PieceColor opponentColor = Color == PieceColor.White ? PieceColor.Black : PieceColor.White;

        var (letterIndex, numberIndex) = Square.GetCoordinates();

        // Moves
        var legalSquares = new List<Square?>()
        {
            board[letterIndex, numberIndex + 1],
            board[letterIndex, numberIndex - 1],
            board[letterIndex + 1, numberIndex],
            board[letterIndex - 1, numberIndex],
            board[letterIndex + 1, numberIndex + 1],
            board[letterIndex + 1, numberIndex - 1],
            board[letterIndex - 1, numberIndex + 1],
            board[letterIndex - 1, numberIndex - 1],
        };

        legalSquares.RemoveAll(
            s => s is null
            || s.Piece?.Color == Color
            || s.IsThreatened(opponentColor)
            );

        // Castle
        if (!IsEverPlayed && !IsChecked)
        {
            // Long Castle
            Piece? longCastleRook = board[0, numberIndex]!.Piece;

            bool isLongCastleAllowed =
                longCastleRook is Rook
                && longCastleRook.Color == Color
                && !((Rook)longCastleRook).IsEverPlayed;

            if (isLongCastleAllowed)
            {
                for (int i = letterIndex - 1; i > 0; i--)
                {
                    if (board[i, numberIndex]!.Piece is null 
                        && (i != letterIndex - 1 || !board[i, numberIndex]!.IsThreatened(opponentColor))) 
                        continue;

                    isLongCastleAllowed = false;
                    break;
                }

                if (isLongCastleAllowed) legalSquares.Add(board[letterIndex - 2, numberIndex]);
            }

            // Short Castle
            int lastLetterIndex = board.Squares.GetUpperBound(0);
            Piece? shortCastleRook = board[lastLetterIndex, numberIndex]!.Piece;

            bool isShortCastleAllowed =
                shortCastleRook is Rook
                && shortCastleRook.Color == Color
                && !((Rook)shortCastleRook).IsEverPlayed;

            if (isShortCastleAllowed)
            {
                for (int i = letterIndex + 1; i < lastLetterIndex; i++)
                {
                    if (board[i, numberIndex]!.Piece is null
                        && (i != letterIndex + 1 || !board[i, numberIndex]!.IsThreatened(opponentColor)))
                        continue;

                    isShortCastleAllowed = false;
                    break;
                }

                if (isShortCastleAllowed) legalSquares.Add(board[letterIndex + 2, numberIndex]);
            }
        }

        return legalSquares
            .Select(s => new Move(Square, s!))
            .ToArray();
    }

}
