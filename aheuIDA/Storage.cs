using System;
using System.Collections.Generic;
using System.Text;

namespace aheuIDA
{
    public delegate T StorageTFromInt<T>(in int val);
    public delegate T StorageFunction<T>(in T a, in T b);
    public delegate bool StorageComparer<T>(in T a, in T b);
    public class Storage<T>
    {
        private StorageFunction<T> _add, _sub, _mul, _div, _mod;
        private StorageComparer<T> _bigeq, _eq;
        private StorageTFromInt<T> _inttot;

        internal readonly Dictionary<char, Stack<T>> _stacks;
        internal List<T> _queue;

        public Storage(
            StorageFunction<T> add,
            StorageFunction<T> sub,
            StorageFunction<T> mul,
            StorageFunction<T> mod,
            StorageFunction<T> per,
            StorageComparer<T> eq,
            StorageComparer<T> bigeq,
            StorageTFromInt<T> inttot)
        {
            _add = add;
            _sub = sub;
            _mul = mul;
            _div = mod;
            _mod = per;
            _eq = eq;
            _bigeq = bigeq;
            _inttot = inttot;

            _stacks = new Dictionary<char, Stack<T>>(28)
            {
                { ' ', new Stack<T>() },
                { 'ㄱ', new Stack<T>() },
                { 'ㄴ', new Stack<T>() },
                { 'ㄷ', new Stack<T>() },
                { 'ㄹ', new Stack<T>() },
                { 'ㅁ', new Stack<T>() },
                { 'ㅂ', new Stack<T>() },
                { 'ㅅ', new Stack<T>() },
                { 'ㅈ', new Stack<T>() },
                { 'ㅊ', new Stack<T>() },
                { 'ㅋ', new Stack<T>() },
                { 'ㅌ', new Stack<T>() },
                { 'ㅍ', new Stack<T>() },
                { 'ㄲ', new Stack<T>() },
                { 'ㄳ', new Stack<T>() },
                { 'ㄵ', new Stack<T>() },
                { 'ㄶ', new Stack<T>() },
                { 'ㄺ', new Stack<T>() },
                { 'ㄻ', new Stack<T>() },
                { 'ㄼ', new Stack<T>() },
                { 'ㄽ', new Stack<T>() },
                { 'ㄾ', new Stack<T>() },
                { 'ㄿ', new Stack<T>() },
                { 'ㅀ', new Stack<T>() },
                { 'ㅄ', new Stack<T>() },
                { 'ㅆ', new Stack<T>() },
                { 'ㅎ', new Stack<T>() }
            };
            _queue = new List<T>();
        }

        public char CurrentStorage { get; private set; } = ' ';

        private Stack<T> CurrentStack => _stacks[CurrentStorage];
        private IReadOnlyCollection<T> CurrentCollection
        {
            get
            {
                if (CurrentStorage == 'ㅇ')
                    return _queue;
                else
                    return CurrentStack;
            }
        }

        public bool CanPop(int count)
        {
            return CurrentCollection.Count >= count;
        }

        public T Pop()
        {
            if (CurrentStorage == 'ㅇ')
            {
                var res = _queue[0];
                _queue.RemoveAt(0);
                return res;
            }
            else
                return CurrentStack.Pop();
        }

        public bool TryPop(out T val)
        {
            if (!CanPop(1))
            {
                val = default;
                return false;
            }
            else
            {
                val = Pop();
                return true;
            }
        }

        public void Push(T value)
        {
            if (CurrentStorage == 'ㅇ')
                _queue.Add(value);
            else
                CurrentStack.Push(value);
        }

        public bool Add()
            => PopTwiceAndPush(_add);

        public bool Sub()
            => PopTwiceAndPush(_sub);

        public bool Mul()
            => PopTwiceAndPush(_mul);

        public bool Div()
            => PopTwiceAndPush(_div);

        public bool Mod()
            => PopTwiceAndPush(_mod);

        public bool Duplicate()
        {
            if (!CanPop(1))
                return false;
            if (CurrentStorage == 'ㅇ')
            {
                _queue.Insert(0, _queue[0]);
            }
            else
            {
                var pop = Pop();
                Push(pop);
                Push(pop);
            }
            return true;
        }

        public bool Swap()
        {
            if (!CanPop(2))
                return false;
            if (CurrentStorage == 'ㅇ')
            {
                var temp = _queue[0];
                _queue[0] = _queue[1];
                _queue[1] = temp;
            }
            else
            {
                var first = Pop();
                var last = Pop();
                Push(first);
                Push(last);
            }
            return true;
        }

        public void Select(char consonant)
        {
            CurrentStorage = consonant;
        }

        public bool Move(char consonant)
        {
            if (!CanPop(1))
                return false;
            char current = CurrentStorage;
            var pop = Pop();
            Select(consonant);
            Push(pop);
            Select(current);
            return true;
        }

        public bool Compare()
        {
            if (!CanPop(2))
                return false;
            var first = Pop();
            var second = Pop();
            if (_bigeq(first, second))
                Push((T)(object)1);
            else
                Push((T)(object)0);
            return true;
        }

        public bool Conditional()
        {
            return !Pop().Equals((T)(object)0);
        }

        private bool PopTwiceAndPush(StorageFunction<T> func)
        {
            if (!CanPop(2))
                return false;

            var first = Pop();
            var last = Pop();
            Push(func(last, first));
            return true;
        }
    }
}
