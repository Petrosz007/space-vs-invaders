using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace SpaceVsInvaders.View.Components
{
    public class ErrorDisplay : Component
    {
        
        private Texture2D Texture;
        public List<(string, int)> Errors;
        private int LastSecond;
        private SpriteFont Font;
        public ErrorDisplay(Vector2 position, int height, int width)
            : base(position, height, width)
        {
            Errors = new List<(string, int)>();
            LastSecond = 0;
            Texture = ContentLoader.CreateSolidtexture(Color.Beige);
            Font = ContentLoader.GetFont("Fonts/EpicFont");
        }

       public void AddError(string error)
       {
           Errors.Add((error, 2));
       }

        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.Seconds > LastSecond + 1)
            {
                LastSecond = gameTime.TotalGameTime.Seconds;
                for(int i = 0; i < Errors.Count; i++)
                {
                    if( Errors[i].Item2 > 0)  Errors[i] = (Errors[i].Item1, Errors[i].Item2 -1);
                    if(Errors[i].Item2 == 0) Errors.Remove(Errors[i]);
                }
            }
        }
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
            while(Errors.Count>0 && i < Errors.Count){
            int StringHalf = (int)Font.MeasureString(Errors[i].Item1).X / 2;
            spriteBatch.DrawString(Font,Errors[i].Item1, new Vector2(position.X+width/2-StringHalf, position.Y+30*i),Color.Red);
            i++;
            }
            
        }
    }
}

