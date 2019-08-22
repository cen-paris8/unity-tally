using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTrav : MonoBehaviour
{
    Touch touch;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        transform.Translate(Input.GetTouch(0).deltaPosition * Time.deltaTime * 1f);

        touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            //StartPosition = 
               print( touch.position);
        }
    }
}
