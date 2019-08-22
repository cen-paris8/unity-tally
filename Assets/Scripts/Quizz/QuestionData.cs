using UnityEngine;

[System.Serializable]
public class QuestionData
{
    public string questionText;
    public string questionAudio;
    public string questionImage;
    public string questionResponse;
    public string questionType;
    public AnswerData[] answers;
}
