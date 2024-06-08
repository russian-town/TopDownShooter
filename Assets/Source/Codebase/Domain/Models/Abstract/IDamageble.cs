using System;

namespace Source.Codebase.Domain.Models.Abstract
{
    public interface IDamageble
    {
        public event Action Hit;

        public void TakeDamage(float damage);
    }
}
