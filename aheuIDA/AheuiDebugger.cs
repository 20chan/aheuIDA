using System;
using System.Collections.Generic;
using System.Linq;

namespace aheuIDA
{
    public class AheuiDebugger : IntAheui
    {
        private List<(int x, int y)> _breakpoints;
        public IReadOnlyCollection<(int x, int y)> BreakPoints { get; }
        
        public IReadOnlyCollection<int>[] Stacks { get; }
        public (char, IReadOnlyCollection<int>)[] StacksWithKey { get; }

        public int X => _cursor.X;
        public int Y => _cursor.Y;
        public int XSpeed => _cursor.XSpeed;
        public int YSpeed => _cursor.YSpeed;

        public event Action Break;

        public AheuiDebugger(string code, bool utf32included = false) : base(code, utf32included)
        {
            _breakpoints = new List<(int, int)>();
            BreakPoints = _breakpoints.AsReadOnly();
            var stacksonly = _storage._stacks.Values.Select(s => (IReadOnlyCollection<int>)s).ToList();
            stacksonly.Insert(8, _storage._queue.AsReadOnly());
            Stacks = stacksonly.ToArray();
            StacksWithKey = Hangul.Jongseongs.Zip(stacksonly, (c, s) => (c, s)).ToArray();
        }

        public void RunUntilBreaks()
        {
            while (!IsExited)
            {
                if (_breakpoints.Contains((X, Y)))
                {
                    Break();
                    break;
                }
                else
                    Step();
            }
        }
    }
}
