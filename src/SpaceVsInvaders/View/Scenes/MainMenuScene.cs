using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Scenes
{
    public class MainMenuScene : Scene
    {
        public event EventHandler<Difficulty> NewGame;
        public event EventHandler Exit;
        private List<Component> components;
        private Texture2D background;

        public MainMenuScene(int width, int height)
            : base(width, height)
        {

        }

        public override void LoadContent()
        {
            int btnWidth = 400;
            int btnHeight = 100;
            int btnMargin = 50;

            var normalGameButton = new Button(new Vector2((Width - btnWidth)/2, Height * 45 / 100), btnHeight, btnWidth, "Normal Game");
            normalGameButton.LeftClicked += new EventHandler((o, e) => NewGame?.Invoke(o, Difficulty.Normal));

            var hardGameButton = new Button(new Vector2((Width - btnWidth)/2, Height * 45 / 100 + (btnHeight + btnMargin)), btnHeight, btnWidth, "Hard Game");
            hardGameButton.LeftClicked += new EventHandler((o, e) => NewGame?.Invoke(o, Difficulty.Hard));

            var exitButton = new Button(new Vector2((Width - btnWidth)/2, Height * 45 / 100 + (btnHeight + btnMargin)*2), btnHeight, btnWidth, "Exit");
            exitButton.LeftClicked += new EventHandler((o, e) => Exit?.Invoke(o, e));

            components = new List<Component>
            {
                normalGameButton,
                hardGameButton,
                exitButton,
            };

            background = ContentLoader.GetTexture("Backgrounds/pause-background");
        }
        public override void UnloadContent()
        {

        }
        public override void Update(GameTime gameTime)
        {           
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