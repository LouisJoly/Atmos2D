using Atmos2D.ECS;
using Atmos2D.Core.Managers;
using Atmos2D.Core.Systems;
using Atmos2D.Core.Components;
using Atmos2D.GameExample.Components;
using System.Collections.Generic;
using static Raylib_cs.Raylib;

using KeyboardKey = Raylib_cs.KeyboardKey;

namespace Atmos2D.GameExample.Systems
{
    public class SnakeInputSystem : ISystem
    {
        private readonly EntityManager _entityManager;

        public SnakeInputSystem(EntityManager entityManager)
        {
            _entityManager = entityManager ?? throw new ArgumentNullException(nameof(entityManager));
        }

        public void Update(float deltaTime)
        {
            var inputEntities = _entityManager.GetEntitiesWithComponent<SnakeInputComponent>().ToList();
            foreach (var entity in inputEntities)
            {
                var input = entity.GetComponent<SnakeInputComponent>();
                if (input == null) continue;

                foreach (var key in input.WasActionJustPressed.Keys.ToList())
                {
                    input.WasActionJustPressed[key] = false;
                }             

                UpdateActionState(input, "MoveUp", KeyboardKey.KEY_W);
                UpdateActionState(input, "MoveUp", KeyboardKey.KEY_UP);
                UpdateActionState(input, "MoveRight", KeyboardKey.KEY_D);
                UpdateActionState(input, "MoveRight", KeyboardKey.KEY_RIGHT);
                UpdateActionState(input, "MoveDown", KeyboardKey.KEY_S);
                UpdateActionState(input, "MoveDown", KeyboardKey.KEY_DOWN);
                UpdateActionState(input, "MoveLeft", KeyboardKey.KEY_Q);
                UpdateActionState(input, "MoveLeft", KeyboardKey.KEY_LEFT);
            }
        }

        /// <summary>
        /// Helper method to update the state of an input action.
        /// </summary>
        /// <param name="inputComponent">The InputComponent to update.</param>
        /// <param name="actionName">The name of the action (e.g., "MoveRight").</param>
        /// <param name="key">The Raylib KeyboardKey associated with this action.</param>
        private void UpdateActionState(SnakeInputComponent inputComponent, string actionName, KeyboardKey key)
        {
            bool isCurrentlyPressed = IsKeyDown(key);
            bool wasPressedInPreviousFrame = inputComponent.IsActionPressed.ContainsKey(actionName) && inputComponent.IsActionPressed[actionName];
            bool justPressed = IsKeyPressed(key); // Raylib's IsKeyPressed checks for just pressed this frame

            inputComponent.IsActionPressed[actionName] = isCurrentlyPressed;
            inputComponent.WasActionJustPressed[actionName] = justPressed;
        }

        public void Draw() { /* No drawing operations for input systems */ }
    }
}