using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Boards
{
    public class Catastrophe
    {
        public CatastropheType type { get; set; }
        public int X { get; set; }
        public double Y { get; set; }
        public int SecRemaining { get; set; }
        public Catastrophe(CatastropheType type, int x, int y, int sec)
        {
            this.type = type;
            this.X = y;
            this.Y = x;
            this.SecRemaining = sec;
        }
    }
    public class CatastropheAnimator : Component
    {
        private int colWidth;
        private int rowHeight;
        private List<Catastrophe> catastrophes;
        private double LastSecond;
        private Texture2D texture;
        private Texture2D healingTexture;
        public CatastropheAnimator(Vector2 position, int height, int width, int colWidth, int rowHeight)
            : base(position, height, width)
        {
            this.colWidth = colWidth;
            this.rowHeight = rowHeight;
            texture = ContentLoader.GetTexture("SvsI_SPrites/enemybase");
            healingTexture = ContentLoader.GetTexture("SvsI_SPrites/green-cross-png");
            catastrophes = new List<Catastrophe>();
        }

        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalSeconds > LastSecond + 1)
            {
                LastSecond = gameTime.TotalGameTime.TotalSeconds;

                var toBeRemoved = new List<Catastrophe>();
                for(int i = 0; i < catastrophes.Count; i++)
                {
                    if(catastrophes[i].SecRemaining > 0)  catastrophes[i].SecRemaining--;
                    else toBeRemoved.Add(catastrophes[i]);
                }
                
                catastrophes.RemoveAll(c => toBeRemoved.Contains(c));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(var item in catastrophes)
            {
                var rect = new Rectangle(
                    (int) position.X + (colWidth * item.X),
                    (int) (position.Y + (rowHeight * item.Y)),
                    colWidth,
                    rowHeight
                );

                if (item.type == CatastropheType.Asteroid)
                {
                    spriteBatch.Draw(texture, rect,
                    new Rectangle(0, 0, texture.Width, texture.Height), Color.White*0.5f);
                }
                else
                {
                    spriteBatch.Draw(healingTexture, rect,
                     new Rectangle(0, 0, healingTexture.Width, healingTexture.Height), Color.White*0.5f);
                }
                
              
            }
        }

        public void HandleAsteroids(object sender, SVsIEventArgs args)
        {
            Catastrophe tmp = new Catastrophe(CatastropheType.Asteroid, args.Where.X, args.Where.Y, 0);
            
            catastrophes.Add(tmp);
        }
         public void HandleHealing(object sender, SVsIEventArgs args)
        {
            Catastrophe tmp = new Catastrophe(CatastropheType.Healing, args.Where.X, args.Where.Y, 0);
            
            catastrophes.Add(tmp);  
        }
    }
}