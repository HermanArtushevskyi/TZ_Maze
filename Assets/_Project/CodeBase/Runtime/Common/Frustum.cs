using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Common
{
    public struct Frustum
    {
        public Vector3 FarTopLeft { get; private set; }
        public Vector3 FarTopRight { get; private set; }
        public Vector3 FarBottomLeft { get; private set; }
        public Vector3 FarBottomRight { get; private set; }
        
        public Vector3 NearTopLeft { get; private set; }
        public Vector3 NearTopRight { get; private set; }
        public Vector3 NearBottomLeft { get; private set; }
        public Vector3 NearBottomRight { get; private set; }
        
        public Plane TopPlane { get; private set; }
        public Plane BottomPlane { get; private set; }
        public Plane LeftPlane { get; private set; }
        public Plane RightPlane { get; private set; }
        public Plane NearPlane { get; private set; }
        public Plane FarPlane { get; private set; }
        
        public List<Plane> Planes => new List<Plane>
        {
            TopPlane,
            BottomPlane,
            LeftPlane,
            RightPlane,
            NearPlane,
            FarPlane
        };
        
        public List<Vector3> Points => new List<Vector3>
        {
            FarTopLeft,
            FarTopRight,
            FarBottomLeft,
            FarBottomRight,
            NearTopLeft,
            NearTopRight,
            NearBottomLeft,
            NearBottomRight
        };
        
        public Frustum(float fov, float farDistance, float nearDistance, float aspect, Vector3 up, Vector3 right, Vector3 forward, Vector3 position)
        {
            Vector3 nearCenter = position + forward * nearDistance;
            Vector3 farCenter = position + forward * farDistance;
            
            float nearHeight = 2 * Mathf.Tan((Mathf.Deg2Rad * fov) / 2) * nearDistance;
            float farHeight = 2 * Mathf.Tan((Mathf.Deg2Rad * fov) / 2) * farDistance;
            float nearWidth = nearHeight * aspect;
            float farWidth = farHeight * aspect;
            
            FarTopLeft = farCenter + up * farHeight / 2 - right * farWidth / 2;
            FarTopRight = farCenter + up * farHeight / 2 + right * farWidth / 2;
            FarBottomLeft = farCenter - up * farHeight / 2 - right * farWidth / 2;
            FarBottomRight = farCenter - up * farHeight / 2 + right * farWidth / 2;
            
            NearTopLeft = nearCenter + up * nearHeight / 2 - right * nearWidth / 2;
            NearTopRight = nearCenter + up * nearHeight / 2 + right * nearWidth / 2;
            NearBottomLeft = nearCenter - up * nearHeight / 2 - right * nearWidth / 2;
            NearBottomRight = nearCenter - up * nearHeight / 2 + right * nearWidth / 2;

            // Creating planes, if points lay clockwise, then normal is pointing at you
            // Creating planes that are pointing outside the frustum
            TopPlane = new Plane(FarTopRight, NearTopRight, NearTopLeft);
            BottomPlane = new Plane(NearBottomLeft, NearBottomRight, FarBottomRight);
            LeftPlane = new Plane(FarTopLeft, NearTopLeft, NearBottomLeft);
            RightPlane = new Plane(NearTopRight, FarTopRight, FarBottomRight);
            NearPlane = new Plane(NearTopLeft, NearTopRight, NearBottomRight);
            FarPlane = new Plane(FarTopRight, FarTopLeft, FarBottomLeft);
        }
    }
}