using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using DriftingGame.Interfaces;

namespace DriftingGame
{
    internal class Player : IGameObject
    {
        // IGameObject properties
        public Texture2D Texture { get; }
        public float X { get; private set; }
        public float Y { get; private set; }
        public int Width { get; }
        public int Height { get; }
        public Vector2 Origin { get; }
        public float Rotation { get; private set; }
        public Color Color { get; }
        public SpriteEffects SpriteEffect { get; }
            
        // Player properties
        public float Speed { get; private set; }
        public float RotationSpeed { get; private set; }
        public int Frame { get; private set; }
        public int FrameWidth { get; }
        public int FrameHeight { get; }

        // Player private fields


        public Player(Texture2D texture, float x, float y, int width, int height, int frameWidth, int frameHeight, float speed, float rotationSpeed) 
        {
            // IGameObject properties
            Texture = texture;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Origin = new Vector2(64f / 2, 128f / 2);
            Rotation = 0f;
            Color = Color.White;
            SpriteEffect = SpriteEffects.None;

            // Player properties
            Frame = 0;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            Speed = speed;
            RotationSpeed = rotationSpeed;
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            Frame = 0;

            // Move in the direction of rotation
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                X += (float)Math.Sin(Rotation) * Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                Y -= (float)Math.Cos(Rotation) * Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    Frame = 2;
                    Rotation -= RotationSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    Frame = 5;
                    Rotation += RotationSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)X, (int)Y, Width, Height), new Rectangle(Frame * FrameWidth, 0, FrameWidth, FrameHeight), Color, Rotation, Origin, SpriteEffect, 0.0f);
        }
    }
}
