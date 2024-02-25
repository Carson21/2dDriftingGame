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

        private Camera camera;
        private KeyboardState keyboardState;

        private Texture2D playerTexture;
        private Player player;


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

            // Disable window resizing
            Window.AllowUserResizing = false;

            // Initialize your camera with the game viewport
            camera = new Camera();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create a new player object with the rectangle texture
            playerTexture = Content.Load<Texture2D>("SportsCar");
            player = new Player(playerTexture, 0, 0, 64, 128, 64, 128, 0.4f, 0.003f);
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime, keyboardState);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CadetBlue);

            // Begin drawing with the camera's transform matrix and a point clamp sampler state for pixel art
            _spriteBatch.Begin(transformMatrix: camera.Transform, samplerState: SamplerState.PointClamp);
            
            player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
