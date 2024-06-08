using UnityEngine;

namespace Source.Codebase.Domain.Configs
{
    [CreateAssetMenu(fileName = "Entity Config", menuName = "Top Down Shooter/New Entity Config", order = 59)]
    public class EntityConfig : ScriptableObject
    {
        [field: SerializeField] public ContactFilter2D ContactFilter2D { get; private set; }
    }
}
