using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{

    [SerializeField] private GameObject Icon;

    private bool isActive;
    private bool isFlag;

    public string dialogue;
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        isFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        ShowIcon(isActive);
    }

    private void ShowIcon(bool isActive)
    {
        Icon.SetActive(isActive);
    }
    private void select()
    {
        isActive = false;
        UIManager.Instance.SetNpcNotice(true);
        UIManager.Instance.SetNpcText(dialogue, 1f);
        Debug.Log("into select");
    }

    public void YesSelect()
    {
        Debug.Log("into YesSelect");
    }

    public void NoSelect()
    {
        Debug.Log("into NoSelect");

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && isFlag)
        {
            isActive = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                isFlag = false;
                select();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActive = false;
            isFlag = true;
            UIManager.Instance.SetNpcNotice(false);
        }
    }
}
