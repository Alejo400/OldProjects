using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedPlayer : MonoBehaviour
{
    //If the player is in the zone can talk with npc
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //With this line code, we can work with a specific player. The player in zone and its components
            GameManager._sharedInstance._playerDetected = collision.gameObject;
            GetComponentInParent<TouchController>().playerInZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager._sharedInstance._playerDetected = null;
            GetComponentInParent<TouchController>().playerInZone = false;
        }
    }
}
