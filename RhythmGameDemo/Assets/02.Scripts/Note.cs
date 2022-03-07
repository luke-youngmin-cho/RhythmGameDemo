using UnityEngine;
public class Note :MonoBehaviour
{
    public KeyCode keyCode;
    Transform tr;
    private void Awake()
    {
        tr = transform;
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        tr.Translate(Vector2.down * NoteManager.noteFallingSpeed * Time.fixedDeltaTime);
    }
    public void Hit(HitType type)
    {
        switch (type)
        {
            case HitType.None:
                break;
            case HitType.Miss:
                break;
            case HitType.Good:
                ScoreUI.instance.score += 50;
                break;
            case HitType.Great:
                ScoreUI.instance.score += 80;
                break;
            case HitType.Cool:
                ScoreUI.instance.score += 100;
                break;
            default:
                break;
        }
    }
}
