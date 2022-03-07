using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class NoteHitter : MonoBehaviour 
{
    public KeyCode keyCode;
    public Transform tr;
    public LayerMask noteLayer;


    private void Awake()
    {
        tr = transform;
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
            TryHitNote();
    }

    private HitType TryHitNote()
    {
        HitType hitType = HitType.None;
        List<Collider2D> overlaps = Physics2D.OverlapBoxAll(tr.position, 
                                                            new Vector2(tr.lossyScale.x, SongDataPlay.judgeHit_Miss),
                                                            0, 
                                                            noteLayer).ToList();
        if(overlaps.Count > 0)
        {
            overlaps.OrderByDescending(x => x.transform.position.y);
            float distance = overlaps[0].transform.position.y - tr.position.y;

            if(distance < SongDataPlay.judgeHit_Cool)
                hitType = HitType.Cool;
            else if(distance < SongDataPlay.judgeHit_Great)
                hitType = HitType.Great;
            else if(distance < SongDataPlay.judgeHit_Good)
                hitType = HitType.Good;
            else if (distance < SongDataPlay.judgeHit_Miss)
                hitType= HitType.Miss;

            Destroy(overlaps[0]);
        }
        return hitType;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(tr.position, new Vector3(tr.lossyScale.x, SongDataPlay.judgeHit_Miss, 0));
    }
}

public enum HitType
{
    None,
    Miss,
    Good,
    Great,
    Cool,
}