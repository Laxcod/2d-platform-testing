using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    bool isImune;
    public float imunityTime;
    Blink material;
    SpriteRenderer sprite;
    public float knockbackForceX;
    public float knockbackForceY;
    Rigidbody2D rb;
    public static event Action OnPlayerDamaged;
    public static event Action OnHealed;
    public static event Action OnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        material = GetComponent<Blink>();
        currentHealth = maxHealth;
        material.original = sprite.material;
    }

    private void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !isImune)
        {
            currentHealth = Mathf.Clamp(currentHealth - collision.GetComponent<Enemy>().damageToGive, 0, maxHealth);
            OnPlayerDamaged?.Invoke();
            StartCoroutine(Imunity());

            if(collision.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector2 (-knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2 (knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }

            // game over
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("GAME OVER!");
                OnPlayerDeath?.Invoke();
            }
        }
    }

    public void AddHealth()
    {
        if(currentHealth != maxHealth)
        {
            currentHealth += (float)HeartStatus.Full;
            if(FindObjectOfType<HealthHeart>().emptyHeart || FindObjectOfType<HealthHeart>().halfHeart)
            {
                FindObjectOfType<HealthHeart>().SetHeartImage(HeartStatus.Full);
            }
            OnHealed?.Invoke();
        }
    }

    IEnumerator Imunity()
    {
        isImune = true;
        sprite.material = material.blink;
        yield return new WaitForSeconds(1.5f);
        sprite.material = material.original;
        isImune = false;
    }
}