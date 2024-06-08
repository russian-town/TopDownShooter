using Source.Codebase.Presentation.Abstract;
using UnityEngine;

namespace Source.Codebase.Presentation
{
    public class PlayerView : EntityView 
    {
        private RaycastHit2D[] _results;

        public void SetResults(RaycastHit2D[] results)
            => _results = results;

        private void OnDrawGizmos()
        {
            if (_results == null || _results.Length == 0)
                return;

            Gizmos.color = Color.red;

            for (int i = 0; i < _results.Length; i++)
            {
                if (i != 0)
                {
                    Vector3 cross = Vector3.Cross(_results[i].point, _results[i].normal);
                    Gizmos.DrawRay(_results[i].point, cross);
                }
                else
                {
                    Gizmos.DrawRay(transform.position + Vector3.right, _results[i].point);
                    continue;
                }
            }
        }
    }
}
