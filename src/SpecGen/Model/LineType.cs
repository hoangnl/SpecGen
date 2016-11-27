using System;

namespace SpecGen.Model
{
    [Serializable]
    public enum LineType
    {
        Unknown,
        Step,
        ScenarioTitle,
        StoryTitle,
        StoryActorOrRole,
        StoryActorDesiredAction,
        StoryActorDesiredOutcome,
        Empty
    }
}