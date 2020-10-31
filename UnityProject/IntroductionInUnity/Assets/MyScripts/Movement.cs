using UnityEngine;

namespace EscapeRoom {

    public class Movement : MonoBehaviour {

        #region Fields

        [SerializeField] private float _speed = 5.0F;
        [SerializeField] private float _rotation = 10.0F;

        private Rigidbody _rigidbody;

        private Vector3 _direction;

        #endregion

        #region UnityMethods

        private void Start() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update() {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");

            _direction.Normalize();

            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, _direction, _rotation * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(desiredForward);
        }


        private void FixedUpdate() {
            var speed = (_direction.sqrMagnitude > 0) ? _speed : 0;
            speed = speed * 10.0F * Time.fixedDeltaTime;

            var moveDirection = transform.forward * speed;
            _rigidbody.velocity = moveDirection;
        }

        #endregion


        #region Methods

        private void Move() {
            Vector3 targetVelocity = _speed * transform.forward * Time.deltaTime;
            targetVelocity.y = GetComponent<Rigidbody>().velocity.y;

            GetComponent<Rigidbody>().velocity = targetVelocity;
            GetComponent<Rigidbody>().angularVelocity = _rotation * Vector3.up * Time.deltaTime;
        }

        #endregion
    }
}