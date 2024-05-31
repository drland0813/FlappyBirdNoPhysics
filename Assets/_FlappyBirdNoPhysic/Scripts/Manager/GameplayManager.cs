using System;
using System.Collections;
using Drland.F036.Obstacle;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Drland.F036
{
    public enum GameState
    {
        WaitToStart,
        Playing,
        GameOver
    }
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private Player _player;

        private int _score;
        private bool _isGameOver;
        private GameState _gameState;
        
        private Action _onGameOver;
        private Action _onStartGame;
        private Action _onRestart;
        private Action _onPause;

        private static GameplayManager _instance;
        public static GameplayManager Instance => _instance;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            _instance = this;
        }
        
        private void Start()
        {
            StartCoroutine(InitCoroutine());
        }
        
        private IEnumerator InitCoroutine()
        {
            _score = 0;
            _gameState = GameState.WaitToStart;
            
            _levelManager.Init();
            _isGameOver = false;
            yield return new WaitForSeconds(1f);
            _gameState = GameState.Playing;
        }

        private void Update()
        {
            switch (_gameState)
            {
                case GameState.WaitToStart:
                    
                    break;
                case GameState.Playing:
                    _levelManager.UpdateData();
                    _isGameOver = _player.IsCollided;
                    if (_isGameOver)
                    {
                        if (_gameState == GameState.GameOver) return;
                        _gameState = GameState.GameOver;
                        _onGameOver?.Invoke();
                    }
                    break;
                case GameState.GameOver:
                    SceneManager.LoadScene(0);
                    break;
            }
        }

        private void ReloadGame()
        {
            StartCoroutine(InitCoroutine());
        }
        
    }

}