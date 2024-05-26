
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

        IsLegalToMove u square dan kaldýrýlmalý mý
        Piece ve Pieceden derivered tüm classlarý kaldýrýp, pieceleri enum yapmak daha mý mantýklý?

        þaha IsCheckMated eklenebilir
        boarddaki IsChecked yerine IsKingChecked yapýlmalý sanýrým veya böyle kalmalý bilmiyorum

        þahýn gidebileceði yerler için her karede IsThreatened kullanmaktansa toplu olarak çevre 8 kareyi kontrol et
        gidebiliceði yerleri kontrol ederken þah orda yokmuþ gibi kontrol et çünkü þahýn arkasýndaki kareye gitmesine izin verio þuanki sistem

        Board.SelectedSquare yerine Board.SelectedPiece olmalýydý sanýrým
        IsPlayable square yerine piece de olmalý, piece kendi içinden square daki picturebox cursorunu deðiþmeli bi þekilde

        tehtid altýnda castle atýlamaz
        check
        checkmate
        stalemate
        draw
        piyon sona gelme
        þahlar tehtid edilen yere gidemez, þahýn önü açýlamaz
        3 kere tekrarda, 50 hamlede vs. beraberlik kurallarý

        location, size gösterilmelimi?

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