using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceVsInvaders.View.Components
{
    public abstract class Clickable : Component
    {
        private ButtonState prevLeftButtonState;
        private ButtonState prevRightButtonState;
        private bool wasMouseOver;
        public event EventHandler LeftClicked;
        public event EventHandler RightClicked;
        public event EventHandler MouseEnter;

        protected bool CurrentlyClicked { get; private set; }

        public Clickable(Vector2 position, int height, int width)
            : base(position, height, width)
        {
            wasMouseOver = false;
        }

        protected bool isMouseOver()
        {
            return area.Contains(Mouse.GetState().Position);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            CurrentlyClicked = false;

            if (isMouseOver())
            {
                // Emit click events
                if (prevLeftButtonState == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
                {
                    LeftClicked?.Invoke(this, new EventArgs());
                }

                if (prevRightButtonState == ButtonState.Released && mouseState.RightButton == ButtonState.Pressed)
                {
                    RightClicked?.Invoke(this, new EventArgs());
                }
                
                // Check if it is strill currently clicked
                if(mouseState.LeftButton == ButtonState.Pressed || mouseState.RightButton == ButtonState.Pressed)
                {
                    CurrentlyClicked = true;
                }

                if(!wasMouseOver)
                {
                    MouseEnter?.Invoke(this, new EventArgs());
                }
            }

            prevLeftButtonState = mouseState.LeftButton;
            prevRightButtonState = mouseState.RightButton;
            wasMouseOver = isMouseOver();
        }
    }
}