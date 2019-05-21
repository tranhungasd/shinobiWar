using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerUseSkill : MonoBehaviour
{
    public SwordHitScript swordHitbox;
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private SpriteRenderer sp;
    public TextMeshProUGUI[] keys = new TextMeshProUGUI[5];

    private KeyCode[] keycodes = new KeyCode[5];
    public bool isAtk = false;
    private bool isDfn = false;

    GameObject objPlayer;
    private ParameterPlayer paraPlayer;
    [SerializeField]
    private GameObject[] prefabsSpell;
    public AudioSource buttonSpaceAudio;
    public AudioSource buttonQAudio;
    public AudioSource buttonWAudio;
    public AudioSource buttonEAudio;
    public AudioSource buttonRAudio;
    public AudioSource buttonTAudio;
    [SerializeField]
    private Transform exitPoint;
    [SerializeField]
    private Transform ultiPoint;
    [SerializeField]
    private Transform shadowPoint;
    [SerializeField]
    private Transform shieldPoint;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        objPlayer = GameObject.Find("MainPlayer");
        paraPlayer = objPlayer.GetComponent<ParameterPlayer>();




    }

    // Update is called once per frame
    void Update()
    {
        ReadOptions();
        objPlayer = GameObject.Find("MainPlayer");
        swordHitbox.GetComponent<SwordHitScript>().UpdateDamage(paraPlayer.getDamage("DMG0"));
        prefabsSpell[0].GetComponent<ShurikenScript>().UpdateDamage(paraPlayer.getDamage("DMG1"));
        prefabsSpell[2].GetComponent<FireSkillScipt>().UpdateDamage(paraPlayer.getDamage("DMG3"));
        prefabsSpell[4].GetComponent<RasenganScript>().UpdateDamage(paraPlayer.getDamage("DMG5"));
        if (!isAtk)
        {
            GetInput();
        }
    }
    private void ReadOptions()
    {
        for (int i = 0; i < 5; i++)
        {
            keycodes[i] = (KeyCode)Enum.Parse(typeof(KeyCode), keys[i].text);
        }
    }
    private void GetInput()
    {
        paraPlayer = objPlayer.GetComponent<ParameterPlayer>();
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.Space) && paraPlayer.waitRecSkill[0] == false)
        {
            buttonSpaceAudio.Play();
            startAttack();
            swordHitbox.swordbox.enabled = true;
            swordHitbox.isHitting = true;

            StartCoroutine(AttackNormal());
            paraPlayer.useSkill();

        }
        if (Input.GetKeyDown(keycodes[0]) && Input.GetKey(keycodes[0]) && paraPlayer.waitRecSkill[1] == false)
        {
            if (paraPlayer.getLevel() >= 3)
            {
                prefabsSpell[0].GetComponent<ShurikenScript>().aquired = true;
                buttonQAudio.Play();
                startAttack();
                StartCoroutine(Attack1());
                paraPlayer.useSkill();
            }
        }
        if (Input.GetKeyDown(keycodes[1]) && Input.GetKey(keycodes[1]) && paraPlayer.waitRecSkill[2] == false)
        {
            if (paraPlayer.getLevel() >= 5)
            {
                prefabsSpell[1].GetComponent<ShieldScript>().aquired = true;
                startDefend();
                buttonWAudio.Play();
                StartCoroutine(ShieldDefend());
                paraPlayer.useSkill();
            }
        }
        if (Input.GetKeyDown(keycodes[2]) && Input.GetKey(keycodes[2]) && paraPlayer.waitRecSkill[3] == false)
        {
            if (paraPlayer.getLevel() >= 6)
            {
                prefabsSpell[2].GetComponent<FireSkillScipt>().aquired = true;
                buttonEAudio.Play();
                startAttack();
                StartCoroutine(Attack3());
                paraPlayer.useSkill();
            }
        }
        if (Input.GetKeyDown(keycodes[3]) && Input.GetKey(keycodes[3]) && paraPlayer.waitRecSkill[4] == false)
        {
            if (paraPlayer.getLevel() >= 8)
            {
                buttonRAudio.Play();
                startAttack();
                StartCoroutine(Teleport());
                paraPlayer.useSkill();
            }
        }
        if (Input.GetKeyDown(keycodes[4]) && Input.GetKey(keycodes[4]) && paraPlayer.waitRecSkill[5] == false)
        {
            if (paraPlayer.getLevel() >= 12)
            {
                prefabsSpell[4].GetComponent<FireSkillScipt>().aquired = true;
                buttonTAudio.Play();
                startAttack();
                StartCoroutine(Ultimate());
                paraPlayer.useSkill();
            }
        }

    }
    private IEnumerator AttackNormal()
    {
        sword();
        paraPlayer.skill = 0;
        yield return new WaitForSeconds(0.5f);
        stopAttack();
    }
    private IEnumerator Attack1()
    {
        shuriken();
        paraPlayer.skill = 1;
        yield return new WaitForSeconds(0.2f);
        GameObject objEffSpell = Instantiate(prefabsSpell[0], exitPoint.position, Quaternion.identity);
        Rigidbody2D rgbSpell = objEffSpell.GetComponent<Rigidbody2D>();
        rgbSpell.velocity = new Vector2(objPlayer.transform.localScale.x * 20f, rgbSpell.velocity.y);
        yield return new WaitForSeconds(0.1f);

        stopAttack();
    }
    private IEnumerator ShieldDefend()
    {
        paraPlayer.skill = 2;
        yield return new WaitForSeconds(0.4f);
        GameObject objEffSpell = Instantiate(prefabsSpell[1], shieldPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
    }
    private IEnumerator Attack3()
    {
        fire();
        paraPlayer.skill = 3;
        yield return new WaitForSeconds(0.4f);
        GameObject objEffSpell = Instantiate(prefabsSpell[2], exitPoint.position, Quaternion.identity);
        Vector3 theScale = objEffSpell.transform.localScale;
        theScale.x = objPlayer.transform.localScale.x * 2.6286f;
        objEffSpell.transform.localScale = theScale;
        Rigidbody2D rgbSpell = objEffSpell.GetComponent<Rigidbody2D>();
        rgbSpell.velocity = new Vector2(objPlayer.transform.localScale.x * 20f, rgbSpell.velocity.y);
        yield return new WaitForSeconds(0.5f);
        stopAttack();
    }
    private IEnumerator Teleport()
    {
        paraPlayer.skill = 4;
        GameObject objEffSpell = Instantiate(prefabsSpell[3], exitPoint.position, Quaternion.identity);
        Vector3 theScale = objPlayer.transform.localScale;
        Vector3 thePos = objPlayer.transform.localPosition;
        Vector3 theScaleSpell = objEffSpell.transform.localScale;
        theScaleSpell.x = theScale.x;
        thePos.x = thePos.x + theScale.x * 15f;
        Debug.Log(thePos.x.ToString() + " - " + theScale.x.ToString());
        objPlayer.transform.localPosition = thePos;
        objEffSpell.transform.localScale = theScaleSpell;
        yield return new WaitForSeconds(0.2f);
        Destroy(objEffSpell);
        Debug.Log("attack");
        stopAttack();
    }
    private IEnumerator Ultimate()
    {
        Rigidbody2D mainPlayer = objPlayer.GetComponent<Rigidbody2D>();
        mainPlayer.velocity = Vector2.zero;
        rasengan1();
        paraPlayer.skill = 5;
        // create rasengan
        GameObject objEffSpell = Instantiate(prefabsSpell[4], ultiPoint.position, Quaternion.identity);
        objEffSpell.GetComponent<Collider2D>().enabled = !objEffSpell.GetComponent<Collider2D>().enabled;
        Rigidbody2D spell = objEffSpell.GetComponent<Rigidbody2D>();
        Vector3 theScale = objPlayer.transform.localScale;
        Vector3 theScaleSpell = objEffSpell.transform.localScale;
        theScaleSpell.x = objPlayer.transform.localScale.x * 5.433f;
        objEffSpell.transform.localScale = theScaleSpell;
        float horizontal = theScale.x;
        float speed = 50f;
        yield return new WaitForSeconds(0.3f);
        // move
        rasengan2();
        GameObject[] shadow = new GameObject[10];
        // rasengan đi
        // create shadow
        objEffSpell.GetComponent<Collider2D>().enabled = !objEffSpell.GetComponent<Collider2D>().enabled;
        for (int i = 0; i < 12; i++)
        {
            StartCoroutine(moveRD2D(spell, horizontal, speed));
            // t muốn nhân vật di chuyển theo rasengan chỗ này nè
            //StartCoroutine(moveRD2D(mainPlayer, horizontal, speed));
            Vector3 thePos = objPlayer.transform.localPosition;
            Vector3 thePosSpell = spell.transform.localPosition;
            thePos.x = thePosSpell.x - theScale.x * 2;
            objPlayer.transform.localPosition = thePos;
            yield return new WaitForSeconds(0.01f);
            if (i % 3 != 0)
            { continue; }
            shadow[i] = Instantiate(prefabsSpell[3], shadowPoint.position, Quaternion.identity);
            Vector3 scaleShadow = shadow[i].transform.localScale;
            scaleShadow.x = theScale.x;
            shadow[i].transform.localScale = scaleShadow;
        }
        //stop
        yield return new WaitForSeconds(0.2f);
        spell.velocity = Vector2.zero; // ulti dừng 
        //delete shadow
        for (int i = 0; i < 10; i++)
        {
            Destroy(shadow[i]);
        }
        stopAttack();
    }
    private IEnumerator moveRD2D(Rigidbody2D myRD2D, float horizontal, float speed)
    {
        myRD2D.velocity = new Vector2(horizontal * speed, myRD2D.velocity.y);
        yield return null;
    }
    private void startDefend()
    {
        isDfn = true;
    }
    private void stopDefend()
    {
        isDfn = false;
    }
    private void startAttack()
    {
        isAtk = true;
        resetAttack();
        ActivateLayer("AttackPlayer");
        myAnimator.SetBool("attack", true);
    }
    private void stopAttack()
    {
        isAtk = false;

        swordHitbox.isHitting = false;
        swordHitbox.swordbox.enabled = false;

        myAnimator.SetBool("attack", false);
        ActivateLayer("MovePlayer");
    }
    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }
    private void resetAttack()
    {
        myAnimator.SetFloat("sword", 0);
        myAnimator.SetFloat("shuriken", 0);
        myAnimator.SetFloat("fire", 0);
        myAnimator.SetFloat("rasengan1", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
    private void sword()
    {
        myAnimator.SetFloat("sword", 1);
        myAnimator.SetFloat("shuriken", 0);
        myAnimator.SetFloat("fire", 0);
        myAnimator.SetFloat("rasengan1", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
    private void fire()
    {
        myAnimator.SetFloat("sword", 0);
        myAnimator.SetFloat("shuriken", 0);
        myAnimator.SetFloat("fire", 1);
        myAnimator.SetFloat("rasengan1", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
    private void shuriken()
    {
        myAnimator.SetFloat("sword", 0);
        myAnimator.SetFloat("shuriken", 1);
        myAnimator.SetFloat("fire", 0);
        myAnimator.SetFloat("rasengan1", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
    private void rasengan1()
    {
        myAnimator.SetFloat("sword", 0);
        myAnimator.SetFloat("shuriken", 0);
        myAnimator.SetFloat("fire", 0);
        myAnimator.SetFloat("rasengan1", 1);
        myAnimator.SetFloat("resengan2", 0);
    }
    public void rasengan2()
    {
        myAnimator.SetFloat("sword", 0);
        myAnimator.SetFloat("shuriken", 0);
        myAnimator.SetFloat("fire", 0);
        myAnimator.SetFloat("rasengan1", 0);
        myAnimator.SetFloat("resengan2", 1);
    }
}
