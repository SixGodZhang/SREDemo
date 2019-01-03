using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace SREDemo
{
    public partial class SRWindow : Form
    {
        private Graphics canvas;
        private Bitmap buffer;

        public SRWindow()
        {
            InitializeComponent();

            canvas = this.CreateGraphics();
            buffer = new Bitmap(this.Width, this.Height);
            RenderCore.FrameBuffer = buffer;

            RenderTimer();
        }

        /// <summary>
        /// 渲染定时器
        /// </summary>
        public void RenderTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer(1000 / 60);
            timer.Elapsed += new ElapsedEventHandler(OnUpdate);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();
        }

        private void OnUpdate(object sender, ElapsedEventArgs e)
        {
            lock (buffer)
            {
                ClearBuffer(Color.Black);
                RenderingData();
                canvas.DrawImage(buffer, 0, 0);
            }
        }

        private void RenderingData()
        {
            ////水平线
            //RenderCore.DrawSpecialLine(100, 100, 200, 100);
            //////垂直线
            //RenderCore.DrawSpecialLine(100, 100, 100, 200);
            ////45度斜线
            //RenderCore.DrawSpecialLine(100, 100, 200, 200);
            ////非45度斜线
            //RenderCore.DrawSpecialLine(100, 100, 200, 150);
            //RenderCore.DrawSpecialLine(100, 100, 200, 110);
            //RenderCore.DrawSpecialLine(100, 100, 200, 180);
        }

        /// <summary>
        /// clear bitmap buffer
        /// </summary>
        public void ClearBuffer(Color color = default(Color))
        {
            for (int x = 0; x < buffer.Width; ++x)
                for (int y = 0; y < buffer.Height; ++y)
                    buffer.SetPixel(x, y, color == default(Color)? Color.Black:color);
        }
    }
}
