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
    public GameObject deathEffect;
    public bool isGameover;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
            audioManager.PlaySFX(audioManager.playerDamaged);
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
            Debug.Log("collide");
            // game over
            if(currentHealth <= 0 && !isGameover)
            {
                audioManager.PlaySFX(audioManager.gameOver);
                Instantiate(deathEffect,transform.position,Quaternion.identity);
                currentHealth = 0;
                gameObject.SetActive(false);
                Debug.Log("GAME OVER!");
                OnPlayerDeath?.Invoke();
                isGameover = true;
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
        yield return new WaitForSeconds(0.8f);
        sprite.material = material.original;
        isImune = false;
    }
}