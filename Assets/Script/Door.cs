using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    [Header("¾È³»¹®")]
    [SerializeField] private GameObject text;
    private bool isActive;

    [SerializeField] private GameObject Icon;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        Icon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<SpriteRenderer>().sprite.name == "1st_props_7")
            return;

        ShowIcon(isActive);
        CheckPlayer();

    }

    private void NextLevel()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("nextLevel");

            StageManager.Instance.CallStage();
        }
    }
    private void ShowIcon(bool isActive)
    {
        Icon.SetActive(isActive);
    }

    private void CheckPlayer()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position + new Vector3(-3, -1, 0), Vector2.right, 6f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position + new Vector3(-3, -1, 0), Vector2.right * 6f, Color.red);

        if (ray)
        {
            isActive = true;

            if (Input.GetKeyDown(KeyCode.E))
                NextLevel();
        }
        else
        {
            isActive = false;

        }

    }
   
}
