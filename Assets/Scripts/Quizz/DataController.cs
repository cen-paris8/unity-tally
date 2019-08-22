using System.IO;
using System.Text;
using UnityEngine;

// Attached to GO DataController in Persistent scene.
public class DataController : MonoBehaviour
{
    //List<RoundData> allRoundData = new List<RoundData>();
    public GameData[] gameDatas;

    private PlayerProgress playerProgress;
    private string gameDataFileName = "data.json"; // A json for all game;
    private string resourcesPath = "Assets/Resources/";



    // Start is called before the first frame update
    // Go To Menu Screen.
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadDataGame();
        LoadPlayerProgress();

        UnityEngine.SceneManagement.SceneManager.LoadScene("Swipe");       

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

        if (PlayerPrefs.HasKey("highestScore"))
        {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }

    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
    }

    private void LoadDataGame()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath, Encoding.UTF8);
            GameArrayData loadedData = JsonUtility.FromJson<GameArrayData>(dataAsJson);
            gameDatas = loadedData.allGameData;
        }
    }

    public Sprite getImageSprite(string questionDataImage)
    {


            Sprite newSprite;
            Texture2D spriteTexture;
            string filePath = resourcesPath + questionDataImage; // "RenoirValadon.jpg";
            Texture2D tex2D;
            byte[] fileData;


            if (File.Exists(filePath)) // filePath
            {
                print("File exists");
                fileData = File.ReadAllBytes(filePath);
                tex2D = new Texture2D(2, 2);           // Create new "empty" texture
                if (tex2D.LoadImage(fileData))
                {
                    print("Load Image");
                    spriteTexture = tex2D; // If data = readable -> return texture
                    newSprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

                //imageDisplay.GetComponent<Image>().sprite = newSprite;
                return newSprite;
                }
            }
        newSprite = null;
        return newSprite;



    }
}