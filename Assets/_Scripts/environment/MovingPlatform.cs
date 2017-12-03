using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public float speed = 2f;

    public Vector3[] path;

    private float minDistance = 0.1f;

    private int currentPosition = 0;

    private int nextPosition = 1;

	// Update is called once per frame
	void Update () {
        // Calculate distance
        Vector3 distance = path[nextPosition] - transform.position;
        if (distance.magnitude < minDistance)
        {
            currentPosition = nextPosition;
            nextPosition++;
            if (nextPosition >= path.Length)
            {
                nextPosition = 0;
            }
        }

        // Move platform
        Vector3 movDirection = (path[nextPosition] - transform.position).normalized;
        transform.Translate(movDirection * speed * Time.deltaTime, Space.World);
	}
}
