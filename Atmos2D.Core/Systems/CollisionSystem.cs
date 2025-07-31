using Atmos2D.ECS;
using Atmos2D.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Atmos2D.Core.Systems
{
    /// <summary>
    /// Handles generic collision detection between entities that possess
    /// a TransformComponent and a CollisionComponent.
    /// It performs AABB (Axis-Aligned Bounding Box) collision checks
    /// and notifies ICollidable components of detected collisions.
    /// </summary>
    public class CollisionSystem : ISystem
    {
        private readonly EntityManager _entityManager;

        /// <summary>
        /// Initializes a new instance of the CollisionSystem.
        /// </summary>
        /// <param name="entityManager">The central EntityManager instance.</param>
        public CollisionSystem(EntityManager entityManager)
        {
            _entityManager = entityManager ?? throw new ArgumentNullException(nameof(entityManager));
        }

        /// <summary>
        /// Updates the collision state for all relevant entities.
        /// This method performs collision detection for the current frame.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        public void Update(float deltaTime)
        {
            // Get all entities that can collide
            var collidableEntities = _entityManager.GetEntitiesWithComponent<TransformComponent>()
                                                    .Where(e => e.HasComponent<CollisionComponent>())
                                                    .ToList(); // Convert to list to avoid multiple enumerations

            // Simple N^2 collision check (can be optimized for larger scenes using spatial partitioning)
            for (int i = 0; i < collidableEntities.Count; i++)
            {
                var entityA = collidableEntities[i];
                var transformA = entityA.GetComponent<TransformComponent>();
                var collisionA = entityA.GetComponent<CollisionComponent>();

                for (int j = i + 1; j < collidableEntities.Count; j++)
                {
                    var entityB = collidableEntities[j];
                    var transformB = entityB.GetComponent<TransformComponent>();
                    var collisionB = entityB.GetComponent<CollisionComponent>();

                    // For now, only handle Rectangle vs Rectangle collision.
                    // Extend with other shapes (Circle, etc.) as needed.
                    if (collisionA.Shape == CollisionComponent.ColliderShape.Rectangle &&
                        collisionB.Shape == CollisionComponent.ColliderShape.Rectangle)
                    {
                        if (CheckAABBCollision(transformA, collisionA, transformB, collisionB))
                        {
                            // Notify ICollidable components on both entities
                            NotifyCollision(entityA, entityB);
                            NotifyCollision(entityB, entityA);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Performs an Axis-Aligned Bounding Box (AABB) collision check.
        /// </summary>
        private bool CheckAABBCollision(TransformComponent tA, CollisionComponent cA, TransformComponent tB, CollisionComponent cB)
        {
            // Calculate effective AABB for Entity A
            Vector2 minA = tA.Position + cA.Offset - (cA.Size * tA.Scale / 2.0f);
            Vector2 maxA = tA.Position + cA.Offset + (cA.Size * tA.Scale / 2.0f);

            // Calculate effective AABB for Entity B
            Vector2 minB = tB.Position + cB.Offset - (cB.Size * tB.Scale / 2.0f);
            Vector2 maxB = tB.Position + cB.Offset + (cB.Size * tB.Scale / 2.0f);
            // AABB Overlap Check
            return minA.X < maxB.X && maxA.X > minB.X &&
                   minA.Y < maxB.Y && maxA.Y > minB.Y;
        }

        /// <summary>
        /// Notifies all ICollidable components on an entity that a collision has occurred.
        /// </summary>
        /// <param name="entity">The entity whose ICollidable components will be notified.</param>
        /// <param name="otherEntity">The entity it collided with.</param>
        private void NotifyCollision(Entity entity, Entity otherEntity)
        {
            // Get all components of the entity that implement ICollidable
            var collidableComponents = entity.GetAllComponents().OfType<ICollidable>();

            foreach (var collidable in collidableComponents)
            {
                collidable.OnCollisionEnter(otherEntity);
            }
        }

        /// <summary>
        /// Collision systems typically do not perform any drawing.
        /// </summary>
        public void Draw() { /* No drawing operations for collision systems */ }
    }
}