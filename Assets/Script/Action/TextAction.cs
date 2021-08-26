using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAction : MonoBehaviour
{

    public float Duration;
    public string Content;
    ObjData objData;

    private void Awake()
    {
        objData = GetComponent<ObjData>();
    }

    private void Action()
    {
        UIManager.Instance.SetNotice(true, objData.IsNpc);
        //UIManager.Instance.SetText(Content, Duration);
    }
}
