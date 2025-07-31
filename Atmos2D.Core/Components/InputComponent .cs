using Atmos2D.ECS;
using System.Collections.Generic;

// Note: This component is primarily a marker to indicate an entity
// should be processed by an InputSystem. Actual input states
// can be stored here for simpler entities, or an InputSystem
// could publish events that other systems listen to.

// Might add specific key codes or mouse button enums later if needed,
// but string-based actions are more flexible for mapping.
namespace Atmos2D.Core.Components
{
    /// <summary>
    /// Marks an entity as capable of receiving and reacting to user input.
    /// Stores the current state of specific inputs relevant to this entity.
    /// </summary>
    public class InputComponent : IComponent
    {
        /// <summary>
        /// Dictionary to store the pressed state of specific keys relevant to this entity.
        /// Key: String identifier for the input action (e.g., "MoveLeft", "Jump").
        /// Value: True if the key associated with the action is currently pressed.
        /// </summary>
        public Dictionary<string, bool> IsActionPressed { get; private set; } = new Dictionary<string, bool>();

        /// <summary>
        /// Dictionary to store if an input action was just pressed in the current frame.
        /// </summary>
        public Dictionary<string, bool> WasActionJustPressed { get; private set; } = new Dictionary<string, bool>();

        /// <summary>
        /// Indicates if the left mouse button is currently pressed for this entity.
        /// </summary>
        public bool IsMouseLeftPressed { get; set; } = false;

        /// <summary>
        /// Indicates if the right mouse button is currently pressed for this entity.
        /// </summary>
        public bool IsMouseRightPressed { get; set; } = false;

        /// <summary>
        /// The current X-coordinate of the mouse cursor relative to the window.
        /// </summary>
        public float MouseX { get; set; } = 0.0f;

        /// <summary>
        /// The current Y-coordinate of the mouse cursor relative to the window.
        /// </summary>
        public float MouseY { get; set; } = 0.0f;

        public InputComponent() { }
    }
}