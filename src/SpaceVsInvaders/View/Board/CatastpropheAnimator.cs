using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Boards
{
    /// <summary>
    /// Includes main informations about a Catastrophe
    /// </summary>
    public class Catastrophe
    {
        /// <summary>
        /// The type of the catastrophe
        /// </summary>
        /// <value></value>
        public CatastropheType type { get; set; }
        /// <summary>
        /// the X coordinate of the catastrophe
        /// </summary>
        /// <value>int</value>
        public int X { get; set; }
        /// <summary>
        /// The Y coordinate of the catastrophe
        /// </summary>
        /// <value>int</value>
        public double Y { get; set; }
        /// <summary>
        /// How many seconds the catastrophe is on the board
        /// </summary>
        /// <value>int</value>
        public int SecRemaining { get; set; }
        /// <summary>
        /// Constructor of a Catastrophe
        /// </summary>
        /// <param name="type">Type of the catastrophe</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="sec">Seconds on the board</param>
        public Catastrophe(CatastropheType type, int x, int y, int sec)
        {
            this.type = type;
            this.X = y;
            this.Y = x;
            this.SecRemaining = sec;
        }
    }
    /// <summary>
    /// Puts the catastrophes on the board
    /// </summary>
    public class CatastropheAnimator : Component
    {
        private int colWidth;
        private int rowHeight;
        private List<Catastrophe> catastrophes;
        private double LastSecond;
        private Texture2D texture;
        private Texture2D healingTexture;
        /// <summary>
        /// Constructor of CatastropheAnimator
        /// </summary>
        /// <param name="position">where it is on the board</param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="colWidth"></param>
        /// <param name="rowHeight"></param>
        public CatastropheAnimator(Vector2 position, int height, int width, int colWidth, int rowHeight)
            : base(position, height, width)
        {
            this.colWidth = colWidth;
            this.rowHeight = rowHeight;
            texture = ContentLoader.GetTexture("SvsI_SPrites/enemybase");
            healingTexture = ContentLoader.GetTexture("SvsI_SPrites/green-cross-png");
            catastrophes = new List<Catastrophe>();
        }

        /// <summary>
        /// This handles the function of the class, is called in every tick
        /// </summary>
        /// <param name="gameTime">The time elapsed ingame</param>
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

         /// <summary>
        /// Draws the Catastrophe on the screen
        /// </summary>
        /// <param name="spriteBatch">Includes texture of the Catastrophe</param>
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
        /// <summary>
        /// Eventhandler, that handles Damage Catastrophes
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">The information that is sent</param>
        public void HandleAsteroids(object sender, SVsIEventArgs args)
        {
            Catastrophe tmp = new Catastrophe(CatastropheType.Asteroid, args.Where.X, args.Where.Y, 0);
            
            catastrophes.Add(tmp);
        }

        /// <summary>
        /// Eventhandler, that handles Healing Catastrophes
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">The information that is sent</param>
         public void HandleHealing(object sender, SVsIEventArgs args)
        {
            Catastrophe tmp = new Catastrophe(CatastropheType.Healing, args.Where.X, args.Where.Y, 0);
            
            catastrophes.Add(tmp);  
        }
    }
}