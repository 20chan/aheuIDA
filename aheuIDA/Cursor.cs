namespace aheuIDA
{
    public class Cursor
    {
        public readonly int Width, Height;
        public int X, Y;
        public int XSpeed, YSpeed;

        public Cursor()
        {
            X = Y = 0;
            XSpeed = 1;
            YSpeed = 0;
        }

        public Cursor(Cursor previous)
        {
            Width = previous.Width;
            Height = previous.Height;
            X = previous.X;
            Y = previous.Y;
            XSpeed = previous.XSpeed;
            YSpeed = previous.YSpeed;
        }

        public void Up(int speed = 1)
            => (XSpeed, YSpeed) = (0, speed);

        public void Down(int speed = 1)
            => Up(-speed);

        public void Left(int speed = 1)
            => (XSpeed, YSpeed) = (-speed, 0);

        public void Right(int speed = 1)
            => Left(-speed);

        public void Reverse()
            => (XSpeed, YSpeed) = (-XSpeed, -YSpeed);
    }
}
