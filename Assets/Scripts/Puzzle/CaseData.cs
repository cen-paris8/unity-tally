using UnityEngine;


public class CaseData : MonoBehaviour
{
    public int index;
    public Vector3 position;

    public CaseData(int newIndex, Vector3 newPosition)
    {
        index = newIndex;
        position = newPosition;
    }
}
