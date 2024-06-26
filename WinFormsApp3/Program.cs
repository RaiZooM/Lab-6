using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace RotatingRectangleAnimation
{
    public class MainForm : Form
    {
        private float angle = 0;
        private float x = 0;
        private System.Windows.Forms.Timer timer;

        public MainForm()
        {
            this.Text = "Прямокутник, що обертається";
            this.Size = new Size(800, 400);
            this.DoubleBuffered = true;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 16; 
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            angle += 5; 
            x += 2; 
            if (x > this.Width + 50) x = -50; 
            this.Invalidate(); 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            
            float y = (float)(this.Height / 2 + Math.Sin(x / 50) * 100);

            
            using (Matrix matrix = new Matrix())
            {
                matrix.Translate(x, y);
                matrix.Rotate(angle);

                
                Rectangle rect = new Rectangle(-50, -25, 100, 50);

               
                g.Transform = matrix;

                
                g.FillRectangle(Brushes.Blue, rect);
                g.DrawRectangle(Pens.Black, rect);

                
                g.ResetTransform();
            }
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