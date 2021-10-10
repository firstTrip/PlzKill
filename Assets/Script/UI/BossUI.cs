using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUI : MonoBehaviour
{
    private GameObject Boss;

    [SerializeField] private Image bossImg;
    [SerializeField] private Sprite[] bossIMG;

    [SerializeField] private Image Hpbar;
    [SerializeField] private TextMeshProUGUI HpText;

    [SerializeField] private GameObject bossUi;

    float MHp;
    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.FindGameObjectWithTag("Boss");
        SetBossImg();

        bossUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameMode ==GameManager.GameMode.boss)
        {
            bossUi.SetActive(true);
            HpFillAmount();

        }

    }

    private void HpFillAmount()
    {
        Hpbar.fillAmount = (Boss.GetComponent<Boss>().setHp() / Boss.GetComponent<Boss>().setMaxHp());

        string v1 = Boss.GetComponent<Boss>().setHp().ToString();

        HpText.text = v1 + "/" + Boss.GetComponent<Boss>().setMaxHp().ToString();
    }

    void SetBossImg()
    {


        switch (Boss.name)
        {
            case "1_Boss" :
                Debug.Log(Boss.name);

                bossImg.sprite = bossIMG[0];
                break;

            case "2_Boss":
                Debug.Log(Boss.name);

                bossImg.sprite = bossIMG[1];
                break;

            case "3_Boss":
                Debug.Log(Boss.name);

                bossImg.sprite = bossIMG[2];
                break;

            case "4_Boss":
                Debug.Log(Boss.name);

                bossImg.sprite = bossIMG[3];
                break;
        }
    }
}
