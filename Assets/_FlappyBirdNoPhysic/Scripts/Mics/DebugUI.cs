using System;
using TMPro;
using UnityEngine;

namespace Mics
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _verticalSpeed;
        [SerializeField] private TextMeshProUGUI _horizontalSpeed;


        public void UpdateDebug(int score, float xSpeed, float ySpeed)
        {
            _score.text = score.ToString();
            _verticalSpeed.text = ySpeed.ToString();
            _horizontalSpeed.text = xSpeed.ToString();
        }
    }
}