using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SREDemo
{
    public class Utility
    {
        /// <summary>
        /// 确保左值小于右值
        /// </summary>
        public static void EnsureLeftLowerRight(ref int left,ref int right)
        {
            if (left <= right)
                return;
            int temp = left;
            left = right;
            right = temp;

            return;
        }

        /// <summary>
        /// 限制值在某一个范围内
        /// </summary>
        /// <returns></returns>
        public static float ClampValue(float value, float min, float max)
        {
            if (value <= min)
                return min;
            else if (value >= max)
                return max;
            else
                return value;
        }

        /// <summary>
        /// 按比例差值
        /// </summary>
        public static float Lerp(float min, float max, float factor)
        {
            if (factor <= 0)
                return min;
            else if (factor >= 1)
                return max;
            else
                return min + (max - min) * factor;
        }

        //float四舍五入到int
        public static int RoundToInt(float f)
        {

            int x = (int)f;
            float frac = f - x;

            if (frac > 0.5f)
                return x + 1;

            return x;

        }
    }
}
