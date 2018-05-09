using System;
using System.Collections.Generic;
using System.Text;

namespace aheuIDA
{
    public abstract class Aheui<T>
    {
        public static int Execute(string code, out string output, params int[] args)
        {
            throw new NotImplementedException();
        }
    }

    public class IntAheui : Aheui<int>
    {

    }
}
