using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DriftingGame
{
    public class DriftingGame : Game
    {
        public static readonly int SCREEN_WIDTH = 1280;
        public static readonly int SCREEN_HEIGHT = 720;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera _camera;
        private RenderTarget2D _renderTarget;
        private Rectangle _renderRectangle;
        private KeyboardState _keyboardState;

        private Texture2D _playerTexture;
        private Player _player;


        public DriftingGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Enable VSync, turn off fixed time step, and set window size to 1280x720 with IsFullScreen set to false
            IsFixedTimeStep = false;
            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            // Allow window resizing and recalculate the render rectangle when the window size changes
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += (sender, args) => {
                if (Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0)
                {
                    CalculateRenderRectangle();
                }
            };

            // Initialize the render target and calculate the render rectangle
            _renderTarget = new RenderTarget2D(GraphicsDevice, SCREEN_WIDTH, SCREEN_HEIGHT);
            CalculateRenderRectangle();

            // Initialize the camera
            _camera = new Camera();

            base.Initialize();
        }

        private void CalculateRenderRectangle()
        {
            Point windowSize = GraphicsDevice.Viewport.Bounds.Size;

            float scaleX = (float)windowSize.X / _renderTarget.Width;
            float scaleY = (float)windowSize.Y / _renderTarget.Height;
            float scale = Math.Min(scaleX, scaleY);

            _renderRectangle.Width = (int)(_renderTarget.Width * scale);
            _renderRectangle.Height = (int)(_renderTarget.Height * scale);
            _renderRectangle.X = (windowSize.X - _renderRectangle.Width) / 2;
            _renderRectangle.Y = (windowSize.Y - _renderRectangle.Height) / 2;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create a new player object with the rectangle texture
            _playerTexture = Content.Load<Texture2D>("SportsCar");
            _player = new Player(_playerTexture, 0, 0, 64, 128, 64, 128, 0.3f, 0.003f);
        }

        protected override void Update(GameTime gameTime)
        {
            _keyboardState = Keyboard.GetState();

            if (_keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime, _keyboardState);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Set the render target to the render target we created
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CadetBlue);

            // Begin drawing with the camera's transform matrix and a point clamp sampler state for pixel art.
            // Always draw in between the Begin and End calls.
            _spriteBatch.Begin(transformMatrix: _camera.Transform, samplerState: SamplerState.PointClamp);

            _player.Draw(_spriteBatch);

            _spriteBatch.End();


            // Target the back buffer and draw the render target
            GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_renderTarget, _renderRectangle, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
