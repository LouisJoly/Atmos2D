using Atmos2D.ECS;

namespace Atmos2D.Core.Components
{
    /// <summary>
    /// Interface for components that can be notified of a collision.
    /// Implementing components can then define specific reactions to collisions.
    /// </summary>
    public interface ICollidable : IComponent // It should also be a component
    {
        /// <summary>
        /// Called when this component's entity has collided with another entity.
        /// </summary>
        /// <param name="otherEntity">The entity that collided with this one.</param>
        void OnCollisionEnter(Entity otherEntity);
    }
}