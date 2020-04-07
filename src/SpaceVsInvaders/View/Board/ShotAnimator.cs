using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Boards
{
    public class Shot
    {
        public int X { get; set; }
        public double Y { get; set; }
        public int FromY { get; set; }
        public int ToY { get; set; }
        public double SecRemaining { get; set; }
    }
    public class ShotAnimator : Component
    {
        private readonly double ShotSpeed = Config.GetValue<double>("ShotSpeed");
        private int colWidth;
        private int rowHeight;
        private List<Shot> shots;
        public ShotAnimator(Vector2 position, int height, int width, int colWidth, int rowHeight)
            : base(position, height, width)
        {
            this.colWidth = colWidth;
            this.rowHeight = rowHeight;

            shots = new List<Shot>();
        }

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
        }
    }
}