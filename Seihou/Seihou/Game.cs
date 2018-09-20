﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Seihou
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        readonly EntityManager entityManager;
        Player player;
        LevelManager levelManager;
        SpriteFont font;

        public Game()
        {
			this.IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = Global.screenWidth;
			graphics.PreferredBackBufferHeight = Global.screenHeight;
			graphics.ApplyChanges();
            
			Content.RootDirectory = "Content";
            font = Content.Load<SpriteFont>("DefaultFont");
            entityManager = new EntityManager();
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            ResourceManager.Load(Content);
        }
		
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            levelManager = new LevelManager(entityManager);
            levelManager.LoadLevel(new Level1(spriteBatch, entityManager));
            
            player = new Player(new Vector2(300,300), spriteBatch, entityManager);
            entityManager.AddEntity(player);


            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            entityManager.Update(gameTime);
            levelManager.Update(gameTime);

			if (Keyboard.GetState().IsKeyDown(Keys.F11) || (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.Enter)))
			{
				graphics.ToggleFullScreen();
				graphics.SynchronizeWithVerticalRetrace = true;
				graphics.ApplyChanges();
			}

			base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            
            entityManager.Draw(gameTime);

			UI.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(font, $"FPS: {1 / gameTime.ElapsedGameTime.TotalSeconds} \nENTITIES: {entityManager.GetEntityCount()} \nREMOVE {entityManager.GetPollRemoveEntityCount()}", new Vector2(20, 20), Color.Green);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
