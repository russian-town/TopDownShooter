using Source.Codebase.Presentation;
using UnityEngine;

namespace Source.Codebase.Domain.Configs
{
    [CreateAssetMenu(fileName = "Level Config", menuName = "Top Down Shooter/New Level Config", order = 59)]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public WallView WallViewTemplate { get; private set; }
        [field: SerializeField] public PlayerView PlayerViewTemplate { get; private set; }
        [field: SerializeField] public EnemyView EnemyViewTemplate { get; private set; }
        [field: SerializeField] public BulletView BulletViewTemplate { get; private set; }
        [field: SerializeField] public EntityConfig PlayerConfig { get; private set; }
        [field: SerializeField] public EntityConfig EnemyConfig { get; private set; }
    }
}
