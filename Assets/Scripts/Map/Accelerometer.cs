using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    //public bool isFlat = true;
    //private Rigidbody rigid;

    //private void Start()
    //{
    //    rigid = GetComponent<Rigidbody>();
    //}
    public Vector3 tilt;
    public static Accelerometer Instance;

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        tilt = Input.acceleration;

        //if (isFlat)
        //    tilt = Quaternion.Euler(90, 0, 0) * tilt;

        //rigid.AddForce(tilt);
    }
}
