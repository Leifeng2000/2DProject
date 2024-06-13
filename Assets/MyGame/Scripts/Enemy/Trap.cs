using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage = 1000;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                
                playerScript.TakeDamage(damage, Vector2.zero, null);

                Debug.Log("Player has hit a trap and took damage.");
            }
        }
    }
}
