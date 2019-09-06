using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;

public class CalculPosition : MonoBehaviour
{
    public Dictionary<string, object> myPosition = new Dictionary<string, object>();
    public Text panelTextPos;
    public Text panelTextSol;

    // from GPS
    public float latitude;
    public float longitude;
    public float trueHeading;
    // form Accelerometer
    public Vector3 acMeter;//{ get; set; }
    // from GyroControl
    public Vector3 transformRot;

    private List<Case> cases;
    private List<string> gameNames = new List<string>();
    public Case casePos;
    public bool endSetting;

    // Start is called before the first frame update
    void Start()
    {
        // Init List of cases 
        GetAllSettingCases();

        gameNames.Add("Quizz");
        gameNames.Add("Intru");
        gameNames.Add("Puzzle");
        gameNames.Add("QuesRep");

        InvokeRepeating("GetPosition", 10.0f, 5.0f);
    }

    private void GetAllSettingCases()
    {
        //With LINQ(requires using System.Linq;):

        //list = GetComponents<SomeComponent>().ToList();

        cases = GetComponent<Coordinate>().cases;

     //   Without LINQ:

     //var list = new List<SomeComponent>(GetComponents<SomeComponent>());

       
    }

    private void GetPosition()
    {
        Debug.Log("Start GetPosition : " + endSetting);
        if (endSetting == false)
        {
            return;
        }

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

            acMeter.x = myPositionAccel.x;
            acMeter.y = myPositionAccel.y;
            acMeter.z = myPositionAccel.z;

            Debug.Log("End scancoordinate");

            // from GPS
            latitude = GPS.Instance.latitude;
            longitude = GPS.Instance.longitude;

            trueHeading = myPositionMagneto;

            transformRot = myPositionGyro;


        panelTextPos.text = "Accelerometer: " + acMeter.ToString();
        panelTextPos.text += "latitude:" + latitude.ToString() + " " + " longitude:" + longitude + " ";
        panelTextPos.text += "Compass: " + "trueHeading:" + trueHeading.ToString() + " ";
        panelTextPos.text += "Gyroscope Rotation sur les axes: " + "x:" + transformRot.x.ToString() + " "
                                + "y:" + transformRot.y.ToString() + " "
                                + "z:" + transformRot.z.ToString();

        GetCasePosition();
        GetCaseGame(casePos.caseName);

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCasePosition()
    {
        // Get Random case 1st sol.
        //Case propCase = cases[UnityEngine.Random.Range(0, cases.Count)];
        
        // Get the closest case 2 sol.
        // cases.Sort();

        casePos = cases[cases.Count - 1];
        Debug.Log("Case closest: " + cases[cases.Count - 1].caseName);
        List<CaseButton> list = GetComponentsInChildren<CaseButton>().ToList();
        int distanceFromCase = 99999;

        for (int i = 0; i < cases.Count; i++)
        {
            if (cases[i].latitude == 0)
            {
                break;
            }
            int calculDistance = Mathf.RoundToInt((latitude - cases[i].latitude) + (longitude - cases[i].longitude) + (trueHeading - cases[i].trueHeading));
            if (distanceFromCase < calculDistance)
            {
                distanceFromCase = calculDistance;
                casePos = cases[i];
            }
        }
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].name == cases[cases.Count - 1].caseName)
            {
                list[i].GetComponentInParent<Image>().color = Color.black;

            }
        }

        panelTextSol.text = "Accelerometer: " + casePos.acMeter.ToString();
        panelTextSol.text += "latitude:" + casePos.latitude.ToString() + " " + " longitude:" + casePos.longitude + " ";
        panelTextSol.text += "Compass: " + "trueHeading:" + casePos.trueHeading.ToString() + " ";
        panelTextSol.text += "Gyroscope Rotation sur les axes: " + "x:" + casePos.transformRot.x.ToString() + " "
                                + "y:" + casePos.transformRot.y.ToString() + " "
                                + "z:" + casePos.transformRot.z.ToString();

        // Get IA sol.

        // Change color of case
        //With LINQ(requires using System.Linq;):



        // Ask if it good ? and save the result
        //if (EditorUtility.DisplayDialog("Valid position",
        //        "Case: " + cases[0].caseName + " Is it ok ? ", "Yes", "No"))
        //{
        //    // Cool
        //    
        //}
        //else
        //{
        //    EditorUtility.DisplayDialog("Valid position 2",
        //        "Choose the good one", "ok");
        //    // To Do

        //}

    }

    public void GetCaseGame(string caseName)
    {
        if (caseName == "3:2" || caseName == "3:1" || caseName == "4:3" ||caseName == "4:4" || caseName == "5:3")
        {
            //SceneManager.LoadScene(gameNames[UnityEngine.Random.Range(0, gameNames.Count)]);
        }
    }

    
}
