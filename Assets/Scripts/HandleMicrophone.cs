using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMicrophone : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject player;

    private bool playerIsPresent;
    private bool recording;

    // Start is called before the first frame update
    void Start()
    {
        playerIsPresent = false;
        recording = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && playerIsPresent && !recording)
        {
            // start record
            recording = true;
            Debug.Log("Start recording");
            // TODO

        } else if ((Input.GetKeyUp(KeyCode.M) || !playerIsPresent) && recording)
        {
            // stop recording
            Debug.Log("Stop recording" + Input.GetKeyDown(KeyCode.M) + playerIsPresent);
            recording = false;
            // TODO
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == player)
        {
            playerIsPresent = true;
            Debug.Log("Player entered the record area.");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == player)
        {
            playerIsPresent = false;
            Debug.Log("Player left record area.");
        }
    }
}
