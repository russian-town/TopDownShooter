using Source.Codebase.Presentation;
using UnityEngine;

namespace Source.Codebase.Domain.Configs
{
    [CreateAssetMenu(fileName = "Level Config", menuName = "Top Down Shooter/New Level Config", order = 59)]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public WallView WallViewTemplate { get; private set; }
    }
}
