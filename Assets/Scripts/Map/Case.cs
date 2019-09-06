using System;
using UnityEngine;
using UnityEngine.UI;

public class Case : IComparable<Case>
{
    public Text panelText;
    

    public  string caseName;


    // from GPS
    public float latitude;
    public float longitude;
    public float trueHeading;
    // form Accelerometer
    public Vector3 acMeter;//{ get; set; }
    // from GyroControl
    public Vector3 transformRot;
    public Color color = Color.white;
    

    public Case(string newName)
    {
        caseName = newName;

    }


    public int CompareTo(Case other)
    {
        if (other == null)
        {
            return 1;
        }
        Debug.Log("caseName to compare :" + caseName);
        Debug.Log("other caseName to compare :" + other.caseName);

        return Mathf.RoundToInt((latitude - other.latitude) + (longitude - other.longitude) + (trueHeading - other.trueHeading));
        
    }



}
