using Field;

namespace Enemy
{
    public interface IMovementAgent
    {
        void TickMovement();

        Node GetCurrentNode();

        public void Die();
    }
}