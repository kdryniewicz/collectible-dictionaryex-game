using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Collectables;
using Engine.Engines;
using Microsoft.Xna.Framework.Input;

namespace Sprites
{
    public class Player
    {
        public Texture2D Image;
        public Vector2 Position;
        public Rectangle BoundingRect;
        public int Score;
        public List<Collectable> collected;
        private bool show;

        // Constructor expects to see a loaded Texture
        // and a start position
        public Player( Texture2D spriteImage,
                            Vector2 startPosition)
        {
            //
            // Take a copy of the texture passed down
            Image = spriteImage;
            // Take a copy of the start position
            Position = startPosition;
            // Calculate the bounding rectangle
            BoundingRect = new Rectangle((int)startPosition.X, (int)startPosition.Y, Image.Width, Image.Height);
            collected = new List<Collectable>();
        }

        public void draw(SpriteFont font, SpriteBatch sp)
        {
            if (Image != null)
            {
                int halfx = (int)Position.X + Image.Width / 2;
                int y = (int)Position.Y - 20;
                sp.DrawString(font, Score.ToString(), new Vector2(halfx, y), Color.White);
                sp.Draw(Image, Position, Color.White);
            }
            if(show)
                foreach (var item in collected)
                    item.draw(font, sp);
        }
        public void showInventory()
        {
            var gold = collected.Where(c => c.type == CTYPE.GOLD).OrderBy(c => c.val);
            var potions = collected.Where(c => c.type == CTYPE.POTION);
            var tools = collected.Where(c => c.type == CTYPE.TOOL);
            Vector2 goldStart = new Vector2(64, 64);
            Vector2 potionStart = new Vector2(64, 128);
            Vector2 toolStart = new Vector2(64, 192);
            foreach (var item in gold)
            {
                item.position = goldStart;
                item.collected = false;
                goldStart += new Vector2(68, 0);
            }
            foreach (var item in potions)
            {
                item.position = potionStart;
                item.collected = false;
                potionStart += new Vector2(68, 0);
            }
            foreach (var item in tools)
            {
                item.position = toolStart;
                item.collected = false;
                toolStart += new Vector2(68, 0);
            }
        }
        public void Update()
        {
            BoundingRect = new Rectangle(Position.ToPoint(), Image.Bounds.Size);
            if (InputEngine.IsKeyPressed(Keys.U))
                show = !show;
            if (show)
            {
                showInventory();
               
            }
                

        }

        public void Move(Vector2 delta)
        {
            Position += delta;
            BoundingRect = new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);
            BoundingRect.X = (int)Position.X;
            BoundingRect.Y = (int)Position.Y;
        }

    }
}
