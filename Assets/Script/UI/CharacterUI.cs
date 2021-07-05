using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{

    [SerializeField] private Image Hpbar;
    [SerializeField] private Text BloodText;
    [SerializeField] private Text CoinText;
    [SerializeField] private Image[] DashSlot;
    private float startDashCnt;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        startDashCnt = player.setDashCnt();
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

    }

    private void DashCnt()
    {

        if(startDashCnt != player.setDashCnt())
        {
            Debug.Log(player.setDashCnt());

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
