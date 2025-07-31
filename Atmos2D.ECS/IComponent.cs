namespace Atmos2D.ECS
{
    /// <summary>
    /// Marker interface for all components.
    /// Components should only contain data.
    /// </summary>
    public interface IComponent
    {
        // Components typically don't have methods, just data.
        // This interface serves to identify classes as components.
    }
}