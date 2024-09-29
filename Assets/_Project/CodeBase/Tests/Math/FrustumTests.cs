using _Project.CodeBase.Runtime.Common;
using NUnit.Framework;
using UnityEngine;

namespace _Project.CodeBase.Tests.Math
{
    public class FrustumTests
    {
        [Test]
        public void Frustum_aabb_intersection_test_1()
        {
            AABB aabb = new AABB(new Vector3(0, 0, 0), new Vector3(1, 1, 1));
            Frustum frustum = new Frustum(60, 10, 0.2f, 0.5f, Vector3.up, Vector3.right, Vector3.forward, Vector3.zero);
            Assert.IsTrue(MyMath.IsAABBIntersectingFrustum(frustum, aabb));
        }
        
        [Test]
        public void Frustum_aabb_intersection_test_2()
        {
            AABB aabb = new AABB(new Vector3(0, 0, 0), new Vector3(1, 1, 1));
            Frustum frustum = new Frustum(60, 10, 0.2f, 0.5f, Vector3.up, Vector3.right, Vector3.forward, Vector3.zero);
            Assert.IsFalse(MyMath.IsAABBInFrustum(frustum, aabb));
        }
    }
}