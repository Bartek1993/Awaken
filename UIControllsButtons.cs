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
    public GameObject hitBox;
    void Start()
    {
        canRoll = true;
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
       
      //  if (Input.GetKey(KeyCode.Z))
      //  {
        //    isAttacking = true;
       // }
       // else
       // {
         //   isAttacking = false;
        //}

        if (isAttacking)
        {
            attackID += 1;
        }
        else
        {
            attackID = 0;
        }
        animator.SetInteger("attackID", attackID);

      //  if (Input.GetKey(KeyCode.X) && canRoll)
      //  {
     //       onRoll();
     //   }
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


    public IEnumerator Roll()
    {
        Player.GetComponent<CapsuleCollider>().excludeLayers = LayerMask.GetMask("Enemy");
        animator.SetTrigger("roll");
        canRoll = false;
        yield return new WaitForSeconds(0.5f);
        Player.GetComponent<CapsuleCollider>().includeLayers = LayerMask.GetMask("Enemy");
        animator.ResetTrigger("roll");
        yield return new WaitForSeconds(1);
        canRoll = true;
    }


    public void OnApply(GameObject panel)
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        panel.SetActive(false);
        Player.GetComponent<PlayerStats>().adrenaline -= 100;
    }
}
