using System;
using System.Collections.Generic;

namespace Atmos2D.ECS
{
    /// <summary>
    /// Represents an entity in the ECS system, identified by a unique ID and possessing components.
    /// </summary>
    public class Entity
    {
        public Guid Id { get; private set; }
        private readonly Dictionary<Type, IComponent> _components;

        public Entity()
        {
            Id = Guid.NewGuid();
            _components = new Dictionary<Type, IComponent>();
        }

        /// <summary>
        /// Adds a component to this entity.
        /// </summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        /// <param name="component">The instance of the component.</param>
        /// <exception cref="ArgumentException">Thrown if the component already exists.</exception>
        public void AddComponent<T>(T component) where T : class, IComponent
        {
            if (_components.ContainsKey(typeof(T)))
            {
                throw new ArgumentException($"Entity already has a component of type {typeof(T).Name}");
            }
            _components[typeof(T)] = component;
        }

        /// <summary>
        /// Retrieves a component from this entity.
        /// </summary>
        /// <typeparam name="T">The type of the component to retrieve.</typeparam>
        /// <returns>The component if found, otherwise null.</returns>
        public T GetComponent<T>() where T : class, IComponent
        {
            if (_components.TryGetValue(typeof(T), out var component))
            {
                return (T)component;
            }
            return null;
        }

        /// <summary>
        /// Checks if the entity possesses a component of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of the component to check.</typeparam>
        /// <returns>True if the entity possesses the component, otherwise false.</returns>
        public bool HasComponent<T>() where T : class, IComponent
        {
            return _components.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Checks if the entity possesses a component of a specified Type.
        /// </summary>
        /// <param name="componentType">The Type of the component to check.</param>
        /// <returns>True if the entity possesses the component, otherwise false.</returns>
        public bool HasComponent(Type componentType)
        {
            // Vérifie si le type fourni est bien un IComponent
            if (!typeof(IComponent).IsAssignableFrom(componentType))
            {
                throw new ArgumentException($"Type '{componentType.Name}' is not a valid component type (must implement IComponent).");
            }
            return _components.ContainsKey(componentType);
        }

        /// <summary>
        /// Removes a component from this entity.
        /// </summary>
        /// <typeparam name="T">The type of the component to remove.</typeparam>
        /// <returns>True if the component was removed, otherwise false.</returns>
        public bool RemoveComponent<T>() where T : class, IComponent
        {
            return _components.Remove(typeof(T));
        }

        /// <summary>
        /// Retrieves all component types attached to this entity.
        /// </summary>
        /// <returns>A collection of component types.</returns>
        public IReadOnlyCollection<Type> GetComponentTypes()
        {
            return _components.Keys;
        }

        /// <summary>
        /// <B>Retrieves all components attached to this entity.</B>
        /// This is the method we're adding for generic system access.
        /// </summary>
        /// <returns>An enumerable collection of all components.</returns>
        public IEnumerable<IComponent> GetAllComponents()
        {
            return _components.Values;
        }
    }
}