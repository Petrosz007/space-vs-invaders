using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceVsInvaders.View.Components
{
    /// <summary>
    /// Component that emits click events and handles mouse hovers
    /// </summary>
    public abstract class Clickable : Component
    {
        private ButtonState prevLeftButtonState;
        private ButtonState prevRightButtonState;
        private bool wasMouseOver;

        /// <summary>
        /// The component has been left clicked event
        /// </summary>
        public event EventHandler LeftClicked;

        /// <summary>
        /// The component has been right clicked event
        /// </summary>
        public event EventHandler RightClicked;

        /// <summary>
        /// The mouse entered the area of the component
        /// </summary>
        public event EventHandler MouseEnter;

        /// <summary>
        /// Whether the component is currently clicked
        /// </summary>
        /// <value>Whether the component is currently clicked</value>
        protected bool CurrentlyClicked { get; private set; }

        /// <summary>
        /// Constructor of <c>Clickable</c>
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="height">Height of the component</param>
        /// <param name="width">Width of the component</param>
        public Clickable(Vector2 position, int height, int width)
            : base(position, height, width)
        {
            wasMouseOver = false;
        }

        /// <summary>
        /// Returns whether the mouse is over this component
        /// </summary>
        /// <returns>Whether the mouse is over this component</returns>
        protected bool isMouseOver()
        {
            return area.Contains(Mouse.GetState().Position);
        }

        /// <summary>
        /// Handles mouse clicks and hovers based on the current mouse position
        /// </summary>
        /// <param name="gameTime">Game time</param>
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