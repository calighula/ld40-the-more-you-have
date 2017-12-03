using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour {

    public GameObject proyectile;

    public Transform bulletsParent;

    public float throwRate = 5f;

    public bool throwRight = true;

    private float currentTime = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(ThrowProjectiles());
	}
	
	IEnumerator ThrowProjectiles ()
    {
        while (true)
        {
            currentTime += Time.deltaTime;
            //Debug.Log("currentTime:" + currentTime);
            if (currentTime > throwRate)
            {
                currentTime = 0;
                //Debug.Log("THROW");
                GameObject shot = Instantiate(proyectile, transform.position, transform.rotation, bulletsParent) as GameObject;
                shot.GetComponent<Shot>().SetDartThrowSettings(2, 5, Vector3.right);
            }
            yield return new WaitForEndOfFrame();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter:" + other.gameObject.name);
        if (other.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
    }
}
