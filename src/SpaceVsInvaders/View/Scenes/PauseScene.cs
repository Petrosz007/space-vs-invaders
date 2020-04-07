using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Scenes
{
    public class PauseScene : Scene
    {
        public event EventHandler Resume;
        public event EventHandler Exit;
        private List<Component> components;
        private Texture2D background;

        private bool prevEscapeState;
        public PauseScene(int width, int height)
            : base(width, height)
        {

        }

        public override void LoadContent()
        {
            prevEscapeState = true;

            int btnWidth = 400;
            int btnHeight = 100;
            int btnMargin = 100;

            var resumeButton = new Button(new Vector2((Width - btnWidth)/2, Height * 45 / 100), btnHeight, btnWidth, "Resume");
            resumeButton.LeftClicked += new EventHandler((o, e) => Resume?.Invoke(o, e));

            var exitButton = new Button(new Vector2((Width - btnWidth)/2, Height * 45 / 100 + btnHeight + btnMargin), btnHeight, btnWidth, "Exit");
            exitButton.LeftClicked += new EventHandler((o, e) => Exit?.Invoke(o, e));

            components = new List<Component>
            {
                resumeButton,
                exitButton,
            };

            background = ContentLoader.GetTexture("Backgrounds/pause-background");
        }
        public override void UnloadContent()
        {

        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) != prevEscapeState && !prevEscapeState)
            {
                Resume?.Invoke(this, new EventArgs());
            }
            prevEscapeState = Keyboard.GetState().IsKeyDown(Keys.Escape);
            
            foreach(var component in components)
            {
                component.Update(gameTime);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0,0,Width,Height), new Rectangle(0,0,background.Width,background.Height), Color.White);

            foreach(var component in components)
            {
                component.Draw(spriteBatch);
            }
        }
    }
}