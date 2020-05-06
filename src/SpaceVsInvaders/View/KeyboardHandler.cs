using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceVsInvaders.View
{
    /// <summary>
    /// Handles key presses and emits events accordingly
    /// </summary>
    public class KeyboardHandler
    {
        private Dictionary<Keys, bool> prevKeyState;

        /// <summary>
        /// Event of a key pressed
        /// </summary>
        public event EventHandler<Keys> KeyPressed;

        /// <summary>
        /// Constructor of <c>KeyboardHandler</c>
        /// </summary>
        public KeyboardHandler()
        {
            prevKeyState = new Dictionary<Keys, bool>();
        }
        
        /// <summary>
        /// Checks the key states and emits pressed events
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public void Update(GameTime gameTime)
        {
            var pressed = prevKeyState.Keys.Select(key => (key, false)).ToList();

            foreach(var key in Keyboard.GetState().GetPressedKeys())
            {
                if(prevKeyState.GetValueOrDefault(key) == false)
                {
                    KeyPressed?.Invoke(this, key);
                }

                pressed.Add((key, true));
            }

            foreach((var key, bool state) in pressed)
            {
                prevKeyState[key] = state;
            }
        }
    }
}