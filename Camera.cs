using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SREDemo
{
    /// <summary>
    /// 照相机
    /// </summary>
    public class Camera
    {
        #region 字段
        /// <summary>
        /// 摄像机在世界空间下的坐标
        /// </summary>
        public Vector3 pos;

        /// <summary>
        /// 观察目标点
        /// </summary>        
        public Vector3 target;

        /// <summary>
        /// 自定义的竖直方向,这里取(0,0,1)
        /// </summary>
        public Vector3 up;

        /// <summary>
        /// y的视域视域,弧度
        /// </summary>
        public float fov;

        /// <summary>
        /// 宽高比
        /// </summary>
        public float aspect;

        /// <summary>
        /// 近平面
        /// </summary>
        public float zn;

        /// <summary>
        /// 远平面
        /// </summary>
        public float zf;
        #endregion

        #region 构造函数
        public Camera(Vector3 pos, Vector3 target, Vector3 up, float fov, float aspect, float zn, float zf)
        {
            this.pos = pos;
            this.target = target;
            this.up = up;
            this.fov = fov;
            this.aspect = aspect;
            this.zn = zn;
            this.zf = zf;
        }
        #endregion
    }
}
