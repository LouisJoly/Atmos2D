using Atmos2D.ECS;
using System.Numerics;

namespace Atmos2D.Core.Components
{
    /// <summary>
    /// Represents the spatial properties of an entity: position, rotation, and scale.
    /// This component defines where and how an entity exists in the 2D world.
    /// </summary>
    public class TransformComponent : IComponent
    {
        /// <summary>
        /// The position of the entity in 2D space (X, Y coordinates).
        /// </summary>
        public Vector2 Position { get; set; } = Vector2.Zero;

        /// <summary>
        /// The rotation of the entity around its origin, in degrees.
        /// </summary>
        public float Rotation { get; set; } = 0.0f;

        /// <summary>
        /// The scale of the entity (X, Y scaling factors).
        /// A scale of (1,1) means no scaling.
        /// </summary>
        public Vector2 Scale { get; set; } = Vector2.One;

        // Constructor for convenience
        public TransformComponent(Vector2 position, float rotation = 0.0f, Vector2 scale = default)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale == default ? Vector2.One : scale;
        }

        public TransformComponent() { } // Parameterless constructor for easy creation
    }
}