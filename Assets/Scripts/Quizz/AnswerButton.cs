using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public Text answerText;

    private AnswerData answerData;
    private GameController gameController;

private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    public void SetUp(AnswerData Data)
    {
        answerData = Data;
        // Manage Text answer;
        answerText.text = answerData.answerText;
        //Manage Image answer
        if (answerData.answerImage != "" && answerData.answerImage != null)
        {
            Sprite newSprite;
            Texture2D spriteTexture = Resources.Load(answerData.answerImage, typeof(Texture2D)) as Texture2D; ;
            newSprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
            GetComponent<Image>().sprite = newSprite;
            GetComponent<Image>().color = new Color(255, 255, 255);
        }
        else
        {
            GetComponent<Image>().sprite = null;
            GetComponent<Image>().color = new Color(0, 0, 0);
        }
    }

    public void HandleClick()
    {
        gameController.ResultButtonClicked(answerData.isCorrect);
    }

}
