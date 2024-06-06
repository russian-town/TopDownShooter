using Source.Codebase.Domain.Configs;
using Source.Codebase.Domain.Models;
using Source.Codebase.Services;
using UnityEngine;

namespace Source.Codebase.Infrastructure
{
    public class CompossitionRoot : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;

        private void Awake()
        {
            StaticDataService staticDataService = new(_levelConfig);
            int height = Mathf.RoundToInt(Camera.main.orthographicSize * 2f);
            int width = Mathf.RoundToInt(height * Screen.width / Screen.height);
            Gameboard gameboard = new(width, height);
            WallViewFactory wallViewFactory = new(staticDataService);
            GameboardService gameboardService = new(gameboard, wallViewFactory);
            gameboardService.GenerateWalls();
        }
    }
}
