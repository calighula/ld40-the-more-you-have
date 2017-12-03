using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float pushForceMagnitud = 2f;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            // The player touch me, push and damage him
            Vector3 pushForce =
                collision.gameObject.transform.position - transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(pushForce.normalized * pushForceMagnitud, ForceMode.Impulse);

            // Notify the GameController that the player is damaged
            GameController.Instance.PlayerDamaged();
        }
    }
}
