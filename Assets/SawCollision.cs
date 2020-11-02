using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {

            collision.gameObject.GetComponent<PlayerController>().kill();
        }
    }

}
