using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// Attached to GO DataController in Persistent scene.
public class DataController : MonoBehaviour
{
    //List<RoundData> allRoundData = new List<RoundData>();
    public GameData[] gameDatas;
    public Text debudText;

    private DBHandler dbHandler;

    private PlayerProgress playerProgress;
    private string gameDataFileName = "data.json"; // A json for all game;
    private string resourcesPath = "Assets/Resources/";
    private string firstScene;


    // Start is called before the first frame update
    // Go To Menu Screen.
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        firstScene = "Map"; // "Swipe";
        //LoadDataGame();

        // Call in UseDataGame()
        //LoadPlayerProgress();

        //UnityEngine.SceneManagement.SceneManager.LoadScene("Swipe");       

    }

    // Get content of questions for feed game.
    public RoundData GetCurrentRoundData(string value)
    {
        RoundData[] allCurrentRoundData = GetRoundDataByType(value);

        return allCurrentRoundData[0]; // allRoundData["Quizz"];
    }

    // Get All Round Data by type of question Quizz, Intru, Puzzle.
    public RoundData[] GetRoundDataByType( string value)
    {
        foreach (GameData gameData in gameDatas)
        {
            if (gameData.name == value)
            {
                return gameData.allRoundData;
            }
        }
        return gameDatas[0].allRoundData;
    }

    public int GetPlayerScore()
    {
        if (PlayerPrefs.HasKey("highestScore"))
        {
            return(PlayerPrefs.GetInt("playerScore"));
        }
        return 0;
    }

    public void SubmitNewPlayerScore(int newScore)
{
        PlayerPrefs.SetInt("playerScore", newScore);

        if (newScore > playerProgress.highestScore)
    {
        playerProgress.highestScore = newScore;
        SavePlayerProgress();
    }
}

    public int GetHighestPlayerScore(){
        return playerProgress.highestScore;
    }
    private void LoadPlayerProgress()
    {
        playerProgress = new PlayerProgress();

        /*
         * if (PlayerPrefs.HasKey("highestScore"))
        {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
        }
        */
        
        Load();
    }

    private void SavePlayerProgress()
    {
        //PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
        Save();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        bf.Serialize(file, playerProgress);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            playerProgress = (PlayerProgress)bf.Deserialize(file);
            file.Close();

            //playerProgress.highestScore = playerData.score;

        }


    }
    public void UseDataGame(string dataAsJson)
    {
        GameArrayData loadedData = JsonUtility.FromJson<GameArrayData>(dataAsJson);
        gameDatas = loadedData.allGameData;
        LoadPlayerProgress();
        //debudText.GetComponentInParent<Canvas>().gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(firstScene);       
    }
    public void LoadDataGame()
    {
        //String dataJson = dbHandler.Get
        //GameArrayData loadedData = JsonUtility.FromJson<GameArrayData>(dataJson);
        /*
        gameDatas = loadedData.allGameData;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath, Encoding.UTF8);
            GameArrayData loadedData = JsonUtility.FromJson<GameArrayData>(dataAsJson);
            gameDatas = loadedData.allGameData;

        }
        */
    }

    public Sprite getImageSprite(string questionDataImage)
    {
        //debudText.GetComponentInParent<Canvas>().gameObject.SetActive(true);
        debudText.text += "; Image name : " + questionDataImage;

    Sprite newSprite;
            Texture2D spriteTexture;
            string filePath = resourcesPath + questionDataImage; // "RenoirValadon.jpg";
            Texture2D tex2D;
            byte[] fileData;
        debudText.text += "; file path : " + filePath;

            if (File.Exists(filePath)) // filePath
            {
                print("File exists");
            debudText.text += "; File exists  ";
                fileData = File.ReadAllBytes(filePath);
                tex2D = new Texture2D(2, 2);           // Create new "empty" texture
                if (tex2D.LoadImage(fileData))
                {
                debudText.text += "; Load Image ";
                print("Load Image");
                    spriteTexture = tex2D; // If data = readable -> return texture
                    newSprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

                //imageDisplay.GetComponent<Image>().sprite = newSprite;
                return newSprite;
                }
            }
        debudText.text += "; getImageSprite  ";
        newSprite = null;
        return newSprite;



    }
}