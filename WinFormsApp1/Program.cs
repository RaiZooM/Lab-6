using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrawingShapesApp
{
    public class MainForm : Form
    {
        public MainForm()
        {
            this.Text = "Малювання фігур";
            this.Size = new Size(800, 600);
            this.Paint += MainForm_Paint;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Сектор еліпса
            g.FillPie(Brushes.Red, 50, 50, 200, 100, 0, 90);

            // Сегмент кола
            g.FillPie(Brushes.Blue, 300, 50, 150, 150, 45, 90);

            // Квадрат
            g.FillRectangle(Brushes.Green, 50, 300, 100, 100);

            // Зафарбований трикутник
            Point[] trianglePoints = {
                new Point(400, 300),
                new Point(500, 400),
                new Point(300, 400)
            };
            g.FillPolygon(Brushes.Yellow, trianglePoints);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}