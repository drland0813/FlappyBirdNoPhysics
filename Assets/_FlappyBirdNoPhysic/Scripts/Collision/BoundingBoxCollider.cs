using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Drland.F036
{
    public struct BoundingBox
    {
        public Vector2 Min;
        public Vector2 Max;

        public BoundingBox(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }

        public BoundingBox(float minX, float minY, float maxX, float maxY)
        {
            Min = new Vector2(minX, minY);
            Max = new Vector2(maxX, maxY);
        }   
        public void Update(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }
    }
    
    [RequireComponent(typeof(Image))]
    public class BoundingBoxCollider : MonoBehaviour
    {
        private Image _image;

        
        private BoundingBox _boundingBox;
        public BoundingBox BoundingBox => _boundingBox;
        
        Vector3[] _corners = new Vector3[4];

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            InitBoundingBox();
        }

        private void Update()
        {
            _image.rectTransform.GetWorldCorners(_corners);
            _boundingBox.Update(_corners[0], _corners[2]);
            // DrawBox();
        }

        private void DrawBox()
        {
            Vector3[] corners = new Vector3[4];
            _image.rectTransform.GetWorldCorners(corners);
            Debug.DrawLine(corners[0], corners[1], Color.red);
            Debug.DrawLine(corners[1], corners[2], Color.red);
            Debug.DrawLine(corners[2], corners[3], Color.red); 
            Debug.DrawLine(corners[3], corners[0], Color.red);
        }

        private void InitBoundingBox()
        {
            _image.rectTransform.GetWorldCorners(_corners);
            _boundingBox = new BoundingBox(_corners[0], _corners[2]);
        }
    }
}
