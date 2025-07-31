using Atmos2D.ECS;
using System.Numerics; // For Vector2

namespace Atmos2D.GameExample.Components
{
    /// <summary>
    /// Represents a single segment of the snake's body.
    /// Stores the order of the segment and its position in the previous frame.
    /// </summary>
    public class SnakeBodySegmentComponent : IComponent
    {
        /// <summary>
        /// The order of this segment in the snake's body (0 for the first segment after head, etc.).
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// The position this segment occupied in the previous frame.
        /// Used by the movement system to make segments follow the one in front.
        /// </summary>
        public Vector2 PreviousPosition { get; set; }

        public SnakeBodySegmentComponent(int order, Vector2 previousPosition)
        {
            Order = order;
            PreviousPosition = previousPosition;
        }

        public SnakeBodySegmentComponent() { }
    }
}