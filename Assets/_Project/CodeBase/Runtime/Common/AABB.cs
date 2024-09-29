using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Common
{
    public struct AABB
    {
        public Vector3 Min { get; set; }
        public Vector3 Max { get; set; }
        
        public Vector3 Center => (Min + Max) / 2;
        public List<Vector3> Points => new List<Vector3>
        {
            new Vector3(Min.x, Min.y, Min.z),
            new Vector3(Max.x, Min.y, Min.z),
            new Vector3(Min.x, Max.y, Min.z),
            new Vector3(Min.x, Min.y, Max.z),
            new Vector3(Max.x, Max.y, Min.z),
            new Vector3(Min.x, Max.y, Max.z),
            new Vector3(Max.x, Min.y, Max.z),
            new Vector3(Max.x, Max.y, Max.z)
        };
        
        public List<Vector3> HalfPoints => new List<Vector3>
        {
            new Vector3(Center.x, Min.y, Center.z),
            new Vector3(Center.x, Max.y, Center.z),
            new Vector3(Min.x, Center.y, Center.z),
            new Vector3(Max.x, Center.y, Center.z),
            new Vector3(Center.x, Center.y, Min.z),
            new Vector3(Center.x, Center.y, Max.z)
        };
        
        public AABB(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }

        public bool Contains(Vector3 point)
        {
            return point.x >= Min.x && point.x <= Max.x &&
                   point.y >= Min.y && point.y <= Max.y &&
                   point.z >= Min.z && point.z <= Max.z;
        }

        public bool Intersects(AABB other)
        {
            return Min.x <= other.Max.x && Max.x >= other.Min.x &&
                   Min.y <= other.Max.y && Max.y >= other.Min.y &&
                   Min.z <= other.Max.z && Max.z >= other.Min.z;
        }
    }
}