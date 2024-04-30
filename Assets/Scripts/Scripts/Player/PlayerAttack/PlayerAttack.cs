using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private List<AttackSO> combos;

    private float lastClickedTime;
    private float lastComboEnd;
    private int comboCount;
    private Animator animator;
    public static bool isAttack { get; set; }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !PlayerDash.isDashing && !PlayerDash.DashAttack && !ShopInteract.IsShopOpen)
        {
            Attack();
        }
        else if (Input.GetMouseButtonDown(0) && PlayerDash.DashAttack == true)
        {
            animator.SetTrigger("DashAttack");
            Weapon.Instance.OpenTriggerBox();
            Weapon.Instance.damage = 2;
            StartCoroutine(Weapon.Instance.CloseTriggerBoxDelay(0.7f));
        }
        ExitAttack();
    }

    private void Attack()
    {
        if (Time.time - lastComboEnd > 0.8f && comboCount <= combos.Count)
        {
            CancelInvoke(nameof(EndCombo));

            if (Time.time - lastClickedTime > 1.1f)
            {
                isAttack = true;
                animator.runtimeAnimatorController = combos[comboCount].animatorController;
                animator.Play("Attack", 0, 0);
                Weapon.Instance.damage = combos[comboCount].damage;
                Weapon.Instance.OpenTriggerBox();
                comboCount++;
                lastClickedTime = Time.time;
                if (comboCount >= combos.Count) comboCount = 0;
            }
        }
    }

    private void ExitAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.75f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke(nameof(EndCombo),2);
            isAttack = false;
            Weapon.Instance.CloseTriggerBox();
            Destroy(MonsterEnemyMovement.bloodEffect);
        }
    }

    private void EndCombo()
    {
        comboCount = 0;
        lastComboEnd = Time.time;
    }
}
