using Atmos2D.ECS;
using Atmos2D.Core.Components; // Pour CollisionComponent
using System;

namespace Atmos2D.GameExample.Components
{
    /// <summary>
    /// Marks an entity as a food item for the snake and defines its collision behavior.
    /// </summary>
    public class FoodComponent : IComponent, ICollidable
    {
        public bool IsEaten { get; set; } = false;

        public FoodComponent() { }

        /// <summary>
        /// Reacts to a collision with another entity.
        /// When the food is eaten by the player, it sets its IsEaten flag.
        /// </summary>
        /// <param name="otherEntity">The entity that collided with the food.</param>
        public void OnCollisionEnter(Entity otherEntity)
        {
            if (otherEntity.HasComponent<CollisionComponent>())
            {
                var otherCollision = otherEntity.GetComponent<CollisionComponent>();

                if (otherCollision.Tag == "SnakeHead")
                {
                    IsEaten = true; // Mark itself as eaten. The FoodSpawnSystem will handle actual removal and spawning.
                    Console.WriteLine($"[FoodComponent] Food was eaten by player! Flagged for removal and new one to be spawned.");
                }
            }
        }
    }
}