namespace SREDemo
{
    public class Vector3
    {
        public float x;
        public float y;
        public float z;

        public static readonly Vector3 Zero = new Vector3(0, 0, 0);
        public static readonly Vector3 Up = new Vector3(0, 1, 0);
        public static readonly Vector3 Down = new Vector3(0, -1, 0);
        public static readonly Vector3 Right = new Vector3(1, 0, 0);
        public static readonly Vector3 Left = new Vector3(-1, 0, 0);
        public static readonly Vector3 Front = new Vector3(0, 0, 1);
        public static readonly Vector3 Back = new Vector3(0, 0, -1);

        public Vector3() { }

        public Vector3(float x, float y, float z)
        {
            this.x = x;this.y = y;this.z = z;;
        }

        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1}, {2:F1})", x, y, z);
        }

        #region 重载
        public static Vector3 operator +(Vector3 a, Vector3 b) { return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z); }
        public static Vector3 operator -(Vector3 a, Vector3 b) { return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z); }
        public static Vector3 operator -(Vector3 a) { return new Vector3(-a.x, -a.y, -a.z); }
        public static Vector3 operator *(Vector3 a, float d) { return new Vector3(a.x * d, a.y * d, a.z * d); }
        public static Vector3 operator *(float d, Vector3 a) { return new Vector3(a.x * d, a.y * d, a.z * d); }
        public static Vector3 operator /(Vector3 a, float d) { return new Vector3(a.x / d, a.y / d, a.z / d); }
        #endregion
    }
}
