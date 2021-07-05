using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseAction : MonoBehaviour
{

    [Header("����� �� :")]
    [SerializeField] private int NumOfMonster;
    [Space]

    [Header("����� ���� :")]
    [SerializeField] private GameObject[] TypeOfMonsters;

    private int cnt;

   private void Action()
    {
        do
        {
            if (TypeOfMonsters == null)
            {
                Debug.Log("into finish");
                return;
            }
            int n = Random.Range(0, TypeOfMonsters.Length);
            Instantiate(TypeOfMonsters[n], transform.position, Quaternion.identity);
            cnt++;

        }while (cnt < NumOfMonster) ;
       
    }
}
