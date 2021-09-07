using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] GameObject modelFBX;
    private Rigidbody rigidbody;

    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] float speed;

    [SerializeField] AudioClip spawnSound;
    [SerializeField] AudioClip deathSound;
    private AudioSource audioSource;
    private bool isDead;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(spawnSound);

        currentHealth = maxHealth;

        rigidbody.AddForce(-Vector3.forward * speed);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <=0 )
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
