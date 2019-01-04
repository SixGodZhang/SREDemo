using System;
using System.Drawing;

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

        public static void DrawTriangle(Vertex p1, Vertex p2, Vertex p3)
        {
            //1.局部=>世界
            Transform.Local2World(ref p1);
            Transform.Local2World(ref p2);
            Transform.Local2World(ref p3);

            //2.世界=>视图
            Transform.World2View(ref p1);
            Transform.World2View(ref p2);
            Transform.World2View(ref p3);

            //3.视图=>裁剪
            Transform.View2Homogeneous(ref p1);
            Transform.View2Homogeneous(ref p2);
            Transform.View2Homogeneous(ref p3);

            //4.裁剪=>视口
            Transform.Homogeneous2Viewport(ref p1, _frameBuffer.Width, _frameBuffer.Height);
            Transform.Homogeneous2Viewport(ref p2, _frameBuffer.Width, _frameBuffer.Height);
            Transform.Homogeneous2Viewport(ref p3, _frameBuffer.Width, _frameBuffer.Height);

            //5.光栅化
            RasterizationTriangle(p1, p2, p3);
        }

        /// <summary>
        /// 光栅化三角形
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        private static void RasterizationTriangle(Vertex v1, Vertex v2, Vertex v3)
        {
            Vertex[] sortedVertices = SortVertex(v1, v2, v3);

            if (sortedVertices == null)
                return;

            Vertex top = sortedVertices[0];
            Vertex middle = sortedVertices[1];
            Vertex bottom = sortedVertices[2];

            //平顶三角形
            if (top.position.y == middle.position.y)
            {
                RasterizationTriangleTop(top.position.x < middle.position.x ? top : middle, top.position.x < middle.position.x ? middle : top, bottom);
            }
            else if (middle.position.y == bottom.position.y)
            {
                RasterizationTriangleBottom(middle.position.x < bottom.position.x ? middle : bottom, middle.position.x < bottom.position.x ? bottom : middle, top);
            }
            else
            {
                float factor = (middle.position.y - top.position.y) / (bottom.position.y - top.position.y);
                //找到一个新的middle
                Vertex newMiddle = Vertex.Lerp(top, bottom, factor);

                Vertex left;
                Vertex right;

                if (newMiddle.position.x < middle.position.x)
                {
                    left = newMiddle;
                    right = middle;
                }
                else
                {
                    left = middle;
                    right = newMiddle;
                }

                //绘制平底三角形
                RasterizationTriangleBottom(left, right, top);

                //绘制平顶三角形
                RasterizationTriangleTop(left, right, bottom);
            }
        }

        /// <summary>
        /// 画平底三角形
        /// </summary>
        private static void RasterizationTriangleBottom(Vertex bottomLeft, Vertex bottomRight, Vertex top)
        {
            //paint top point
            int topx = Utility.RoundToInt(top.position.x);
            int topy = Utility.RoundToInt(top.position.y);
            float topw = 1 / top.rhw;
            Color4 color = top.color * topw;
            _frameBuffer.SetPixel(topx, topy, color);

            float miny = top.position.y;
            float maxy = bottomLeft.position.y;

            for (float y = miny + 1.0f; y <= maxy; y+=1.0f)
            {
                float factor = (y - miny) / (maxy - miny);
                Vertex left = Vertex.Lerp(top, bottomLeft, factor);
                Vertex right = Vertex.Lerp(top, bottomRight, factor);

                int yindex = Utility.RoundToInt(y);

                ScanlineFill(left, right, yindex);    
            }
        }

        /// <summary>
        /// 画平顶三角形
        /// </summary>
        private static void RasterizationTriangleTop(Vertex topLeft, Vertex topRight, Vertex bottom)
        {
            float miny = topLeft.position.y;
            float maxy = bottom.position.y;

            for (float y = miny; y < maxy; y += 1.0f)
            {
                float factor = (y - miny) / (maxy - miny);
                Vertex left = Vertex.Lerp(topLeft, bottom, factor);
                Vertex right = Vertex.Lerp(topRight, bottom, factor);
                int yindex = Utility.RoundToInt(y);

                ScanlineFill(left, right, yindex);

            }

            //画bottom点
            int bottomx = Utility.RoundToInt(bottom.position.x);
            int bottomy = Utility.RoundToInt(bottom.position.y);
            float bottomw = 1 / bottom.rhw;
            Color4 color = bottom.color * bottomw;
            _frameBuffer.SetPixel(bottomx, bottomy, color);
        }

        /// <summary>
        /// 填充扫描线
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="yindex"></param>
        private static void ScanlineFill(Vertex left, Vertex right, int yindex)
        {
            float minx = left.position.x;
            float maxx = right.position.x;

            for (float x = minx; x <= maxx; x += 1.0f)
            {
                int xindex = Utility.RoundToInt(x);
                float factor = (x - minx) / (maxx - minx);

                Vertex v = Vertex.Lerp(left, right, factor);

                //顶点颜色
                Color4 vertexColor = v.color * (1 / v.rhw);
                _frameBuffer.SetPixel(xindex, yindex, vertexColor);
            }

        }

        //===================================================================
        //               
        //                    0----------------------------> X
        //                    |
        //                    |
        //                    |
        //                    |
        //                    |
        //                    |
        //                    |
        //                    V
        //                    Y
        /// <summary>
        /// 排序顶点
        /// </summary>
        /// <returns></returns>
        private static Vertex[] SortVertex(Vertex v1, Vertex v2, Vertex v3)
        {
            Vertex[] result = new Vertex[3];

            Vertex top = new Vertex();
            Vertex middle = new Vertex();
            Vertex bottom = new Vertex();

            if (v1.position.y == v2.position.y && v2.position.y == v3.position.y)
            {//三点共线
                return null;
            }

            //1.v1 > v2 > v3
            if (v1.position.y >= v2.position.y && v2.position.y >= v3.position.y)
            {
                top = v3;
                middle = v2;
                bottom = v1;
            }
            //2.v1 > v3 > v2
            else if (v1.position.y >= v3.position.y && v3.position.y >= v2.position.y)
            {
                top = v2;
                middle = v3;
                bottom = v1;
            }
            //3.v2 > v1 > v3
            else if (v2.position.y >= v1.position.y && v1.position.y >= v3.position.y)
            {
                top = v3;
                middle = v1;
                bottom = v2;
            }
            //4.v2 > v3 > v1
            else if (v2.position.y >= v3.position.y && v3.position.y >= v1.position.y)
            {
                top = v1;
                middle = v3;
                bottom = v2;
            }
            //5.v3 > v1 > v2
            else if (v3.position.y >= v1.position.y && v1.position.y >= v2.position.y)
            {
                top = v2;
                middle = v1;
                bottom = v3;
            }
            //6.v3 > v2 > v1
            else if (v3.position.y >= v2.position.y && v2.position.y >= v1.position.y)
            {
                top = v1;
                middle = v2;
                bottom = v3;
            }

            result[0] = top;
            result[1] = middle;
            result[2] = bottom;

            return result;

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
