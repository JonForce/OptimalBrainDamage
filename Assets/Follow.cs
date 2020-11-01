using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Transform transform;
    public Transform target;
    public bool
        lockX,
        lockY,
        lockZ;
    public float
        bufferX = 0,
        bufferY = 0,
        bufferZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float newX = this.transform.position.x;
        float newY = this.transform.position.y;
        float newZ = this.transform.position.z;
        if (!lockX)
        {
            newX = target.position.x + bufferX;
        }
        if (!lockY)
        {
            newY = target.position.y + bufferY;
        }
        if (!lockZ)
        {
            newZ = target.position.z + bufferZ;
        }
        this.transform.position = new Vector3(newX, newY, newZ);
    }
}
