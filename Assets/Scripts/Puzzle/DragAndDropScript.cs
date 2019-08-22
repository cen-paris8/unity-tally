using System.Collections.Generic;
using UnityEngine;

public class DragAndDropScript : MonoBehaviour
{
    public bool isDraging = false;

    public GameObject[] puzzle;
 
    private Vector2 startTouch, swipeDelta;
    private Ray ray;

    private int indexPositionStart;
    private int indexPositionEnd;

    private GameObject caseToMove, caseToPush;
    private Vector3 startTransform;
    private List<CaseData> winnerPuzzle = new List<CaseData>();

    public void RandomPuzzle()
    {
        
        List<CaseData> arrayInt = new List<CaseData>();
        Debug.Log("puzzle.Length : " + puzzle.Length);
        for (int z = 0; z < puzzle.Length; z++) //puzzle.Length
        { 
            Debug.Log("z : " + z);
            arrayInt.Add(new CaseData(z, puzzle[z].transform.position));
            winnerPuzzle.Add(new CaseData(z, puzzle[z].transform.position));

        }

        int l = 0;
        int m = 0;
        while (arrayInt.Count > 0)
        {
            m = Random.Range(0, arrayInt.Count);
            puzzle[l].transform.position = arrayInt[m].position;
            l++;
            arrayInt.RemoveAt(m);
        }

    }

    // Update is called once per frame
    void Update()
    {
        #region Standalone Inputs
        
        if (Input.GetMouseButtonDown(0))    // Get contact case
        {
            startTouch = Input.mousePosition;
            Ray rayStart = Camera.main.ScreenPointToRay(Input.mousePosition);
            isDraging = true;
            indexPositionStart = GetClosestCase(rayStart.origin);
            caseToMove = puzzle[indexPositionStart];
            startTransform = caseToMove.transform.position;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Reset(); 
        }
        #endregion

        // Attached Case and Mouse or Touch
        if (isDraging)
        {
            if (Input.GetMouseButton(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                caseToMove.transform.position = ray.origin;
            }
        }
    }
    
    private int GetClosestCase(Vector3 origin, int indexToAvoid=-1)
    {
        float distance = 1000f, newDistance = 1000f;
        int indexClosest = 0;
            
        for (int i = 0; i < puzzle.Length; i++)
        {
            if (indexToAvoid >= 0 && i == indexToAvoid)
            {
                i++;
                if (i == puzzle.Length) return indexClosest;
            }
            newDistance = Vector3.Distance(puzzle[i].transform.position, origin);
            if (newDistance < distance)
            {
                distance = newDistance;
                indexClosest = i;
            }
        }
        return indexClosest;
    }
    
    // Put case attached and move other ones.
    private void Reset()
    {
        isDraging = false;
        indexPositionEnd = GetClosestCase(ray.origin, indexPositionStart);
        caseToMove.transform.position = puzzle[indexPositionEnd].transform.position;
        caseToPush = puzzle[indexPositionEnd];
        pushCases(indexPositionStart, indexPositionEnd, startTransform);
        if (checkWinPuzzle())
        {
            Debug.Log("WIN !!!!");
            // Manage Score;
        }

    }

    private bool checkWinPuzzle()
    {
        for (int i = 0; i < puzzle.Length; i++)
        {
            if (puzzle[i].transform.position != winnerPuzzle[i].position)
                return false;
        }
        GetComponentInParent<PuzzleControler>().SubmitPlayerScore();
        return true;
    }

    private void pushCases(int indexPositionStart, int indexPositionEnd, Vector3 startPosition)
    {
        if (indexPositionStart == indexPositionEnd)
        {
            return;
        }
        if (indexPositionStart < indexPositionEnd)
        {
            int i = indexPositionEnd;
            while( i > indexPositionStart + 1){
                puzzle[i].transform.position = puzzle[i-1].transform.position;
                i--;
            }
            puzzle[i].transform.position = startPosition;
        }
        else
        {
            int i = indexPositionEnd;
            while (i < indexPositionStart - 1)
            {
                puzzle[i].transform.position = puzzle[i+1].transform.position;
                i++;
            }
            puzzle[i].transform.position = startPosition;
        }
    }
}