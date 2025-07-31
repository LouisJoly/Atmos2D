using System.Collections.Generic;
using System.Linq;

namespace Atmos2D.ECS
{
    /// <summary>
    /// Manages the registration and execution of ECS systems.
    /// </summary>
    public class SystemManager
    {
        private readonly List<ISystem> _systems;
        private readonly EntityManager _entityManager;

        public SystemManager(EntityManager entityManager)
        {
            _systems = new List<ISystem>();
            _entityManager = entityManager;
        }

        /// <summary>
        /// Registers a new system with the manager.
        /// </summary>
        /// <param name="system">The system to register.</param>
        public void RegisterSystem(ISystem system)
        {
            _systems.Add(system);
            // Optional: Sort systems here if execution order is important (ex: Physics before Render)
        }

        /// <summary>
        /// Unregisters a system from the manager.
        /// </summary>
        /// <param name="system">The system to unregister.</param>
        public void UnregisterSystem(ISystem system)
        {
            _systems.Remove(system);
        }

        /// <summary>
        /// Updates all registered systems.
        /// </summary>
        /// <param name="deltaTime">Time elapsed since the last frame.</param>
        public void Update(float deltaTime)
        {
            // Process entity changes before updating systems
            _entityManager.ProcessPendingChanges(); 

            foreach (var system in _systems)
            {
                system.Update(deltaTime);
            }
        }

        /// <summary>
        /// Asks all registered systems to draw if necessary.
        /// </summary>
        public void Draw()
        {
            foreach (var system in _systems)
            {
                system.Draw();
            }
        }
    }
}