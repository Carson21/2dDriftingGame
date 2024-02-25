using Microsoft.Xna.Framework;


namespace DriftingGame
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }

        public Camera()
        {
            Zoom = 1.0f;
            Rotation = MathHelper.ToRadians(0f);
            Position = Vector2.Zero;

            UpdateTransform();
        }

        public void UpdateTransform()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateScale(Zoom) *
                        Matrix.CreateTranslation(new Vector3(DriftingGame.SCREEN_WIDTH * 0.5f, DriftingGame.SCREEN_HEIGHT * 0.5f, 0.0f));
        }
    }
}
