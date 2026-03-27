
using BekoS.Chess;




namespace ChessFromZeroTest
{

    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

       
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