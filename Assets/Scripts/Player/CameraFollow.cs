using UnityEngine;
using Zenject;

namespace PocketZone.Player
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float smoothSpeed = 0.125f;

        private Transform _mainCameraTransform;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
            _mainCameraTransform = Camera.main.transform;
        }

        private void FixedUpdate()
        {
            Vector3 desiredPosition = transform.position;
            var smoothedPosition =
                Vector3.Lerp(_mainCameraTransform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
            _mainCameraTransform.position = smoothedPosition;
        }
    }
}
