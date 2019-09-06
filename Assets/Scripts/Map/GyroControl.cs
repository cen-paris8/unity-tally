using UnityEngine;

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    public Vector3 transformRot;
    public static GyroControl Instance;

    //private GameObject cameraContainer;
    private Quaternion rot;

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //cameraContainer = new GameObject("Camera Container");
        //cameraContainer.transform.position = transform.position;
        //transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnabledGyro();
    }

    private bool EnabledGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            //cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
            //Debug.Log("gyro.attitude.eulerAngles:" + gyro.attitude.eulerAngles.ToString());
            //Debug.Log("gyro.rotationRate:" + gyro.rotationRate.ToString());
            //Debug.Log("gyro.attitude.x:" + gyro.attitude.x.ToString());
            //Debug.Log("gyro.attitude.y:" + gyro.attitude.y.ToString());
            //Debug.Log("gyro.attitude.z:" + gyro.attitude.z.ToString());
            transformRot = gyro.attitude.eulerAngles;
        }
    }
}