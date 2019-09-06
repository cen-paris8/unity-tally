using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coordinate : MonoBehaviour
{

    public List<Case> cases = new List<Case>();
    public Button buttonPrefab;
    public bool endSetting = false;
    //public GameObject endSettingCanvas;
    //public List<Button> buttons;

    // Start is called before the first frame update
    void Start()
    {
        DrawingMap();
    }

    private void DrawingMap()
    {
        
        int longPrintedSpace = 0;
        int ligne = 1;
        
        while (longPrintedSpace < Screen.height)
        {
            int largePrintedSpace = 0;
            int col = 1;
            while (largePrintedSpace < Screen.width)
            {
                
                Transform transformParent = this.transform;
                Button btn = Instantiate(buttonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                btn.transform.SetParent(transformParent);

                string caseName = ligne + ":" + col;
                btn.GetComponent<CaseButton>().name = ligne + ":" + col;

                if (( ligne == 6 && (col == 8 || col == 14)) || ( ligne == 14 && col == 1 )
                    || (ligne == 20 && col == 8))
                {

                    ColorBlock colors = btn.GetComponent<Button>().colors;
                    Color colorBalck = Color.black;
                    colorBalck.a = 1;
                    colors.normalColor = colorBalck; // Color.black;
                    btn.GetComponent<Button>().colors = colors;
                    Color imageColor = btn.GetComponent<Image>().color;
                    imageColor.a = 1;
                    imageColor = Color.black;
                    btn.GetComponent<Image>().color = imageColor;
                }
                
                else if (ligne == 14 && col == 4)
                {
                    ColorBlock colors = btn.GetComponent<Button>().colors;
                    Color colorBlue = Color.blue;
                    colorBlue.a = 1;
                    colors.normalColor = colorBlue; // Color.black;
                    btn.GetComponent<Button>().colors = colors;
                    Color imageColor = btn.GetComponent<Image>().color;
                    imageColor.a = 1;
                    imageColor = Color.blue;
                    btn.GetComponent<Image>().color = imageColor;
                }
   
                cases.Add(new Case(caseName));
                //buttons.Add(btn);
                largePrintedSpace += 55;
                col++;
            }
           longPrintedSpace += 55;
            ligne++;

        }
    }

    public Case GetCaseByName(string nameToFind)
    {
        foreach(Case caseToFind in cases)
        {
            if (nameToFind == caseToFind.caseName)
            {
                return caseToFind;
            }
        }
        return cases[0];
    }
    
    //public void EndSetting()
    //{
    //    endSetting = true;
    //    CaseButton[] array = GetComponentsInChildren<CaseButton>();
    //    GetComponent<CalculPosition>().endSetting = true;

    //    for (int i = 0; i<array.Length; i++)
    //    {
    //        array[i].endSetting = true;
    //    }
    //    endSettingCanvas.SetActive(false);
    //}


}
