using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{


    #region SingleTon
    /* SingleTon */
    private static ShakeCamera instance;
    public static ShakeCamera Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(ShakeCamera)) as ShakeCamera;
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "ShakeCamera";
                    instance = container.AddComponent(typeof(ShakeCamera)) as ShakeCamera;
                }
            }

            return instance;
        }
    }

    #endregion

    [Header(" 흔들림 시간과 세기")]
    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeIntensity;



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
        if (Input.GetKeyDown(KeyCode.Z))
            OnShakeCamera();
    }

    public void OnShakeCamera(float shakeTime = 0.1f,float shakeIntensity =0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByPos");
        StartCoroutine("ShakeByPos");
    }

    private IEnumerator ShakeByPos()
    {
        Vector3 startPos = transform.position;

        while(shakeTime >0.0f)
        {
            transform.position = startPos + Random.insideUnitSphere * shakeIntensity;

            shakeTime -= Time.deltaTime;
            Debug.Log("sibal");
            yield return null;

        }

        transform.position = startPos;
    }

    private IEnumerator ShakeByRot()
    {
        Vector3 startRot = transform.eulerAngles;

        float power = 10f;

        while (shakeTime > 0.0f)
        {

            float x = 0;
            float y = 0;
            float z = Random.Range(-1f,1f);

            transform.rotation = Quaternion.Euler(startRot + new Vector3(x, y, z) * shakeIntensity * power);

            shakeTime -= Time.deltaTime;
            Debug.Log("sibal");
            yield return null;

        }

        transform.rotation = Quaternion.Euler(startRot);
    }
}
