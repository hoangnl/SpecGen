using System;
using System.Collections.Generic;
using System.Linq;

namespace SpecGen.Model
{
    public class Scenario : MarshalByRefObject
    {
        private readonly Line[] _lines;
        internal Scenario(IEnumerable<Line> lines)
        {
            _lines = lines.ToArray();
        }

        public Line Title => _lines.FirstOrDefault(x => x.Type == LineType.ScenarioTitle);

        public IEnumerable<Line> Steps => _lines.Where(x => x.Type == LineType.Step);

    }
}