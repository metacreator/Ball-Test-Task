using UnityEngine;

namespace Player
{
    public class PlayerBall : MonoBehaviour
    {
        [SerializeField] private float startVolume = 10f;
        [SerializeField] private float minVolume = 2f;
        [SerializeField] private float moveSpeed = 5f;

        private float _currentVolume;
        private bool _isMoving;
        private Vector3 _targetPosition;

        private void Awake()
        {
            _currentVolume = startVolume;
            UpdateScale();
        }

        private void Update()
        {
            if (!_isMoving) return;

            transform.position = Vector3.MoveTowards(
                transform.position,
                _targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
                _isMoving = false;
        }

        private void UpdateScale()
        {
            var radius = Mathf.Pow(_currentVolume, 1f / 3f);
            transform.localScale = Vector3.one * radius;
        }

        public bool CanSubtractVolume(float amount)
        {
            return _currentVolume - amount >= minVolume;
        }

        public void SubtractVolume(float amount)
        {
            _currentVolume -= amount;
            UpdateScale();
        }

        public void MoveTo(Vector3 destination)
        {
            _targetPosition = destination;
            _isMoving = true;
        }


        public float GetCurrentVolume()
        {
            return _currentVolume;
        }
    }
}