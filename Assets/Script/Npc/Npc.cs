using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Npc : MonoBehaviour
{

    [SerializeField] private GameObject Icon;

    private bool isActive;
    [SerializeField] private SPUM_Prefabs anim;


    ObjData objData;
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
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
 
    public void YesSelect()
    {
        UIManager.Instance.IntializeText();
        UIManager.Instance.SetNotice(false, objData.IsNpc);

        this.objData.IsNpc = false;
        QuestManager.Instance.UpQuestInDex();
        GameManager.Instance.TalkAction(this.gameObject);
        GameManager.Instance.MakeReward();
        UIManager.Instance.ActiveReward();

        StartCoroutine(DelayDeath());
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(2f);

        anim.PlayAnimation(2);

        yield return new WaitForSeconds(2f);

        DestroyNpc();



    }

    public void NoSelect()
    {

        UIManager.Instance.IntializeText();
        UIManager.Instance.SetNotice(false, objData.IsNpc);

        this.objData.IsNpc = false;
        QuestManager.Instance.DownQuestInDex();
        GameManager.Instance.TalkAction(this.gameObject);
        GameManager.Instance.UpNpcCnt();

        Invoke("DestroyNpc", 2f);

       

       
    }

    void DestroyNpc()
    {
        UIManager.Instance.SetNotice(false, objData.IsNpc);
        Destroy(gameObject);
    }
}
