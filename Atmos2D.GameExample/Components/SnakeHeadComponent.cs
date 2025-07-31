using Atmos2D.ECS;

namespace Atmos2D.GameExample.Components
{
    /// <summary>
    /// A marker component to identify the head of the snake entity.
    /// Also stores movement timing and game-specific states.
    /// </summary>
    public class SnakeHeadComponent : IComponent
    {
        /// <summary>
        /// The time (in seconds) that must pass before the snake moves one grid unit.
        /// A smaller value means faster movement.
        /// </summary>
        public float MoveInterval { get; set; }

        /// <summary>
        /// Accumulates the delta time. When it reaches MoveInterval, the snake moves.
        /// </summary>
        public float MoveTimer { get; set; }

        public SnakeHeadComponent(float moveInterval = 0.2f) // Default speed: move every 0.2 seconds
        {
            MoveInterval = moveInterval;
            MoveTimer = 0.0f; // Start with timer at 0
        }
    }
}