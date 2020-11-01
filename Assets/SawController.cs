using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SawController : MonoBehaviour
{
    public float speed;
    public Rigidbody sawBlade;
    // Start is called before the first frame update
    void Start()
    {
        sawBlade = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        sawBlade.maxAngularVelocity = speed;
        sawBlade.AddTorque(transform.forward);
    }
}
