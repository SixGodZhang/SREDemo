using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SREDemo.Data
{
    public class Triangle
    {
        /// <summary>
        /// 位置
        /// </summary>
        public static Vector3[] positions = {
            new Vector3(-1, -1, 1),
            new Vector3(1,  -1, 1),
            new Vector3(1, -1, -1)
        };

        /// <summary>
        /// 索引
        /// </summary>
        public static int[,] indices = {
            { 0, 1, 2}
        };

        /// <summary>
        /// 颜色
        /// </summary>
        public static Color4[] colors = {
            new Color4(Color.Red),
            new Color4(Color.Green),
            new Color4(Color.Blue)
        };
    }
}
