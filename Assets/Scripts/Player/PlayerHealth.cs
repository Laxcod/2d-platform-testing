using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthImg;
    bool isImune;
    public float imunityTime;
    Blink material;
    SpriteRenderer sprite;
    public float knockbackForceX;
    public float knockbackForceY;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        material = GetComponent<Blink>();
        health = maxHealth;
        material.original = sprite.material;
    }

    private void Update()
    {
        healthImg.fillAmount = health / 100;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !isImune)
        {
            health -= collision.GetComponent<Enemy>().damageToGive;
            StartCoroutine(Imunity());

            if(collision.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector2 (-knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2 (knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }

            if(health <= 0)
            {
                // game over
                print("player dead");
            }
        }
    }

    IEnumerator Imunity()
    {
        isImune = true;
        sprite.material = material.blink;
        yield return new WaitForSeconds(imunityTime);
        sprite.material = material.original;
        isImune = false;
    }
}
