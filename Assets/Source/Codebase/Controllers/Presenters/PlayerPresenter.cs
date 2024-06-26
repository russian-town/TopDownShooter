using Source.Codebase.Controllers.GameInput.Abstract;
using Source.Codebase.Controllers.Presenters.Abstract;
using Source.Codebase.Domain.Models;
using Source.Codebase.Presentation;
using Source.Codebase.Services;
using UnityEngine;

namespace Source.Codebase.Controllers.Presenters
{
    public class PlayerPresenter : IPresenter
    {
        private readonly Player _player;
        private readonly PlayerView _playerView;
        private readonly IInput _input;
        private readonly GameLoopService _gameLoopService;

        public PlayerPresenter(
            Player player,
            PlayerView playerView,
            Vector3 worldPosition,
            IInput input,
            GameLoopService gameLoopService)
        {
            _player = player;
            _playerView = playerView;
            _input = input;
            _gameLoopService = gameLoopService;
            _player.SetWorldPosition(worldPosition);
            _player.SetStartAngle(0f);
            _playerView.SetWorldPosition(worldPosition);
            _playerView.SetRotation(_player.Angle);
        }

        public void Enable()
        {
            _player.PositionChanged += OnPositionChanged;
            _input.ShootButtonDown += OnShootButtonDown;
            _input.AimButtonDown += OnAimButtonDown;
            _input.DirectionChanged += OnDirectionChaged;
        }

        public void Disable()
        {
            _player.PositionChanged -= OnPositionChanged;
            _input.ShootButtonDown -= OnShootButtonDown;
            _input.AimButtonDown -= OnAimButtonDown;
            _input.DirectionChanged -= OnDirectionChaged;
        }

        private void OnPositionChanged(Vector3 newWorldPosition) { }

        private void OnShootButtonDown()
        {
        }

        private void OnAimButtonDown()
        {
        }

        private void OnDirectionChaged(Vector2 direction)
        {
            Vector3 targetPosition =
                _player.CalculatePosition(direction);

            if (_gameLoopService.CanChangePosition(targetPosition))
            {
                _player.Move(targetPosition);
                _playerView.Move(targetPosition);
                _playerView.SetRotation(_player.Angle);
            }
        }
    }
}
