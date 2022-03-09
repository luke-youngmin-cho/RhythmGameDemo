using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class NoteManager: MonoBehaviour
{
    static public NoteManager instance;
    public static float judgeHit_None = 3;
    public static float judgeHit_Miss = 2;
    public static float judgeHit_Good = 1.5f;
    public static float judgeHit_Great = 1.2f;
    public static float judgeHit_Cool = 1.1f;
    static public float noteFallingSpeed = 1f;
    public float noteFallingDistance
    {
        get
        {
            return noteSpawnersTransform.position.y - noteHittersTransform.position.y;
        }
    }
    public float noteFallingTime
    {
        get { return noteFallingDistance / noteFallingSpeed; }
    }
    public Transform noteSpawnersTransform;
    public Transform noteHittersTransform;
    public Dictionary<KeyCode,NoteSpawner> spawners = new Dictionary<KeyCode, NoteSpawner>();
    public Queue<NoteData> queue = new Queue<NoteData>();

    private void Awake()
    {
        if (instance == null) instance = this;
        noteSpawnersTransform = transform.Find("NoteSpawners");
        noteHittersTransform = transform.Find("NoteHitters");
        NoteSpawner[] tmpSpawners = noteSpawnersTransform.GetComponentsInChildren<NoteSpawner>();
        foreach (NoteSpawner spawner in tmpSpawners)
        {
            spawners.Add(spawner.keyCode, spawner);
        }
        SetDataQueue(SongSelector.instance.songData.notes);
    }
    private void Start()
    {
    }

    public void SetDataQueue(List<NoteData> notes)
    {
        notes.Sort((x,y) => x.time.CompareTo(y.time));
        foreach (NoteData note in notes)
            queue.Enqueue(note);
    }
    public void StartSpawn()
    {
        if (queue.Count == 0) return;

        StartCoroutine(E_SpawnNotes());
    }
    public void StopSpawn()
    {
        StopCoroutine(E_SpawnNotes());
    }
    IEnumerator E_SpawnNotes()
    {   
        while (queue.Count > 0)
        {
            for (int i = 0; i < queue.Count; i++)
            {
                if (queue.Peek().time < GamePlay.instance.playTime)
                {
                    NoteData data = queue.Dequeue();
                    spawners[data.keyCode].SpawnNote();
                }
            }
            yield return null;
        }
    }
}