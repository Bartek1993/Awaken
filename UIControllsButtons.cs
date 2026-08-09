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
    public bool isAttacking, canRoll, canDashAttack;
    public GameObject hitBoxMagic;
    public float cooldowntime;
    public Button[] buttons;
    public Button rollButton;
    public Button interactButton;
    public bool interact;
    public int magicIdentity;
    public GameObject closeCam, farCam;
    public bool camSwitched;
    public LayerMask treasureMask, portalMask;
    public Text chestPrice;
    public GameObject chestPriceBanner;
    public GameObject SlashIndicator;
    void Start()
    {
        camSwitched = false;
        closeCam.SetActive(false);
        farCam.SetActive(true);
        canRoll = true;
        canDashAttack = true;
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
        Collider [] colliders = Physics.OverlapSphere(Player.transform.position,2f, treasureMask);
        if (colliders.Length > 0)
        {
            interactButton.GetComponentInChildren<Text>().text = "OPEN";
            chestPriceBanner.SetActive(true);
            interactButton.gameObject.SetActive(true);
            chestPrice.gameObject.SetActive(true);
            foreach (var collider1 in colliders)
            {
                chestPrice.text = "REQUIRED " + collider1.GetComponent<Chest>().cost + " COINS";
            }
        }
        else
        {
            chestPriceBanner.SetActive(false);
            chestPrice.gameObject.SetActive(false);
            interactButton.gameObject.SetActive(false);
        }
        
        Collider[] colliders2 = Physics.OverlapSphere(Player.transform.position,2f, portalMask);
        if (colliders2.Length > 0)
        {
            interactButton.GetComponentInChildren<Text>().text = "ENTER";
            interactButton.gameObject.SetActive(true);
        }
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

    public void onSlashDash()
    {
        if (canDashAttack)
        {
            StartCoroutine("DashSlash");
        }

      
    }

    public void GetMagic(int magicId)
    {
        GameObject go = Instantiate(hitBoxMagic,Player.transform.position,Player.transform.rotation);
        Destroy(go,2f);
        magicIdentity = magicId;
        StartCoroutine(Cooldown(magicIdentity));
        
    }

    public void IndicatorOn(GameObject go)
    {
        go.SetActive(true);
    }
    public void IndicatorOff(GameObject go)
    {
        go.SetActive(false);
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

    public IEnumerator DashSlash()
    {
        animator.SetTrigger("dashAttack");
        yield return new WaitForSeconds(0.05f);
    
        float timer = 0;
        float moveTimer = 0;
        while (moveTimer < 1f) 
        {
            moveTimer += Time.deltaTime;
            Player.transform.position = Vector3.Lerp(Player.transform.position, Vector3.forward, 2 * Time.deltaTime);
        }
        while (timer < Player.GetComponent<PlayerStats>().staminaCoolDown)
        {
            animator.ResetTrigger("dashAttack");
            rollButton.GetComponent<Image>().fillAmount =  Mathf.Clamp01(timer / Player.GetComponent<PlayerStats>().staminaCoolDown);
            timer += Time.deltaTime;
            canDashAttack = false;
            yield return null;
        }
        canDashAttack = true;

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

    public void Interact()
    {
        StartCoroutine(OnInteractButtonDown());
    }

    public IEnumerator OnInteractButtonDown()
    {
        interact = true;
        yield return new WaitForSeconds(0.5f);
        interact = false;
    }


}
