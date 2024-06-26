using System;
using System.Drawing;
using System.Windows.Forms;

namespace LandscapeDrawingApp
{
    public class MainForm : Form
    {
        public MainForm()
        {
            this.Text = "Пейзаж";
            this.Size = new Size(800, 600);
            this.Paint += MainForm_Paint;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            
            g.FillRectangle(Brushes.SkyBlue, 0, 0, this.Width, this.Height);

            
            g.FillEllipse(Brushes.Yellow, 50, 50, 80, 80);

            
            Point[] mountains = {
                new Point(0, 400),
                new Point(200, 200),
                new Point(400, 350),
                new Point(600, 200),
                new Point(800, 400)
            };
            g.FillPolygon(Brushes.DarkGray, mountains);

            
            g.FillRectangle(Brushes.Green, 0, 400, this.Width, 200);

           
            g.FillRectangle(Brushes.Brown, 650, 300, 20, 150);
            g.FillEllipse(Brushes.DarkGreen, 600, 200, 120, 120);

           
            g.FillRectangle(Brushes.Red, 100, 300, 200, 150);
            g.FillRectangle(Brushes.White, 150, 350, 40, 40);
            g.FillRectangle(Brushes.White, 220, 350, 40, 40);

            
            Point[] roof = {
                new Point(100, 300),
                new Point(200, 200),
                new Point(300, 300)
            };
            g.FillPolygon(Brushes.Brown, roof);
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