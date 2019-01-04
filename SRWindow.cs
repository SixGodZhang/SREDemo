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
        private Camera camera;

        public SRWindow()
        {
            InitializeComponent();

            canvas = this.CreateGraphics();
            buffer = new Bitmap(this.Width, this.Height);
            RenderCore.FrameBuffer = buffer;

            //创建摄像机, 摄像机位置默认为(0,0,0),朝向z轴方向(0,0,1),以(0,1,0)为单位up向量,y方向的视角为90度,zn = 1, zf = 500
            float aspect = (float)this.Width / (float)this.Height;
            camera = new Camera(new Vector3(0, 0, 0, 1), new Vector3(0, 0, 1, 0), new Vector3(0, 1, 0, 0),
                                 3.1415926f / 4, aspect,
                                 1.0f, 500.0f);

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
                ProcessInput();
                ClearBuffer(Color.Black);
                RenderingData();
                canvas.DrawImage(buffer, 0, 0);
            }
        }

        //根据键盘的输入调整摄像机等,确定各个变换矩阵
        public void ProcessInput()
        {
            Matrix4 scale = Matrix4.ScaleMatrix(1, 1, 1);
            Matrix4 rotation = Matrix4.RotateMatrix(new Vector3(0, 1, 0, 0), 0f);
            Matrix4 translate = Matrix4.TranslateMatrix(0, 0, 10);

            //1.设定world矩阵
            Transform.world = Matrix4.WorldMatrix(scale, rotation, translate);

            //2.设定view变换矩阵
            Transform.view = Matrix4.ViewMatrix(camera);

            //3.设定projection矩阵
            Transform.projection = Matrix4.PerspectiveMatrix(camera.fov, camera.aspect, camera.zn, camera.zf);

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

            ////平顶三角形
            //Vertex p1 = new Vertex(new Vector3(-1, 1, 1));
            //Vertex p2 = new Vertex(new Vector3(1, 1, 1));
            //Vertex p3 = new Vertex(new Vector3(0, -1, 1));
            //RenderCore.DrawTriangle(p1, p2, p3);

            ////平底三角形
            //Vertex p4 = new Vertex(new Vector3(0, 1, 1));
            //Vertex p5 = new Vertex(new Vector3(1, -1, 1));
            //Vertex p6 = new Vertex(new Vector3(-1, -1, 1));
            //RenderCore.DrawTriangle(p4, p5, p6);

            //任意三角形
            Vertex p7 = new Vertex(new Vector3(0, 1, 1));
            Vertex p8 = new Vertex(new Vector3(1, 0.5f, 1));
            Vertex p9 = new Vertex(new Vector3(-1, -1, 1));
            RenderCore.DrawTriangle(p7, p8, p9);
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
