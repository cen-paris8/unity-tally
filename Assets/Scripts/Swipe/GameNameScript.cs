using System.Collections.Generic;
using UnityEngine;

// Attached to GO GameName in Swipe scene
// Singleton peristent.
public class GameNameScript : GenericSingletonClass<GameNameScript> //MonoBehaviour
{
    public string gameName;
    public List<string> statusButtonStart;

    public void GetStartBtnStatus()
    {
        for (int i = 0; i < statusButtonStart.Count; i++)
        {
            GameObject.FindGameObjectWithTag(statusButtonStart[i]).SetActive(false);
        }
    }

}
