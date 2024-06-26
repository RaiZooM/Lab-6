using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TrainAnimation
{
    public class MainForm : Form
    {
        public float trainPosition = 0;
       public List<SmokeParticle> smokeParticles = new List<SmokeParticle>();
        public Random random = new Random();
        public System.Windows.Forms.Timer timer;

        public MainForm()
        {
            this.Text = "Паровоз";
            this.Size = new Size(800, 400);
            this.DoubleBuffered = true;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 16; // приблизно 60 кадрів в секунду
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            trainPosition += 1;
            if (trainPosition > this.Width)
                trainPosition = -300;

            if (random.Next(100) < 20)
            {
                smokeParticles.Add(new SmokeParticle(trainPosition + 80, 200));
            }

            for (int i = smokeParticles.Count - 1; i >= 0; i--)
            {
                smokeParticles[i].Update();
                if (smokeParticles[i].Alpha <= 0)
                    smokeParticles.RemoveAt(i);
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.SkyBlue);

            // Малюємо фон
            DrawBackground(g);

            // Малюємо рейки
            DrawRails(g);

            // Малюємо паровоз
            DrawTrain(g, trainPosition);

            // Малюємо дим
            foreach (var particle in smokeParticles)
            {
                particle.Draw(g);
            }
        }

        private void DrawBackground(Graphics g)
        {
            // Небо
            g.FillRectangle(new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, 300),
                Color.LightSkyBlue,
                Color.White
            ), 0, 0, this.Width, 300);

            // Земля
            g.FillRectangle(Brushes.Green, 0, 300, this.Width, 100);

            // Сонце
            g.FillEllipse(Brushes.Yellow, 50, 50, 60, 60);
        }

        private void DrawRails(Graphics g)
        {
            Pen railPen = new Pen(Color.DarkGray, 3);
            g.DrawLine(railPen, 0, 340, this.Width, 340);
            g.DrawLine(railPen, 0, 345, this.Width, 345);

            // Шпали
            for (int x = 0; x < this.Width; x += 30)
            {
                g.FillRectangle(Brushes.SaddleBrown, x, 335, 20, 15);
            }
        }

        private void DrawTrain(Graphics g, float x)
        {
            // Корпус
            g.FillRectangle(Brushes.Black, x, 250, 200, 80);
            g.FillRectangle(Brushes.DarkRed, x + 200, 270, 100, 60);

            // Труба
            g.FillRectangle(Brushes.DarkGray, x + 70, 210, 30, 40);
            g.FillEllipse(Brushes.DarkGray, x + 65, 205, 40, 10);

            // Кабіна
            g.FillRectangle(Brushes.DarkBlue, x + 140, 220, 60, 50);

            // Вікна
            g.FillRectangle(Brushes.LightBlue, x + 150, 230, 20, 20);
            g.FillRectangle(Brushes.LightBlue, x + 180, 230, 20, 20);

            // Колеса
            DrawWheel(g, x + 30, 310);
            DrawWheel(g, x + 100, 310);
            DrawWheel(g, x + 170, 310);
            DrawWheel(g, x + 240, 310);

            // Деталі
            g.DrawLine(new Pen(Color.Silver, 2), x + 30, 280, x + 270, 280);
            g.FillRectangle(Brushes.Gold, x - 10, 290, 15, 20);
        }

        private void DrawWheel(Graphics g, float x, float y)
        {
            g.FillEllipse(Brushes.DarkGray, x, y, 50, 50);
            g.DrawEllipse(new Pen(Color.Black, 2), x, y, 50, 50);
            g.DrawEllipse(new Pen(Color.Black, 2), x + 10, y + 10, 30, 30);
            g.DrawLine(new Pen(Color.Black, 2), x + 25, y, x + 25, y + 50);
            g.DrawLine(new Pen(Color.Black, 2), x, y + 25, x + 50, y + 25);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    class SmokeParticle
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Size { get; set; }
        public int Alpha { get; set; }
        private Random random = new Random();

        public SmokeParticle(float x, float y)
        {
            X = x;
            Y = y;
            Size = 5;
            Alpha = 255;
        }

        public void Update()
        {
            X += (float)(random.NextDouble() - 0.5) * 2;
            Y -= 0.5f;
            Size += 0.2f;
            Alpha -= 2;
        }

        public void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color.FromArgb(Alpha, Color.Gray)))
            {
                g.FillEllipse(brush, X - Size / 2, Y - Size / 2, Size, Size);
            }
        }
    }
}