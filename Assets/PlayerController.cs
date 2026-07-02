using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    public GameObject water;
    private float heldTime;
    public float power = 20;
    private Rigidbody2D rb;  

    public GameObject gameManagerObject;
    private GameManager gManager;
    private bool charging;
    public Interval heldTimeClamp;
    [SerializeField] private bool inWater;
    public UnityEngine.UI.Image uiBar;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        gManager = gameManagerObject.GetComponent<GameManager>();
        uiBar.fillAmount = 0;
    }

    void OnEnable() {
        charging = false;
        inWater = true;
		uiBar.fillAmount = 0;
	}

	// Update is called once per frame
	void Update() {
        uiBar.fillAmount = ((float)(heldTime - heldTimeClamp.Min) / (float)(heldTimeClamp.Max - heldTimeClamp.Min)) * 100;
        Debug.Log(((float)(heldTime - heldTimeClamp.Min) / (float)(heldTimeClamp.Max - heldTimeClamp.Min)) * 100);

		if (Input.GetKeyDown(KeyCode.Space) && inWater) {
            charging = true;
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            charging = false;
        } else if (heldTime != 0 && !charging) {
            charging = false;
            HandleJump();
        }

        if (charging) {
            heldTime += Time.deltaTime;
        }

        // vel.y = vel.y < 0 ? 0 : vel.y - gravity * Time.deltaTime;
        // transform.position += vel;
        if (transform.position.y >= 5.3) {
            rb.velocity = Vector2.zero;
        }
        transform.position = new Vector3(transform.position.x, Math.Clamp(transform.position.y, -2, float.PositiveInfinity), transform.position.z);
    }

    void HandleJump() {
        rb.velocity = Vector2.zero;
        float pow = heldTime * power;
        pow = Mathf.Clamp(pow, heldTimeClamp.Min, heldTimeClamp.Max);
        Debug.Log("Jumping with power of " + pow);
        rb.AddForce(new Vector3(0, pow, 0));

        heldTime = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log($"ooooOOOOoOoo {collision.name} with tag: {collision.tag} hit me something baAAAaAAAAaaAAaad");
        if (collision.CompareTag("Obstacle - Target")) {
            gManager.EndGame();
        } else if (collision.CompareTag("Obstacle - Score Point")) {
            gManager.AddScore(1);
        } 
    }

    void OnTriggerStay2D(Collider2D collision) {
        inWater = false;
        //Debug.Log($"ooooOOOOoOoo {collision.name} with tag: {collision.tag} hit me something baAAAaAAAAaaAAaad");
        if (collision.CompareTag("water")) {
            inWater = true;
        }
    }
}
