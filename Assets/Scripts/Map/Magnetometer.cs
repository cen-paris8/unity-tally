using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetometer : MonoBehaviour
{
    public static Magnetometer Instance;
    public float trueHeading;
    public float accuracyHeading;
    public float magneticHeading;
    public Vector3 rawVector;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (!Input.compass.enabled)
        {
            Debug.Log("Compass désactivé");
            return;
        }
            
        Input.location.Start();
    }

    // Update is called once per frame
    void Update()
    {
        trueHeading = Input.compass.trueHeading;
        accuracyHeading = Input.compass.headingAccuracy;
        magneticHeading = Input.compass.magneticHeading;
        rawVector = Input.compass.rawVector;

    }
}
