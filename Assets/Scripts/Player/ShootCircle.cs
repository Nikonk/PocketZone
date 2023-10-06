using UnityEngine;

namespace PocketZone.Player
{
    [RequireComponent(typeof(LineRenderer))]
    public class ShootCircle : MonoBehaviour
    {
        private const int _numSegments = 35;

        private LineRenderer _lineRenderer;
        private float _radius;
        private float _lastRadius;

        public float Radius
        {
            get => _radius;
            set
            {
                if (value < 0)
                {
                    Debug.LogError("Shoot radius can't be negative");
                    return;
                }

                _radius = value;
            }
        }

        private void Start()
        {
            _lastRadius = _radius;

            _lineRenderer = gameObject.GetComponent<LineRenderer>();
            _lineRenderer.startWidth = 0.05f;
            _lineRenderer.endWidth = 0.05f;
            _lineRenderer.useWorldSpace = false;
            _lineRenderer.positionCount = _numSegments + 1;
            Draw();
        }

        private void Update()
        {
            if (Mathf.Abs(_radius - _lastRadius) > Mathf.Epsilon)
                Draw();
        }

        private void Draw()
        {
            const float deltaTheta = (float)(2.0 * Mathf.PI) / _numSegments;
            float theta = 0f;

            for (int i = 0; i < _numSegments + 1; i++)
            {
                float x = _radius * Mathf.Cos(theta);
                float y = _radius * Mathf.Sin(theta);
                var pos = new Vector3(x, y, 0);
                _lineRenderer.SetPosition(i, pos);
                theta += deltaTheta;
            }
        }
    }
}