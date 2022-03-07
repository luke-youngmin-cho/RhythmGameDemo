using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class NoteManager: MonoBehaviour
{
    static public NoteManager instance;

    static public float noteFallingSpeed = 1f;
    public float noteFallingDistance
    {
        get
        {
            return noteSpawnersTransform.position.y - noteHittersTransform.position.y;
        }
    }
    public Transform noteSpawnersTransform;
    public Transform noteHittersTransform;
    public Dictionary<KeyCode,NoteSpawner> spawners = new Dictionary<KeyCode, NoteSpawner>();
    public Queue<NoteData> queue = new Queue<NoteData>();
    private void Awake()
    {
        noteSpawnersTransform = transform.Find("NoteSpawners");
        noteHittersTransform = transform.Find("NoteHitters");
        NoteSpawner[] tmpSpawners = noteSpawnersTransform.GetComponentsInChildren<NoteSpawner>();
        foreach (NoteSpawner spawner in tmpSpawners)
        {
            spawners.Add(spawner.keyCode, spawner);
        }
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
    IEnumerator E_SpawnNotes()
    {
        while (queue.Count > 0)
        {
            bool doNext = true;
            while (doNext)
            {
                if (queue.Peek().time > SongDataPlay.instance.playTimeElapsed)
                {
                    NoteData data = queue.Dequeue();
                    spawners[data.keyCode].SpawnNote();
                    if (queue.Peek() != null &&
                        queue.Peek().time < SongDataPlay.instance.playTimeElapsed)
                       doNext = false;
                }
            }
            yield return null;
        }
    }
}