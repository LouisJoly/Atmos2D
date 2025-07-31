using System.Collections.Generic;
using Atmos2D.ECS;
using Atmos2D.Core.Components;

namespace Atmos2D.GameExample.Components
{
    public class SnakeInputComponent : InputComponent
    {
        public SnakeInputComponent()
        {
            WasActionJustPressed["MoveUp"] = false;
            WasActionJustPressed["MoveDown"] = false;
            WasActionJustPressed["MoveLeft"] = false;
            WasActionJustPressed["MoveRight"] = false;
            WasActionJustPressed["Quit"] = false; 
            IsActionPressed["MoveUp"] = false;
            IsActionPressed["MoveDown"] = false;
            IsActionPressed["MoveLeft"] = false;
            IsActionPressed["MoveRight"] = false;
            IsActionPressed["Quit"] = false; 
        }        
    }
}
