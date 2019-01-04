using System;

namespace SREDemo
{
    /// <summary>
    /// 矩阵
    /// </summary>
    public class Matrix4
    {
        public float[,] matrix = new float[4,4];

        public float this[int i, int j]
        {
            get { return matrix[i, j]; }
            set { matrix[i, j] = value; }
        }

        #region 构造函数

        public Matrix4()
        {
            SetZeroMatrix();
        }

        public Matrix4(float a1,float a2,float a3,float a4,
                       float b1,float b2,float b3,float b4,
                       float c1,float c2,float c3,float c4,
                       float d1,float d2,float d3,float d4)
        {
            //1.
            matrix[0, 0] = a1;
            matrix[0, 1] = a2;
            matrix[0, 2] = a3;
            matrix[0, 3] = a4;

            //2
            matrix[1, 0] = b1;
            matrix[1, 1] = b2;
            matrix[1, 2] = b3;
            matrix[1, 3] = b4;

            //3.
            matrix[2, 0] = c1;
            matrix[2, 1] = c2;
            matrix[2, 2] = c3;
            matrix[2, 3] = c4;

            //4.
            matrix[3, 0] = d1;
            matrix[3, 1] = d2;
            matrix[3, 2] = d3;
            matrix[3, 3] = d4;
        }
        #endregion

        #region 重载

        // c = a * b
        public static Matrix4 operator *(Matrix4 left, Matrix4 right)
        {
            Matrix4 result = new Matrix4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        result[i, j] += left[i, k] * right[k, j];
                    }
                }
            }

            return result;
        }

        //c = a * f
        public static Matrix4 operator *(Matrix4 sMatrix4, float factor)
        {
            Matrix4 result = new Matrix4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    sMatrix4[i,j] = sMatrix4[i, j] * factor;
                }
            }
            return result;
        }

        //c = f * a
        public static Matrix4 operator *(float factor, Matrix4 sMatrix4)
        {
            return sMatrix4 * factor;
        }
        #endregion

        #region 静态函数

        /// <summary>
        /// 缩放矩阵
        /// </summary>
        /// <param name="x">x轴缩放因子</param>
        /// <param name="y">y轴缩放因子</param>
        /// <param name="z">z轴缩放因子</param>
        /// <returns></returns>
        public static Matrix4 ScaleMatrix(float x, float y, float z)
        {
            return new Matrix4(x, 0, 0, 0,
                               0, y, 0, 0,
                               0, 0, z, 0,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// 平移矩阵
        /// </summary>
        /// <param name="x">x方向上位移距离</param>
        /// <param name="y">y方向上位移距离</param>
        /// <param name="z">z方向上位移距离</param>
        /// <returns></returns>
        public static Matrix4 TranslateMatrix(float x, float y, float z)
        {
            return new Matrix4(1, 0, 0, 0,
                               0, 1, 0, 0,
                               0, 0, 1, 0,
                               x, y, z, 1);
        }

        /// <summary>
        /// 旋转矩阵
        /// </summary>
        /// <param name="vec">旋转轴</param>
        /// <param name="theta">弧度</param>
        /// <returns></returns>
        public static Matrix4 RotateMatrix(Vector3 vec, float theta)
        {
            Vector3 vector = vec.Normalize();
            float x = vector.x;
            float y = vector.y;
            float z = vector.z;

            float sin = (float)Math.Sin(theta);
            float cos = (float)Math.Cos(theta);

            Matrix4 result = new Matrix4();

            result[0, 0] = cos + x * x * (1 - cos);
            result[0, 1] = x * y * (1 - cos) - (z * sin);
            result[0, 2] = x * z * (1 - cos) + (y * sin);

            result[1, 0] = x * y * (1 - cos) + (z * sin);
            result[1, 1] = cos + y * y * (1 - cos);
            result[1, 2] = y * z * (1 - cos) - (x * sin);

            result[2, 0] = x * z * (1 - cos) - (y * sin);
            result[2, 1] = y * z * (1 - cos) + (x * sin);
            result[2, 2] = cos + z * z * (1 - cos);

            result[3, 3] = 1;

            return result;
        }

        /// <summary>
        /// 左手系，绕X轴旋转
        /// </summary>
        /// <param name="xTheta"></param>
        /// <returns></returns>
        public static Matrix4 RotateX(float xTheta)
        {
            float sin = (float)Math.Sin(xTheta);
            float cos = (float)Math.Cos(xTheta);

            return new Matrix4(1, 0, 0, 0,
                               0, cos, sin, 0,
                               0, -sin, cos, 0,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// 左手系，绕Y轴旋转
        /// </summary>
        /// <param name="yTheta">y轴旋转弧度</param>
        /// <returns></returns>
        public static Matrix4 RotateY(float yTheta)
        {
            float sin = (float)Math.Sin(yTheta);
            float cos = (float)Math.Cos(yTheta);

            return new Matrix4(cos, -sin, 0, 0,
                               0,   1,    0, 0,
                               sin ,0,   cos,0,
                               0,   0,    0, 1);
        }

        /// <summary>
        /// 左手系,绕z轴旋转
        /// </summary>
        /// <param name="zTheta">z轴旋转弧度</param>
        /// <returns></returns>
        public static Matrix4 RotateZ(float zTheta)
        {
            float sin = (float)Math.Sin(zTheta);
            float cos = (float)Math.Cos(zTheta);

            return new Matrix4(cos, sin, 0, 0,
                               -sin, cos, 0, 0,
                               0, 0, 1, 0,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// 投影矩阵
        /// </summary>
        /// <param name="flov">视椎角</param>
        /// <param name="aspect">屏幕宽高比</param>
        /// <param name="near">近平面距离</param>
        /// <param name="far">远平面距离</param>
        /// <returns></returns>
        public static Matrix4 PerspectiveMatrix(float flov, float aspect, float near, float far)
        {
            Matrix4 result = new Matrix4();
            float tan = (float)Math.Tan(flov*0.5f);

            result[0, 0] = 1 / (tan * aspect);
            result[1, 1] = 1 / tan;
            result[2, 2] = far / (far - near);
            result[2, 3] = 1.0f;
            result[3, 2] = -(near * far) / (far - near);

            return result;
        }

        /// <summary>
        /// 世界矩阵
        /// </summary>
        /// <returns></returns>
        public static Matrix4 WorldMatrix(Matrix4 scale, Matrix4 rotate, Matrix4 translate)
        {
            return scale * rotate * translate;
        }

        /// <summary>
        /// 根据camera的属性建立world->view变换矩阵
        /// </summary>
        /// <param name="camera">UVN系统的摄像机</param>
        /// <returns></returns>
        public static Matrix4 ViewMatrix(Camera camera)
        {
            Vector3 axisZ = (camera.target - camera.pos).Normalize();
            Vector3 axisX = Vector3.Cross(camera.up, axisZ).Normalize();
            Vector3 axisY = Vector3.Cross(axisZ, axisX).Normalize();

            Matrix4 rotation = new Matrix4(axisX.x, axisY.x, axisZ.x, 0,
                                               axisX.y, axisY.y, axisZ.y, 0,
                                               axisX.z, axisY.z, axisZ.z, 0,
                                               0, 0, 0, 1);
            Matrix4 trans = TranslateMatrix(-camera.pos.x, -camera.pos.y, -camera.pos.z);

            return rotation * trans;

        }

        /// <summary>
        /// 转置矩阵
        /// </summary>
        /// <returns></returns>
        public Matrix4 Transpose()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = i; j < 4; j++)
                {
                    //MathHelper.Swap<float>(ref matrix[i, j], ref matrix[j, i]);
                }
            }
            return this;
        }

        public Matrix4 Inverse()
        {
            float a = Determinate();
            if (a == 0)
            {
                Console.WriteLine("矩阵不可逆");
                return null;
            }
            Matrix4 adj = GetAdjoint();//伴随矩阵
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    adj.matrix[i, j] = adj.matrix[i, j] / a;
                }
            }
            return adj;
        }

        /// <summary>
        /// 伴随矩阵
        /// </summary>
        /// <returns></returns>
        public Matrix4 GetAdjoint()
        {
            int x, y;
            float[,] tempM = new float[3, 3];
            Matrix4 result = new Matrix4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int t = 0; t < 3; ++t)
                        {
                            x = k >= i ? k + 1 : k;
                            y = t >= j ? t + 1 : t;

                            tempM[k, t] = matrix[x, y];
                        }
                    }
                    result.matrix[i, j] = (float)System.Math.Pow(-1, (1 + j) + (1 + i)) * Determinate(tempM, 3);
                }
            }
            return result.Transpose();
        }

        /// <summary>
        /// 求四阶矩阵的行列式
        /// </summary>
        /// <returns></returns>
        public float Determinate()
        {
            return Determinate(matrix, 4);
        }

        /// <summary>
        /// 求n阶矩阵的行列式
        /// </summary>
        public float Determinate(float[,] m, int n)
        {
            if (n == 1)
            {
                return m[0, 0];
            }
            else
            {
                float result = 0;
                float[,] tempM = new float[n - 1, n - 1];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n - 1; j++)
                    {
                        for (int k = 0; k < n - 1; k++)
                        {
                            int x = j + 1;
                            int y = k >= i ? k + 1 : k;
                            tempM[j, k] = m[x, y];
                        }
                    }

                    result += (float)System.Math.Pow(-1, 1 + (1 + i)) * m[0, i] * Determinate(tempM, n - 1);
                }
                return result;
            }
        }

        /// <summary>
        /// 设置单位矩阵
        /// </summary>
        /// <returns></returns>
        public static Matrix4 SetIdentityMatrix4()
        {
            Matrix4 identity = new Matrix4();
            //1.
            identity.matrix[0, 0] = 1;
            identity.matrix[0, 1] = 0;
            identity.matrix[0, 2] = 0;
            identity.matrix[0, 3] = 0;

            //2
            identity.matrix[1, 0] = 0;
            identity.matrix[1, 1] = 1;
            identity.matrix[1, 2] = 0;
            identity.matrix[1, 3] = 0;

            //3.
            identity.matrix[2, 0] = 0;
            identity.matrix[2, 1] = 0;
            identity.matrix[2, 2] = 1;
            identity.matrix[2, 3] = 0;

            //4.
            identity.matrix[3, 0] = 0;
            identity.matrix[3, 1] = 0;
            identity.matrix[3, 2] = 0;
            identity.matrix[3, 3] = 1;
            return identity;
        }

        /// <summary>
        /// 设置0矩阵
        /// </summary>
        public void SetZeroMatrix()
        {
            matrix[0, 0] = 0;
            matrix[0, 1] = 0;
            matrix[0, 2] = 0;
            matrix[0, 3] = 0;

            //2
            matrix[1, 0] = 0;
            matrix[1, 1] = 0;
            matrix[1, 2] = 0;
            matrix[1, 3] = 0;

            //3.
            matrix[2, 0] = 0;
            matrix[2, 1] = 0;
            matrix[2, 2] = 0;
            matrix[2, 3] = 0;

            //4.
            matrix[3, 0] = 0;
            matrix[3, 1] = 0;
            matrix[3, 2] = 0;
            matrix[3, 3] = 0;
        }

        #endregion

    }
}
