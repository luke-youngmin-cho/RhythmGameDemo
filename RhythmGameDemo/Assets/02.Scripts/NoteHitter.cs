using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class NoteHitter : MonoBehaviour 
{
    public KeyCode keyCode;
    public Transform tr;
    public LayerMask noteLayer;
    public SpriteRenderer icon;
    public Color pressedColor;

    private void Awake()
    {
        tr = transform;
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
            TryHitNote();
        if (Input.GetKey(keyCode))
            ChangeColor();
        else
            ClearColor();
    }
    private void ChangeColor()
    {
        icon.color = pressedColor;
    }
    private void ClearColor()
    {
        icon.color = Color.white;
    }
    private HitType TryHitNote()
    {
        HitType hitType = HitType.None;
        List<Collider2D> overlaps = Physics2D.OverlapBoxAll(tr.position, 
                                                            new Vector2(tr.lossyScale.x/2, transform.lossyScale.y * NoteManager.judgeHit_Miss),
                                                            0,noteLayer).ToList();
        if(overlaps.Count > 0)
        {
            overlaps.OrderByDescending(x => x.transform.position.y);
            if(overlaps.Count > 1)
            {
                foreach (var item in overlaps)
                {
                    Debug.Log(item.transform.position.y);
                }
            }

            float distance = overlaps[0].transform.position.y - tr.position.y;

            if(distance < NoteManager.judgeHit_Cool)
                hitType = HitType.Cool;
            else if(distance < NoteManager.judgeHit_Great)
                hitType = HitType.Great;
            else if(distance < NoteManager.judgeHit_Good)
                hitType = HitType.Good;
            else if (distance < NoteManager.judgeHit_Miss)
                hitType= HitType.Miss;


            overlaps[0].gameObject.GetComponent<Note>().Hit(hitType);
            Destroy(overlaps[0].gameObject);
        }
        return hitType;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x/2, transform.lossyScale.y * NoteManager.judgeHit_None, -10));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x/2, transform.lossyScale.y * NoteManager.judgeHit_Miss, -10));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x/2, transform.lossyScale.y * NoteManager.judgeHit_Good, -10));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x/2, transform.lossyScale.y * NoteManager.judgeHit_Great, -10));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x/2, transform.lossyScale.y * NoteManager.judgeHit_Cool, -10));
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