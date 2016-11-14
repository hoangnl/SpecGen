using System;
using System.Diagnostics;

namespace com.bjss.generator.Model
{
    [Serializable]
    [DebuggerDisplay("({Line}/{Column})")]
    public struct Location
    {
        internal Location(int line, int column = 1)
        {
            Line = line;
            Column = column;
        }

        public int Line { get; private set; }
        public int Column { get; private set; }

    }
}