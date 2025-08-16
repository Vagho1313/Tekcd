using System.Collections.Generic;
using UnityEngine;

namespace MyUtilities
{
    public enum GridWrapMode
    {
        Repeat = 0,
        Clamp
    }

    public class AnimationSurface
    {
        private int verticesCount = 0;

        public AnimationCurve3[] curves;

        public AnimationSurface(Vector3[][] values)
        {
            SetValues(values);
        }

        public void SetValues(Vector3[][] values)
        {
            if (curves == null || curves.Length != values.Length)
            {
                curves = new AnimationCurve3[values.Length];
            }
            for (int i = 0; i < values.Length; i++)
            {
                if (curves[i] == null || curves[i].Length != values[i].Length)
                {
                    curves[i] = new AnimationCurve3(values[i]);
                }
                else
                {
                    curves[i].SetValues(values[i]);
                }
            }
        }

        public void DrawMesh(Mesh mesh, Rect rect)
        {
            List<Vector3> vertices = new List<Vector3>(0);
            List<int> triangles = new List<int>(0);
            List<Vector2> uv = new List<Vector2>(0);
            List<Vector3> normals = new List<Vector3>(0);

            if (mesh == null || verticesCount != mesh.vertices.Length)
            {
                mesh = new Mesh();
            }

            int i = 0;
            int k = 0;
            verticesCount = 0;
            foreach (AnimationCurve3 curve in curves)
            {
                float delta = curve.MaxTime / (float)(curves.Length - 1);
                int j = 0;
                int nCount = curves.Length;

                for (float f = 0.0f; j < nCount; f += delta)
                {
                    vertices.Add(curve.Evaluate(f));
                    float deltaX = (float)i / (float)(nCount - 1);
                    float deltaY = (float)j / (float)(nCount - 1);
                    uv.Add(new Vector2(rect.x + rect.width * deltaX, rect.y + rect.height * deltaY));
                    normals.Add(Vector3.up);
                    verticesCount++;
                    if (j < nCount - 1 && i < nCount - 1)
                    {
                        triangles.Add(k);
                        triangles.Add(k + 1);
                        triangles.Add(k + nCount);

                        triangles.Add(k + nCount);
                        triangles.Add(k + 1);
                        triangles.Add(k + nCount + 1);
                    }
                    k++;
                    j++;
                }
                i++;
            }
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.uv = uv.ToArray();
            mesh.normals = normals.ToArray();
            mesh.RecalculateNormals();
        }
    }
}