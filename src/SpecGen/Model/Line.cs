using System;
using System.Diagnostics;

namespace SpecGen.Model
{
    [Serializable]
    [DebuggerDisplay("{Type}   |   {Addition} {Group} -> {Text}")]
    public class Line : MarshalByRefObject
    {
        public Location Location { get; set; }
        public string Group { get; set; }
        public string Addition { get; set; }
        public string Text { get; set; }
        public LineType Type { get; set; }
        public int LineNumber => Location.Line;
    }
}