using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCubeBehaviour : MonoBehaviour
{
    
    void Update() {

        transform.position += Vector3.forward * Time.deltaTime;
        transform.Rotate(Vector3.up * 50.0f * Time.deltaTime);
    }
}
