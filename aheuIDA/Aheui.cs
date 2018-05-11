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
    public abstract class Aheui<T>
    {
        public bool DebugEnabled { get; set; }
        public event AheuiInputDelegate<T> NeedInput;

        public bool IsExited { get; private set; }
        public int ExitCode { get; private set; }

        public string OriginalCode { get; }
        private Hangul[,] _code;
        public int Width { get; }
        public int Height { get; }

        private Cursor _cursor;
        
        public Aheui(string code)
        {
            OriginalCode = code;
            _code = SeparateCode(code);
            _cursor = new Cursor();
        }

        private static Hangul[,] SeparateCode(string code)
        {
            var lines = code.Replace("\r\n", "\n").Split('\n');
            int height = lines.Length;
            int width = lines.Max(s => s.Length);

            var board = new Hangul[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    board[i, j] = new Hangul(lines[i][j]);
                }
            }
            return board;
        }
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
            throw new NotImplementedException();
        }
    }
}
