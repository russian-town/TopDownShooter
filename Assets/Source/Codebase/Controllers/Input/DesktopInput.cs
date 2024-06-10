using System;
using Source.Codebase.Controllers.GameInput.Abstract;
using UnityEngine;

namespace Source.Codebase.Controllers.GameInput
{
    public class DesktopInput : MonoBehaviour, IInput
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public event Action ShootButtonDown;
        public event Action AimButtonDown;
        public event Action<Vector2> DirectionChanged;

        private void Update()
        {
            if(Input.GetMouseButtonUp(0))
                ShootButtonDown?.Invoke();

            if(Input.GetMouseButton(0))
                AimButtonDown?.Invoke();

            Vector2 direction =
                new(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));

            if (direction.sqrMagnitude > 0)
                DirectionChanged?.Invoke(direction);
        }
    }
}
