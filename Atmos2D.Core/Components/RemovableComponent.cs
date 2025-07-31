using Atmos2D.ECS;

namespace Atmos2D.Core.Components
{
    /// <summary>
    /// A marker component indicating that the entity should be removed from the game world.
    /// This allows systems to flag entities for deletion, which is then handled by a dedicated cleanup system.
    /// </summary>
    public class RemovableComponent : IComponent
    {
        // No specific data needed, its presence is enough to signal removal.
        public RemovableComponent() { }
    }
}