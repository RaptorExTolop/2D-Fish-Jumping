using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{   
    public int Speed;

    public void MoveObst() {
        transform.position = new Vector3(
            Speed * Time.deltaTime + transform.position.x,
            transform.position.y, transform.position.z
        );
    }
}
