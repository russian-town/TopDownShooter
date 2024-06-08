using UnityEngine;

namespace Source.Codebase.Presentation.Abstract
{
    public abstract class EntityView : ViewBase
    {
        [SerializeField] private Transform _transform;

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
