using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int health;

    private bool isInvulnarble;

    public bool IsDead => health == 0;

    public event Action OnTakeDamage;
    public event Action OnDie;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void SetInvulnerable(bool isInvulnarble)
    {
        this.isInvulnarble = isInvulnarble;
    }
    public void DealDamage(int damage)
    {
        if (health == 0)
        {
            return;
        }

        if (isInvulnarble)
        {
            return;
        }

        health = Mathf.Max(health - damage, 0);

        OnTakeDamage.Invoke();

        if (health == 0)
        {
            OnDie?.Invoke();
        }

        Debug.Log(health);
    }
}
