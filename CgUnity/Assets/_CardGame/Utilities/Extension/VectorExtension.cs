using UnityEngine;

namespace MyUtilities
{
    public static class VectorExtension
    {
        public static Vector3 ToLocal(this Vector3 wordPosition, Transform parent)
        {
            Vector3 delta = wordPosition - parent.position;
            return new Vector3(
                Vector3.Dot(delta, parent.right),
                Vector3.Dot(delta, parent.up),
                Vector3.Dot(delta, parent.forward));
        }

        public static Vector3 ToWord(this Vector3 localPosition, Transform parent)
        {
            return parent.position +
                localPosition.x * parent.right +
                localPosition.y * parent.up +
                localPosition.z * parent.forward;
        }

        public static Vector3 CastPlane(this Ray ray,
            Vector3 planePosition, Vector3 planeNormal)
        {
            Vector3 projectPoint = ray.origin -
                Vector3.Project(ray.origin - planePosition, planeNormal);

            return ray.origin + (projectPoint - ray.origin).magnitude /
                Vector3.Dot(ray.direction, -planeNormal) * ray.direction;
        }

        public static Vector3 CastPlane(this Ray ray, Transform plane)
        {
            Vector3 projectPoint = ray.origin -
                Vector3.Project(ray.origin - plane.position, plane.up);

            return ray.origin + (projectPoint - ray.origin).magnitude /
                Vector3.Dot(ray.direction, -plane.up) * ray.direction;
        }
    }
}