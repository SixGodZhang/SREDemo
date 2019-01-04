namespace SREDemo
{
    public class Transform
    {
        public static Matrix4 world;
        public static Matrix4 view;
        public static Matrix4 projection;

        public static Matrix4 transform
        {
            get
            {
                return world * view * projection;
            }
        }

        #region 空间变换操作
        /// <summary>
        /// v 从局部坐标系变换到世界坐标系
        /// </summary>
        /// <param name="v"></param>
        public static void Local2World(ref Vertex v)
        {
            v.position = v.position * world;
            v.normal = (v.normal * world.Inverse().Transpose()).Normalize();
        }

        /// <summary>
        /// v 从世界坐标系变化到视图空间
        /// </summary>
        /// <param name="v"></param>
        public static void World2View(ref Vertex v)
        {
            v.position = v.position * view;
        }

        /// <summary>
        /// 视图空间到裁剪空间
        /// </summary>
        /// <param name="v"></param>
        public static void View2Homogeneous(ref Vertex v)
        {
            v.position = v.position * projection;

            float rhw = 1.0f / v.position.w;
            v.rhw = rhw;
            v.u *= rhw;
            v.v *= rhw;
            v.color *= rhw;
        }

        /// <summary>
        /// 把V从标准裁剪空间换到视口
        /// </summary>
        /// <param name="v"></param>
        /// <param name="width">视口的宽度</param>
        /// <param name="height">视口的高度</param>
        /// <returns></returns>
        public static void Homogeneous2Viewport(ref Vertex v, int width, int height)
        {
            if (v.position.w != 0)
            {
                //1.透视除法,完成从三维坐标到二维坐标的映射
                //reciprocal homogeneous w   reciprocal->倒数 homogeneous -> 齐次
                //float rhw = 1.0f / v.position.w;

                float rhw = v.rhw;

                v.position.x *= rhw;
                v.position.y *= rhw;
                v.position.z *= rhw;
                v.position.w = 1.0f;

                //2.屏幕映射
                //调整x,y到输出窗口 这里实际上也是利用了线性插值
                //x[-1,1] -> x'[0,width]      (x'-0)/(width-0) = (x -(-1))/(1-(-1))     整理得  x' = (x + 1) * 0.5 * width
                v.position.x = (v.position.x + 1.0f) * 0.5f * width;

                //y变换时需要特别注意,在屏幕上y是向下增长的,需要倒转y轴
                //-y[-1,1] -> y'[0, height]     (y'-0)/(height-0) = (-y -(-1))/(1-(-1))  整理得  y' = (1 - y) * 0.5 * height
                v.position.y = (1.0f - v.position.y) * 0.5f * height;

            }

        }
        #endregion
    }
}
