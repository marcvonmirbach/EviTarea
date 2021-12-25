using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BolaBehaviour : NetworkBehaviour
{

    public float speed = 5;

    private Vector3 desiredPosition;
    

    // Start is called before the first frame update
    void Start()
    {

        desiredPosition = new Vector3(Random.Range(-5.0f, 5.0f), transform.position.y, Random.Range(-5.0f, 5.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == desiredPosition)
        {
            desiredPosition = new Vector3(Random.Range(-5.0f, 5.0f), transform.position.y, Random.Range(-5.0f, 5.0f));
        }
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * speed);

    }

    void OnCollisionEnter()
    {
        transform.position = new Vector3(Random.Range(-5.0f, 5.0f), transform.position.y, Random.Range(-5.0f, 5.0f));
    }
}
