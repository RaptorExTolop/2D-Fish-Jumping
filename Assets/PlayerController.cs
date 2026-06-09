using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    private float heldTime;
    public float power = 20;
    public float gravity = 200;
    // Start is called before the first frame update
    private Rigidbody2D rb;  
    void Start() {
        // vel = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();
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
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0, 6), 0);
        if (transform.position.y >= 5.3) {
            rb.velocity = Vector2.zero;
        }
    }

    void handleJump() {
        rb.velocity = Vector2.zero;
        float pow = heldTime * power;
        pow = Mathf.Clamp(pow, 50, 215);
        Debug.Log("Jumping with power of " + pow);
        rb.AddForce(new Vector3(0, pow, 0));

        heldTime = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Obstacle - Target")) {
            Debug.Log("AGHHHHHHH");
        } else if (collision.CompareTag("Obstacle - Score Point")) {
            Debug.Log("YEAHHH");
        }
        
    }

}
