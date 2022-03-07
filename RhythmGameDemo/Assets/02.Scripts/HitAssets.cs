using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class HitAssets : MonoBehaviour
{
    private static HitAssets _instance;
    public static HitAssets instance
    {
        get 
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<HitAssets>("Assets/HitAssets"));
            return _instance;
        }
    }

    public Transform GetHitPopUpByHitType(HitType hitType)
    {
        Transform tmpTransform = hitPopUp_None;
        switch (hitType)
        {
            case HitType.None:
                tmpTransform = hitPopUp_None;
                break;
            case HitType.Miss:
                tmpTransform = hitPopUp_Miss;
                break;
            case HitType.Good:
                tmpTransform = hitPopUp_Good;
                break;
            case HitType.Great:
                tmpTransform = hitPopUp_Great;
                break;
            case HitType.Cool:
                tmpTransform = hitPopUp_Cool;
                break;
        }
        return tmpTransform;
    }
    public Transform hitPopUp_None;
    public Transform hitPopUp_Miss;
    public Transform hitPopUp_Good;
    public Transform hitPopUp_Great;
    public Transform hitPopUp_Cool;

    public Transform GetHitEffectByHitType(HitType hitType)
    {
        Transform tmpTransform = hitEffect_None;
        switch (hitType)
        {
            case HitType.None:
                tmpTransform = hitEffect_None;
                break;
            case HitType.Miss:
                tmpTransform = hitEffect_Miss;
                break;
            case HitType.Good:
                tmpTransform = hitEffect_Good;
                break;
            case HitType.Great:
                tmpTransform = hitEffect_Great;
                break;
            case HitType.Cool:
                tmpTransform = hitEffect_Cool;
                break;
        }
        return tmpTransform;
    }
    public Transform hitEffect_None;
    public Transform hitEffect_Miss;
    public Transform hitEffect_Good;
    public Transform hitEffect_Great;
    public Transform hitEffect_Cool;
}
