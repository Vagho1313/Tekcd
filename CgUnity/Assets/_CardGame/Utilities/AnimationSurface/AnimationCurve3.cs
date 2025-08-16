using System.Collections.Generic;
using UnityEngine;

namespace MyUtilities
{
    public class AnimationCurve3
    {
        [System.Serializable]
        public class Keyframe3D
        {
            public Keyframe keyX;
            public Keyframe keyY;
            public Keyframe keyZ;

            public Keyframe3D(float time, Vector3 value)
            {
                keyX = new Keyframe(time, value.x);
                keyY = new Keyframe(time, value.y);
                keyZ = new Keyframe(time, value.z);
            }
        }

        [SerializeField] private AnimationCurve curveX;
        [SerializeField] private AnimationCurve curveY;
        [SerializeField] private AnimationCurve curveZ;

        public float MaxTime { get; private set; }

        public List<Vector3> Values { get; private set; }

        public AnimationCurve CurveX
        {
            get { return curveX; }
        }

        public AnimationCurve CurveY
        {
            get { return curveY; }
        }

        public AnimationCurve CurveZ
        {
            get { return curveZ; }
        }

        public AnimationCurve3()
        {
            curveX = new AnimationCurve();
            curveY = new AnimationCurve();
            curveZ = new AnimationCurve();
            Values = new List<Vector3>(0);
        }
        public void SetValues(Vector3[] values)
        {
            MaxTime = 0.0f;
            Values = new List<Vector3>(0);

            if (curveX == null)
            {
                curveX = new AnimationCurve();
            }
            else
            {
                curveX.keys = null;
            }
            if (curveY == null)
            {
                curveY = new AnimationCurve();
            }
            else
            {
                curveY.keys = null;
            }
            if (curveZ == null)
            {
                curveZ = new AnimationCurve();
            }
            else
            {
                curveZ.keys = null;
            }
            float f = 0.0f;
            for (int i = 0; i < values.Length; i++)
            {
                if (i != 0)
                {
                    float deltaTime = Vector3.Distance(values[i - 1], values[i]);
                    this.MaxTime += deltaTime;
                    f += deltaTime;
                }
                curveX.AddKey(f, values[i].x);
                curveY.AddKey(f, values[i].y);
                curveZ.AddKey(f, values[i].z);
                this.Values.Add(values[i]);
            }
        }
        public AnimationCurve3(Vector3[] values)
        {
            SetValues(values);
        }

        public void DrawCurve(LineRenderer line)
        {
            line.SetPositions(Values.ToArray());
        }

        public void AddKey(float time, float valueX, float valueY, float valueZ)
        {
            if (MaxTime < time)
            {
                MaxTime = time;
            }
            if (Values == null)
            {
                Values = new List<Vector3>(0);
            }
            curveX.AddKey(time, valueX);
            curveY.AddKey(time, valueY);
            curveZ.AddKey(time, valueZ);
            Values.Add(new Vector3(valueX, valueY, valueZ));
        }

        public void AddKey(float time, Vector3 value)
        {
            if (MaxTime < time)
            {
                MaxTime = time;
            }
            if (Values == null)
            {
                Values = new List<Vector3>(0);
            }

            curveX.AddKey(time, value.x);
            curveY.AddKey(time, value.y);
            curveZ.AddKey(time, value.z);
            Values.Add(value);
        }

        public void RemoveKey(int index)
        {
            curveX.RemoveKey(index);
            curveY.RemoveKey(index);
            curveZ.RemoveKey(index);
        }

        public void SmoothTangents(int index, float weight)
        {
            curveX.SmoothTangents(index, weight);
            curveY.SmoothTangents(index, weight);
            curveZ.SmoothTangents(index, weight);
        }

        public void Smooth()
        {
            for (int i = 0; i < Length; i++)
            {
                SmoothTangents(i, 0.0f);
            }
        }

        private WrapMode preWrapMode;
        private WrapMode postWrapMode;

        public WrapMode PreWrapMode
        {
            get => preWrapMode;
            set
            {
                preWrapMode = value;
                curveX.preWrapMode = preWrapMode;
                curveY.preWrapMode = preWrapMode;
                curveZ.preWrapMode = preWrapMode;
            }
        }

        public WrapMode PostWrapMode
        {
            get => postWrapMode;
            set
            {
                postWrapMode = value;
                curveX.postWrapMode = postWrapMode;
                curveY.postWrapMode = postWrapMode;
                curveZ.postWrapMode = postWrapMode;
            }
        }

        public int Length
        {
            get { return this.curveX.length; }
        }

        public Vector3 Evaluate(float time)
        {
            return new Vector3(this.curveX.Evaluate(time), this.curveY.Evaluate(time), this.curveZ.Evaluate(time));
        }

        public Vector3 Tangent(float time)
        {
            return Vector3.Normalize((1.0f / 0.0001f) * (Evaluate(time + 0.0001f) - Evaluate(time)));
        }

        public Vector3 Normal(float time)
        {
            Vector3 tangent0 = Tangent(time);
            Vector3 tangent = Tangent(time + 0.01f);
            return Vector3.Normalize((1.0f / 0.01f) * (tangent - tangent0));
        }

        public Vector3 Binormal(float time)
        {
            return Vector3.Normalize(-Vector3.Cross(Tangent(time), Normal(time)));
        }
    }
}