using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneScript : MonoBehaviour
{
    private AudioSource aud;
    private bool grabando = false;
    private int NumGrab = 0;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>(); // Inicialización 

        foreach (string device in Microphone.devices) { //Para ver los micrófonos detectados
            Debug.Log("Name: " + device); 
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Comprobamos si se pulsa la tecla M, si se pulsa, empezamos la grabación.
        //Si se pulsa M después de estar grabando, entonces se para la grabación.
        if (Input.GetKeyDown(KeyCode.M)) {
            if (!grabando) {
                Debug.Log ("Pulsada tecla M, ahora deberíamos empezar a grabar");
                aud.clip = Microphone.Start("Built-in Microphone", true, 360, 44100);
                grabando = true; 
            }
            else {
                grabando = false;
                Microphone.End("Built-in Microphone");
               // SavWav.Save("audio"+NumGrab,aud.clip); Por qué error???
                NumGrab++;
            }
        }
        
    }
}
