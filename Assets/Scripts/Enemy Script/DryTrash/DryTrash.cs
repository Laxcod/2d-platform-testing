using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryTrash : MonoBehaviour
{
    public float moveSpeed;
    public float moveRange;
    public GameObject bombPrefab;
    public float dropInterval;

    private Vector3 leftPoint;
    private Vector3 rightPoint;
    private bool movingRight = true;

    private void Start()
    {
        // Calculate the left and right points within the move range
        leftPoint = transform.position - new Vector3(moveRange, 0f, 0f);
        rightPoint = transform.position + new Vector3(moveRange, 0f, 0f);

        // Start enemy movement and bomb dropping coroutines
        StartCoroutine(MoveEnemy());
        StartCoroutine(DropBombs());
    }

    private IEnumerator MoveEnemy()
    {
        while (true)
        {
            if (movingRight)
            {
                // Move towards the right point
                transform.position = Vector3.MoveTowards(transform.position, rightPoint, moveSpeed * Time.deltaTime);
                transform.localScale = new Vector3(1f, 1f, 1f);

                // Check if the enemy has reached the right point
                if (transform.position == rightPoint)
                    movingRight = false;
            }
            else
            {
                // Move towards the left point
                transform.position = Vector3.MoveTowards(transform.position, leftPoint, moveSpeed * Time.deltaTime);
                transform.localScale = new Vector3(-1f, 1f, 1f);

                // Check if the enemy has reached the left point
                if (transform.position == leftPoint)
                    movingRight = true;
            }

            yield return null;
        }
    }

    private IEnumerator DropBombs()
    {
        while (true)
        {
            // Instantiate the bomb at the enemy's position
            Instantiate(bombPrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(dropInterval);
        }
    }
}
