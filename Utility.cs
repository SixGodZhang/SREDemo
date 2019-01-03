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
    }
}
