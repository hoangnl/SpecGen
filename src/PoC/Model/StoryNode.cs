using System;
using System.Linq;

namespace com.bjss.generator.Model
{
    public class StoryNode : MarshalByRefObject
    {
        private readonly StoryDocument _document;

        internal StoryNode(StoryDocument document)
        {
            _document = document;
        }

        public Line Title => _document.Lines.FirstOrDefault(x => x.Type == LineType.StoryTitle);

        public Line AsA => _document.Lines.FirstOrDefault(x => x.Type == LineType.StoryActorOrRole);

        public Line IWant => _document.Lines.FirstOrDefault(x => x.Type == LineType.StoryActorDesiredAction);

        public Line SoThat => _document.Lines.FirstOrDefault(x => x.Type == LineType.StoryActorDesiredOutcome);

        public Scenario[] Scenarios => _document.GetScenarios();
    }
}