using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SREDemo
{
    public class Vertex
    {
        public Vector3 position;
        public Vector3 normal;
        public float u, v;
        /// <summary>
        /// w 齐次倒数
        /// </summary>
        public float rhw;
        public Color4 color;

        public Vertex()
        {
            position = new Vector3();
            normal = new Vector3();
            u = v = 0;
            rhw = 1f;
            color = new Color4();
        }

        public Vertex(Vertex v)
        {
            this.position = v.position;
            this.normal = v.normal;
            this.u = v.u;
            this.v = v.v;
            this.color = v.color;
            this.rhw = v.rhw;
        }

        //test
        public Vertex(Vector3 v)
        {
            this.position = v;
            this.normal = new Vector3(0, 0, 0, 0);
            this.u = 0;
            this.v = 0;
            this.color = new Color4(Color.Red);
            this.rhw = 1;
        }

        public override string ToString()
        {
            string pos = string.Format("position : ({0}, {1}, {2}, {3})\n", position.x, position.y, position.z, position.w);
            string uv = string.Format("uv : ({0}, {1})\n", u, v);
            string rhw = string.Format("rhw : {0}\n", this.rhw);
            string inz = string.Format("1/rhw : {0}\n", 1.0f / this.rhw);
            string normal = string.Format("normal : ({0}, {1}, {2}, {3})\n", this.normal.x, this.normal.y, this.normal.z, this.normal.w);
            string color = string.Format("color : [R : {0}, G : {1}, B : {2}, A : {3}]\n", this.color.R, this.color.G, this.color.B, this.color.A);

            return pos + uv + rhw + inz + normal + color;
        }

        #region static方法

        public static Vertex Lerp(Vertex min, Vertex max, float factor)
        {
            Vertex v = new Vertex();

            v.position = Vector3.Lerp(min.position, max.position, factor);
            v.rhw = Utility.Lerp(min.rhw, max.rhw, factor);
            v.u = Utility.Lerp(min.u, max.u, factor);
            v.v = Utility.Lerp(min.v, max.v, factor);
            v.color = Color4.Lerp(min.color, max.color, factor);

            return v;
        }

        #endregion

    }
}
