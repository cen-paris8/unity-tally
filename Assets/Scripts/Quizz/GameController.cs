﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System;

// Attached to GO GameController in Game scene.
public class GameController : MonoBehaviour
{
    public Text questionDisplayText;
    public Text scoreDisplayText;
    public Text highestScore;
    public Text timeRemainingDisplayText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;

    public SimpleObjectPool answerInputObjectPool;
    public Transform answerInputParent;

    public GameObject audioDisplay;
    public GameObject questionDisplay;
    public GameObject imageDisplay;
    public GameObject roundEndDisplay;
    public GameObject imagePresentDisplay;
    public GameObject resultDisplay;

    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;

    private bool isRoundActive;
    private float timeRemaining;
    private int questionIndex;
    private int playerScore;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();
    private List<GameObject> answerInputGameObjects = new List<GameObject>();

    private QuestionData questionData;

    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        //currentRoundData = dataController.GetCurrentRoundData("Quizz");
        string gameName = GameNameScript.Instance.gameName;
        currentRoundData = dataController.GetCurrentRoundData(gameName);
        if (gameName == "EndGame")
        {
            roundEndDisplay.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Fin";
            EndRound();
        } else
        {
            imagePresentDisplay.GetComponent<Image>().sprite = dataController.getImageSprite(gameName);

        }



        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimitInSeconds;

        questionIndex = 0;
        playerScore = dataController.GetPlayerScore();

        ShowQuestion();
        isRoundActive = true;
        
    }

    private void ShowQuestion()
    {
        RemoveAnswerButton();
        questionData = questionPool[questionIndex];
        questionDisplayText.text = questionData.questionText;
        audioDisplay.GetComponent<AudioPanel>().audioClipCommand.GetComponent<AudioController>().UnloadAudio();

        // Manage Audio question.
        if (questionData.questionAudio != null && questionData.questionAudio != "")
        {
            audioDisplay.GetComponent<AudioPanel>().audioClipCommand.GetComponent<AudioController>().LoadAudio(questionData.questionAudio);
            audioDisplay.SetActive(true);
            
        }

        if (questionData.questionImage != null && questionData.questionImage != "")
        {
            imageDisplay.SetActive(true);
            imageDisplay.GetComponent<Image>().sprite = dataController.getImageSprite(questionData.questionImage);
        }
        else
        {
            imageDisplay.SetActive(false);
        }

        // There 2 types of answer: Button or InputField.
        if (questionData.questionType == "Input")
        {
            for (int i = 0; i < questionData.answers.Length; i++)
            {
                GameObject answerInputGameObject = answerInputObjectPool.GetObject();
                answerInputGameObjects.Add(answerInputGameObject);
                answerInputGameObject.transform.SetParent(answerInputParent);


                AnswerInput answerInput = answerInputGameObject.GetComponent<AnswerInput>();
                answerInput.SetUp(questionData.answers[i]);
            }
        }
        else
        {
            for (int i = 0; i < questionData.answers.Length; i++)
            {
                GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
                answerButtonGameObjects.Add(answerButtonGameObject);
                answerButtonGameObject.transform.SetParent(answerButtonParent);


                AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
                answerButton.SetUp(questionData.answers[i]);
            }
        }

        dataController.debudText.text += "question display image : " + questionDisplay.GetComponent<Image>().sprite.name;


    }

    private void RemoveAnswerButton()
    {
        while( answerButtonGameObjects.Count > 0){
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
        while (answerInputGameObjects.Count > 0)
        {
            answerInputObjectPool.ReturnObject(answerInputGameObjects[0]);
            answerInputGameObjects.RemoveAt(0);
        }
    }

    public void ResultButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            scoreDisplayText.text = "Score: " + playerScore.ToString();
            resultDisplay.GetComponentInChildren<Text>().text = "Bravo !";
            resultDisplay.SetActive(true);
        } else
        {
            string answerWas = "";
            for (int i = 0; i < questionData.answers.Length; i++)
            {
                if (questionData.answers[i].isCorrect == true)
                {
                    answerWas = questionData.answers[i].answerText;
                }
            }
                resultDisplay.GetComponentInChildren<Text>().text = "Non, la réponse était : " + answerWas;
                resultDisplay.SetActive(true);
        }

       
    }

    public void AnswerButtonClicked()
    {
        resultDisplay.SetActive(false);
        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            EndRound();
        }
    }

    public void AnswerButtonInput(string response)
    {
        QuestionData questionData = questionPool[questionIndex];

        if (response == questionData.questionResponse)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            scoreDisplayText.text = "Score: " + playerScore.ToString();
        }

        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            EndRound();
        }
    }

    public void EndRound()
    {
        isRoundActive = false;
        dataController.SubmitNewPlayerScore(playerScore);
        highestScore.text = "High score: " + dataController.GetHighestPlayerScore().ToString();

        questionDisplay.SetActive(false);
        imagePresentDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);
    }

    private void UpdateTimeRemainingDisplay()
    {
        timeRemainingDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString(); 
    }

    void Update()
    {
        if (isRoundActive)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeRemainingDisplay();
        }
        if (timeRemaining < 0f)
        {
            EndRound();
        }
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        imagePresentDisplay.SetActive(false);
    }
}
