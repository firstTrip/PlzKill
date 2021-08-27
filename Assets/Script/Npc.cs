using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{

    [SerializeField] private GameObject Icon;

    private bool isActive;
    private bool isFlag;

    public string dialogue;

    ObjData objData;
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        isFlag = true;
        objData = GetComponent<ObjData>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowIcon(isActive);
        CheckPlayer();

    }

    private void CheckPlayer()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position + new Vector3(-3,1,0), Vector2.right, 6f,LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position + new Vector3(-3, 1, 0), Vector2.right *6f, Color.red);

        if (ray)
        {
            isActive = true;
            Debug.Log(ray.collider.name);

            if(Input.GetKeyDown(KeyCode.E))
                GameManager.Instance.TalkAction(this.gameObject);

        }
        else
        {
            isActive = false;
            UIManager.Instance.SetNotice(false, objData.IsNpc);

        }

    }

    private void ShowIcon(bool isActive)
    {
        Icon.SetActive(isActive);
    }
    private void select()
    {

        Debug.Log(objData.ID);
        isActive = false;
        UIManager.Instance.SetNotice(true,objData.IsNpc);
        UIManager.Instance.SetText(objData.ID,objData.IsNpc, 1f);

    }

    public void YesSelect()
    {
        Debug.Log("into YesSelect");
    }

    public void NoSelect()
    {
        Debug.Log("into NoSelect");
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && isFlag)
        {
            isActive = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("into interactoin");
                //isFlag = false;
                GameManager.Instance.TalkAction(this.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActive = false;
            isFlag = true;
            UIManager.Instance.SetNotice(false, objData.IsNpc);

        }
    }
    */
}
