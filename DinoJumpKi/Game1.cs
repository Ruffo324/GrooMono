using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DinoJumpKi
{
    public class Game1 : Game
    {
        // Tutorial
        private const float Skyratio = 0.9333f;

        // Debug
        private SpriteFont _fontDebug;
        private SpriteFont _fontScore;
        private SpriteFont _fontState;

        private bool _gameOver;

        private bool _gameStarted;
        private GraphicsDeviceManager _graphics;
        private float _gravitySpeed;
        private float _pinguinJumpY;
        private float _pinguinSpeedX;
        private Random _random;
        private float _score;
        private float _screenHeight;

        private float _screenWidth;
        private bool _spaceDown;
        private SpriteBatch _spriteBatch;

        private Texture2D _textureGameOver;
        private Texture2D _textureIceGround;
        private SpriteClass _texturePinguin;

        private Texture2D _textureStartGame;
        private SpriteClass _textureWall;
        private float _wallSpeedMultiplier;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            SpriteClass.SetGame(this);

            // Tutorial
            _screenHeight = Window.ClientBounds.Height;
            _screenWidth = Window.ClientBounds.Width;

            //IsMouseVisible = false;
            _wallSpeedMultiplier = 0.5f;
            _spaceDown = false;
            _gameStarted = false;
            _score = 0;
            _random = new Random();
            _pinguinSpeedX = ScaleToWindowSize(1000f);
            _pinguinJumpY = ScaleToWindowSize(-1200f);
            _gravitySpeed = ScaleToWindowSize(30f);


            base.Initialize();
        }

        //TODO: Fix it sometimes (if needed)
        public float ScaleToWindowSize(float f)
        {
            f *= (float) (_screenWidth / _screenHeight * 0.1);
            return f;
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float elapsedTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardHandler(); // Handle keyboard input


            if (_gameOver)
            {
                _texturePinguin.Dx = 0;
                _texturePinguin.Dy = 0;
                _textureWall.Dx = 0;
                _textureWall.Dy = 0;
                _textureWall.Da = 0;
            }

            // Update animated SpriteClass objects based on their current rates of change
            _texturePinguin.Update(elapsedTime);
            _textureWall.Update(elapsedTime);

            // Accelerate the dino downward each frame to simulate gravity.
            _texturePinguin.Dy += _gravitySpeed;

            //float gameGround = (int) (_screenHeight * Skyratio);
            float gameGround = _screenHeight * Skyratio;

            // Game ground for pinguin
            if (_texturePinguin.Y >= gameGround)
            {
                _texturePinguin.Dy = 0;
                _texturePinguin.Y = gameGround;
            }

            // Game left/right for pinguin
            if (_texturePinguin.X > _screenWidth)
            {
                _texturePinguin.X = _screenWidth;
                _texturePinguin.Dx = 0;
            }

            if (_texturePinguin.X < 0 + _texturePinguin.ExactSize.X) //_texturePinguin.Texture.Width)
            {
                _texturePinguin.X = 0 + _texturePinguin.ExactSize.X; //+ _texturePinguin.Texture.Width;
                _texturePinguin.Dx = 0;
            }

            // If the broccoli goes offscreen, spawn a new one and iterate the score
            if (_textureWall.Y > _screenHeight + 100 || _textureWall.Y < -100 || _textureWall.X > _screenWidth + 100 ||
                _textureWall.X < -100)
            {
                SpawnWall();
                _score++;
            }

            if (_texturePinguin.RectangleCollision(_textureWall))
                _gameOver = true;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Tutorial
            _spriteBatch.Begin();
            _spriteBatch.Draw(_textureIceGround, new Vector2(0, _screenHeight * Skyratio), Color.White);

            if (_gameOver)
            {
                // Draw game over texture
                _spriteBatch.Draw(_textureGameOver,
                    new Vector2(_screenWidth / 2 - _textureGameOver.Width / 2,
                        _screenHeight / 4 - _textureGameOver.Width / 2), Color.White);

                const string pressEnter = "Press Enter to restart!";

                // Measure the size of text in the given font
                Vector2 pressEnterSize = _fontState.MeasureString(pressEnter);

                // Draw the text horizontally centered
                _spriteBatch.DrawString(_fontState, pressEnter,
                    new Vector2(_screenWidth / 2 - pressEnterSize.X / 2, _screenHeight - 200), Color.White);
            }

            _textureWall.Draw(_spriteBatch);
            _texturePinguin.Draw(_spriteBatch);


            // Debug
            string debugText = $"Score: {_score}" + Environment.NewLine +
                               $"Screen Height: {_screenHeight}" + Environment.NewLine +
                               $"PerfectGround: {_screenHeight * Skyratio}" + Environment.NewLine +
                               $"IceGround X{_textureIceGround.Width} Y{_textureIceGround.Bounds.Top}" +
                               Environment.NewLine +
                               $"Pingu: X{_texturePinguin.X} Y{_texturePinguin.Y}";
            _spriteBatch.DrawString(_fontDebug, debugText, new Vector2(10, 10), Color.Black);

            _spriteBatch.DrawString(_fontScore, _score.ToString(CultureInfo.InvariantCulture),
                new Vector2(_screenWidth - 100, 50), Color.Black);
            if (!_gameStarted)
            {
                // Fill the screen with black before the game starts
                _spriteBatch.Draw(_textureStartGame, new Rectangle(0, 0,
                    (int) _screenWidth, (int) _screenHeight), Color.White);

                const string title = "Pingu run (\")>";
                const string pressSpace = "Press Space to start";

                // Measure the size of text in the given font
                Vector2 titleSize = _fontState.MeasureString(title);
                Vector2 pressSpaceSize = _fontState.MeasureString(pressSpace);

                // Draw the text horizontally centered
                _spriteBatch.DrawString(_fontState, title,
                    new Vector2(_screenWidth / 2 - titleSize.X / 2, _screenHeight / 3),
                    Color.ForestGreen);
                _spriteBatch.DrawString(_fontState, pressSpace,
                    new Vector2(_screenWidth / 2 - pressSpaceSize.X / 2,
                        _screenHeight / 2), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void StartGame()
        {
            _texturePinguin.X = _screenWidth / 2;
            _texturePinguin.Y = _screenHeight * Skyratio;
            _wallSpeedMultiplier = 0.5f;
            SpawnWall();
            _score = 0;
        }

        private void KeyboardHandler()
        {
            KeyboardState state = Keyboard.GetState();

            // Quit the game if Escape is pressed.
            if (state.IsKeyDown(Keys.Escape))
                Exit();

            // Start the game if Space is pressed.
            if (!_gameStarted)
            {
                if (!state.IsKeyDown(Keys.Space))
                    return;

                StartGame();
                _gameStarted = true;
                _spaceDown = true;
                _gameOver = false;

                return;
            }

            // Jump
            if (state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Up))
            {
                // Jump if the Space is pressed but not held and the dino is on the floor
                if (!_spaceDown && _texturePinguin.Y >= _screenHeight * Skyratio - 1)
                    _texturePinguin.Dy = _pinguinJumpY;

                _spaceDown = true;
            }
            else
            {
                _spaceDown = false;
            }

            // Handle left and right
            if (state.IsKeyDown(Keys.Left))
                _texturePinguin.Dx = _pinguinSpeedX * -1;

            else if (state.IsKeyDown(Keys.Right))
                _texturePinguin.Dx = _pinguinSpeedX;
            else
                _texturePinguin.Dx = 0;


            if (_gameOver && state.IsKeyDown(Keys.Enter))
            {
                StartGame();
                _gameOver = false;
            }
        }

        public void SpawnWall()
        {
            _textureWall.X = _random.Next(0, (int) _screenWidth);
            _textureWall.Y = _screenHeight * Skyratio;

            if (Math.Abs(_score % 5) < 1)
                _wallSpeedMultiplier += 0.2f;

            _textureWall.Dx = (_texturePinguin.X - _textureWall.X) * _wallSpeedMultiplier;
            _textureWall.Dy = (_texturePinguin.Y - _textureWall.Y) * _wallSpeedMultiplier;
            _textureWall.Da = 7f;
        }
    }
}