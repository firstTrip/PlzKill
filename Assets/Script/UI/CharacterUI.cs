using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterUI : MonoBehaviour
{

    #region SingleTon
    /* SingleTon */
    private static CharacterUI instance;
    public static CharacterUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(CharacterUI)) as CharacterUI;
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "CharacterUI";
                    instance = container.AddComponent(typeof(CharacterUI)) as CharacterUI;
                }
            }

            return instance;
        }
    }

    #endregion

    [SerializeField] private Image Hpbar;
    [SerializeField] private TextMeshProUGUI BloodText;
    [SerializeField] private TextMeshProUGUI CoinText;
    [SerializeField] private Image[] DashSlot;
    private float startDashCnt;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        startDashCnt = player.setDashCnt();
    }

    private void Awake()
    {
        #region SingleTon
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        HpFillAmount();
        DashCnt();
        BloodFillAmount();
    }

    private void HpFillAmount()
    {
        Hpbar.fillAmount = (player.setHp()) / (player.setMaxHp());
    }

    private void BloodFillAmount()
    {
        BloodText.text = player.setBlood().ToString();
    }

    private void DashCnt()
    {

        if(startDashCnt != player.setDashCnt())
        {
            for (int n = 0; n < startDashCnt; n++)
            {
                DashSlot[n].gameObject.SetActive(false);
            }

            for (int n = 0; n < player.setDashCnt(); n++)
            {
                DashSlot[n].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int n = 0; n < player.setDashCnt(); n++)
            {
                DashSlot[n].gameObject.SetActive(true);
            }
        }

       
    }

    IEnumerator HPUI()
    {
        yield return null;
    }
}
