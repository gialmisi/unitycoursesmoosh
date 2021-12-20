using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with friendly");
                break;
            case "Finish":
                Debug.Log("Collided with finish");
                break;
            case "Fuel":
                Debug.Log("Collided with fuel");
                break;
            default:
                Debug.Log("Collided with something else");
                break;
        }
    }
}
