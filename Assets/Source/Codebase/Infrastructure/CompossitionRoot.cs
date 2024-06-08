using Source.Codebase.Controllers.GameInput;
using Source.Codebase.Controllers.Presenters;
using Source.Codebase.Domain.Configs;
using Source.Codebase.Domain.Models;
using Source.Codebase.Presentation;
using Source.Codebase.Services;
using UnityEngine;

namespace Source.Codebase.Infrastructure
{
    public class CompossitionRoot : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private DesktopInput _desktopInput;

        private void Awake()
        {
            StaticDataService staticDataService = new(_levelConfig);
            int height = Mathf.RoundToInt(Camera.main.orthographicSize * 2f);
            int width = Mathf.RoundToInt(height * Screen.width / Screen.height);
            Gameboard gameboard = new(width, height);
            GameLoopService gameLoopService = new(gameboard);
            WallViewFactory wallViewFactory = new(staticDataService);
            WallRandomService wallRandomService = new(gameboard, wallViewFactory);
            wallRandomService.GenerateWalls();
            Player player = new();
            PlayerView playerView = Instantiate(_levelConfig.PlayerViewTemplate);
            Vector3 playerSpawnPosition =
                gameboard.GetWorldFromBoardPosition(new(0, height / 2));
            PlayerPresenter playerPresenter =
                new(player, playerView, playerSpawnPosition, _desktopInput, gameLoopService);
            playerView.Construct(playerPresenter);
            Vector3 enemySpawnPosition =
                gameboard.GetWorldFromBoardPosition(new(width - 1, height / 2));
            Enemy enemy = new();
            EnemyView enemyView = Instantiate(_levelConfig.EnemyViewTemplate);
            EnemyPresenter enemyPresenter = new(enemy, enemyView, enemySpawnPosition);
            enemyView.Construct(enemyPresenter);
        }
    }
}
