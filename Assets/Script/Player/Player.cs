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



    [Space]

    private float srSize;

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

    [Header("무적 시간")]
    [SerializeField] private float hurtTime;
    [Space]


    private Vector2 MousePoint;
    int weafonIndex;

    [Header("판단 그룹")]
    private bool coolRunning;
    private bool isAttacking;
    private bool isDashing;
    private bool isBerserk;
    private bool isOverWall;

    [Space]
    [Header(" 애니메이션")]
    //[SerializeField] private SPUM_Prefabs anim;
    [SerializeField] private Animator anim;

    [Space]
    private Rigidbody2D rb;
    public GameObject myWeapon;
    public GameObject dashPaticle;
    public Image CollTimeImage;
    private Camera cam;
    private SpriteRenderer sr;

    [Space]

    public LayerMask GroundLayer;
    public LayerMask MonsterLayer;

    public LayerMask PlayerLayer;

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
            //anim.PlayAnimation(2);
            anim.SetTrigger("death");

            return;
        }
        LookAtMouse();
        CoolTime(dashCoolTime);

        UnderJump();
        Jump();
        BetterJump();

        Dash();

        if (playerCurrentState == PlayerCurrentState.dash)
            dashPaticle.SetActive(true);
        else
            dashPaticle.SetActive(false);


        BerserkMode();

        //Interation();
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
        //anim = GetComponent<SPUM_Prefabs>();
        anim = GetComponent<Animator>();

        cam = Camera.main;
        hasItem = new List<GameObject>();
        playerData = GetComponent<PlayerData>();
        sr = GetComponent<SpriteRenderer>();

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

        srSize = sr.transform.localScale.x;

        //hasItem.Add(myWeapon); // 초기 무기 추가 1번 무기
        dashPaticle.SetActive(false);

        PlayerLayer = LayerMask.NameToLayer("Player");
        GroundLayer = LayerMask.NameToLayer("Ground");
        MonsterLayer = LayerMask.NameToLayer("Monster");
        isAttacking = true;
        isOverWall = true;
    }

    public void LoadStat()
    {
        MaxHP = playerData.HP;

        HP = MaxHP;


        maxBlood = playerData.MaxBlood;
        blood = playerData.Blood;

        att = playerData.att;
        speed = playerData.speed;
        jumpPower = playerData.jumpPower;
        dashPower = playerData.dashPower;
        attSpeed = playerData.attSpeed;

        transform.position= playerData.PlayerPos;

        Debug.Log(att + " playerData.att");
    }

    public void SaveStat()
    {
        playerData.HP = HP;

        playerData.MaxBlood = maxBlood ;
        playerData.Blood = blood;

        playerData.att = att;
        playerData.speed =speed;

        playerData.jumpPower = jumpPower;
        playerData.dashPower = dashPower;
        playerData.attSpeed = attSpeed;

        playerData.PlayerPos = transform.position;

        Debug.Log(playerData.att + " playerData.att");
    }

    private void FixedUpdate()
    {
        if (HP < 0)
            return;



        if (isOverWall) 
        {
            if (rb.velocity.y > 0)
                Physics2D.IgnoreLayerCollision(PlayerLayer, GroundLayer, true);
            else
                Physics2D.IgnoreLayerCollision(PlayerLayer, GroundLayer, false);
        }

        Move();
    }

    private void Move()
    {

        if (playerCurrentState == PlayerCurrentState.dash || playerCurrentState == PlayerCurrentState.hurt)
            return;

        xLaw = Input.GetAxisRaw("Horizontal");

        rb.AddForce(Vector2.right * xLaw, ForceMode2D.Impulse);
        //anim.PlayAnimation(1);
        anim.SetTrigger("walk");

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
            //anim.PlayAnimation(0);
            playerCurrentState = PlayerCurrentState.idle;
        }

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerCurrentState != PlayerCurrentState.dash)
        {

            if (!isOverWall)
                return;

            Debug.Log("isJumping");
            RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 1f, LayerMask.GetMask("Ground"));

            if (rayHit.collider != null)
            {
                anim.SetTrigger("jump");
                playerCurrentState = PlayerCurrentState.jump;
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            }

        }
    }

    private void UnderJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&Input.GetKey(KeyCode.S))
        {
            Debug.Log("under Jump");
            isOverWall = false;
            Physics2D.IgnoreLayerCollision(PlayerLayer, GroundLayer, true);
            anim.SetTrigger("jump");

            Invoke("recoverOverWall", 0.4f);
        }
    }

    private void recoverOverWall()
    {
        isOverWall = true;

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
            //anim.PlayAnimation(4);
            anim.SetTrigger("attack");
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

            anim.SetTrigger("dash");
            Physics2D.IgnoreLayerCollision(PlayerLayer, GroundLayer, true);
            playerCurrentState = PlayerCurrentState.dash;


            leftTime = dashCoolTime;
            dashCnt--;
            isDashing = true;
            isOverWall = false;
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
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1;
        isDashing = false;
        playerCurrentState = PlayerCurrentState.idle;
        isOverWall = true;

        //Physics2D.IgnoreLayerCollision(PlayerLayer, GroundLayer, false);
        //rb.velocity = Vector2.zero;

    }

    private void LookAtMouse()
    {

        MousePoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerOnCam = this.transform.position;

        if (MousePoint.x > playerOnCam.x)
        {
            //  오른쪽 보기

            transform.localScale = new Vector3(-srSize, srSize, 1);
            dashPaticle.transform.localScale = new Vector3(srSize, srSize, 1);
        }
        else if (MousePoint.x < playerOnCam.x)
        {
            //왼쪽 보기
            transform.localScale = new Vector3(srSize, srSize, 1);
            dashPaticle.transform.localScale = new Vector3(-srSize, srSize, 1);
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
            //CollTimeImage.fillAmount = ratio;

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

           // myWeapon.GetComponent<Weafon>().ChangeWeafon(hasItem[weafonIndex].gameObject);
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


                        //myWeapon.GetComponent<Weafon>().ChangeWeafon(hasItem[weafonIndex].gameObject);
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
        if (collision.CompareTag("Monster"))
        {
            GetDamage(collision.GetComponent<Monster>().att, collision.gameObject.transform);
            Debug.Log("touch Monster");
        }else

        if (collision.CompareTag("Boss"))
        {
            Debug.Log(collision.name);
            GetDamage(collision.GetComponent<Boss>().bAtt, collision.gameObject.transform);
        }

        if(collision.CompareTag("Bullet"))
        {
            Debug.Log(collision.name);
            GetDamage(collision.GetComponent<Bullet>().damage, collision.gameObject.transform);
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
        StartCoroutine(changeColor());
        StartCoroutine(TouchMonster());

        Invoke("recoverIdle", 0.2f);
    }

    IEnumerator TouchMonster()
    {

        Physics2D.IgnoreLayerCollision(PlayerLayer, MonsterLayer, true);
        yield return new WaitForSeconds(hurtTime);
        Physics2D.IgnoreLayerCollision(PlayerLayer, MonsterLayer, false);

    }

    IEnumerator changeColor()
    {
        int countime = 0;

        while(countime<8)
        {
            if (countime % 2 == 0)
                sr.color = new Color(255, 255, 255, 0);
            else
                sr.color = new Color(255, 255, 255, 100);

            yield return new WaitForSeconds(0.2f);

            countime++;
        }

        sr.color = new Color(255, 255, 255, 100);

        yield return null;
        
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

    public void getBlood(float Blood)
    {

        if (maxBlood < blood)
            return;

        blood += Blood;

        if (blood > maxBlood)
            blood = maxBlood;

        Debug.Log(Blood + " get Blood");

    }

    public void GetReward(Reward reward)
    {
        switch (reward.ReName) 
        {
            case "공격력 증가":
                Debug.Log("공격력 증가");
                break;

            case "공격 속도 증가":
                Debug.Log("공격 속도 증가");

                break;

            case "체력 증가":
                Debug.Log("체력 증가");

                break;

            case "방어력 증가":
                Debug.Log("방어력 증가");

                break;

            case "흡혈 효율 증가":
                Debug.Log("흡혈 효율 증가");

                break;

            case "이동 속도 증가":
                Debug.Log("이동 속도 증가");

                break;

            case "점프력 증가":
                Debug.Log("점프력 증");

                break;

            case "대시 쿨타임 감소":
                Debug.Log("대시 쿨타임 감소");

                break;

            case "기본 스킬 쿨타임 감소":
                Debug.Log("기본 스킬 쿨타임 감소");

                break;

            case "스킬 추가":
                Debug.Log("스킬 추가");

                break;
        

        }

    }
    public void getDashCnt(int cnt)
    {
        if (dashCnt < 3)
        {
            dashCnt += cnt;
        }
    }
}
