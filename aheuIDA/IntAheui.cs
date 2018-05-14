using System.Collections.Generic;
using System.Text;

namespace aheuIDA
{
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
