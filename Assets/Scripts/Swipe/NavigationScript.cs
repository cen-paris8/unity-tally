using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Attached to GO GameName
public class NavigationScript : MonoBehaviour
{
    //public GameNameScript current = new GameNameScript();
    private GameObject buttonToDisabled;
    private List<string> statusButtonStart = new List<string>();

    private void Start()
    {
        GameNameScript.Instance.GetStartBtnStatus();
    }

    public void OpenGame(string sceneName)
    {

        string[] sceneNamesplited = sceneName.Split(',');

        GameNameScript.Instance.statusButtonStart.Add(sceneNamesplited[0]);

        GameNameScript.Instance.gameName = sceneNamesplited[1];
        SceneManager.LoadScene(sceneNamesplited[2]);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Map");
        GameNameScript.Instance.gameName = "Map";
    }
}

