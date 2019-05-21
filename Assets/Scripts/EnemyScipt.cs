using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyScipt : MonoBehaviour
{
    public GameObject missionControl;
    private Animator myAnimator;
    public int speed;
    public float damage;
    public float maxHealth;
    private float curHealth;
    [SerializeField]
    private float killreward;
    private bool grounded = false;
    private GameObject playerFinder;
    private Vector3 initialPosition;
    public GameObject objectInfo;
    public GameObject hpbar;
    public TextMeshProUGUI hptext;
    public float maxDist;
    public float minDist;
    private float originalPos;
    private bool playerRight;
    private int direction;
    bool waitHit;
    private float dist;
    public float maxPlayerDist;
    public float attackRange;
    public ENSwordHitScript ENSwordHitbox;
    // Start is called before the first frame update
    void Start()
    {
        waitHit = true;
        playerFinder = GameObject.Find("MainPlayer");
        myAnimator = GetComponent<Animator>();
        initialPosition = transform.position;
        //direction -1 is facing left, 1 is right
        direction = 1;
        originalPos = transform.position.x;
        curHealth = maxHealth;
        objectInfo.SetActive(true);
        hpbar.gameObject.GetComponent<Stat>().Initialized(curHealth, maxHealth);
        hptext.text = (double)(((double)curHealth / (double)maxHealth) * 100) + "%";
        objectInfo.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        dist = Mathf.Abs(this.transform.position.x - playerFinder.GetComponent<BoxCollider2D>().bounds.center.x);

        if (curHealth == 0 || curHealth < 0)
        {
            missionControl.GetComponent<MissionLoader>().addKill();
            if (dist < 20)
            {
                playerFinder.GetComponent<ParameterPlayer>().AddExp(killreward);
            }
            objectInfo.SetActive(false);
            Destroy(this.gameObject);
        }
        if (dist < maxPlayerDist)
        {

            PlayerInRange();
        }
        else if (grounded)
        {
            objectInfo.SetActive(false);
            Move();
        }
    }
    private void Move()
    {
        setRun();
        //enemy auto moves left/right within max distance and min distance
        switch (direction)
        {
            case -1:
                // Moving Left    
                if ((transform.position.x - originalPos) > minDist)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    //flip
                    direction = 1;
                    transform.localScale = new Vector2(1, transform.localScale.y);
                }
                break;
            case 1:
                //Moving Right
                if ((transform.position.x - originalPos) < maxDist)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    //flip
                    transform.localScale = new Vector2(-1, transform.localScale.y);
                    direction = -1;
                }
                break;
        }
    }
    private void startAttack()
    {

        ActivateLayer("EnemyAttack");
        myAnimator.SetBool("sword", true);
        myAnimator.SetFloat("attack", 1);
    }
    private IEnumerator Attack()
    {
        myAnimator.SetBool("sword", true);
        myAnimator.SetFloat("attack", 1);
        ENSwordHitbox.isHitting = true;
        yield return new WaitForSeconds(0.5f);
        stopAttack();
    }
    private void stopAttack()
    {
        myAnimator.SetBool("sword", false);
        ENSwordHitbox.isHitting = false;
        myAnimator.SetFloat("attack", 0);
        ENSwordHitbox.ENswordbox.enabled = false;
        waitHit = true;
        if (dist <= attackRange)
        {
            Attack();
        }
        ActivateLayer("EnemyMove");
    }
    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }
    private bool detectSide(float x)
    {
        if ((x - transform.position.x) < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void PlayerInRange()
    {
        if (dist <= attackRange && waitHit == true)
        {
            setIdle();
            waitHit = false;
            objectInfo.SetActive(true);
            startAttack();
            ENSwordHitbox.isHitting = true;
            ENSwordHitbox.ENswordbox.enabled = true;
            StartCoroutine(Attack());
        }
        else
        {

            if (detectSide(playerFinder.transform.position.x) == true && waitHit == true)
            {
                setRun();
                transform.localScale = new Vector2(1, transform.localScale.y);
                direction = 1;
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                if (waitHit == true)
                {
                    setRun();
                    transform.localScale = new Vector2(-1, transform.localScale.y);
                    direction = -1;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }
    public void ReceivesDamage(float damage)
    {
        objectInfo.SetActive(true);
        curHealth -= damage;
        hpbar.GetComponent<Stat>().Initialized(curHealth, maxHealth);
        hptext.text = Mathf.Floor((curHealth / maxHealth) * 100) + "%";
    }
    private void setIdle()
    {
        myAnimator.SetFloat("run", 0);
    }
    private void setRun()
    {
        myAnimator.SetFloat("run", 1);
    }
}
