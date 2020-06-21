using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collectables
{
    public enum CTYPE { TOOL,GOLD,POTION}
    public static class collectionTextures
    {
        public static Dictionary<CTYPE, Texture2D> textureforTypes = new Dictionary<CTYPE, Texture2D>();
    }

    public class Collectable

    {
        public CTYPE type;
        public int val;
        public Vector2 position;
        public bool collected;
        public Collectable(CTYPE t, Vector2 StartPos, int v)
        {
            val = v;
            type = t;
            position = StartPos;
            
        }

        public void Update(Player p)
        {
            
            Texture2D tx;

            if (collected) return;

            collectionTextures.textureforTypes.TryGetValue(type, out tx);
            if(tx != null)
            {
                Rectangle bound = new Rectangle(position.ToPoint(), tx.Bounds.Size);
                if (p.BoundingRect.Intersects(bound))
                {
                    collected = true;
                    p.Score += val;
                    p.collected.Add((Collectable)MemberwiseClone());
                    //p.collected.Add(this);
                }

            }


        }
       

        public void draw(SpriteFont font, SpriteBatch sp)
        {
            Texture2D tx;

            if (collected) return;

            collectionTextures.textureforTypes.TryGetValue(type, out tx);
            if (tx != null)
            {
                Vector2 valSize = font.MeasureString(val.ToString());
                int halfx = (int)position.X + tx.Width / 2 - (int)valSize.X/2;
                int y = (int)position.Y - 20;
                sp.Draw(tx, position, Color.White);
                sp.DrawString(font, val.ToString(), new Vector2(halfx, y), Color.White);
            }
        }

    }
}
