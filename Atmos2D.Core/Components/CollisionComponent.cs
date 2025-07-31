using Atmos2D.ECS;
using System.Numerics;

namespace Atmos2D.Core.Components
{
    /// <summary>
    /// Represents a collider attached to an entity for collision detection.
    /// Defines the shape and properties of the collision boundary.
    /// </summary>
    public class CollisionComponent : IComponent
    {
        /// <summary>
        /// The type of collider shape. For MVP, we might only support Rectangle.
        /// (Could be an enum: Rectangle, Circle, Polygon).
        /// </summary>
        public ColliderShape Shape { get; set; } = ColliderShape.Rectangle;

        /// <summary>
        /// The size of the collider boundary. For a Rectangle, this is Width and Height.
        /// For a Circle, this might represent the radius (e.g., X for radius, Y unused).
        /// </summary>
        public Vector2 Size { get; set; } = Vector2.One;

        /// <summary>
        /// An optional offset from the entity's TransformComponent.Position.
        /// Useful if the collider isn't centered on the entity's origin.
        /// </summary>
        public Vector2 Offset { get; set; } = Vector2.Zero;

        /// <summary>
        /// A tag or type to categorize this collider (e.g., "Player", "Enemy", "Wall", "Pickup").
        /// Used by collision systems to filter interactions.
        /// </summary>
        public string Tag { get; set; } = string.Empty;

        /// <summary>
        /// Indicates if this collider is a trigger (detects overlap but doesn't cause physical response).
        /// </summary>
        public bool IsTrigger { get; set; } = false;

        // Enum for collider shapes
        public enum ColliderShape
        {
            Rectangle
            //Circle,
            // Add other shapes as needed
        }

        public CollisionComponent(Vector2 size, string tag = "Default", Vector2 offset = default, ColliderShape shape = ColliderShape.Rectangle, bool isTrigger = false)
        {
            Size = size;
            Tag = tag;
            Offset = offset;
            Shape = shape;
            IsTrigger = isTrigger;
        }

        public CollisionComponent() { }        
    }
}