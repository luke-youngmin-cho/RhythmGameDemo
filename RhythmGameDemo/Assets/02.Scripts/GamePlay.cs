using UnityEngine;
using UnityEngine.Video;
using System.Collections;
public class GamePlay: MonoBehaviour
{
    static public GamePlay instance;

    public bool onPlay;
    public VideoPlayer vp;
    public NoteManager noteManager;

    public float playTime;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        Play();
        GameManager.gameState = GameState.WaitForFinish;
    }
    private void Update()
    {
        playTime += Time.deltaTime;
        if (onPlay == false)
            GameManager.gameState = GameState.Finish;
    }
    public void Play()
    {   
        onPlay = true;
        noteManager.StartSpawn();
        StartCoroutine(E_VPPlay());
    }
    IEnumerator E_VPPlay()
    {
        yield return new WaitUntil(()=>NoteManager.instance != null);
        yield return new WaitForSeconds(NoteManager.instance.noteFallingTime);
        vp.clip = SongSelector.instance.clip;
        vp.Play();
    }
    public void Stop()
    {
        onPlay = false;
        vp.Stop();
        noteManager.StopSpawn();
    }
}