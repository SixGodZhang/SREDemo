﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SREDemo
{
    public class Cube
    {
        //顶点坐标
        public static Vector3[] positions = {

                new Vector3(-1,  1, -1),
                new Vector3(-1, -1, -1),
                new Vector3(1, -1, -1),
                new Vector3(1, 1, -1),

                new Vector3( -1,  1, 1),
                new Vector3(-1, -1, 1),
                new Vector3(1, -1, 1),
                new Vector3(1, 1, 1)

        };

        //顶点索引,12个面
        public static int[,] indices = {

             { 0,1,2 },
             { 0,2,3 },

             { 7,6,5 },
             { 7,5,4 },

             { 0,4,5 },
             { 0,5,1 },

             { 1,5,6 },
             { 1,6,2 },

             { 2,6,7 },
             { 2,7,3 },

             { 3,7,4 },
             { 3,4,0 }
        };

        //顶点颜色,8个顶点的不同颜色
        public static Color4[] colors = {

            new Color4(Color.Red),
            new Color4(Color.Yellow),
            new Color4(Color.Blue),
            new Color4(Color.BlueViolet),

            new Color4(Color.Green),
            new Color4(Color.HotPink),
            new Color4(Color.Ivory),
            new Color4(Color.Indigo)
        };
    }
}
