using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private int damage;

    private void OnEnable() {
        alreadyCollidedWith.Clear();
    }
    private void OnTriggerEnter(Collider other) {
        if (other == myCollider) 
        {
            return;
        }

        if (alreadyCollidedWith.Contains(other))
        {
            return;
        }

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);

            alreadyCollidedWith.Add(other);
        }
   }

   public void SetAttack(int damage)
   {
        this.damage = damage;
   }
}
