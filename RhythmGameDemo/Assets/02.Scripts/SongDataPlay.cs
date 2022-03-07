using UnityEngine;
using UnityEditor;
using UnityEngine.Video;
public class SongDataPlay : MonoBehaviour
{
    public static SongDataPlay instance;
    public static float judgeHit_None = 3;
    public static float judgeHit_Miss = 2;
    public static float judgeHit_Good = 1.5f;
    public static float judgeHit_Great = 1.2f;
    public static float judgeHit_Cool = 1.1f;

    public bool isPlayable { get { return songData != null && vp.clip != null; } }
    public SongData songData;
    public VideoPlayer vp;
    public float playTimeElapsed { get { return (float)vp.time; } }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }
    /*private void SelectSongJsonFile()
    {
        string dir = EditorUtility.OpenFilePanel("재생할 SongData json파일 선택", "", "json");
        string json = System.IO.File.ReadAllText(dir);
        songData = JsonUtility.FromJson<SongData>(json);
    }*/
    public void LoadSong(string videoName)
    {
        TextAsset songDataText = Resources.Load<TextAsset>($"/SongDatas/{videoName}.json");
        songData = JsonUtility.FromJson<SongData>(songDataText.ToString());
        vp.clip = Resources.Load<VideoClip>(videoName);
    }
    public void Play()
    {
        if (isPlayable)
            vp.Play();
    }
    private void SpawnNote()
    {
        float alphaTime = NoteManager.instance.noteFallingDistance / NoteManager.noteFallingSpeed;

        
    }
}