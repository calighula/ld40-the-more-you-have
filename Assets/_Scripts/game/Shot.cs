using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    public float speed = 5;

    public float direction = 1.0f;

    public float timeToDie = 5.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);
    }

    public void SetDirection(float newDirection)
    {
        direction = newDirection;
        Destroy(gameObject, timeToDie);
    }

    public void SetDartThrowSettings (float speed, float timeToDie, Vector3 directionV)
    {
        this.speed = speed;
        this.timeToDie = timeToDie;
        Destroy(gameObject, timeToDie);
    }
}
