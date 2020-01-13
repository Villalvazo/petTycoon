using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite triste, feliz;
    public enum estadoAnimal { Idle, triste, llorando, enojado, feliz};

    private estadoAnimal miEstado;
    private bool owned;
    private bool petHouse;

    private float minHambre,minFelicidad, maxHambre,maxFelicidad;
    private float felicidad, hambre;
    // Start is called before the first frame update
    void Start()
    {
        owned = false;
        miEstado = estadoAnimal.triste;
    }

    // Update is called once per frame
    void Update()
    {
        if (owned)
        {
            hambre -= 0.5f;
            //felicidad -= 0.5f;
            if (petHouse)
            {
                felicidad += 0.5f;
            }
            if (hambre < minHambre)
            {
                felicidad -= 1.3f;
            }
        }
    }
    private void changeEstado(estadoAnimal _estado)
    {
        miEstado = _estado;
        switch (_estado)
        {
            case estadoAnimal.feliz:
                renderer.sprite = feliz;
                break;
            case estadoAnimal.llorando:
                renderer.sprite = triste;
                break;
        }
    }
    public bool isOwned()
    {
        return owned;
    }
    public void setAdoptado(Vector2 _posicion)
    {
        owned = true;
        changeEstado(estadoAnimal.feliz);
        transform.position = _posicion;
    }
}
