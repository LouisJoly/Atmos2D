using Atmos2D.ECS;
using System.Numerics; // For Vector2

namespace Atmos2D.Core.Components
{
    /// <summary>
    /// Represents the current direction of movement for an entity.
    /// </summary>
    public class DirectionComponent : IComponent
    {
        /// <summary>
        /// The current direction vector (e.g., (1,0) for right, (0,-1) for up).
        /// </summary>
        public Vector2 Direction { get; set; } = Vector2.Zero;

        /// <summary>
        /// The previous direction vector, useful for preventing immediate 180-degree turns.
        /// </summary>
        public Vector2 PreviousDirection { get; set; } = Vector2.Zero;

        public DirectionComponent(Vector2 direction)
        {
            Direction = direction;
            PreviousDirection = direction;
        }

        public DirectionComponent() { }
    }
}