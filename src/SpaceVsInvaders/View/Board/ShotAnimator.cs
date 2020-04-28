using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Boards
{
    /// <summary>
    /// Data of a shot
    /// </summary>
    public class Shot
    {
        public int X { get; set; }
        public double Y { get; set; }
        public int FromY { get; set; }
        public int ToY { get; set; }
        public double SecRemaining { get; set; }
    }
    /// <summary>
    /// Shot Animator component
    /// </summary>
    public class ShotAnimator : Component
    {
        private readonly double ShotSpeed = Config.GetValue<double>("ShotSpeed");
        private int colWidth;
        private int rowHeight;
        private List<Shot> shots;
        private SoundEffectInstance soundEffectInstance;

        /// <summary>
        /// Constructor of <c>ShotAnimator</c>
        /// </summary>
        /// <param name="position">Position of the board to overlay on</param>
        /// <param name="height">Height of the board to overlay on</param>
        /// <param name="width">Width of the board to overlay on</param>
        /// <param name="colWidth">Column width</param>
        /// <param name="rowHeight">Row height</param>
        public ShotAnimator(Vector2 position, int height, int width, int colWidth, int rowHeight)
            : base(position, height, width)
        {
            this.colWidth = colWidth;
            this.rowHeight = rowHeight;

            shots = new List<Shot>();

            soundEffectInstance = ContentLoader.GetSoundEffect("Sounds/laser").CreateInstance();
            soundEffectInstance.Volume = 0.6f;
        }

        /// <summary>
        /// Update each shot's location
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        public override void Update(GameTime gameTime)
        {
            double elapsedSec = gameTime.ElapsedGameTime.TotalSeconds;

            shots.RemoveAll(shot => shot.SecRemaining < 0);

            for(int i = 0; i < shots.Count; ++i)
            {
                shots[i].SecRemaining -= elapsedSec;
                shots[i].Y += (shots[i].ToY - shots[i].FromY) * (elapsedSec / ShotSpeed);

            }
        }

        /// <summary>
        /// Draw each shot to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Spritebatch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(var shot in shots)
            {
                var rect = new Rectangle(
                    (int) position.X + (colWidth * shot.X) + (colWidth + 5)/2,
                    (int) (position.Y + (rowHeight * shot.Y)) + (rowHeight - 50)/2,
                    5,
                    50
                );
                spriteBatch.Draw(ContentLoader.CreateSolidtexture(Color.Red), rect, Color.White);
            }
        }

        /// <summary>
        /// Handles the addition of a enw shot
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">Event args</param>
        public void HandleNewShot(object sender, SVsIEventArgs args)
        {
            var shot = new Shot
            {
                X = args.From.Y,
                Y = args.From.X,
                FromY = args.From.X,
                ToY = args.To.X,
                SecRemaining = ShotSpeed
            };

            shots.Add(shot);

            soundEffectInstance.Play();
        }
    }
}