using System.Collections;
using UnityEngine;

public class GPS : MonoBehaviour
{
    public static GPS Instance { set; get; }
    public float latitude;
    public float longitude;

    public float trueHeading;

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location désactivée");
            yield break;
        }

        Input.compass.enabled = true;

        Input.location.Start();
        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Time out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Cannot determin device location");
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        trueHeading = Input.compass.trueHeading;

        yield break;
    }

    private void Update()
    {
        StartCoroutine(StartLocationService());
    }
}
