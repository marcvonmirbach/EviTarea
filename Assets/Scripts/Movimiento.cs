using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Movimiento : NetworkBehaviour
{

    [Range(1, 10)]
    public float speed = 5;

    [SerializeField] GameObject sphere_prefab;
    private bool isMoving;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        isMoving = false;

        
        if (GameObject.Find("Sphere(Clone)") == null)
        {
            GameObject sphere = (GameObject)Instantiate(sphere_prefab, new Vector3(0, 1, 0), transform.rotation);
            NetworkServer.Spawn(sphere);
            ClientScene.RegisterPrefab(sphere);
            Debug.Log(sphere);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isMoving)
        {
            Move();
        }

        if (this.isLocalPlayer)
        {


            var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.position += move * speed * Time.deltaTime;


            if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += new Vector3(0, 0, 1) * speed * Time.deltaTime;
                //pos.z += speed * Time.deltaTime;
            }
            if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += new Vector3(0, 0, -1) * speed * Time.deltaTime;
                //pos.z -= speed * Time.deltaTime;
            }
            if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
                //pos.x += speed * Time.deltaTime;
            }
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
                //pos.x -= speed * Time.deltaTime;
            }     


        }

    }
    
    void Move()
    {
        transform.LookAt(pos);
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);

        if (transform.position == pos)
        {
            isMoving = false;
        }
    }

}
