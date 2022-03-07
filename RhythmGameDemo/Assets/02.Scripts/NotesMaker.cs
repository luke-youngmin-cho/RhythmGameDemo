using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Video;

public class NotesMaker : MonoBehaviour
{
    SongData songData;
    KeyCode[] keyCodes = { KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.Space, KeyCode.J, KeyCode.K, KeyCode.L };
    public VideoPlayer vp;
    public bool onRecord
    {
        set
        {
            if (value)
                StartRecording();
            else
                StopRecording();
        }
        get { return vp.isPlaying; }
    }
    private void Update()
    {
        if (onRecord)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKeyDown(keyCode))
                    CreateNoteData(keyCode);
            }
            if (Input.GetKeyDown(KeyCode.Insert))
                SaveSongData();
        }
    }
    public void StartRecording()
    {
        songData = new SongData();
        vp.Play();
    }
    public void StopRecording()
    {
        songData = null;
        vp.Stop();
    }
    private void CreateNoteData(KeyCode keyCode)
    {
        Debug.Log($"Create note : {keyCode}");
        NoteData noteData = new NoteData();
        float roundedTime = (float)Math.Round(vp.time, 2);
        noteData.time = roundedTime;
        noteData.keyCode = keyCode;
        songData.notes.Add(noteData);
    }
    private void SaveSongData()
    {
        // panel 만 띄우고 선택시 디렉토리 문자열 반환
        string dir = EditorUtility.SaveFilePanel("저장할 곳을 지정하세요", "", $"{songData.videoName}", "json");
        // 실제 song data를 json 포멧으로 저장
        System.IO.File.WriteAllText(dir, JsonUtility.ToJson(songData));
        //Serialize: 클래스(오브젝트)들을 텍스트로 변환시켜서 저장하는 행위.  Json, XML 등의 포맷이있다.. 
        //Deserialize: 텍스트를 클래스로 변환해주는 작업. 
    }
}
