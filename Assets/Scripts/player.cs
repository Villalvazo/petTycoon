using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float movementSpeed=2.0f;
    public Rigidbody2D rigi;

    private Vector2 movActual;
    public Vector2 posicionAdoptado;
    // Start is called before the first frame update
    void Start()
    {
        posicionAdoptado = new Vector2(-4.7f, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        movActual.x = Input.GetAxis("Horizontal");
        movActual.y = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        rigi.MovePosition(rigi.position + (movActual * movementSpeed * Time.fixedDeltaTime));
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Puchurrae a la E para adoptarme");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.transform.root.GetComponent<Animal>() && !collision.transform.root.GetComponent<Animal>().isOwned())
        {
            Debug.Log("Adoptando");
            collision.transform.root.GetComponent<Animal>().setAdoptado(posicionAdoptado);
        }
    }
}
