using UnityEngine;

namespace Source.Codebase.Presentation.Abstract
{
    public abstract class EntityView : ViewBase
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private LineRenderer _lineRenderer;

        [field: SerializeField] public Transform GunEnd { get; private set; }

        public void ShowTrail(Vector2[] directions, Vector2 position)
        {
            _lineRenderer.positionCount = directions.Length;

            for (int i = 0; i < directions.Length; i++)
            {
                _lineRenderer.SetPosition(i, directions[i]);
            }
        }

        public void SetWorldPosition(Vector3 worldPosition)
            => _transform.position = worldPosition;

        public void SetRotation(float angle)
        {
            Vector3 eulerAngel = _transform.eulerAngles;
            eulerAngel.z = angle;
            _transform.eulerAngles = eulerAngel;
        }

        public void Move(Vector3 position)
            => _transform.position = position;
    }
}
