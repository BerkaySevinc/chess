using BekoS.Chess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Drawing.Color;

namespace BekoS.Chess;

public class Square : IDisposable
{
    public Theme Theme { get; }
    public Board Board { get; }
    public SquareColor Color { get; }
    public string Name { get; }

    private bool _isPlayable;
    public bool IsPlayable
    {
        get => _isPlayable;
        set
        {
            _isPlayable = value;

            _pictureBox.Cursor = _isPlayable ? Cursors.Hand : Cursors.Default;
        }
    }


    private bool _highlight;
    public bool Highlight
    {
        get => _highlight;
        set
        {
            _highlight = value;

            if (_highlight)
            {
                // Highlight
                _pictureBox.BackColor = GetBGColor();
            }
            else
            {
                // Reset Highlight
                _pictureBox.BackColor = GetBGColor();
            }
        }
    }

    private bool _isLegalToMove;
    public bool IsLegalToMove
    {
        get => _isLegalToMove;
        set
        {
            _isLegalToMove = value;

            if (_isLegalToMove)
            {
                ArgumentNullException.ThrowIfNull(Board.SelectedSquare);

                // Add If Not Exists In Boards Legal Moves
                var legalMoves = Board.LegalMoves.ToList();
                var legalMove = legalMoves.FirstOrDefault(m => m.To == this);
                if (legalMove is null)
                {
                    legalMoves.Add(new Move(Board.SelectedSquare, this));
                    Board.LegalMoves = legalMoves.ToArray();
                    return;
                }

                // Set Cursor
                _pictureBox.Cursor = Cursors.Hand;

                // Set Display
                var movableColor = FromArgb(70, 0, 0, 0);

                Bitmap bitmap =
                    _pictureBox.Image == null
                    ? new Bitmap(300, 300)
                    : new Bitmap(_pictureBox.Image);

                var graphics = Graphics.FromImage(bitmap);
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Move Display
                Rectangle circle;
                if (Piece is null)
                {
                    int circleWidth = (int)(bitmap.Width / 2.8);
                    int circleHeight = (int)(bitmap.Height / 2.8);

                    circle =
                        new Rectangle(
                            (bitmap.Width - circleWidth) / 2,
                            (bitmap.Height - circleHeight) / 2,
                            circleWidth,
                            circleHeight
                            );

                    var brush = new SolidBrush(movableColor);

                    graphics.FillEllipse(brush, circle);
                }

                // Take Display
                else
                {
                    circle =
                        new Rectangle(
                            (int)(bitmap.Width * 0.0366),
                            (int)(bitmap.Height * 0.0366),
                            (int)(bitmap.Width * 0.92),
                            (int)(bitmap.Height * 0.92)
                            );

                    var pen = new Pen(movableColor, 24);

                    graphics.DrawEllipse(pen, circle);
                }

                _pictureBox.Image = bitmap;
            }
            else
            {
                // Remove If Exists In Boards Legal Moves
                var legalMoves = Board.LegalMoves.ToList();
                var legalMove = legalMoves.FirstOrDefault(m => m.To == this);
                if (legalMove is not null)
                {
                    legalMoves.Remove(legalMove);
                    Board.LegalMoves = legalMoves.ToArray();
                    return;
                }

                // Reset Cursor
                _pictureBox.Cursor = Cursors.Default;

                // Reset Display
                _pictureBox.Image = Piece?.Bitmap;
            }
        }
    }

    private Piece? _piece;
    public Piece? Piece
    {
        get => _piece;
        set
        {
            // Return If Same
            if (value == _piece) return;

            // Set Variables
            Piece? previousPiece = _piece;
            _piece = value;

            // Set Previous Pieces Square To Null
            if (previousPiece is not null && previousPiece.Square == this) previousPiece.Square = null;

            // If Piece Null
            if (_piece is null)
            {
                _pictureBox.Image = null;
                return;
            }

            // If Piece Not Null
            _piece.Square = this;

            _pictureBox.Image = _piece.Bitmap;
        }

    }


    private PictureBox _pictureBox;
    public Square(Board board, SquareColor color, string name, Point location, Size size)
    {
        Theme = board.Theme;
        Board = board;
        Color = color;
        Name = name;

        _pictureBox = new PictureBox()
        {
            SizeMode = PictureBoxSizeMode.Zoom,
            Name = name,
            BackColor = GetBGColor(),
            Location = location,
            Size = size
        };

        _pictureBox.MouseDown += OnClick;


        board.Form.Controls.Add(_pictureBox);
    }

    private void OnClick(object? sender, MouseEventArgs e) =>
        Board.SelectedSquare = this;

    public (int letterIndex, int numberIndex) GetCoordinates()
    {
        var squares = Board.Squares;

        int w = squares.GetLength(0); // width
        int h = squares.GetLength(1); // height

        for (int i = 0; i < w; i++)
            for (int j = 0; j < h; j++)
            {
                if (squares[i, j].Equals(this))
                    return (i, j);
            }

        return default;
    }

    public bool IsThreatened(PieceColor threatorSide)
    {
        int direction = threatorSide == PieceColor.White ? 1 : -1;
        var (letterIndex, numberIndex) = GetCoordinates();

        // Number+
        for (int i = 1; ; i++)
        {
            Square? square = Board[letterIndex, numberIndex + i];

            if (square is null) break;
            if (square.Piece is null) continue;
            if (square.Piece.Color != threatorSide) break;

            if (square.Piece is Rook or Queen) return true;
            if (i == 1 && square.Piece is King) return true;
        }
        // Number-
        for (int i = -1; ; i--)
        {
            Square? square = Board[letterIndex, numberIndex + i];

            if (square is null) break;
            if (square.Piece is null) continue;
            if (square.Piece.Color != threatorSide) break;

            if (square.Piece is Rook or Queen) return true;
            if (i == -1 && square.Piece is King) return true;
        }
        // Letter+
        for (int i = 1; ; i++)
        {
            Square? square = Board[letterIndex + i, numberIndex];

            if (square is null) break;
            if (square.Piece is null) continue;
            if (square.Piece.Color != threatorSide) break;

            if (square.Piece is Rook or Queen) return true;
            if (i == 1 && square.Piece is King) return true;
        }
        // Letter-
        for (int i = -1; ; i--)
        {
            Square? square = Board[letterIndex + i, numberIndex];

            if (square is null) break;
            if (square.Piece is null) continue;
            if (square.Piece.Color != threatorSide) break;

            if (square.Piece is Rook or Queen) return true;
            if (i == -1 && square.Piece is King) return true;
        }
        // Letter+ Number+
        for (int i = 1; ; i++)
        {
            Square? square = Board[letterIndex + i, numberIndex + i];

            if (square is null) break;
            if (square.Piece is null) continue;
            if (square.Piece.Color != threatorSide) break;

            if (square.Piece is Bishop or Queen) return true;
            if (i == 1)
            {
                if (square.Piece is King) return true;
                if (direction == -1 && square.Piece is Pawn) return true;
            }
        }
        // Letter- Number+
        for (int i = 1; ; i++)
        {
            Square? square = Board[letterIndex - i, numberIndex + i];

            if (square is null) break;
            if (square.Piece is null) continue;
            if (square.Piece.Color != threatorSide) break;

            if (square.Piece is Bishop or Queen) return true;
            if (i == 1)
            {
                if (square.Piece is King) return true;
                if (direction == -1 && square.Piece is Pawn) return true;
            }
        }
        // Letter+ Number-
        for (int i = 1; ; i++)
        {
            Square? square = Board[letterIndex + i, numberIndex - i];

            if (square is null) break;
            if (square.Piece is null) continue;
            if (square.Piece.Color != threatorSide) break;

            if (square.Piece is Bishop or Queen) return true;
            if (i == 1)
            {
                if (square.Piece is King) return true;
                if (direction == 1 && square.Piece is Pawn) return true;
            }
        }
        // Letter- Number-
        for (int i = 1; ; i++)
        {
            Square? square = Board[letterIndex - i, numberIndex - i];

            if (square is null) break;
            if (square.Piece is null) continue;
            if (square.Piece.Color != threatorSide) break;

            if (square.Piece is Bishop or Queen) return true;
            if (i == 1)
            {
                if (square.Piece is King) return true;
                if (direction == 1 && square.Piece is Pawn) return true;
            }
        }
        // Knight
        var knightThreats = KnightThreats(letterIndex, numberIndex);
        foreach (var square in knightThreats)
        {
            if (square is null) continue;
            if (square.Piece is null) continue;
            if (square.Piece.Color != threatorSide) continue;
            if (square.Piece is Knight) return true;
        }

        return false;
    }
    private IEnumerable<Square?> KnightThreats(int letterIndex, int numberIndex)
    {
        yield return Board[letterIndex + 1, numberIndex + 2];
        yield return Board[letterIndex + 1, numberIndex - 2];
        yield return Board[letterIndex - 1, numberIndex + 2];
        yield return Board[letterIndex - 1, numberIndex - 2];
        yield return Board[letterIndex + 2, numberIndex + 1];
        yield return Board[letterIndex + 2, numberIndex - 1];
        yield return Board[letterIndex - 2, numberIndex + 1];
        yield return Board[letterIndex - 2, numberIndex - 1];
    }

    private Color GetBGColor() => BackgroundColors[Theme][Color][Highlight];

    private static readonly Dictionary<Theme, Dictionary<SquareColor, Dictionary<bool, Color>>> BackgroundColors = new()
    {
        {
            Theme.Neo,
            new Dictionary<SquareColor, Dictionary<bool, Color>>
            {
                {
                    SquareColor.White,
                    new Dictionary<bool, Color>
                    {
                        {false, FromArgb(232, 232, 229)},
                        {true, FromArgb(197, 212, 158)}
                    }
                },
                {
                    SquareColor.Black,
                    new Dictionary<bool, Color>
                    {
                        {false, FromArgb(50, 104, 75)},
                        {true, FromArgb(108, 149, 83)}
                    }
                }
            }
        }
    };

    public void Dispose()
    {
        Piece?.Dispose();
        _pictureBox.Dispose();
    }

}
