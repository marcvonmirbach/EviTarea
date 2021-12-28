using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    [SerializeField] private Collider[] colliders;
    void Start()
    {
        foreach (Collider collider in colliders)
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), collider, true);
        }
    }
}
