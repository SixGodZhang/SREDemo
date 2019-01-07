using System;

namespace SREDemo
{
    public class Vector3
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public static readonly Vector3 Zero = new Vector3(0, 0, 0);
        public static readonly Vector3 Up = new Vector3(0, 1, 0);
        public static readonly Vector3 Down = new Vector3(0, -1, 0);
        public static readonly Vector3 Right = new Vector3(1, 0, 0);
        public static readonly Vector3 Left = new Vector3(-1, 0, 0);
        public static readonly Vector3 Front = new Vector3(0, 0, 1);
        public static readonly Vector3 Back = new Vector3(0, 0, -1);

        #region 构造函数
        public Vector3() { }

        public Vector3(float x, float y, float z)
        {
            this.x = x;this.y = y;this.z = z;this.w = 1;
        }

        public Vector3(float x, float y, float z, float w)
        {
            this.x = x;this.y = y;this.z = z;this.w = w;
        }

        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1}, {2:F1})", x, y, z);
        }
        #endregion

        /// <summary>
        /// 长度
        /// </summary>
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(x * x + y * y + z * z);
            }
        }

        /// <summary>
        /// 矢量归一化
        /// </summary>
        /// <returns></returns>
        public Vector3 Normalize()
        {

            float length = Length;

            if (length != 0)
            {
                float s = 1 / length;
                x *= s;
                y *= s;
                z *= s;
            }
            return this;
        }

        #region 重载
        public static Vector3 operator +(Vector3 a, Vector3 b) { return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z); }
        public static Vector3 operator -(Vector3 a, Vector3 b) { return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z); }
        public static Vector3 operator -(Vector3 a) { return new Vector3(-a.x, -a.y, -a.z); }
        public static Vector3 operator *(Vector3 a, float d) { return new Vector3(a.x * d, a.y * d, a.z * d); }
        public static Vector3 operator *(float d, Vector3 a) { return new Vector3(a.x * d, a.y * d, a.z * d); }
        public static Vector3 operator /(Vector3 a, float d) { return new Vector3(a.x / d, a.y / d, a.z / d); }

        /// <summary>
        /// 向量和矩阵相乘
        /// </summary>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 left, Matrix4 matrix)
        {
            Vector3 v = new Vector3();
            v.x = left.x * matrix[0, 0] + left.y * matrix[1, 0] + left.z * matrix[2, 0] + left.w * matrix[3, 0];
            v.y = left.x * matrix[0, 1] + left.y * matrix[1, 1] + left.z * matrix[2, 1] + left.w * matrix[3, 1];
            v.z = left.x * matrix[0, 2] + left.y * matrix[1, 2] + left.z * matrix[2, 2] + left.w * matrix[3, 2];
            v.w = left.x * matrix[0, 3] + left.y * matrix[1, 3] + left.z * matrix[2, 3] + left.w * matrix[3, 3];
            return v;
        }
        #endregion

        #region static level Methods

        public static float Dot(Vector3 left, Vector3 right)
        {
            return left.x * right.x + left.y * right.y + left.z * right.z;
        }

        /// <summary>
        /// 向量插值
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Vector3 Lerp(Vector3 left, Vector3 right, float factor)
        {
            Vector3 vec = new Vector3();

            vec.x = Utility.Lerp(left.x, right.x, factor);
            vec.y = Utility.Lerp(left.y, right.y, factor);
            vec.z = Utility.Lerp(left.z, right.z, factor);
            vec.w = 0;

            return vec;
        }

        /// <summary>
        /// 向量乘法
        /// </summary>
        /// <returns></returns>
        public static Vector3 Cross(Vector3 left, Vector3 right)
        {
            float x = left.y * right.z - left.z * right.y;
            float y = left.z * right.x - left.x * right.z;
            float z = left.x * right.y - left.y * right.x;
            return new Vector3(x, y, z, 0);
        }

        /// <summary>
        /// 两个点的距离
        /// </summary>
        public static float Distance(Vector3 left, Vector3 right)
        {
            Vector3 to = left - right;
            return to.Length;
        }

        #endregion
    }
}
