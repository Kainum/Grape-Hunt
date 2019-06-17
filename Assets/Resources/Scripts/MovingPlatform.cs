using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private float xMin;
    [SerializeField]
    private float xMax;
    [SerializeField]
    private float speedX;
    [SerializeField]
    private float yMin;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float speedY;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move () {
        float xAtual = transform.position.x;
        float yAtual = transform.position.y;
        rb.velocity = new Vector2(speedX, speedY);
        
        if (speedX != 0) {
            if ((xAtual < xMin && speedX < 0) || (xAtual > xMax && speedX > 0)) {
                speedX /= -1;
            }
        }

        if (speedY != 0) {
            if ((yAtual < yMin && speedY < 0) || (yAtual > yMax && speedY > 0)) {
                speedY /= -1;
            }
        }
    }
}
