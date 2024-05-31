using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Drland.F036
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravity = -9.8f;
        [SerializeField] private float _fallMultiplier;
        [SerializeField] private float _jumpTime;

        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private GameObject _groundGameObject;

        private BoundingBoxCollider _collider;

        private Vector2 _velocity = Vector2.zero;
        private bool _isJumping;
        private bool _canJump;
        private Coroutine _jumpCoroutine;
        private List<BoundingBoxCollider> _otherCollider;

        private bool _isCollided;
        public bool IsCollided => _isCollided;

        private void Awake()
        {
            _inputHandler.OnClick += Jump;
            _collider = GetComponent<BoundingBoxCollider>();
            _otherCollider = new List<BoundingBoxCollider>();
        }

        private void Jump()
        {
            if (_jumpCoroutine != null)
            {
                StopCoroutine(_jumpCoroutine);
            }
            _jumpCoroutine = StartCoroutine(JumpCoroutine());
        }

        private void Update()
        { 
            if (_isJumping)
            {
                _velocity.y = _jumpForce;
            }
            else
            {
                if (_velocity.y < 0)
                {
                    _velocity.y += _gravity * (_fallMultiplier - 1) * Time.deltaTime;
                }
                else
                {
                    _velocity.y = _gravity * Time.deltaTime;
                }
            }
            transform.position += (Vector3)(_velocity * Time.deltaTime);

            _isCollided = CheckCollision();
        }

        private IEnumerator JumpCoroutine()
        {
            _isJumping = true;
            var remainTime = _jumpTime;
            while (remainTime > 0)
            {
                remainTime -= Time.deltaTime;
                yield return null;
            }
            _isJumping = false;
        }

        private bool IsCollidedWithOther(BoundingBoxCollider otherCol)
        {
            var isCollided = _collider.BoundingBox.Min.x < otherCol.BoundingBox.Max.x &&
                                _collider.BoundingBox.Max.x > otherCol.BoundingBox.Min.x &&
                                _collider.BoundingBox.Min.y < otherCol.BoundingBox.Max.y &&
                                _collider.BoundingBox.Max.y > otherCol.BoundingBox.Min.y;

            return isCollided;
        }
        
        private bool IsCollidedWithGround()
        {
            var isCollided = transform.position.y <= _groundGameObject.transform.position.y;
            return isCollided;
        }

        public void AddPipeCollider(BoundingBoxCollider collider)
        {
            _otherCollider.Add(collider);
        }
        
        public void RemovePipeCollider(BoundingBoxCollider collider)
        {
            _otherCollider.Remove(collider);
        }

        private bool CheckCollision()
        {
            foreach (var col in _otherCollider)
            {
                if (IsCollidedWithOther(col))
                {
                    return true;
                }
            }

            return IsCollidedWithGround();
        }
    }
}
