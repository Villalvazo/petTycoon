using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public GameObject tileActivable;
    private bool activo;
    public bool trabajo,tienda;
    public Vector3 vectorDesplazamientoPuerta;
    public Menu menu;
    private void Start()
    {
        activo = true;
        menu = GameObject.Find("Canvas").GetComponent<Menu>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (trabajo)
        {
            GameObject.Find("GameManager").GetComponent<Stats>().obtenerDinero(100);
            GameObject.Find("GameManager").GetComponent<Stats>().pasarDia();
            collision.gameObject.transform.position += vectorDesplazamientoPuerta;
        }
        else
        {
            if (tienda)
            {
                menu.setTienda(activo);
            }
            activo = !activo;
            tileActivable.SetActive(activo);
        }
    }
}
