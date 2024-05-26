
using BekoS.Chess;





namespace ChessFromZeroTest
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /*

        TO DO LIST:

        IsLegalToMove u square dan kald�r�lmal� m�
        Piece ve Pieceden derivered t�m classlar� kald�r�p, pieceleri enum yapmak daha m� mant�kl�?

        �aha IsCheckMated eklenebilir
        boarddaki IsChecked yerine IsKingChecked yap�lmal� san�r�m veya b�yle kalmal� bilmiyorum

        �ah�n gidebilece�i yerler i�in her karede IsThreatened kullanmaktansa toplu olarak �evre 8 kareyi kontrol et
        gidebilice�i yerleri kontrol ederken �ah orda yokmu� gibi kontrol et ��nk� �ah�n arkas�ndaki kareye gitmesine izin verio �uanki sistem

        Board.SelectedSquare yerine Board.SelectedPiece olmal�yd� san�r�m
        IsPlayable square yerine piece de olmal�, piece kendi i�inden square daki picturebox cursorunu de�i�meli bi �ekilde

        tehtid alt�nda castle at�lamaz
        check
        checkmate
        stalemate
        draw
        piyon sona gelme
        �ahlar tehtid edilen yere gidemez, �ah�n �n� a��lamaz
        3 kere tekrarda, 50 hamlede vs. beraberlik kurallar�

        location, size g�sterilmelimi?

        movemade, check, checkmate, stalemate, draw, piece taken etc. eventleri olabilir...
        */



        private void Form1_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;

            var board =
                new Board(
                    this,
                    new Point(500, 70),
                    new Size(880, 880)
                    );

            
        }

    }
}