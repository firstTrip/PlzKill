using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatReward : MonoBehaviour
{
    int num;
    string rName;

    public TextMeshProUGUI Nametext;
    public TextMeshProUGUI Numtext;

    Reward reward;

    private void Start()
    {
        Intialized();
    }

    public void Intialized()
    {
        reward = GameManager.Instance.SetReward();
        rName = reward.ReName;
        num = reward.gain;

        Nametext.text = rName;
        Numtext.text = num.ToString() + "%";
    }

    public void SendRewardData()
    {
        UIManager.Instance.DisActiveReward();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetReward(reward);
    }
}
