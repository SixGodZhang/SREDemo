using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SREDemo
{
    public class Color4
    {
        #region 字段&属性
        public float r;
        public float g;
        public float b;
        public float a;

        public float R
        {
            get {
                r = Utility.ClampValue(r, 0, 1);
                return r;
            }

            set
            {
                r = Utility.ClampValue(value, 0, 1);
            }
        }

        public float G
        {
            get
            {
                g = Utility.ClampValue(g, 0, 1);
                return g;
            }

            set
            {
                g = Utility.ClampValue(value, 0, 1);
            }
        }

        public float B
        {
            get
            {
                b = Utility.ClampValue(b, 0, 1);
                return b;
            }

            set
            {
                b = Utility.ClampValue(value, 0, 1);
            }
        }

        public float A
        {
            get
            {
                a = Utility.ClampValue(a, 0, 1);
                return a;
            }

            set
            {
                a = Utility.ClampValue(value, 0, 1);
            }
        }
        #endregion

        #region 构造函数
        public Color4(float r = 0, float g = 0, float b = 0, float a = 1)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color4(Color c)
        {
            R = c.R / 255f;
            G = c.G / 255f;
            B = c.B / 255f;
            A = c.A / 255f;
        }
        #endregion

        #region 公开接口
        public override string ToString()
        {
            return string.Format("R {0}, G {1}, B {2}, A {3}", R, G, B, A);
        }

        /// <summary>
        /// 颜色固定插值
        /// </summary>
        public static Color4 Lerp(Color4 min, Color4 max, float factor)
        {
            Color4 c = new Color4();

            c.R = Utility.Lerp(min.R, max.R, factor);
            c.G = Utility.Lerp(min.G, max.G, factor);
            c.B = Utility.Lerp(min.B, max.B, factor);
            c.A = 1;

            return c;
        }

        /// <summary>
        /// 强转
        /// </summary>
        /// <param name="c"></param>
        public static implicit operator Color(Color4 c)
        {
            float r = c.R * 255f;
            float g = c.G * 255f;
            float b = c.B * 255f;
            float a = c.A * 255f;

            return Color.FromArgb((int)a, (int)r, (int)g, (int)b);
        }
        #endregion

        #region 重载运算符
        public static Color4 operator *(float factor, Color4 c)
        {
            return new Color4(c.r * factor, c.g * factor, c.b * factor, 1.0f);
        }

        public static Color4 operator *(Color4 c, float factor)
        {
            return factor * c;
        }

        public static Color4 operator *(Color4 lhs, Color4 rhs)
        {
            return new Color4(lhs.r * rhs.r, lhs.g * rhs.g, lhs.b * rhs.b, 1.0f);
        }

        public static Color4 operator +(Color4 lhs, Color4 rhs)
        {
            return new Color4(lhs.r + rhs.r, lhs.g + rhs.g, lhs.b + rhs.b, 1.0f);
        }
        #endregion
    }
}
