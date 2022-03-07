using UnityEngine;
public class NoteSpawner : MonoBehaviour
{
    public KeyCode keyCode;
    public GameObject notePrefab;

    public void SpawnNote()
    {
        Instantiate(notePrefab, transform.position, Quaternion.identity);
    }

}