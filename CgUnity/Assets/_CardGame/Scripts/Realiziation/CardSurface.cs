using MyUtilities;
using UnityEngine;

namespace CardGame
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class CardSurface : MonoBehaviour
    {
        private GameObject card;
        private Mesh mesh;
        private Vector3[][] nodes;
        private Rect rect;
        private AnimationSurface surface;
        private int subdivisions;

        public void SetRect(Rect rect)
        {
            this.rect = rect;
        }

        public void Init(int subdivisions)
        {
            this.subdivisions = subdivisions;
            card = gameObject;

            mesh = card.GetComponent<MeshFilter>().sharedMesh = new Mesh();

            MeshRenderer render = card.GetComponent<MeshRenderer>();
            render.receiveShadows = false;
            render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            render.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            render.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            render.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
            render.allowOcclusionWhenDynamic = false;

            subdivisions = Mathf.Clamp(subdivisions, 2, subdivisions);
            nodes = new Vector3[subdivisions + 1][];

            CalculateNodes(0f);

            surface = new AnimationSurface(nodes);
        }

        public void DeformCard(float valueNormalized)
        {
            CalculateNodes(valueNormalized);
            surface.SetValues(nodes);
            surface.DrawMesh(mesh, rect);
        }

        private void CalculateNodes(float valueNormalized)
        {
            valueNormalized = Mathf.Clamp(valueNormalized, -1f, 1f);

            if (Mathf.Abs(valueNormalized) < 0.01f)
            {
                CreatePlane();
            }
            else
            {
                Deform(valueNormalized);
            }
        }

        private void CreatePlane()
        {
            for (int i = 0; i <= subdivisions; i++)
            {
                float z01 = (float)i / (float)subdivisions;

                nodes[i] = new Vector3[subdivisions + 1];

                for (int j = 0; j <= subdivisions; j++)
                {
                    float x01 = (float)j / (float)subdivisions;
                    nodes[i][j] = (-0.5f + x01) * Vector3.right + (-0.5f + z01) * Vector3.forward;
                }
            }
        }

        private void Deform(float valueNormalized)
        {
            float maxAngle = 0.5f * Mathf.PI * valueNormalized;

            float radius = 0.5f / maxAngle;

            for (int i = 0; i <= subdivisions; i++)
            {
                float v01 = (float)i / (float)subdivisions;

                float angle = Mathf.Lerp(-maxAngle, maxAngle, v01);

                nodes[i] = new Vector3[subdivisions + 1];

                for (int j = 0; j <= subdivisions; j++)
                {
                    float x01 = (float)j / (float)subdivisions;
                    nodes[i][j] = (-0.5f + x01) * Vector3.right + radius * Mathf.Sin(angle) * Vector3.forward + radius * (1f - Mathf.Cos(angle)) * Vector3.up;
                }
            }
        }
    }
}