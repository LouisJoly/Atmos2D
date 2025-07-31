using Atmos2D.Core;
using System;

namespace Atmos2D.GameExample
{
    /// <summary>
    /// Entry point of the game application.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            SnakeExample game = new SnakeExample(800, 600, "Snake Example");
            game.Run();
        }
    }
}