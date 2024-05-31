using System;
using UnityEngine;

namespace Drland.F036.Obstacle
{
    public class Pipe : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private BoundingBoxCollider _collider;
        
        public RectTransform RectTransform => _rectTransform;
        public BoundingBoxCollider Collider => _collider;
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _collider = GetComponent<BoundingBoxCollider>();
        }

        public void SetHeight(float height)
        {
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, height);
        }
    }
}