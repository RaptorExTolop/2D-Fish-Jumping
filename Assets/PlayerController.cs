using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    private float heldTime;
    public float power = 20;
    public float gravity = 200;
    // Start is called before the first frame update
    void Start() {
        // vel = Vector3.zero;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            heldTime += Time.deltaTime;
        } else if (heldTime != 0) {
            handleJump();
        }

        // vel.y = vel.y < 0 ? 0 : vel.y - gravity * Time.deltaTime;
        // transform.position += vel;
        transform.position = (new Vector3(0, Mathf.Clamp(transform.position.y, 0, 10), 0));
    }

    void handleJump() {
        heldTime = Mathf.Clamp(heldTime, 0.01f, 5);
        float pow = heldTime * power;
        Debug.Log("Jumping with power of " + pow);
        // vel.y += pow;
        heldTime = 0;
    }
}
