namespace Atmos2D.ECS
{
    /// <summary>
    /// Interface for all ECS systems.
    /// Systems contain game logic and operate on entities.
    /// </summary>
    public interface ISystem
    {
        /// <summary>
        /// Updates the system logic.
        /// </summary>
        /// <param name="deltaTime">Time elapsed since the last frame.</param>
        void Update(float deltaTime);

        /// <summary>
        /// Draws the elements managed by the system (optional).
        /// </summary>
        void Draw();
    }
}