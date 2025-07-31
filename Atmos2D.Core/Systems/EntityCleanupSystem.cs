using Atmos2D.ECS;
using Atmos2D.Core.Components;
using System;
using System.Linq;

namespace Atmos2D.Core.Systems
{
    /// <summary>
    /// A generic system responsible for removing entities that have been marked with a RemovableComponent.
    /// This ensures entities are cleaned up properly at the end of a frame or update cycle.
    /// </summary>
    public class EntityCleanupSystem : ISystem
    {
        private readonly EntityManager _entityManager;

        /// <summary>
        /// Initializes a new instance of the EntityCleanupSystem.
        /// </summary>
        /// <param name="entityManager">The central EntityManager instance.</param>
        public EntityCleanupSystem(EntityManager entityManager)
        {
            _entityManager = entityManager ?? throw new ArgumentNullException(nameof(entityManager));
        }

        /// <summary>
        /// Iterates through entities marked for removal and removes them from the EntityManager.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        public void Update(float deltaTime)
        {
            // Get all entities marked as removable
            var entitiesToRemove = _entityManager.GetEntitiesWithComponent<RemovableComponent>().ToList();

            foreach (var entity in entitiesToRemove)
            {
                Console.WriteLine($"[EntityCleanupSystem] Removing entity: {entity.Id}");
                _entityManager.RemoveEntity(entity);
            }
            // Important: EntityManager.ProcessPendingChanges() will be called by SystemManager after all updates,
            // so these removals will be applied before the next frame.
        }

        /// <summary>
        /// Entity cleanup systems do not perform any drawing.
        /// </summary>
        public void Draw() { /* No drawing operations */ }
    }
}