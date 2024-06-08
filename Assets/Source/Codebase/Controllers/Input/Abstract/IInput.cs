using System;
using UnityEngine;

namespace Source.Codebase.Controllers.GameInput.Abstract
{
    public interface IInput
    {
        public event Action ShootButtonDown;
        public event Action AimButtonDown;
        public event Action<Vector2> DirectionChanged;
    }
}
