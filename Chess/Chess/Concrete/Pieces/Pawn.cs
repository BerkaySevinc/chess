using System;
using System.Buffers.Text;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BekoS.Chess;

public class Pawn : Piece
{
    public Pawn(PieceColor color) : this(color, default) { }
    public Pawn(PieceColor color, Theme theme) : base(color, theme) { }


    public override Move[] GetLegalMoves()
    {
        ArgumentNullException.ThrowIfNull(Square);

        Board board = Square.Board;

        int direction = Color == PieceColor.White ? 1 : -1;
        var (letterIndex, numberIndex) = Square.GetCoordinates();

        // Moves
        var legalSquares = new List<Square>();

        // Standart Move
        Square? squareFront = board[letterIndex, numberIndex + direction];
        if (squareFront is not null && squareFront.Piece is null)
        {
            legalSquares.Add(squareFront);

            // If First Move
            if (
                Color == PieceColor.White && numberIndex == 1 ||
                Color == PieceColor.Black && numberIndex == 6
                )
            {
                Square? squareFront2 = board[letterIndex, numberIndex + direction * 2];
                if (squareFront2 is not null && squareFront2.Piece is null) legalSquares.Add(squareFront2);
            }
        }

        // Diagonal Takes
        Square? squareDiagonalRight = board[letterIndex + 1, numberIndex + direction];
        if (squareDiagonalRight?.Piece is not null && squareDiagonalRight.Piece.Color != Color)
            legalSquares.Add(squareDiagonalRight);

        Square? squareDiagonalLeft = board[letterIndex - 1, numberIndex + direction];
        if (squareDiagonalLeft?.Piece is not null && squareDiagonalLeft.Piece.Color != Color)
            legalSquares.Add(squareDiagonalLeft);

        // En Passant
        int enPassantLocation = (int)(3.5 + (direction / 2.0));
        if (numberIndex == enPassantLocation)
        {
            PlayedMove? lastMove = board.Notation.LastOrDefault();

            if (lastMove is not null && lastMove.Piece is Pawn)
            {
                var (lastMoveFromLetterIndex, lastMoveFromNumberIndex) = lastMove.From.GetCoordinates();
                var (_, lastMoveToNumberIndex) = lastMove.To.GetCoordinates();

                if (
                    (lastMoveFromLetterIndex == letterIndex + 1 || lastMoveFromLetterIndex == letterIndex - 1) 
                    && lastMoveFromNumberIndex == enPassantLocation + direction * 2 
                    && lastMoveToNumberIndex == enPassantLocation
                    )
                {
                    Square squareEnPassant = board[lastMoveFromLetterIndex, enPassantLocation + direction]!;
                    legalSquares.Add(squareEnPassant);
                }
            }
        }


        return legalSquares.Select(s => new Move(Square, s)).ToArray();
    }


    protected override Dictionary<Theme, Dictionary<PieceColor, string>> Base64Bitmaps { get; } = Base64BitmapConst;

    private static readonly Dictionary<Theme, Dictionary<PieceColor, string>> Base64BitmapConst = new()
    {
        {
            Theme.Neo,
            new Dictionary<PieceColor, string>
            {
                {PieceColor.White, "iVBORw0KGgoAAAANSUhEUgAAASwAAAEsBAMAAACLU5NGAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAwUExURUxpcUNDQ0REREREREVFRUVFRf////j4+O7u7uPj49PT07q6up+fn35+fl9fX0ZGRseiDMcAAAAGdFJOUwArV4Ow1uZrUh8AAAhMSURBVHja7VzPaxtHFI5s+WxT2rNSaM4OTe9Of9zlHnr2bfUf5Elq06OTiJLeUipaeovpWnJvCQQa32woNLolFAq61U0oyFHkxj8kbZmZ3dVqtTu71s6+J+j7bokN/njvm2/evHk7V64wGAwGg8FgMBgMBoPBYDAYDAaDwWAwGAwGg8FgMBgMBoPBIEXhnRs3bnz4/mKRWvrIUTj5dG1xWH3gBPDJouSv7EzhzUIEbGnLCWGwALwKM6wWgVc4g24eqWmtu0RGfz75ubn7e8/95+e0rIouqd8sywIAqLZcYtcXQFj/bFtWBRTqz+jlpVL4ypqwAoCn1Glc8llZAGFea7TBugizgmpH/H+fNFij7TArgHqPMlwyWAfTwlL4UfzkNdEyjE6hQK0rqgkaWiuC1sOoYAG0xM9uktAS285ZZLAAGh2qLWhZEyyotahEX4pVlvAIW6hrkyiHBzHBAmi2abIoTGsUFyyAht0jyaJYh3/HBgtq9j7JWhRe+ig2WFC12ySOuqXNIYAtsjigkNZ5fA4BmnaHQFyiLH2uCRY07D2CIrXkemksrbrdInCuDSWt2BxCzbYJiq4tZfGxrKBqC6NH1rwoat4m0dpHL26WlOLjcygcYg99KRaVmeppCUO9ir71bOtyKIyrhb79rDrO2EqiZaM7REn6QyWBVs9xjrE36mEyrS72Zr0hbSsFrT42rTO9tAStDjatclpab7BpnS4mrbd6aYEtdx90Wv8uJq2EaFWJaJ3qpVWzCbQlDSINLXTfOtfTqtsEdio3Hy2thk2w+ZQcZ1hJsi38rVoUNon+gF/YiDIwUfG76GWgKJofJym+jX5+FUeMF0nSwj9iyANZgscTHMjk8TUhhwTHV3nYT8ih8Afsw75ojXynz6E4jx0j0ypqNS8snqSRJJbimdZLSdpuskmpFTxJk1K2dB9rlEXU0pUNcI2yiBrg8rpAEyyi6wIhrugsNv0cUlyRCec6j02hXIcUV1Hi4m68E1nRuF6K3HMLZPFljLCk4GkmbUoR4fJYyWBtktCS8wavIllJZVFNHMiBg+BivOexahMOHKjb6tGdWVa7PTLBu0f+SRprTXsqhWSjZctqdsulddfHvR7lCJeaVYywLvhGzaCu0aXQOYw4xdZ+pZsQvOYOb0Vuix2q0TI1FTi8FXnkr+92aeTlCuthdEuwpmweX16usOK6zbZNIq9r/lRgbIH6DF9enrBi71fcXiCuvNQE5fiBbpbFttVkLJ68Cr6wNLMs3oaNJy81BPvS0s+y2MjyUlvh0NLSUvu2ktdVPFpSWNoRG+wKZ9kbN9XRagTqQTxaf1jpaNlPUWntQAKagbPG/57W47S02mgnIOHxh0m0bHRahaSe/OTIrzqVSC7vOM5pAq26PTn0Y/Xmy/qefFBawuexNsUNxxnrWfmH/l3E03UpcSk2gorH6pAU49umoWDJfhJWISiW4jCVsmQJgXy+8PDEDqFF9Jlb6TK08Jpvxam/G2Yll98EeP2kwiVoYV50bgT+7niGlh2khXmjuJqeFmZfd+kLF+KebJZWz3FOvN8gaXKJoZZZWl26fq4vsotmFK0+Ka2y45zXZmh1qL8V3hKl10y49inuXUMGdgS1CFonlLTcmZZwuPYwN+iYQ9AhzISrTfpFrrqw3oGZcLWIv/cWpcQdmAnXLtmtne+mo1Dh5+0+r2ltazh9LlwEPy1MbtGbYT8d0C7EI4gI1y+kS3El0I1ohI3rOulC3IGIcLVIl2I5eLxuhJZin1LxFxFnVrUUTygVfwqR4don1Lyo6A8gMlx7ZJ/sq/PPNkSGq0UnLiGtUQWiw9UjE5c4W59VIDpcHbL5DOFazyvRDSQprmMSWuK2+kElut0mxTWgsodh6HalOiUukiyKHB6FL30aU861SZTDR1Zse7JNk8Vl9R5EbDNXZhG/ilhX70Hou7l9Ci8VnzDre9/o++J7TszXr9WpLG4SCP4o8vY1eN2DvQHJpu6D6EvhqRYqbhlR1jyeUQ/uiwP0YMU+gBK8eUUNV9mbz9DfcXZxwyWD9Tx+aKQZKCPwwiXfvxtZ+nkk3yPQRpKEZzkvdNMZU+FCGv1RT7rd0g3+VAmmiMu+sqzE61cZrgFaCof6CZtJGrtIaVTTW4+SaFWD3pV/GtUrlOf6mbLgauygvFC5EZgps9Lc7rcwru+ksJQ5JDye4ctLvVD5cf7CurBS0fJ45T4gWPAGmVPkcMJLpXGQdwoPUwbL55XzA6iB501TBcv/aqST6wOo694gc9pgebzU9Hw/R737wkoVLOGrTV9ea/kF68UlWbkBe5rb90hLQW9ImUIX37sukUe4St6LnZdmBVC990NeMxFbl12F09jPx7uKGYIl8GU+nxqsTwqHeVipcBkXfWFSZc2TQgD4Ko+zf3EyuT8fK4BuDllc9+v3+VIIAPdzyOKWevouAyuom1+LyxMrhblh/smDVT+Hlflp3TfuqBuqnZWJlbSuvmlpDTOmEABMvxAhtunTrMFSjmpSXCveJzSZWMHXhp2r5EorW7CkuI7NKn6UPVhSXH2zij/PHizpXAOz+/RfBoIFPxkdg1tWis8cLLht9Hi9oirAzKyk5m+aXIhjEzmEmtHtJ8Wjq+mX4muT/nBhIlhyKfZN+sOpGVpG53elP5jIIXxrsJ4vyGLLBCu5K5qsHw7M0LptsIYQbvrQSA7lqcyUnxZnp1ay+Ol1gyZ/ywytullaYzPSgqrB3WdVVFtmaIHB3Wc1NNWWcffZNLdTm6R1bI7W0BStrllawLQuUW7dNQSztEyCaTEtpjUX3v3MKKjm6BkMBoPBYDAYDAaDwWAwwvgPzJiT/MN/tYUAAAAASUVORK5CYII="},
                {PieceColor.Black, "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"}
            }
        }
    };
}
