using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Common
{
    public static class MyMath
    {
        #region Frustum
        public static bool IsAABBInFrustum(Frustum frustum, AABB aabb)
        {
            int inCount = 8;
            foreach (Plane frustumPlane in frustum.Planes)
            {
                foreach (Vector3 point in aabb.Points)
                {
                    if (frustumPlane.GetDistanceToPoint(point) < 0f)
                    {
                        inCount--;
                    }
                    
                    if (inCount <= 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
        public static bool IsAABBInFrustum(AABB aabb, Frustum frustum) => IsAABBInFrustum(frustum, aabb);

        public static bool IsAABBIntersectingFrustum(Frustum frustum, AABB aabb)
        {
            foreach (Vector3 point in aabb.Points)
            {
                int inCount = 0;
                foreach (Plane plane in frustum.Planes)
                {
                    if (plane.GetSide(point) == false)
                    {
                        inCount++;
                    }
                }
                
                if (inCount >= 6)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        public static bool IsAABBIntersectingFrustum(AABB aabb, Frustum frustum) => IsAABBIntersectingFrustum(frustum, aabb);

        public static void DrawFrustum(Frustum frustum, Color color)
        {
            Debug.DrawLine(frustum.NearTopLeft, frustum.FarTopLeft, color);
            Debug.DrawLine(frustum.NearTopRight, frustum.FarTopRight, color);
            Debug.DrawLine(frustum.NearBottomLeft, frustum.FarBottomLeft, color);
            Debug.DrawLine(frustum.NearBottomRight, frustum.FarBottomRight, color);
            
            Debug.DrawLine(frustum.NearTopLeft, frustum.NearTopRight, color);
            Debug.DrawLine(frustum.NearTopRight, frustum.NearBottomRight, color);
            Debug.DrawLine(frustum.NearBottomRight, frustum.NearBottomLeft, color);
            Debug.DrawLine(frustum.NearBottomLeft, frustum.NearTopLeft, color);
            
            Debug.DrawLine(frustum.FarTopLeft, frustum.FarTopRight, color);
            Debug.DrawLine(frustum.FarTopRight, frustum.FarBottomRight, color);
            Debug.DrawLine(frustum.FarBottomRight, frustum.FarBottomLeft, color);
            Debug.DrawLine(frustum.FarBottomLeft, frustum.FarTopLeft, color);
            
            DrawPlaneNormalFromPoint(frustum.TopPlane, frustum.FarTopLeft, color);
            DrawPlaneNormalFromPoint(frustum.BottomPlane, frustum.FarBottomLeft, color);
            DrawPlaneNormalFromPoint(frustum.LeftPlane, frustum.FarTopLeft, color);
            DrawPlaneNormalFromPoint(frustum.RightPlane, frustum.FarTopRight, color);
            DrawPlaneNormalFromPoint(frustum.NearPlane, frustum.NearTopLeft, color);
            DrawPlaneNormalFromPoint(frustum.FarPlane, frustum.FarTopLeft, color);
        }
        
        private static void DrawPlaneNormalFromPoint(Plane plane, Vector3 point, Color color)
        {
            Debug.DrawRay(point, plane.normal, color, 3f);
        }
        #endregion

        #region AABB
        
        public static void DrawAABB(AABB aabb, Color color)
        {
            Debug.DrawLine(aabb.Points[0], aabb.Points[1], color);
            Debug.DrawLine(aabb.Points[0], aabb.Points[2], color);
            Debug.DrawLine(aabb.Points[0], aabb.Points[3], color);
            Debug.DrawLine(aabb.Points[1], aabb.Points[4], color);
            Debug.DrawLine(aabb.Points[1], aabb.Points[6], color);
            Debug.DrawLine(aabb.Points[2], aabb.Points[4], color);
            Debug.DrawLine(aabb.Points[2], aabb.Points[6], color);
            Debug.DrawLine(aabb.Points[3], aabb.Points[4], color);
            Debug.DrawLine(aabb.Points[3], aabb.Points[6], color);
            Debug.DrawLine(aabb.Points[5], aabb.Points[7], color);
            Debug.DrawLine(aabb.Points[5], aabb.Points[6], color);
            Debug.DrawLine(aabb.Points[5], aabb.Points[4], color);
            Debug.DrawLine(aabb.Points[5], aabb.Points[1], color);
            Debug.DrawLine(aabb.Points[7], aabb.Points[6], color);
            Debug.DrawLine(aabb.Points[7], aabb.Points[4], color);
            Debug.DrawLine(aabb.Points[7], aabb.Points[3], color);
            Debug.DrawLine(aabb.Points[7], aabb.Points[2], color);
        }
        #endregion
    }
}