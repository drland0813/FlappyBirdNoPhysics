using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.F036
{
    public class GroundLoop : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private List<RectTransform> _grounds;
        [SerializeField] private Transform _checkPointToSetup;
        private float _width;

        private void Awake()
        {
            _width = _grounds[0].rect.width;
        }

        public void UpdateMovement(float subPosition)
        {
            for (int i = 0; i < _grounds.Count; i++)
            {
                var ground = _grounds[i].transform;
                var newPos = ground.position;
                newPos.x -= subPosition;
                ground.position = newPos;

                if(ground.position.x <= _checkPointToSetup.position.x)
                {
                    SetUpPosition(i);
                }
            }
        }

        private void SetUpPosition(int curIndex)
        {
            int nextIndex = (curIndex + 1) % _grounds.Count;
            var newPos = _grounds[nextIndex].anchoredPosition;
            var offSet = _checkPointToSetup.position.x - newPos.x; 
            newPos.x += (_width - 10);
            _grounds[curIndex].anchoredPosition = newPos;
        }
    }
}
