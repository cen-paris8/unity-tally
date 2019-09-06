using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CaseButton : MonoBehaviour
{
    //public Text panelText;
    public bool endSetting = false;
    public Button btn;


    private bool isCoordScanned;


    private Case caseAsso;

    public CaseButton(string newName)
    {
        name = newName;
        
    }

    public void ScanCoordinate() //void Update()
    {
        Debug.Log("endSetting : " + endSetting);
        if (endSetting == true)
        {
            return;
        }
        if (!isCoordScanned)
        {
            isCoordScanned = true;
            GetComponent<Image>().color = Color.white;

            Debug.Log("Start scancoordinate");
            int maxScanTime = 5;

            Vector3 myPositionAccel = Accelerometer.Instance.tilt;

            //from GPS 
            float myPositionMagneto = GPS.Instance.trueHeading;

            // from GyroControl
            Vector3 myPositionGyro = GyroControl.Instance.transformRot;


            while (maxScanTime > 0)
            {
                myPositionAccel.x = (Accelerometer.Instance.tilt.x + myPositionAccel.x) / 2;
                myPositionAccel.y = (Accelerometer.Instance.tilt.y + myPositionAccel.y) / 2;
                myPositionAccel.z = (Accelerometer.Instance.tilt.z + myPositionAccel.z) / 2;

                myPositionMagneto = (GPS.Instance.trueHeading + myPositionMagneto) / 2;

                myPositionGyro.x = (GyroControl.Instance.transformRot.x + myPositionGyro.x) / 2;
                myPositionGyro.y = (GyroControl.Instance.transformRot.y + myPositionGyro.y) / 2;
                myPositionGyro.z = (GyroControl.Instance.transformRot.z + myPositionGyro.z) / 2;

                maxScanTime--;
                //Debug.Log("End while");
            }


            caseAsso = GetComponentInParent<Coordinate>().GetCaseByName(name);

            caseAsso.acMeter.x = myPositionAccel.x;
            caseAsso.acMeter.y = myPositionAccel.y;
            caseAsso.acMeter.z = myPositionAccel.z;

            Debug.Log("End scancoordinate");

            // from GPS
            caseAsso.latitude = GPS.Instance.latitude;
            caseAsso.longitude = GPS.Instance.longitude;

            caseAsso.trueHeading = myPositionMagneto;

            caseAsso.transformRot = myPositionGyro;

        }

        //panelText.text = "Accelerometer: " + caseAsso.acMeter.ToString();
        //panelText.text += "latitude:" + caseAsso.latitude.ToString() + " " + " longitude:" + caseAsso.longitude + " ";
        //panelText.text += "Compass: " + "trueHeading:" + caseAsso.trueHeading.ToString() + " ";
        //panelText.text += "Gyroscope Rotation sur les axes: " + "x:" + caseAsso.transformRot.x.ToString() + " "
        //                        + "y:" + caseAsso.transformRot.y.ToString() + " "
        //                        + "z:" + caseAsso.transformRot.z.ToString();



    }

    //public void chooseRightCase()
    //{
    //    Debug.Log("Choose right case");
    //    GetComponentInParent<CalculPosition>().casePos.caseName = name;
    //    GetComponent<Image>().color = Color.black;
    //}


    public void OpenGame()
    {
        if (name == "6:8")
        {
            Debug.Log("Load Scene QCM");

            ColorBlock colors = btn.GetComponentInParent<Button>().colors;
            Color colorRed = Color.red;
            colorRed.a = 1;
            colors.normalColor = colorRed; // Color.black;
            btn.GetComponent<Button>().colors = colors;
            Color imageColor = btn.GetComponent<Image>().color;
            imageColor.a = 1;
            imageColor = Color.red;
            btn.GetComponent<Image>().color = imageColor;
            GameNameScript.Instance.gameName = "Quizz";
            SceneManager.LoadScene("Game");
        }
        if (name == "6:14")
        {
            Debug.Log("Load Scene Intru");
            ColorBlock colors = btn.GetComponentInParent<Button>().colors;
            Color colorRed = Color.red;
            colorRed.a = 1;
            colors.normalColor = colorRed; // Color.black;
            btn.GetComponent<Button>().colors = colors;
            Color imageColor = btn.GetComponent<Image>().color;
            imageColor.a = 1;
            imageColor = Color.red;
            btn.GetComponent<Image>().color = imageColor;
            GameNameScript.Instance.gameName = "Intru";
            SceneManager.LoadScene("Game");
        }
        if (name == "14:1")
        {
            Debug.Log("Load Scene Puzzle");
            ColorBlock colors = btn.GetComponentInParent<Button>().colors;
            Color colorRed = Color.red;
            colorRed.a = 1;
            colors.normalColor = colorRed; // Color.black;
            btn.GetComponent<Button>().colors = colors;
            Color imageColor = btn.GetComponent<Image>().color;
            imageColor.a = 1;
            imageColor = Color.red;
            btn.GetComponent<Image>().color = imageColor;
            GameNameScript.Instance.gameName = "Puzzle";
            SceneManager.LoadScene("Puzzle");
        }
        if (name == "20:8")
        {
            Debug.Log("Load Scene QueRep");
            ColorBlock colors = btn.GetComponentInParent<Button>().colors;
            Color colorRed = Color.red;
            colorRed.a = 1;
            colors.normalColor = colorRed; // Color.black;
            btn.GetComponent<Button>().colors = colors;
            Color imageColor = btn.GetComponent<Image>().color;
            imageColor.a = 1;
            imageColor = Color.red;
            btn.GetComponent<Image>().color = imageColor;
            GameNameScript.Instance.gameName = "Input";
            SceneManager.LoadScene("Game");
        }
        if (name == "14:4")
        {
            Debug.Log("Load Scene EndGame");
            ColorBlock colors = btn.GetComponentInParent<Button>().colors;
            Color colorRed = Color.red;
            colorRed.a = 1;
            colors.normalColor = colorRed; // Color.black;
            btn.GetComponent<Button>().colors = colors;
            Color imageColor = btn.GetComponent<Image>().color;
            imageColor.a = 1;
            imageColor = Color.red;
            btn.GetComponent<Image>().color = imageColor;
            GameNameScript.Instance.gameName = "EndGame";
            SceneManager.LoadScene("Game");
        }
    }
}
