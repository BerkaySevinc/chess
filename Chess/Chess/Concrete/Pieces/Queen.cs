using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BekoS.Chess;

public class Queen : Piece
{
    public Queen(PieceColor color) : this(color, default) { }
    public Queen(PieceColor color, Theme theme) : base(color, theme) { }


    protected override Dictionary<Theme, Dictionary<PieceColor, string>> Base64Bitmaps { get; } = Base64BitmapConst;

    private static readonly Dictionary<Theme, Dictionary<PieceColor, string>> Base64BitmapConst = new()
    {
        {
            Theme.Neo,
            new Dictionary<PieceColor, string>
            {
                {PieceColor.White, "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"},
                {PieceColor.Black, "iVBORw0KGgoAAAANSUhEUgAAASwAAAEsBAMAAACLU5NGAAAABGdBTUEAALGPC/xhBQAAAAFzUkdCAK7OHOkAAAAqUExURUxpcTMyMiUlJSUlJSUlJSUlJSUlJSYmJlZTUkVCQnh1dR4eHk5LSmlmZUgI/O0AAAAHdFJOUwD9rifZUHxvyjgTAAAOzUlEQVR42uxdvW8byRWXSJFuLQfBUqYPUGFDrRPcHVvhnIStEBysVsgVar0WpZUiunCV3cO6YEVteOkFAqpVqHJl7cFXpDpCgP6X7Hzs7rz3Zqn9mFETvuZO8mjmxze/9+a9eTPDtbWVrGQlK1nJSlaykpWsZCUrWclKVrKSlazk/07a//iL57k//LSkyduXjuf98Or1I6L6czIil2dFo3Z2ZIvRT4+G6kcvk9GetkXXyZu8enxUBbhUVJ735nFm0APyns5jewibbD8Cqo4Dx/T+SJocohajR+C9HNPdijblqAeoRUv+fiuayP87to5KjOn+Np9fLYKpd8OmETURRti7S1r4Ez1y48LHPLtLUPmJfL65IdzZ4ED+I1sE/evkpxPLqLoAle9/ubnGg+6oqBJc5wz5nl1Y+2zMn+fzuRjTD/t4UA78m6TFQjb5zJAf2V10HDSmH52jQRlwV20RMOQj64RnY16lY/rxFA7Kgf9bbRFGTF0HtufwuaoKP+bqOgDAz0CLMJrdWJ5F5r7vVFX4AR/0adZiIIArLfxokqjrvWU7PAWqSGCxQU+AHd6BFn4UMXZZ9PQbmDdsitgsuhm1JHClRTLPjH/bdqmVqOISwmKzuKfo8zlskcBiCn1n1cWfIVUksCaKLtaFW1tAWEyhFh19Yvwf4Bwm3IoixdD2hQNRWyTcYuSyx/m2ZoYYrEQXx3l8MUYtGCw2z1YN8Vc6Q1E/t/8h1+cCTTPn/J5NH3+HZojBml1nfl7o08ewGOcPbPoHRC02Q9H0Op2iNvcgsAWbZrb+WPMQTzifL7EqOCzhLTt8mgm1rMM6Q8ThqphksLrcPxBqcVi7dmFRVTBYexmsOw1wZqzWYA0IrFALi9gEh/XUKqwrqgoE64oCj5LI2e4kXpI5xNy6osqKbuzCcvE6TS3xZw1wBmvbqt9akDnEfuuStrDqILiXX9AZmnnAy2taWPXynDkLOkN9D6yJCzLLyZp4bW9NZMx5TqkVnXsggljQOZwlsOzFWwmsDxriJNR6p8RbVJ124y0eneoZv61YxQVRJ3NbFndtWCx/Sxl/DWP5U6JOy7E800WPwDpX0mrmIVzcgFPLYubDdHGGYbEMA+aJF5ha53bzRJ5V3yJYbANrF2TVY0Qt21k1J9cYwmJjKj6J7yPdQmr1kyZW9yBanqquWNo+UMUwRx6kyrqxvGPDt4nOVEPjY+7C6CdlV5Ary+ocyt3AF2qOeHMN6dzxMmMUsBwM3NbeaYorlLCOyQ65AitBde25tnfmxbb81q2SxqNFuJuRK4E1SVAlLf5gewNcqEt68lik8a/Jgn6awupzVCaV9f1LR1cy3FdgBSJkOSDWmlF+xqaQFqPevnyoHFkUxKQFwb+hTyrqTKf5LhJeV9YzHyJg0TJHYd8Pz9WwsATWBbAi4irz2CYQqyGu7XWGD5UjC3U1XFKa+z6PEsR6d1IQ/XBYZGhQ1qtUPGvvgMLbCV0Ze5mfT0xxhGPFsbqBqa9UFfW9TH70ltYp8/AmoKbYyf9VC+tb1Pebqk7A20wLgkjVYOApWvByQ5SwRsR9iGKjLEe6e9VcJi8Iho7mIynBoDDFXZjjpou5Dta+0ve0Ug20CwqCXGHAH7YVWDExRTXJ4LocUWWlfX/mP5VU1778S7nz4mB1qbACHgaeFKQhHJa7huOLb7K+v7Afy61MbQ+WUGIHFX7bSlwcYlNsqzmGYB52DmOlPMP6LlfUY3mEq9QqggmqNndguA5NsatmIQQWDyPVWtbn0gcSDoGamULYRzoqhDUBpggSxRjDYvz4oJYTAqck6dseKuyE3E2MCmDFHBZMMVAR6jUMbkHRKJyUnMUW/kAJfRxgLwBWAIoYXNeuDzHDGUb1M953iUB/IEoVl+pGAnMwu6DzCwV0Xw3Vh2omiWENROFP3QCLYd/LqOXCSmayHgMCdEGyyJN9V2uIvAaq6vmQ1xvmcAPsuhS5hlzPV3DbxVFdRBflsFNUT+wVwqJ9J036Xom8qEOqYLyU5ClzAWEFoJ64ofm3veK+ea3bK7ETIKtguJSkcr4Fhg55PfGdzhCZEaveo6WpsAV8H2qvjDO9w9VoDmtb7R3MMawnuoWwaN9codclHCotN/FsTzUXBCtG9cRxIawBLWWx3stktwNSboolrHeFsGbZBngHGKKc4APFx4/RHHK/5z28J7fPjYXUIRxl+dmAsFR2tOC+nM9hbSv+AfWduuOjEm7rFE5/JGEdF8AKFVNch7uYFNYHRC0ZGZWB9UFT10GwXB+PfaTbaC4BK+awjivDElt6UyVHWUdjx4wdJ7qtaARrh3zkarBoXUfV1hMEK2Adj1Aylg6qmtkO7lvurpSDdTr3K8EKuSm+hsmYDhb/yD4p+5fhFjdiLawj1f3ATzyVR7haoFiQzu9uYd/Z6Y6jMn5LU6dU/RaBFU/kZvMTZIgC1tPCvkXvZWoJrGc9rN1iWKkpYkNMlKH6StK3sKcyVX/mlMhpFb4mqs76DHUuTREbIoLF5vgTUVapWmMX0yOIcGCjgdXnAXkbGyKCRSxCHO4oc/6GdB1Ixo/WCmH50hS7eFgB60jdJTgjnrpf6rTSkHglQa2TJbA45w+oIQo9HoE9JN0RifelYnlgTWm49VRtgWAFwhRhDKiBNQC446wgVKLyso5nQsyhEqlRWCEzxWMUA2awjoE9jXE1slwxoYsJEPA5hKkgJHYCnecJQw3ePvrLDHisFPXK7NkMMUOEIeZ/q4EVJ3mCC5OxHO8x2KHq1Szq7Reo60ghLoYVMOBviSEKWCdqzy4trpcr6rU8vbqyrbcdqpSQcf4lMUQIi2+5jXEVu3RRb1igrjdLYLHc2COGCGHtKw3ycnHpot5ALc5JejrKxq4Glh/J/VlfQ7oTZStoDJnVZ8oqudfc8TAusZeU/v1QAyvue55HOcdhSW38iZaLqxX19tXinKquYljBTKlQaWHxva0xWP85qvJFvXTv3MXq2kZFDEWfYjO7VwzrW9WSBK14Ua9CeSWtNGB1iQEczfDyQs+tTovvs83cM3BC9kZf1Ft+mMZDp2Vydem04sfXOkPksEbpupP1J2vv9DJOuQrZqVZdWliB55GlR4UFvA4/ccxvLlW8XibqiWNijAewWqD8u6MzRO7+R8RHp+69aj1RVkhd4FMdEXUVwJrpf53AciUrXBCUsiX6WVVU9FSULxbsgwJYvE5yoYXlSWX14LGOmgdcNtA4gYxv2trxeT3itgjWIVB9elqw1tmpDnaPMr7p6GEFmqUnhdUFygrTI131jiE4yLRCoa4CWKHOEH2f50xQWUGz04IkBuZb+953eli+ozFE4Wa/gwtALI/t1jwtOMBsEep6VgDri84QOKxDDxUYaM224mGtnkZdBbA+an+bXn89xTlr7WO7hPOinqVd+zjnNb+UdV9YQ4ia3SqjmUzsFMMK3SWwxihpbXI+VpP3TYph+VtLYN3iuxkNzscOKACpLi2s22JYZ/hSSZMrIy3NMjPRZhLF8hGbSNyQ8bqNIVlcrgCLBDwVdmnKc16qqyKsC7yfft7oRPi+BgFXVzVYZ2TzqNn52HWd55xo1+RlsHrkvlKzo9ddXWgVVIIVwtZhdiJ8rz6stjYQnlaE1SO7/OQoWkXZ0WkmrALLx/WhSHdwzwDnE3VVgtWjl0qangjf0AfoVWDR/eH6AfNSzifmVAHWCzqHs6a3fvScry1hZITxBZyvLUFkhPHaIKKBxKau12wURMj1JDLDeF3g3HwOZwau1zj69K/BHBpgvC5wbmqHjQJmC5wP8rrF06awWnqH2mAOpyZudprjfBiZY7w+cG4yhw0DZuOcj/NLwwauUJrivFqp220OyxTng8gg4wXnx8YWnqYpomnOq7dzjVy/O6yUFz5E+MjUFconRoKIyCzjCwPneoQHLxc1EiOBc6ww3l0zIgYC54zwBgLm5clivTmMzF0aNhA4Z6im5u6jN+d8Poczc48lNed8RngjAbMpzufKMsh4WlmsT3ijLzA05Xxkg/H47Ykmczgz+gKD1yiIyAlvlPENOa8oy1DAbCRwVpRlJEU0EzirypqaffynSbIYRLYY3yhwVlAZC5ibJ4uqsgwzvgnnVVQT0w/P1eY8UNbU9FMotZNFFZWF11hrch4oyzjjayeLAFVk/pWdeskiVNbE/FOL9TgPlTUz/7porcA5jjDj3TXDUiOICCEqKw/q1kgWkbIiG+9KVQ+cA4RqauPJssrJIp5CG4yvwXk8hYYD5pqcx1No6wnpapwnU1j/oKlBzlNUdhhfMVmMKayZpUf6KiSLGlSWGF+F8zpUxgPmyoGzFpXhFBFyvlcXlfmAuWKyqEdljfHlqulhVCDn1h5iLZEsfi1CZY3xD3P+azEoGwFzUeD8NaHRZiIeEddlv588BuNVzn+NdGB0kqCzlCLCZDGMJ45XUdzNrXOLj3geek3E1tPDnZ1GsCpfNSonf/caivvqMVXFrG6yldnc5mRzs4h9z0yzvjMkNrb1y+2n+/u5Xq7u732Ndbzfs4hq6xe/CA6Rez+eLP/quQZpTz6D7kVpRLksci9n0k3syz6dF3fzmhKk8/lXo6sOC5k/zZvIwjH8xXLyS+vmDeXqc+WnRB8KS8VrkE3lo8mvU9xJ35E0hMuMurrpG5Um5EuFJzsfNkN3mQXe3d0Dubpawi/PM/QNbkP5BXIQSjL8YrE8gl4wjPjz/NeQ7+rKb9tTl5WyOxHyvyq4K8fMLA4UZZVHRORTCu13z8j76fJL6xJMi0ZnIRg01s+lmYhQPud67xuRZEF1THzJI8sr/mkKFJO73z0D2QZ/sHHhG5TAxLo4MHJgEcPaNeBMXbOwfBMOdcfg7QIpjoFlcWj0og+XLwb8vLlT/Jn8q7mHaFuA9dFrfKq5Y/SeTw7rdfOF2jSsoPli3TJ6WcuYP20Z96b83mlTWBuWYP2vPTu2ARiEoSioVIyQFjFCmrSMx9jMgGxHKe4meJ2/5P6/rBXPuvJvT0ZWK8qasmRVZN2yPs16ZcmSFR42KVklZMmSdeIZNZIeLAAAAAARG+3wsHNDevsCAAAAAElFTkSuQmCC"}
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
