using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon Instance;
    private Collider collider;
    public float damage;
    //private int damageCount = 0;
    public int DamageCount { get; set; } = 0;
    private void Awake()
    {
        Instance = this;
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Enemy enemy))
        {
            DamageCount++;
            if (DamageCount == 1)
            {
                enemy.GetDamage(damage);
            }
        }
    }

    public void OpenTriggerBox()
    {
        collider.isTrigger = true;
    }

    public void CloseTriggerBox()
    {
        collider.isTrigger = false;
        DamageCount = 0;
    }

    public IEnumerator CloseTriggerBoxDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.isTrigger = false;
        DamageCount = 0;
    }
}
