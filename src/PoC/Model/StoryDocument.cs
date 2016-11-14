using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace com.bjss.generator.Model
{
    public class StoryDocument
    {
        private Regex _stepIdentifier = new Regex(@"^\s*(Given|Then|Transform|When|And|But)", RegexOptions.Singleline | RegexOptions.Compiled);


        public StoryDocument(string contents)
        {
            Errors = new ObservableCollection<Error>();
            Lines = new List<Line>(LoadLines((contents ?? string.Empty)));
            ParseLines();
        }

        public List<Line> Lines { get; }

        public StoryNode Story
        {
            get
            {
                return Lines.Any(x => x.Type == LineType.Unknown) 
                    || Errors.Any()
                    ? null 
                    : new StoryNode(this);
            }
        }

        public ObservableCollection<Error> Errors { get; }

        private IEnumerable<Line> LoadLines(string contents)
        {
            using (var reader = new StringReader(contents))
            {
                var lineNum = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineNum++;
                    yield return new Line
                    {
                        Type = string.IsNullOrEmpty(line.Trim()) ? LineType.Empty : LineType.Unknown,
                        Text = line,
                        Location = new Location(lineNum)
                    };
                }
            }
        }

        private void ParseLines()
        {
            Errors.Clear();

            if (!Lines.Any())
            {
                Errors.Add(new Error(0, 0, "Empty file.", Severity.Info));
            }
            else
            {
                foreach (var line in Lines)
                {
                    if (line.Type == LineType.Empty)
                    {
                        continue;
                    }

                    var isStepLine = _stepIdentifier.IsMatch(line.Text);

                    if (isStepLine)
                    {
                        line.Type = LineType.Step;
                    }
                    else if (ParseMataDataLines(line))
                    {
                        return;
                    }
                }

                ValidateParsedResults();
            }
        }

        private bool ParseMataDataLines(Line line)
        {
            if (line.Text.StartsWith("As an") || line.Text.StartsWith("As a"))
            {
                line.Type = LineType.StoryActorOrRole;
            }
            else if (line.Text.StartsWith("I want"))
            {
                line.Type = LineType.StoryActorDesiredAction;
            }
            else if (line.Text.StartsWith("So that") || line.Text.StartsWith("In order to"))
            {
                line.Type = LineType.StoryActorDesiredOutcome;
            }
            else
            {
                if (Lines.All(x => x.Type != LineType.StoryTitle))
                {
                    var line1 = line;
                    if (Lines.Any(x => x.LineNumber < line1.LineNumber && x.Type != LineType.Empty))
                    {
                        Errors.Add(new Error(line.Location, "Story title is blank/empty"));
                        return true;
                    }

                    line.Type = LineType.StoryTitle;
                }
                else 
                {
                    if (Lines[line.LineNumber - 1].Type == LineType.Empty)
                    {
                        Errors.Add(new Error(line.Location, "Scenario title must be preceeded buy a blank line"));
                        return true;
                    }

                    line.Type = LineType.ScenarioTitle;
                }
            }

            return false;
        }

        private void ValidateParsedResults()
        {
            if (Lines.All(x => x.Type != LineType.ScenarioTitle))
            {
                Errors.Add(new Error(0, 0, "Missing Scenario Title"));
            }

            if (Lines.All(x => x.Type != LineType.StoryTitle))
            {
                Errors.Add(new Error(0, 0, "Missing Story Title"));
            }

            if (Lines.All(x => x.Type != LineType.StoryActorOrRole))
            {
                Errors.Add(new Error(0, 0, "Missing Story Actor / Role"));
            }

            if (Lines.All(x => x.Type != LineType.StoryActorDesiredAction))
            {
                Errors.Add(new Error(0, 0, "Missing Story Desired action"));
            }

            if (Lines.All(x => x.Type != LineType.StoryActorDesiredOutcome))
            {
                Errors.Add(new Error(0, 0, "Missing Story Desired outcome"));
            }
        }

        public Scenario[] GetScenarios()
        {
            var markers = Lines.Where(x => x.Type == LineType.ScenarioTitle).OrderBy(x=>x.LineNumber).ToArray();

            var result = new Scenario[markers.Length];

            for (int i = 0; i < markers.Length; i++)
            {
                var marker = markers[i];
                var n = i + 1;
                var nextOffset = n == markers.Length 
                               ? Lines.Last().LineNumber + 1
                               : markers[n].LineNumber;
                result[i] = new Scenario(Lines.Where(x => x.LineNumber >= marker.LineNumber && x.LineNumber < nextOffset).OrderBy(x => x.LineNumber)); 
            }

            return result;
        }
    }
}
