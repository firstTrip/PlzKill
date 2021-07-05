using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAction : MonoBehaviour
{

    public float Duration;
    public string Content;

    private void Action()
    {
        UIManager.Instance.SetNotice(true);
        UIManager.Instance.SetText(Content, Duration);
    }
}
