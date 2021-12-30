using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HandleMicrophone : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] Text microphoneTextField;

    private bool playerIsPresent;
    private bool recording;
    private string microphone;
    private int microphoneIdx;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        playerIsPresent = false;
        recording = false;
        microphoneIdx = -1;
        ChangeMicrophone();
    }


    // Update is called once per frame
    void Update()
    {
        // record memo
        if (Input.GetKeyDown(KeyCode.M) && playerIsPresent && !recording)
        {
            // start recording
            recording = true;
            Debug.Log("Start recording");
            audioSource.clip = Microphone.Start(microphone, false, 60, 44100);
            // did recording work?
            if (Microphone.IsRecording(microphone))
            {
                Debug.Log("Recording started with " + microphone);
            }
            else
            {
                Debug.LogError(microphone + " doesn't work!");
            }
        }
        else if ((Input.GetKeyUp(KeyCode.M) || !playerIsPresent) && recording)
        {
            // stop recording
            Debug.Log("Stop recording");
            Microphone.End(microphone);
            recording = false;
        }

        // change microphone
        if (Input.GetKeyDown(KeyCode.N))
        {
            ChangeMicrophone();
        }

        // play recorded memo
        if (Input.GetKeyDown(KeyCode.P))
        {
            audioSource.Play(); ;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Contains("Player"))
        {
            playerIsPresent = true;
            Debug.Log("Player entered the record area.");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name.Contains("Player"))
        {
            playerIsPresent = false;
            Debug.Log("Player left record area.");
        }
    }

    void ChangeMicrophone()
    {
        // don't switch device while recording
        if (recording)
        {
            return;
        }
        // increment device
        microphoneIdx += 1;
        if (microphoneIdx >= Microphone.devices.Length)
        {
            microphoneIdx = 0;
        }
        microphone = Microphone.devices[microphoneIdx];
        // update text
        microphoneTextField.text = microphone;
    }
}
