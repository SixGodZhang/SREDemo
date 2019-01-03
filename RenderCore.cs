using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SREDemo
{
    public class RenderCore
    {
        private static Bitmap _frameBuffer;
        public static Bitmap FrameBuffer
        {
            get { return _frameBuffer; }
            set { _frameBuffer = value; }
        }

        /// <summary>
        /// 画线
        /// </summary>
        public static void DrawSpecialLine(int x1,int y1,int x2,int y2)
        {
            if (Math.Abs(y1 - y2) < 0.001f)
            {//y1 == y2
                Utility.EnsureLeftLowerRight(ref x1, ref x2);
                for (int i = x1; i < x2; i++)
                    _frameBuffer.SetPixel(i, y1, Color.Red);
            }
            else if (Math.Abs(x1 - x2) < 0.001f)
            {//x1 == x2
                Utility.EnsureLeftLowerRight(ref y1, ref y2);
                for (int i = y1; i < y2; i++)
                    _frameBuffer.SetPixel(x1, i, Color.Red);
            }
            else
            {
                int dx = Math.Abs(x1 - x2);
                int dy = Math.Abs(y1 - y2);
                int tempX, tempY;

                if (dx == dy)
                {
                    //保证x1 < x2
                    if (x1 > x2)
                    {
                        tempX = x1;
                        x1 = x2;
                        x2 = tempX;

                        tempY = y1;
                        y1 = y2;
                        y2 = tempY;
                    }

                    for (int x = x1,y =y1; x < x2; x++)
                    {
                        y += (y2 >= y1) ? 1 : -1;
                        _frameBuffer.SetPixel(x, y, Color.Red);
                    }
                }
                else if (dx < dy)
                {//y的增长速度大于x

                    //保证x1 < x2
                    if (x1 > x2)
                    {
                        tempX = x1;
                        x1 = x2;
                        x2 = tempX;

                        tempY = y1;
                        y1 = y2;
                        y2 = tempY;
                    }

                    //dx每移动多少，增加一个y
                    int delta = (dx % dy) == 0 ? 0 : dy / (dx % dy);
                    //整数比关系
                    int offset = dy / dx;

                    int count = 0;//计数器
                    for (int x = x1, y = y1; x < x2; x++)
                    {
                        _frameBuffer.SetPixel(x, y, Color.Red);
                        for (int i = 0; i < offset; i++)
                        {
                            y += (y2 >= y1) ? 1 : -1;
                            _frameBuffer.SetPixel(x, y, Color.Red);
                        }
                        count++;
                        if (count >= delta)
                        {
                            count = 0;
                            y += (y2 >= y1) ? 1 : -1;
                            _frameBuffer.SetPixel(x, y, Color.Red);
                        }
                    }
                }
                else
                {//x的增长速度小于y

                    //保证x1 < x2
                    if (y1 > y2)
                    {
                        tempX = x1;
                        x1 = x2;
                        x2 = tempX;

                        tempY = y1;
                        y1 = y2;
                        y2 = tempY;
                    }

                    //dx每移动多少，增加一个y
                    int delta = (dx % dy) == 0 ? 0 : dy / (dx % dy);
                    //整数比关系
                    int offset = dx / dy;

                    int count = 0;//计数器
                    for (int y = y1, x = x1; y < y2; y++)
                    {
                        _frameBuffer.SetPixel(x, y, Color.Red);
                        for (int i = 0; i < offset; i++)
                        {
                            x += (x2 >= x1) ? 1 : -1;
                            _frameBuffer.SetPixel(x, y, Color.Red);
                        }
                        count++;
                        if (count >= delta)
                        {
                            count = 0;
                            x += (x2 >= x1) ? 1 : -1;
                            _frameBuffer.SetPixel(x, y, Color.Red);
                        }
                    }
                }
            }
        }
    }
}
