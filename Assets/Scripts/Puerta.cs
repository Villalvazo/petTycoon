using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public GameObject tileActivable;
    private bool activo;

    private void Start()
    {
        activo = true;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        activo = !activo;
        tileActivable.SetActive(activo);
    }
}
