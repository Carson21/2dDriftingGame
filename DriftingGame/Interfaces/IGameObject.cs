using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DriftingGame.Interfaces
{
    internal interface IGameObject
    {
        public Texture2D Texture { get; }
        public float X { get; }
        public float Y { get; }
        public int Width { get; }
        public int Height { get; }
        public Vector2 Origin { get; }
        public float Rotation { get; }
        public Color Color { get; }
        public SpriteEffects SpriteEffect { get; }

        public void Update(GameTime gameTime, KeyboardState keyboardState);
        public void Draw(SpriteBatch spriteBatch);

    }
}
