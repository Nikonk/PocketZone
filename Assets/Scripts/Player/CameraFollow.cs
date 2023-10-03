using UnityEngine;

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

            if (Camera.main != null)
                _mainCameraTransform = Camera.main.transform;
            else
                Debug.LogError("Main camera is empty");

        }

        private void FixedUpdate()
        {
            Vector3 desiredPosition = _transform.position;
            var smoothedPosition =
                Vector3.Lerp(_mainCameraTransform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
            _mainCameraTransform.position = smoothedPosition;
        }
    }
}
