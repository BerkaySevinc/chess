
using Chess;




namespace ChessFromZeroTest
{

    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

       
        private void Main_Load(object sender, EventArgs e)
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