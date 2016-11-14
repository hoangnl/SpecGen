using System;
using System.Diagnostics;

namespace com.bjss.generator.Model
{
    [Serializable]
    [DebuggerDisplay("{Type}   |   {Text}")]
    public class Line : MarshalByRefObject
    {
        public Location Location { get; set; }
        public string Text { get; set; }
        public LineType Type { get; set; }
        public int LineNumber => Location.Line;
    }
}