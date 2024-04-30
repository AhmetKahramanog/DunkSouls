using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    private KnightEnemyScript knighEnemy;
    private Animator animator;
    [SerializeField] private ParticleSystem particle;
    public static bool IsKnightParried { get; set; } = false;

    private float parryTime;

    public static bool IsParryAnimation { get; set; } = false;

    //public static bool doHit { get; set; } = true;



    private void Start()
    {
        knighEnemy = FindAnyObjectByType<KnightEnemyScript>();
        animator = GetComponent<Animator>();
        particle.Stop();
    }

    private void Update()
    {
        ParryClick();
        ParryAble();
    }

    private void ParryClick()
    {
        if (Input.GetMouseButtonDown(1) && Time.time - parryTime >= 2f && PlayerHealth.DoAbleParry)
        {
            animator.SetBool("isParry", true);
            parryTime = Time.time;
            IsParryAnimation = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("isParry", false);
        }
    }

    private void ParryAble()
    {
        if (knighEnemy.IsAbleParry && animator.GetBool("isParry"))
        {
            //doHit = false;
            var knightRB = knighEnemy.GetComponent<Rigidbody>();
            knightRB.AddForce(Vector3.forward * 350f);
            particle.Play();
            IsKnightParried = true;
            StartCoroutine(Delay(3f));
        }
        //else
        //{
        //    doHit = true;
        //}
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        IsKnightParried = false;
    }

    public void ParryAnimationStop()
    {
        IsParryAnimation = false;
    }

}
