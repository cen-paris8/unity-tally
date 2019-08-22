using UnityEngine;

[System.Serializable]
public class RoundData
{
    public string name;
    public int timeLimitInSeconds;
    public int pointsAddedForCorrectAnswer;
    public QuestionData[] questions;

    public RoundData(string NewName, int NewTimeLimitInSeconds, int NewPointsAddedForCorrectAnswer, QuestionData[] NewQuestion)
    {
        name = NewName;
        timeLimitInSeconds = NewTimeLimitInSeconds;
        pointsAddedForCorrectAnswer = NewPointsAddedForCorrectAnswer;
        questions = NewQuestion;
    }
}

