using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DriftingGame
{
    internal class Utils
    {
        public static Texture2D CreateRectangleTexture(GraphicsDevice graphicsDevice, int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);

            // Create an array to hold the color data
            Color[] data = new Color[width * height];

            // Set each pixel to the specified color
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = color;
            }

            // Set the color data on the texture
            texture.SetData(data);

            return texture;
        }
    }
}
