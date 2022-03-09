using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameState gameState;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        DontDestroyOnLoad(instance);
    }
    private void Update()
    {
        switch (gameState)
        {
            case GameState.Select:
                break;
            case GameState.Play:
                break;
            case GameState.WaitForFinish:
                break;
            case GameState.Finish:
                GoToScoreScene();
                gameState = GameState.Score;
                break;
            case GameState.Score:
                break;
            default:
                break;
        }
    }
    public void GoToPlayScene()
    {
        if (SongSelector.instance.isPlayable)
        {
            SceneManager.LoadScene("Play");
            gameState = GameState.Play;
        }
    }
    public void GoToScoreScene()
    {
        SceneManager.LoadScene("Score");
    }
}
public enum GameState
{
    Select,
    Play,
    WaitForFinish,
    Finish,
    Score,
}