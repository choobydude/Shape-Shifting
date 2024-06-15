using UnityEngine;

namespace WhackAMole
{
    public static class VectorExtensions 
    {
        public static Vector3 ToXZPlane3D(this Vector2 i_Vector)
        {
            return new Vector3(i_Vector.x, 0, i_Vector.y);
        }
    }
}