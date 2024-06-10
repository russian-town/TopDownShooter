namespace Source.Codebase.Domain.Models
{
    public class Bullet
    {
        public Bullet(float speed)
        {
            Speed = speed;
        }

        public float Speed { get; private set; }
    }
}
