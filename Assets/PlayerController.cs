using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Vector3 vel;
    // Start is called before the first frame update
    void Start() {
        vel = Vector3.zero;
    }

    // Update is called once per frame
    void Update() {


        transform.position += vel;
    }
}
