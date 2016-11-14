using System;

namespace com.bjss.generator.Model
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