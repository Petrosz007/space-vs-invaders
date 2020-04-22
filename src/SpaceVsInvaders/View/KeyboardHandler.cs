using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceVsInvaders.View
{
    public class KeyboardHandler
    {
        private Dictionary<Keys, bool> prevKeyState;

        public event EventHandler<Keys> KeyPressed;

        public KeyboardHandler()
        {
            prevKeyState = new Dictionary<Keys, bool>();
        }
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