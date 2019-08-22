using UnityEngine;
using UnityEngine.UI;

public class AnswerInput : MonoBehaviour
{
    public Text answerText;

    private AnswerData answerData;
    private GameController gameController;
    //private string artPath = "Assets/Art/";

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    public void SetUp(AnswerData Data)
    {
        answerData = Data;
        // Manage Text answer;
        answerText.text = answerData.answerText;
    }


    public void HandleEdit(InputField _responseField)
    {
        gameController.AnswerButtonInput(_responseField.text);
    }


}
