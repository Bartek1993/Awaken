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
        cooldowntime = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerStats>().mana < Player.GetComponent<PlayerStats>().magicConsumption)
        {
            foreach (Button button in buttons)
            {
                if (button.IsActive())
                {
                    button.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (Button button in buttons)
            {
                    button.gameObject.SetActive(true);
            }
        }

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
            StartCoroutine("Roll");
        }

        
    }

    public void GetMagic(int magicId)
    {
        GameObject go = Instantiate(hitBoxMagic,Player.transform.position,Player.transform.rotation);
        Destroy(go,2f);
        magicIdentity = magicId;
        if (Player.GetComponent<PlayerStats>().mana > 25)
        {
            StartCoroutine(Cooldown(magicIdentity));
        }

        
        
    }


    public IEnumerator Roll()
    {
        Player.GetComponent<PlayerStats>().canTakeDamage = false;
        animator.SetTrigger("roll");
        canRoll = false;
        yield return new WaitForSeconds(0.05f);
        animator.ResetTrigger("roll");
        yield return new WaitForSeconds(Player.GetComponent<PlayerStats>().invisibilityFramesRoll);
        Player.GetComponent<PlayerStats>().canTakeDamage = true;
        canRoll = true;
    }


    public IEnumerator Cooldown(int id)
    {
      
        Player.gameObject.GetComponent<PlayerStats>().mana -= 25;
        buttons[id].gameObject.SetActive(false);
        buttons[id].enabled = false;
        yield return new WaitForSeconds(cooldowntime);
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
