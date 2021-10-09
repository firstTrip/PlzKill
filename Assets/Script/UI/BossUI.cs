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

    float MHp;
    // Start is called before the first frame update
    void Awake()
    {
        Boss = GameObject.FindGameObjectWithTag("Boss");
        SetBossImg();
    }

    // Update is called once per frame
    void Update()
    {
        HpFillAmount();

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
