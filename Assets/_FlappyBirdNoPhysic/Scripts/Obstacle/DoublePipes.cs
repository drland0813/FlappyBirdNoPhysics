using System;
using System.Collections;
using System.Collections.Generic;
using Drland.F036.Obstacle;
using UnityEngine;

namespace Drland.F036
{
    public class DoublePipes : MonoBehaviour
    {
        [SerializeField] private Pipe _topPipe;
        [SerializeField] private Pipe _bottomPipe;
        private RectTransform _rectTransform;

        public BoundingBoxCollider TopBoundingBoxCollider => _topPipe.Collider;
        public BoundingBoxCollider BottomBoundingBoxCollider => _bottomPipe.Collider;
        private BoundingBoxCollider _collider;
        
        public RectTransform RectTransform => _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }


        public void SetHeight(float topPipeHeight, float bottomPipeHeight)
        {
            _topPipe.SetHeight(topPipeHeight);
            _bottomPipe.SetHeight(bottomPipeHeight);
        }
    }
}
