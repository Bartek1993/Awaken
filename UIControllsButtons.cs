using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIControllsButtons : MonoBehaviour
{

    public Button [] actionButtons;
    public int attackID;
    public Animator animator;
    public GameObject Player;
    public bool isAttacking, canRoll;
    public GameObject hitBoxMagic;
    public float cooldowntime;
    public Button[] buttons;
    public Button rollButton;
    public int magicIdentity;
    public GameObject closeCam, farCam;
    public bool camSwitched;
    
    void Start()
    {
        camSwitched = false;
        closeCam.SetActive(false);
        farCam.SetActive(true);
        canRoll = true;
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isAttacking)
        {
            attackID += 1;
        }
        else
        {
            attackID = 0;
        }
        animator.SetInteger("attackID", attackID);
        
        
    }

    public void OnAttackButtonDown()
    {
        isAttacking = true;
    }

    public void onAttackButtonUp()
    {
        isAttacking = false;
    }

    public void onRoll()
    {
        if (!isAttacking)
        {
            animator.SetTrigger("roll");
            StartCoroutine("Roll");
        }

        
    }

    public void GetMagic(int magicId)
    {
        GameObject go = Instantiate(hitBoxMagic,Player.transform.position,Player.transform.rotation);
        Destroy(go,2f);
        magicIdentity = magicId;
        StartCoroutine(Cooldown(magicIdentity));
        

        
        
    }


    public IEnumerator Roll()
    {
        yield return new WaitForSeconds(0.1f);
        rollButton.interactable = false;
        yield return new WaitForSeconds(Player.GetComponent<PlayerStats>().invisibilityFramesRoll);
        Player.GetComponent<PlayerStats>().canTakeDamage = true;
        float timer = 0;
        while (timer < Player.GetComponent<PlayerStats>().staminaCoolDown)
        {
            animator.ResetTrigger("roll");
            rollButton.GetComponent<Image>().fillAmount =  Mathf.Clamp01(timer / Player.GetComponent<PlayerStats>().staminaCoolDown);
            timer += Time.deltaTime;
            canRoll = false;
            yield return null;
        }
        canRoll = true;
        rollButton.interactable = true;
    }


    public IEnumerator Cooldown(int id)
    {
        cooldowntime = Player.GetComponent<PlayerStats>().magicCooldown;
        float timer = 0;
        while (timer < cooldowntime)
        {
            timer += Time.deltaTime;
            buttons[id].GetComponent<Image>().fillAmount = Mathf.Clamp(timer / cooldowntime, 0f, 1f);
            buttons[id].enabled = false;
            yield return null;
        }
        buttons[id].enabled = true;
        buttons[id].gameObject.SetActive(true);

    }

    public void switchCam()
    {
        camSwitched = !camSwitched;
        if (camSwitched)
        {
            closeCam.SetActive(false);
            farCam.SetActive(true);
        }
        else
        {
            closeCam.SetActive(true);
            farCam.SetActive(false);
        }
    }


}
