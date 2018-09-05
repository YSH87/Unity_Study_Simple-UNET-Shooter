using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //총알이 어딘가 부딪히면 사라짐.
    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;
        Health targetHealth = target.GetComponent<Health>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(10);
        }

        Destroy(gameObject);
    }
}