using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

    public float timeToWin = 2;

    private float currentTime = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentTime = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentTime += Time.deltaTime;
            if (currentTime > timeToWin)
            {
                GameController.Instance.Win();
            }
        }
    }


}
