using System;
using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Sprites;

namespace Collectables
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Collectable> collectables = new List<Collectable>();
        private Player p;
        private SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            new InputEngine(this);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            collectionTextures.textureforTypes[CTYPE.GOLD] = Content.Load<Texture2D>(@"Collectables\Gold");
            collectionTextures.textureforTypes[CTYPE.POTION] = Content.Load<Texture2D>(@"Collectables\PotionYellow");
            collectionTextures.textureforTypes[CTYPE.TOOL] = Content.Load<Texture2D>(@"Collectables\Tool");
            p = new Player(Content.Load<Texture2D>("Player"), new Vector2(100, 100));
            createCollectables();
            font = Content.Load<SpriteFont>("font");

            // TODO: use this.Content to load your game content here
        }

        private void createCollectables()
        {
            Random r = new Random();
            int count = r.Next(10,20);
            for (int i = 0; i < count; i++)
            {
                collectables.Add(new
                    Collectable((CTYPE)r.Next(0, 2),
                    new Vector2(r.Next(GraphicsDevice.Viewport.Width - 64)
                            , r.Next(GraphicsDevice.Viewport.Height - 64)), 
                    r.Next(10,25)
                    ));
            }
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (InputEngine.IsKeyPressed(Keys.R))
                reset();
            if (GraphicsDevice.Viewport.Bounds.Contains(InputEngine.MousePosition))
                if (InputEngine.MousePosition != InputEngine.PreviousMouseState.Position.ToVector2())
                    p.Position = InputEngine.MousePosition;
            // To update the position of the Bounding box of the player
            p.Update();
            // Se if the collectables are collected by the player
            foreach (var c in collectables)
                c.Update(p);

            base.Update(gameTime);
        }

        private void reset()
        {
            collectables.Clear();
            createCollectables();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
                p.draw(font,spriteBatch);
                foreach (var c in collectables)
                    c.draw(font, spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
