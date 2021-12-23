using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
  private Rigidbody rb;
  [Range(1,10)]
	public float velocidad = 1000;
  
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        		
		float movimientoH = Input.GetAxis("Horizontal");
		float movimientoV = Input.GetAxis("Vertical");

	
		Vector3 movimiento = new Vector3(movimientoH, 0.0f, movimientoV);
		rb.AddForce(movimiento);
    }
}