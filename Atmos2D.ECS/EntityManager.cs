using System;
using System.Collections.Generic;
using System.Linq;

namespace Atmos2D.ECS
{
    /// <summary>
    /// Manages the creation, storage, and retrieval of entities and their components.
    /// </summary>
    public class EntityManager
    {
        private readonly List<Entity> _entities;
        private readonly List<Entity> _entitiesToAdd;
        private readonly List<Entity> _entitiesToRemove;

        public EntityManager()
        {
            _entities = new List<Entity>();
            _entitiesToAdd = new List<Entity>();
            _entitiesToRemove = new List<Entity>();
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <returns>The newly created entity.</returns>
        public Entity CreateEntity()
        {
            var entity = new Entity();
            _entitiesToAdd.Add(entity); // Deferred addition
            return entity;
        }

        /// <summary>
        /// Marks an entity for deletion. Deletion is deferred until the end of the frame.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void RemoveEntity(Entity entity)
        {
            _entitiesToRemove.Add(entity);
        }

        /// <summary>
        /// Retrieves all entities possessing all specified component types.
        /// </summary>
        /// <param name="componentTypes">The types of components that the entity must possess.</param>
        /// <returns>A collection of corresponding entities.</returns>
        public IEnumerable<Entity> GetEntitiesWithComponents(params Type[] componentTypes)
        {
            foreach (var entity in _entities)
            {
                bool hasAllComponents = true;
                foreach (var compType in componentTypes)
                {
                    if (!entity.HasComponent(compType))
                    {
                        hasAllComponents = false;
                        break;
                    }
                }
                if (hasAllComponents)
                {
                    yield return entity;
                }
            }
        }
        
        /// <summary>
        /// Retrieves all entities possessing the specified component type.
        /// Optimized for a single type.
        /// </summary>
        /// <typeparam name="T">The type of component to verify.</typeparam>
        /// <returns>A collection of entities possessing the component.</returns>
        public IEnumerable<Entity> GetEntitiesWithComponent<T>() where T : class, IComponent
        {
            // Note: This implementation is rudimentary. An advanced implementation would use a cache per component type.
            return _entities.Where(e => e.HasComponent<T>());
        }

        /// <summary>
        /// Processes entities to add and remove at the end of the frame.
        /// </summary>
        public void ProcessPendingChanges()
        {
            foreach (var entity in _entitiesToAdd)
            {
                _entities.Add(entity);
            }
            _entitiesToAdd.Clear();

            foreach (var entity in _entitiesToRemove)
            {
                _entities.Remove(entity);
            }
            _entitiesToRemove.Clear();
        }

        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <returns>The entity if found, otherwise null.</returns>
        public Entity GetEntityById(Guid id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }
    }
}