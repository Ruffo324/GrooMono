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

        /// <summary>
        /// KeyboardState from last Game update.
        /// </summary>
        public KeyboardState LastKeyboardState;

        protected GraphicsDeviceManager Graphics;

        /// <summary>
        /// Size of game window.
        /// </summary>
        public Size ScreenSize;

        /// <summary>
        /// Sprite batch of the game.
        /// </summary>
        protected SpriteBatch SpriteBatch;

        public GameInstance(string contentRootDirectory = "Content")
        {
            // Instace already setten -> exception.
            if (Instance != null)
                throw new GameInstanceException("One game can only have one GameInstance!");

            // Setup graphics & content.
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = contentRootDirectory;

            // Remeber current instance.
            Instance = this;
        }

        public float Skyratio { get; set; } = 0.9333f;

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
            // Set LastKeyboardState.
            LastKeyboardState = Keyboard.GetState();

            // Call event & base.
            ManagerUpdate.ManageNow(gameTime, LastKeyboardState);
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