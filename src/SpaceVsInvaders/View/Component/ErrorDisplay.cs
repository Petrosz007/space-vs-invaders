using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Audio;

namespace SpaceVsInvaders.View.Components
{
    /// <summary>
    /// Displays errors ingame
    /// </summary>
    public class ErrorDisplay : Component
    {
        
        private Texture2D Texture;
        /// <summary>
        /// List of Errors
        /// </summary>
        public List<(string, int)> Errors;
        private double LastSecond;
        private SpriteFont Font;
        private SoundEffectInstance soundEffectInstance;
        /// <summary>
        /// constructor of ErroDisplay
        /// </summary>
        /// <param name="position"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public ErrorDisplay(Vector2 position, int height, int width)
            : base(position, height, width)
        {
            Errors = new List<(string, int)>();
            LastSecond = 0;
            Texture = ContentLoader.CreateSolidtexture(Color.Beige);
            Font = ContentLoader.GetFont("Fonts/InfoFont");

            soundEffectInstance = ContentLoader.GetSoundEffect("Sounds/error").CreateInstance();
        }

        /// <summary>
        /// Adds an error to the Error List
        /// </summary>
        /// <param name="error">This is the message of the error</param>
       public void AddError(string error)
       {
           Errors.Add((error, 2));
           soundEffectInstance.Play();
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

                var toBeRemoved = new List<(string, int)>();
                for(int i = 0; i < Errors.Count; i++)
                {
                    if( Errors[i].Item2 > 0)  Errors[i] = (Errors[i].Item1, Errors[i].Item2 - 1);
                    else toBeRemoved.Add(Errors[i]);
                }

                Errors.RemoveAll(e => toBeRemoved.Contains(e));
            }
        }

        /// <summary>
        /// Draws the error on the screen
        /// </summary>
        /// <param name="spriteBatch">The texture of the Error</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle divRect = new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    width,
                    35*Errors.Count
                );

            spriteBatch.Draw(Texture, divRect, Color.Black*0.5f); 
            int i = 0;
            while(Errors.Count>0 && i < Errors.Count)
            {
                var measure = Font.MeasureString(Errors[i].Item1);
                spriteBatch.DrawString(Font,Errors[i].Item1, new Vector2(position.X + (width - measure.X)/2, position.Y + (measure.Y / 2) + (35 * i)),Color.Red);
                i++;
            }
            
        }
    }
}

