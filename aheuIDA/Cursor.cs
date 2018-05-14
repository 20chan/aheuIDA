namespace aheuIDA
{
    public sealed class Cursor
    {
        public readonly int Width, Height;
        private int _x, _y;
        public int X
        {
            get => _x;
            private set
            {
                if (value < 0)
                    _x = Width - 1;
                else if (value >= Width)
                    _x = 0;
                else
                    _x = value;
            }
        }
        public int Y
        {
            get => _y;
            private set
            {
                if (value < 0)
                    _y = Height - 1;
                else if (value >= Height)
                    _y = 0;
                else
                    _y = value;
            }
        }
        public int XSpeed { get; private set; }
        public int YSpeed { get; private set; }

        public Cursor(int width, int height)
        {
            X = Y = 0;
            XSpeed = 0;
            YSpeed = 1;
            Width = width;
            Height = height;
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
            => (XSpeed, YSpeed) = (0, -speed);

        public void Down(int speed = 1)
            => Up(-speed);

        public void Left(int speed = 1)
            => (XSpeed, YSpeed) = (-speed, 0);

        public void Right(int speed = 1)
            => Left(-speed);

        public void Reverse()
            => (XSpeed, YSpeed) = (-XSpeed, -YSpeed);

        public void ReverseX()
            => XSpeed = -XSpeed;

        public void ReverseY()
            => YSpeed = -YSpeed;

        public void Step()
        {
            X += XSpeed;
            Y += YSpeed;
        }

        public Cursor GetSteppedCursor()
        {
            var next = new Cursor(this);
            return next;
        }
    }
}
