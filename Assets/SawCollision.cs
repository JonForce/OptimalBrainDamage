using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawCollision : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player Controller collision 1");
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player Controller collision 2");
            collision.gameObject.GetComponent<PlayerController>().kill();
        }
    }
}
