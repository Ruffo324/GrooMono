using System.Drawing;
using GrooMono.Core.GrooGame.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;

namespace GrooMono.Core.GrooGame
{
    public class GameInstance : Game
    {
        public static GameInstance Instance;
        public readonly ManagerContent ManagerContent = new ManagerContent();
        public readonly ManagerDraw ManagerDraw = new ManagerDraw();
        public readonly ManagerInitialization ManagerInitialization = new ManagerInitialization();
        public readonly ManagerUpdate ManagerUpdate = new ManagerUpdate();

        protected GraphicsDeviceManager Graphics;

        public Size ScreenSize;
        protected SpriteBatch SpriteBatch;

        public GameInstance()
        {
            if (Instance != null)
                throw new GameInstanceException("One game can only have one GameInstance!");

            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content"; //TODO: Check this

            Instance = this;
        }

        public float Skyratio { get; set; }

        protected override void Initialize()
        {
            // Set screensize
            ScreenSize.Height = Window.ClientBounds.Height;
            ScreenSize.Width = Window.ClientBounds.Width;

            // Call event and base
            ManagerInitialization.ManageNow();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            ManagerContent.ManageNow();
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            ManagerUpdate.ManageNow(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            ManagerDraw.ManageNow(gameTime, SpriteBatch);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}