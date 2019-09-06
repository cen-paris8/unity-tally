using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonScript : MonoBehaviour
{
    //public Text menuButtonText;
    private Text menuButtonText;
    private void Start()
    {
        Scene sceneActive = SceneManager.GetActiveScene();
        menuButtonText = GetComponentInChildren<Text>();
        if (sceneActive == SceneManager.GetSceneByName("Swipe"))
        {
            menuButtonText.text = "Jeux";
        }
        else
        {
            menuButtonText.text = "Carte";
        }
    }

    public void ChangeMode()
    {
        
        if (menuButtonText.text == "Jeux")
        {
            menuButtonText.text = "Carte";
            SceneManager.LoadScene("Map");
        }
        else
        {
            menuButtonText.text = "Jeux";
            SceneManager.LoadScene("Swipe");
        }

    }
}
