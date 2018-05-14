using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace aheuIDA
{
    /// <summary>
    /// Delegate for getting input from stream
    /// </summary>
    /// <typeparam name="T">Numeric type for storage</typeparam>
    /// <param name="isNumeric">Is input is numeric type or char type</param>
    /// <returns>Input</returns>
    public delegate T AheuiInputDelegate<T>(bool isNumeric);
    public delegate void AheuiOutputDelegate<T>(T value, bool isNumeric);
    public abstract class Aheui<T>
    {
        public bool DebugEnabled { get; set; }
        public event AheuiInputDelegate<T> NeedInput;
        public event AheuiOutputDelegate<T> NeedOutput;

        public bool IsExited { get; private set; }
        public int ExitCode { get; private set; }

        public string OriginalCode { get; }
        private Hangul[,] _code;
        public int Width { get; }
        public int Height { get; }

        private readonly Storage<T> _storage;
        private Cursor _cursor;

        public Hangul CurrentCode => _code[_cursor.Y, _cursor.X];
        
        public Aheui(string code)
        {
            OriginalCode = code;
            _code = SeparateCode(code, out var width, out var height);
            Width = width;
            Height = height;
            _storage = new Storage<T>(Add, Sub, Mul, Div, Mod, IsEqual, IsBiggerOrEqual, IntToT);
            _cursor = new Cursor(Width, Height);
        }

        private static Hangul[,] SeparateCode(string code, out int width, out int height)
        {
            var lines = code.Replace("\r\n", "\n").Split('\n');
            height = lines.Length;
            width = lines.Max(s => s.Length);

            var board = new Hangul[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (lines[i].Length > j)
                        board[i, j] = new Hangul(lines[i][j]);
                    else
                        board[i, j] = new Hangul(' ');
                }
            }
            return board;
        }

        public void RunAll()
        {
            while (!IsExited)
                Step();
        }

        public void Step()
        {
            var next = GetNextCursor();

            bool? straight = null;
            switch (CurrentCode.Choseong)
            {
                case 'ㅎ':
                    IsExited = true;
                    if (_storage.CanPop(1))
                    {
                        ExitCode = TToInt(_storage.Pop());
                    }
                    break;
                // ㄷ 묶음
                case 'ㄷ':
                    straight = _storage.Add();
                    break;
                case 'ㄸ':
                    straight = _storage.Mul();
                    break;
                case 'ㅌ':
                    straight = _storage.Sub();
                    break;
                case 'ㄴ':
                    straight = _storage.Div();
                    break;
                case 'ㄹ':
                    straight = _storage.Mod();
                    break;
                // ㅂ 묶음
                case 'ㅂ':
                    T value;
                    if (CurrentCode.Jongseong == 'ㅇ')
                        value = NeedInput(true);
                    else if (CurrentCode.Jongseong == 'ㅎ')
                        value = NeedInput(false);
                    else
                        value = IntToT(Hangul.GetStrokeCount(CurrentCode.Jongseong));
                    _storage.Push(value);
                    break;
                case 'ㅁ':
                    if (_storage.TryPop(out T popped))
                    {
                        if (CurrentCode.Jongseong == 'ㅇ')
                            NeedOutput(popped, true);
                        else if (CurrentCode.Jongseong == 'ㅎ')
                            NeedOutput(popped, false);
                    }
                    else
                        straight = false;
                    break;
                case 'ㅃ':
                    straight = _storage.Duplicate();
                    break;
                case 'ㅍ':
                    straight = _storage.Swap();
                    break;
                // ㅅ 묶음
                case 'ㅅ':
                    _storage.Select(CurrentCode.Jongseong);
                    break;
                case 'ㅆ':
                    straight = _storage.Move(CurrentCode.Jongseong);
                    break;
                case 'ㅈ':
                    straight = _storage.Compare();
                    break;
                case 'ㅊ':
                    straight = _storage.Conditional();
                    break;
            }
            if (straight == false)
                next.Reverse();

            next.Step();
            _cursor = next;
        }

        private Cursor GetNextCursor()
        {
            if (CurrentCode.IsInvalid)
                return _cursor.GetSteppedCursor();
            else
            {
                var next = new Cursor(_cursor);
                switch(CurrentCode.Jungseong)
                {
                    case 'ㅏ':
                        next.Right(1);
                        break;
                    case 'ㅑ':
                        next.Right(2);
                        break;
                    case 'ㅓ':
                        next.Left(1);
                        break;
                    case 'ㅕ':
                        next.Left(2);
                        break;
                    case 'ㅗ':
                        next.Up(1);
                        break;
                    case 'ㅛ':
                        next.Up(2);
                        break;
                    case 'ㅜ':
                        next.Down(1);
                        break;
                    case 'ㅠ':
                        next.Down(2);
                        break;
                    case 'ㅡ':
                        next.ReverseY();
                        break;
                    case 'ㅣ':
                        next.ReverseX();
                        break;
                    case 'ㅢ':
                        next.Reverse();
                        break;
                }
                return next;
            }
        }

        protected abstract T Add(in T a, in T b);
        protected abstract T Sub(in T a, in T b);
        protected abstract T Mul(in T a, in T b);
        protected abstract T Div(in T a, in T b);
        protected abstract T Mod(in T a, in T b);
        protected abstract bool IsEqual(in T a, in T b);
        protected abstract bool IsBiggerOrEqual(in T a, in T b);
        protected abstract T IntToT(in int val);
        protected abstract int TToInt(in T val);
    }

    public class IntAheui : Aheui<int>
    {
        public IntAheui(string code) : base(code)
        {

        }

        /// <summary>
        /// Execute aheui code and return exit code
        /// </summary>
        /// <param name="code">Full aheui code</param>
        /// <param name="output">Output</param>
        /// <param name="args">Arguments</param>
        /// <returns>Exit code</returns>
        public static int Execute(string code, out string output, params int[] args)
        {
            var res = new StringBuilder();
            var queue = new Queue<int>(args);
            var aheui = new IntAheui(code);
            aheui.NeedInput += (num) => queue.Dequeue();
            aheui.NeedOutput += (val, num) =>
            {
                if (num)
                    res.Append(val);
                else
                    res.Append((char)val);
            };
            aheui.RunAll();
            output = res.ToString();
            return aheui.ExitCode;
        }

        protected override int Add(in int a, in int b)
            => a + b;

        protected override int Sub(in int a, in int b)
            => a - b;

        protected override int Mul(in int a, in int b)
            => a * b;

        protected override int Div(in int a, in int b)
            => a / b;

        protected override int Mod(in int a, in int b)
            => a % b;

        protected override bool IsEqual(in int a, in int b)
            => a == b;

        protected override bool IsBiggerOrEqual(in int a, in int b)
            => a <= b;

        protected override int IntToT(in int val)
            => val;

        protected override int TToInt(in int val)
            => val;
    }
}
