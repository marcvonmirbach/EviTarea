using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordCollision : MonoBehaviour
{

    [SerializeField]
    private Text score;

    private PathFollower pf;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("Score").GetComponent<Text>();
        score.text = "0";


        pf = GameObject.Find("Ball(Clone)").GetComponent<PathFollower>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.name.Contains("Ball"))
        {
           
            score.text = (int.Parse(score.text) + 1).ToString();

            pf.restartSphere();
            Debug.Log("Restart!!");

        }

    }
}
