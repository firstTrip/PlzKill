using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update


    private float xLaw;

    [Header("가지고 있는 아이탬")]
    [SerializeField] private List<GameObject> hasItem;

    [Space]
    [Header("HP:")]
    [SerializeField] private float HP;
    [SerializeField] private float MaxHP;



    [Space]
    [Header("스탯")]
    [SerializeField] private float att;
    [SerializeField] private float speed;
    [SerializeField] private float dashPower;
    [SerializeField] private float jumpPower;
    [SerializeField] private float attSpeed;




    private float BAtt;
    private float BSpeed;
    private float BAttSpeed;
    [Space]
    [Header("보정 점프")]
    [SerializeField] private float fallMultiplierFloat;
    [SerializeField] private float lowJumpMultiplierFloat;

    [Space]

    [Header("흡혈")]
    [SerializeField] private float maxBlood;
    [SerializeField] private float blood;

    [Space]

    private int cnt = 0;

    [Space]

    [Header("현재 가지고 있는 대쉬 갯수")]
    [SerializeField] private int dashCnt;

    [Space]

    [Header("대쉬 쿨타임")]
    [SerializeField] private float dashCoolTime;
    private float leftTime = 0;

    [Space]


    private Vector2 MousePoint;
    int weafonIndex;

    [Header("판단 그룹")]
    private bool coolRunning;
    private bool isAttacking;
    private bool isDashing;
    private bool isBerserk;

    [Space]
    [Header(" 애니메이션")]
    [SerializeField] private SPUM_Prefabs anim;

    [Space]
    private Rigidbody2D rb;
    public GameObject myWeapon;
    public Image CollTimeImage;
    private Camera cam;

    [Space]

    public PlayerData playerData;
    #region enum
    public enum PlayerCurrentState
    {

        idle = 0,
        attack,
        jump,
        dash,
        move,
        hurt,
        talk
    }
    #endregion

    PlayerCurrentState playerCurrentState;

    private void Awake()
    {
        Initailized();
    }

    void Update()
    {

        if(HP < 0)
        {
            anim.PlayAnimation(2);
            return;
        }
        LookAtMouse();
        CoolTime(dashCoolTime);

        Jump();
        BetterJump();
        Dash();
        BerserkMode();

        Interation();
        Swap();

        if (isBerserk)
        {
            blood -= 5* Time.deltaTime;

            if (blood < 0.001f)
            {
                HP -= 5 * Time.deltaTime;
                blood = 0;
            }
        }


        if (isAttacking)
        {
            Attack();
        }
        StopSpeed(); //  관성 삭제

    }

    private void Initailized()
    {
        playerCurrentState = PlayerCurrentState.idle;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<SPUM_Prefabs>();
        cam = Camera.main;
        hasItem = new List<GameObject>();
        playerData = GetComponent<PlayerData>();


        MaxHP = playerData.HP;

        HP = MaxHP;


        maxBlood = playerData.MaxBlood;
        blood = 0;

        att = playerData.att;
        speed = playerData.speed;
        jumpPower = playerData.jumpPower;
        dashPower = playerData.dashPower;
        attSpeed = playerData.attSpeed;


        dashCnt = playerData.dashCnt;
        dashCoolTime = playerData.dashCoolTime;

        BAtt = att;
        BSpeed = speed;
        BAttSpeed = attSpeed;

        fallMultiplierFloat = 3f;
        lowJumpMultiplierFloat =2f;

        hasItem.Add(myWeapon); // 초기 무기 추가 1번 무기

        isAttacking = true;
    }

    private void FixedUpdate()
    {
        if (HP < 0)
            return;

        Move();
    }

    private void Move()
    {

        if (playerCurrentState == PlayerCurrentState.dash || playerCurrentState == PlayerCurrentState.hurt)
            return;

        Debug.Log("int move");
        xLaw = Input.GetAxisRaw("Horizontal");

        rb.AddForce(Vector2.right * xLaw, ForceMode2D.Impulse);
        anim.PlayAnimation(1);

        if (rb.velocity.x > speed)
        {
            playerCurrentState = PlayerCurrentState.move;
            rb.velocity = new Vector2(speed, rb.velocity.y);
            Debug.Log(playerCurrentState);
        }
        else if (rb.velocity.x < speed * (-1))
        {
            playerCurrentState = PlayerCurrentState.move;
            rb.velocity = new Vector2(speed * (-1), rb.velocity.y);
            Debug.Log(playerCurrentState);
        }
        else
        {
            anim.PlayAnimation(0);
            playerCurrentState = PlayerCurrentState.idle;
        }

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerCurrentState != PlayerCurrentState.dash)
        {
            Debug.Log("isJumping");
            RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 0.5f, LayerMask.GetMask("Ground"));

            if (rayHit.collider != null)
            {
                playerCurrentState = PlayerCurrentState.jump;
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                
            }
        }
    }

    private void BetterJump()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y *(fallMultiplierFloat -1) * Time.deltaTime;

        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplierFloat - 1) * Time.deltaTime;
        }
    }
    private void StopSpeed()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * 0.5f, rb.velocity.y);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //MousePoint = cam.WorldToScreenPoint(Input.mousePosition);
            isAttacking = false;
            anim.PlayAnimation(4);
            int SwishSound = Random.Range(1, 4);
            SoundManager.Instance.PlaySound("Swish"+ SwishSound.ToString());
            Debug.Log("Attack" + SwishSound.ToString());
            myWeapon.GetComponent<Weafon>().Attack();
            Invoke("CancleAttack", attSpeed);
        }
    }
    private void CancleAttack()
    {
        isAttacking = true;
    }

    private void Dash()
    {

        if (Input.GetMouseButtonDown(1) && dashCnt > 0)
        {

            if (playerCurrentState == PlayerCurrentState.dash)
                return;

            playerCurrentState = PlayerCurrentState.dash;
            
            leftTime = dashCoolTime;
            dashCnt--;
            isDashing = true;
            //rb.constraints = RigidbodyConstraints2D.None;
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.gravityScale = 0;

            if (isDashing)
            {
                if (MousePoint.x > this.gameObject.transform.position.x)
                {
                    rb.velocity = new Vector2(MousePoint.x - transform.position.x, MousePoint.y - transform.position.y).normalized * dashPower;

                }
                else
                {
                    rb.velocity = new Vector2(MousePoint.x - transform.position.x, MousePoint.y - transform.position.y).normalized * dashPower;

                }
            }

            Invoke("recoverIdle", 0.25f);
        }

    }

    private void recoverIdle()
    {
        //rb.velocity = new Vector2(rb.velocity.normalized.x * 0.5f, rb.velocity.y);
        rb.gravityScale = 1;
        rb.velocity = Vector2.zero;
        isDashing = false;
        playerCurrentState = PlayerCurrentState.idle;
    }

    private void LookAtMouse()
    {

        MousePoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerOnCam = this.transform.position;

        if (MousePoint.x > playerOnCam.x)
        {
            //  오른쪽 보기

            transform.localScale = new Vector3(-2f, 2f, 1);

        }
        else if (MousePoint.x < playerOnCam.x)
        {
            //왼쪽 보기
            transform.localScale = new Vector3(2f, 2f, 1);
        }
    }

    // 쿨타임 초기화 되는거 고쳐야함
    private void CoolTime(float dashCoolTime)
    {
        if (dashCnt == 3)
        {
            return;
        }
        else if (dashCnt < 3 && !coolRunning)
        {
            leftTime = dashCoolTime;
        }

        if (dashCnt < 3 && leftTime > 0)
        {

            coolRunning = true;
            leftTime -= Time.deltaTime;


            if (leftTime < 0)
            {
                leftTime = 0;
                dashCnt++;
                coolRunning = false;
            }

            float ratio = 1.0f - (leftTime / dashCoolTime);
            CollTimeImage.fillAmount = ratio;

        }

    }

    private void Swap()
    {

        if (Input.GetButtonDown("Swap1")) weafonIndex = 0;
        if (Input.GetButtonDown("Swap2")) weafonIndex = 1;
        if (Input.GetButtonDown("Swap3")) weafonIndex = 2;
        if (Input.GetButtonDown("Swap4")) weafonIndex = 3;
        if (Input.GetButtonDown("Swap5")) weafonIndex = 4;

        if (Input.GetButtonDown("Swap1") || Input.GetButtonDown("Swap2") || Input.GetButtonDown("Swap3") || Input.GetButtonDown("Swap4") || Input.GetButtonDown("Swap5"))
        {
            if (hasItem.Count < weafonIndex + 1)
                return;

            myWeapon.GetComponent<Weafon>().ChangeWeafon(hasItem[weafonIndex].gameObject);
        }
    }

    private void BerserkMode()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Into BerserkMode");
            cnt++;

            if (cnt > 1)
            {
                CancleBerserkMode(BAtt, BSpeed, BAttSpeed);
                cnt = 0;
                isBerserk = false;

                return;
            }

            att = BAtt * 2;
            speed = BSpeed * 2;
            attSpeed = BAttSpeed / 2;
            isBerserk = true;
        }
    }

    private void CancleBerserkMode(float Batt,float Bspeed,float BAttSpeed)
    {
        Debug.Log("out BerserkMode");

        att = Batt;
        speed = Bspeed;
        attSpeed = BAttSpeed;

        Debug.Log("att :  " + att + "speed :  " + speed + "attSpeed :   " + attSpeed);
    }

    private void Interation()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Interation"));

            if (ray.collider != null)
            {
                if (ray.collider.gameObject.CompareTag("Weafon"))
                {

                    if (hasItem.Count == 5)
                    {
                        Debug.Log(weafonIndex);
                        hasItem.Remove(hasItem[weafonIndex]);
                        Drop(hasItem[weafonIndex].gameObject);
                        hasItem.Insert(weafonIndex, ray.collider.gameObject);


                        myWeapon.GetComponent<Weafon>().ChangeWeafon(hasItem[weafonIndex].gameObject);
                        ray.collider.gameObject.SetActive(false);
                        hasItem[weafonIndex].gameObject.SetActive(true);
                        return;
                    }

                    hasItem.Add(ray.collider.gameObject);
                    ray.collider.gameObject.SetActive(false);

                }
            }
        }
        
    }

    private void Drop(GameObject temp)
    {
        Instantiate(temp, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Blood"))
        {
            if (maxBlood < blood)
                return;

            blood += collision.GetComponent<Blood>().setBlood();

            if (blood > maxBlood)
                blood = maxBlood;
            Debug.Log(blood);

        }
        
        if (collision.CompareTag("Monster"))
        {
            GetDamage(collision.GetComponent<Monster>().att, collision.gameObject.transform);
            Debug.Log("touch Monster");
        }
    }

    public void OnTrampoline(float JumpPower)
    {
        jumpPower = JumpPower;
    }

    public void OffTrampoline()
    {
        jumpPower = 5;
    }

    public void GetDamage(float damage,Transform target)
    {
        if (playerCurrentState == PlayerCurrentState.hurt)
            return;

        playerCurrentState = PlayerCurrentState.hurt;

        HP -= damage;

        int dir = transform.position.x - target.position.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dir, 1) *1.5f, ForceMode2D.Impulse);
        Debug.Log(HP);

        Invoke("recoverIdle", 0.2f);
    }
    public float setAtt()
    {
        return att;
    }

    public float setHp()
    {
        return HP;
    }

    public void getHp(float point)
    {
        HP += point;
    }

    public float setMaxHp()
    {
        return MaxHP;
    }

    public float setDashCnt()
    {
        return dashCnt;
    }

    public float setBlood()
    {
        return blood;
    }

    public void getDashCnt(int cnt)
    {
        if (dashCnt < 3)
        {
            dashCnt += cnt;
        }
    }
}
