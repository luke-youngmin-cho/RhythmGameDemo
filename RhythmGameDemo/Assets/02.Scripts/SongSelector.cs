using UnityEngine;
using UnityEditor;
using UnityEngine.Video;
public class SongSelector : MonoBehaviour
{
    public static SongSelector instance;
    public bool isPlayable { get { return songData != null && clip != null; } }
    [HideInInspector] public SongData songData;
    [HideInInspector] public VideoClip clip;
    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
        DontDestroyOnLoad(instance);
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
        TextAsset songDataText = Resources.Load<TextAsset>($"SongDatas/{videoName}");
        songData = JsonUtility.FromJson<SongData>(songDataText.ToString());
        clip = Resources.Load<VideoClip>($"Videos/{videoName}");
    }
}