using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleControler : MonoBehaviour
{
    // Get Data from Data Controller 
    // => Text, Image
    // Découper l'image
    // Insérer les images sur les cases.
    public GameObject imagePuzzle;
    public Text scoreDisplayText;
    public Text highestScore;
    public Text timeRemainingDisplayText;
    public GameObject roundEndDisplay;
    public GameObject puzzleDisplay;

    private GameObject[] puzzleListCase;
    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;


    private float timeRemaining;
    private int questionIndex;
    private int playerScore;
    private QuestionData questionData;



    void Awake()
    {
        
        dataController = FindObjectOfType<DataController>();
        currentRoundData = dataController.GetCurrentRoundData(GameNameScript.Instance.gameName); // Puzzle

        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimitInSeconds;

        questionIndex = 0;
        playerScore = 0;

        ShowPuzzle();
    }

    private void Start()
    {
        playerScore = dataController.GetPlayerScore();
    }

    private void ShowPuzzle()
    {
        questionData = questionPool[questionIndex];

        puzzleListCase = GetComponentInParent<DragAndDropScript>().puzzle;

        imagePuzzle.GetComponent<Image>().sprite = dataController.getImageSprite(questionData.questionImage);


        for (int  i = 0; i < puzzleListCase.Length; i++)
        {
            Vector3 originaleScale = puzzleListCase[i].transform.localScale;
            originaleScale = Vector3.one;
            Vector3 originalePosition = puzzleListCase[i].transform.position;
            originalePosition = Vector3.zero;
            GameObject duplicate = Instantiate(imagePuzzle);

            duplicate.transform.SetParent(puzzleListCase[i].transform);

            RectTransform objectRectTransform = duplicate.GetComponent<RectTransform>();
            objectRectTransform.localScale = originaleScale;
            objectRectTransform.position = originalePosition;

        }

        imagePuzzle.SetActive(false);
        GetComponentInParent<DragAndDropScript>().RandomPuzzle();
    }

    private void Update()
    {
            timeRemaining -= Time.deltaTime;
            UpdateTimeRemainingDisplay();
        if (timeRemaining < 0f)
        {
            EndRound();
        }
    }

    private void UpdateTimeRemainingDisplay()
    {
        timeRemainingDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
    }

    public void EndRound()
    {
        dataController.SubmitNewPlayerScore(playerScore);
        highestScore.text = "High score: " + dataController.GetHighestPlayerScore().ToString();

        puzzleDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);
    }

    public void SubmitPlayerScore() // Player Win
    {
        playerScore += currentRoundData.pointsAddedForCorrectAnswer;
        scoreDisplayText.text = "Score: " + playerScore.ToString();
        EndRound();
    }
}
