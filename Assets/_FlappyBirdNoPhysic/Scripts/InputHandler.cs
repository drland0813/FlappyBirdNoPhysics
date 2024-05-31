using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.F036
{
    public class InputHandler : MonoBehaviour
    {
        private readonly float _maxTimeDelayToClick = 0.3f;
        private float _timeDelayToClick = 0.3f;
        private bool _isClick;
        private bool _isDelaying;
        
        public Action OnClick;
        private void Update()
        {
            if (_isClick)
            {
                _timeDelayToClick -= Time.deltaTime;
                _isDelaying = true;
                if (_timeDelayToClick < 0)
                {
                    _timeDelayToClick = _maxTimeDelayToClick;
                    _isDelaying = false;
                    _isClick = false;
                }
            }
        }

        public void Click()
        {
            if (!_isClick)
            {
                _isClick = true;
            }
            if (!_isDelaying)
            {
                OnClick?.Invoke();
            }
        }
    }
}
