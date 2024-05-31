using System.Collections.Generic;
using Drland.F036.Obstacle;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Drland.F036
{
    public enum LevelDifficult
    {
        Easy,
        Medium,
        Hard,
        Impossible
    }

    public enum PositionType
    {
        Top,
        Bottom
    }
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private DoublePipes _pipePrefab;
        [SerializeField] private RectTransform _pipeCanvas;
        [SerializeField] private float _pipeSpeed;
        [SerializeField] private RectTransform _startPoint;
        [SerializeField] private RectTransform _endPoint;
        [SerializeField] private Player _player;
        [SerializeField] private GroundLoop _groundLoop;


        private ObjectPool<DoublePipes> _pipePool;
        
        private List<DoublePipes> _pipeList;

        private void Awake()
        {
            _pipeList = new List<DoublePipes>();
            _pipePool = new ObjectPool<DoublePipes>(_pipePrefab);
        }

        private void SpawnPipe(float topHeight, float bottomHeight)
        {
            CreatePipe(topHeight, bottomHeight);
        }
        
        private void CreatePipe(float topHeight, float bottomHeight)
        {
            var pipe = _pipePool.Get();
            var pipeTransform = pipe.transform;
            pipeTransform.SetParent(_pipeCanvas);
            
            pipeTransform.position = _startPoint.position;
            pipe.transform.localScale = Vector3.one;
            pipe.SetHeight(topHeight, bottomHeight);
            _pipeList.Add(pipe);
            _player.AddPipeCollider(pipe.TopBoundingBoxCollider);
            _player.AddPipeCollider(pipe.BottomBoundingBoxCollider);
        }

        public void Init()
        {
            InvokeRepeating(nameof(GeneratePipes), 1f, 1f);
        }

        public void UpdateData()
        {
            UpdatePipesMovement();
        }
        
        private void GeneratePipes()
        {
            var height = _pipeCanvas.rect.height;
            var spaceHeight = height * 0.3f;
            var totalPipeHeight = height - spaceHeight;
            var randomSpaceRate = Random.Range(0.2f, 0.8f + Mathf.Epsilon);
            var firstPipeHeight = totalPipeHeight * randomSpaceRate;
            var secondPipeHeight = totalPipeHeight - firstPipeHeight;

            SpawnPipe(firstPipeHeight, secondPipeHeight);
        }

        private void UpdatePipesMovement()
        {
            var subPosition = _pipeSpeed * Time.deltaTime;
            _groundLoop.UpdateMovement(subPosition);
            for (var i = 0; i < _pipeList.Count; i++)
            {
                var pipe = _pipeList[i];
                var pipeTransform = pipe.transform;
                var newPos = pipeTransform.position;
                newPos.x -= subPosition;
                pipeTransform.position = newPos;
                if (IsPipeOutOfScreen(pipeTransform))
                {
                    _pipeList.Remove(pipe);
                    _player.RemovePipeCollider(pipe.TopBoundingBoxCollider);
                    _player.RemovePipeCollider(pipe.BottomBoundingBoxCollider);

                    _pipePool.Store(pipe);
                    pipe.transform.position = Vector2.zero;
                }
            }
        }

        private bool IsPipeOutOfScreen(Transform pipe)
        {
            bool isGroundedCheckPoint = pipe.transform.position.x <= _endPoint.transform.position.x;
            return isGroundedCheckPoint;
        }
    }
}