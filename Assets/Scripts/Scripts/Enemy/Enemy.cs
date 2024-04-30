using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
    public virtual void GetDamage(float amount)
    {
        
    }

    public virtual IEnumerator Death(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}



