using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public GameObject tileActivable;
    private bool activo;
    public bool trabajo;
    private void Start()
    {
        activo = true;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        activo = !activo;
        tileActivable.SetActive(activo);
        if (trabajo && collision.GetComponent<player>())
        {
            GameObject.Find("GameManager").GetComponent<Stats>().obtenerDinero(100);
        }
    }
}
