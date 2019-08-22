using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public Text answerText;

    private AnswerData answerData;
    private GameController gameController;
    private string artPath = "Assets/Resources/";

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
        if (answerData.answerImage != "")
        {

            Sprite newSprite;
            Texture2D spriteTexture;
            string filePath = artPath + answerData.answerImage; // "RenoirValadon.jpg";
            Texture2D tex2D;
            byte[] fileData;


            if (File.Exists(filePath)) // filePath
            {
                print("File exists");
                fileData = File.ReadAllBytes(filePath);
                tex2D = new Texture2D(2, 2);           // Create new "empty" texture
                if (tex2D.LoadImage(fileData))
                {
                    print("Load Image");
                    spriteTexture = tex2D; // If data = readable -> return texture
                    newSprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

                    GetComponent<Image>().sprite = newSprite;
                    GetComponent<Image>().color = new Color(255, 255, 255);
                }
            }
        }
        else
        {
            GetComponent<Image>().sprite = null;
            GetComponent<Image>().color = new Color(0, 0, 0);
        }

    }

    public void HandleClick()
    {
        gameController.AnswerButtonClicked(answerData.isCorrect);
    }

}
