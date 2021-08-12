using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    [Header("�ȳ���")]
    [SerializeField] private GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextLevel()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("nextLevel");

            StageManager.Instance.CallStage();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NextLevel();
        }   
    }
}