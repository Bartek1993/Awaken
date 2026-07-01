using System.Collections;
using Invector.vCharacterController;
using UnityEngine;

public class AnimationsScripts : MonoBehaviour
{
    public GameObject DamageBox, hitBox;
    public UIControllsButtons uiControllsButtons;
    public AudioSource audioSourceSword;
    public AudioClip swordSwoosh;
    public vThirdPersonInput vInput;
    public vThirdPersonController vController;
    public Animator animator;
    public float timer, animationFrameTime;
    public int targetAnimationFrame;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        targetAnimationFrame = 8;
        animator = GetComponent<Animator>();
        vInput = GetComponent<vThirdPersonInput>();
        vController = GetComponent<vThirdPersonController>();
        uiControllsButtons = FindFirstObjectByType<UIControllsButtons>();
       // animator.enabled = false;
        animator.speed = 0.45f;
        animationFrameTime = 1f / targetAnimationFrame;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
      
      if (timer >= animationFrameTime)
      {
          animator.Update(timer);
          timer %= animationFrameTime;
      }

      if (uiControllsButtons.isAttacking)
        {
            vController.freeSpeed.walkSpeed = 0;
            vController.freeSpeed.runningSpeed = 0f;
        }
        else
        {
            vController.freeSpeed.walkSpeed = 4;
            vController.freeSpeed.runningSpeed = 4f;
        }
    }

    public void onRollInit()
    {
        StartCoroutine("Rolling");
    }

    public IEnumerator Rolling()
    {
        DamageBox.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        DamageBox.SetActive(true);
    }


    public void onHit()
    {
        GameObject go = Instantiate(hitBox, transform.position, transform.rotation);
        transform.parent = null;

    }

    public void onHitExit()
    {
        
    }

    public void PlaySFXSwoosh()
    {
        audioSourceSword.pitch = Random.Range(0.85f, 1f);
        audioSourceSword.PlayOneShot(swordSwoosh);
    }


}
