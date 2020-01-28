using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float movementSpeed=2.0f;
    public Rigidbody2D rigi;

    private Vector2 movActual;
    public Vector2 posicionAdoptado;
    private bool ocupado;
    private Stats stats;
    // Start is called before the first frame update
    void Start()
    {
        posicionAdoptado = new Vector2(-4.5f, 0.8f);
        ocupado = false;
        stats = GameObject.Find("GameManager").GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ocupado)
        {
            movActual.x = Input.GetAxis("Horizontal");
            movActual.y = Input.GetAxis("Vertical");
        }
    }
    private void FixedUpdate()
    {
        if (!ocupado)
        {
            rigi.MovePosition(rigi.position + (movActual * movementSpeed * Time.fixedDeltaTime));
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.GetComponent<Animal>() && !collision.transform.root.GetComponent<Animal>().isOwned())
        {
            FindObjectOfType<audioManager>().Play("CatMeow");
        }
    }
    private IEnumerator jugar(GameObject animal)
    {
        ocupado = true;
        animal.GetComponent<Animal>().recibirFelicidadTiempo(1.5f);
        yield return new WaitForSeconds(1.5f);        
        ocupado = false;
    }
    private IEnumerator alimentar(GameObject animal)
    {
        ocupado = true;
        animal.GetComponent<Animal>().alimentarTiempo(1.5f);
        yield return new WaitForSeconds(1.5f);
        ocupado = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.root.GetComponent<Animal>())
        {
            if (Input.GetKeyDown(KeyCode.E) && !collision.transform.root.GetComponent<Animal>().isOwned() && stats.checkifAvailableCasa())
            {
                Debug.Log("Adoptando");
                collision.transform.root.GetComponent<Animal>().setAdoptado(stats.adoptarMascota(collision.transform.root.gameObject));
            }
            if (Input.GetKeyDown(KeyCode.F) && collision.transform.root.GetComponent<Animal>().isOwned() && !ocupado && stats.checkifEnoughComida("Gato",300))
            {
                Debug.Log("F");
                stats.restarComida("Gato", 300);
                StartCoroutine(alimentar(collision.transform.root.gameObject));
            }
            if (Input.GetKeyDown(KeyCode.P) && collision.transform.root.GetComponent<Animal>().isOwned() && !ocupado)
            {
                Debug.Log("P");
                StartCoroutine(jugar(collision.transform.root.gameObject));
            }
        }
    }
}