using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reward 
{
    public string ReName;
    public int gain; // È¹µæ ¼öÄ¡ 
    public int weight;

    public Reward(Reward reward)
    {
        this.ReName = reward.ReName;
        this.gain = reward.gain;
        this.weight = reward.weight;
    }

}
