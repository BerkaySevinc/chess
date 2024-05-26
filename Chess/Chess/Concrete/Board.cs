using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BekoS.Chess;

public class Board : IDisposable
{
    public Form Form { get; }
    public Theme Theme { get; }
    public Square[,] Squares { get; } = new Square[8, 8];
    public List<PlayedMove> Notation { get; } = new List<PlayedMove>();
    public bool IsChecked { get; private set; }
    public bool IsCheckMate { get; private set; }

    private PieceColor _turn;
    public PieceColor Turn
    {
        get => _turn;
        set
        {
            if (value == _turn) return;

            foreach (var square in GetSquareListByPieceColor(value))
                square.IsPlayable = true;

            foreach (var square in GetSquareListByPieceColor(_turn))
                square.IsPlayable = false;

            _turn = value;
        }
    }

    private Square? _selectedSquare;
    public Square? SelectedSquare
    {
        get => _selectedSquare;
        set
        {
            // Reset Selection
            if (value is null)
            {
                // Return If Already Not Selected
                if (_selectedSquare is null) return;

                // Reset Movable Squares
                LegalMoves = Array.Empty<Move>();

                // Reset Selected
                _selectedSquare = value;

                return;
            }

            // If Same Square, Deselect
            if (value == _selectedSquare)
            {
                Square initialSquare = _selectedSquare;

                SelectedSquare = null;

                // Reset Highlight
                initialSquare.Highlight = false;

                return;
            }

            // Piece Already Selected
            if (_selectedSquare is not null)
            {
                Square initialSquare = _selectedSquare;
                bool isLegalToMove = value.IsLegalToMove;

                // Reset Selected
                SelectedSquare = null;

                // If Legal, Move Piece
                if (isLegalToMove)
                {
                    Play(new Move(initialSquare, value));
                    return;
                }

                // Reset Highlight
                initialSquare.Highlight = false;

                // If New Selection (Same Color)
                if (value.Piece?.Color == initialSquare.Piece!.Color) SelectedSquare = value;
            }

            // Selecting This Square First
            else if (value.Piece is not null && value.Piece.Color == Turn)
            {
                _selectedSquare = value;

                // Highlight
                value.Highlight = true;

                // Set Legal Moves
                LegalMoves = value.Piece.GetLegalMoves();
            }

        }
    }

    private Move[] _legalMoves = Array.Empty<Move>();
    public Move[] LegalMoves
    {
        get => _legalMoves;
        set
        {
            var initialLegalMoves = _legalMoves;
            _legalMoves = value;

            // Reset Initial Movable Squares
            foreach (var movableSquare in initialLegalMoves)
                movableSquare.To.IsLegalToMove = false;

            // Set New Movable Squares
            foreach (var movableSquare in _legalMoves)
                movableSquare.To.IsLegalToMove = true;
        }
    }


    public Board(Form form, Point location, Size size, Theme theme) : this(form, location, size) =>
        Theme = theme;

    public Board(Form form, Point location, Size size)
    {
        Form = form;

        CreateBoard(location, size);
    }


    public Square? this[string name]
    {
        get
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    var square = Squares[i, j];
                    if (square.Name == name) return square;
                }

            return null;
        }
    }

    public Square? this[int letterIndex, int numberIndex]
    {
        get
        {
            try
            {
                return Squares[letterIndex, numberIndex];
            } 
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }
    }


    private void CreateBoard(Point location, Size size)
    {
        int squareWidth = size.Width / 8;
        int squareHeight = size.Height / 8;
        Size squareSize = size / 8;

        // Numbers
        for (int i = 0; i < 8; i++)
        {
            var squareNumber = i + 1;

            PieceColor pieceColor = squareNumber < 5 ? PieceColor.White : PieceColor.Black;
            List<Square> listByPieceColors = GetSquareListByPieceColor(pieceColor);

            // Letters
            for (int j = 0; j < 8; j++)
            {
                char squareLetter = (char)('a' + j);

                var squareLocation = new Point(location.X + j * squareWidth, location.Y + (7 - i) * squareHeight);
                SquareColor squareColor = (i + j) % 2 == 0 ? SquareColor.White : SquareColor.Black;

                Piece? piece =
                    (squareLetter, squareNumber) switch
                    {
                        ('a' or 'h', 1 or 8) => new Rook(pieceColor, Theme),
                        ('b' or 'g', 1 or 8) => new Knight(pieceColor, Theme),
                        ('c' or 'f', 1 or 8) => new Bishop(pieceColor, Theme),
                        ('d', 1 or 8) => new Queen(pieceColor, Theme),
                        ('e', 1 or 8) => new King(pieceColor, Theme),
                        (_, 2 or 7) => new Pawn(pieceColor, Theme),
                        _ => null
                    };


                var square =
                    new Square(
                        this,
                        squareColor,
                        squareLetter + squareNumber.ToString(),
                        squareLocation,
                        squareSize
                        )
                    {
                        Piece = piece
                    };

                if (piece is not null) listByPieceColors.Add(square);

                Squares[j, i] = square;
            }
        }

        // Make Whites Playable
        foreach (var square in GetSquareListByPieceColor(Turn))
            square.IsPlayable = true;
    }


    private Dictionary<PieceColor, List<Square>> _squareListByPieceColor = new();
    private List<Square> GetSquareListByPieceColor(PieceColor pieceColor)
    {
        if (!_squareListByPieceColor.TryGetValue(pieceColor, out var list))
        {
            list = new List<Square>();
            _squareListByPieceColor.Add(pieceColor, list);
        }

        return list;
    }


    public bool TryPlay(Move move, out PlayedMove? playedMove)
    {
        ArgumentNullException.ThrowIfNull(move);

        playedMove = null;

        if (!move.IsLegalNow) return false;

        playedMove = Play(move);

        return true;
    }

    private PlayedMove Play(Move move)
    {
        Square from = move.From;
        var (_, fromNumberIndex) = from.GetCoordinates();

        Square to = move.To;
        var (toLetterIndex, toNumberIndex) = to.GetCoordinates();

        Piece piece = from.Piece!;

        // Reset Last Moves Highlight
        PlayedMove? lastMove = Notation.LastOrDefault();
        if (lastMove is not null) lastMove.From.Highlight = lastMove.To.Highlight = false;

        // New Moves Highlight
        from.Highlight = to.Highlight = true;

        // Add To Notation
        var thisMove = new PlayedMove(from, to, piece);
        Notation.Add(thisMove);

        // Change Is Ever Played
        if (piece is IKeepEverPlayed) ((IKeepEverPlayed)piece).IsEverPlayed = true;

        // Edit Lists
        if (to.Piece is not null) GetSquareListByPieceColor(to.Piece.Color).Remove(to);
        var teamSquareList = GetSquareListByPieceColor(piece.Color);
        teamSquareList.Add(to);

        // Change Turn
        Turn = Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;

        // Continue Edit Lists
        teamSquareList.Remove(from);

        // Move
        to.Piece = piece;

        // If En Passant
        if (thisMove.Type == MoveType.EnPassant)
            this[toLetterIndex, fromNumberIndex]!.Piece = null;

        // If Long Castle
        if(thisMove.Type == MoveType.LongCastle)
            this[toLetterIndex + 1, toNumberIndex]!.Piece = this[0, toNumberIndex]!.Piece;
        
        // If Short Castle
        if (thisMove.Type == MoveType.ShortCastle)
            this[toLetterIndex - 1, toNumberIndex]!.Piece = this[Squares.GetUpperBound(0), toNumberIndex]!.Piece;

        //Is Checked
        Square opponentKing = GetSquareListByPieceColor(Turn).Single(s => s.Piece is King);
        if (opponentKing.IsThreatened(piece.Color))
        {
            IsChecked = true;
            ((King)opponentKing.Piece!).IsChecked = true;
        }


        return thisMove;
    }

    public void Dispose()
    {
        foreach (var square in Squares) square.Dispose();
    }
}