using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BekoS.Chess;

public abstract class Piece : IDisposable
{
    public Theme Theme { get; }
    public PieceColor Color { get; }

    private Square? _square;
    public Square? Square
    {
        get => _square;
        set
        {
            // Return If Same
            if (value == _square) return;

            // Set Variables
            Square? previousSquare = _square;
            _square = value;

            // Set Previous Squares Piece To Null
            if (previousSquare is not null && previousSquare.Piece == this) previousSquare.Piece = null;

            // If Square Null
            if (_square is null) return;

            // If Square Not Null
            _square.Piece = this;
        }
    }

    private Bitmap? _bitmap;
    public Bitmap Bitmap => _bitmap ??= GetBitmap();

    public Piece(PieceColor color, Theme theme) =>
        (Color, Theme) = (color, theme);

    private Bitmap GetBitmap()
    {
        string base64Bitmap = Base64Bitmaps[Theme][Color];
        byte[] imageData = Convert.FromBase64String(base64Bitmap);

        Bitmap bmp;
        using (var ms = new MemoryStream(imageData))
            bmp = new Bitmap(ms);

        return bmp;
    }

    public abstract Move[] GetLegalMoves();

    protected abstract Dictionary<Theme, Dictionary<PieceColor, string>> Base64Bitmaps { get; }

    public void Dispose() => Bitmap?.Dispose();

}
