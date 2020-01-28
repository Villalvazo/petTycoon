using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animal : MonoBehaviour
{
    public Image felicidadBar, hambreBar;
    public SpriteRenderer renderer;
    public Sprite idle, triste, feliz, hambriento, enojado;
    public enum estadoAnimal { Idle, triste, hambriento, enojado, feliz };

    private estadoAnimal miEstado;
    private bool owned;
    private bool petHouse;
    public float maxFelicidad, maxHambre;
    public float minHambre, minFelicidad, minTristeza, minIra;
    private float felicidad, hambre;

    private audioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        owned = false;
        miEstado = estadoAnimal.triste;
        audioManager = FindObjectOfType<audioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (owned)
        {
            felicidad -= 0.7f * Time.deltaTime;
            hambre -= 0.5f * Time.deltaTime;
            //felicidad -= 0.5f;
            if (hambre < minHambre)
            {
                felicidad -= 1.3f * Time.deltaTime;
            }
            else
            {
                felicidad += 0.5f * Time.deltaTime;
                if (felicidad > maxFelicidad)
                {
                    felicidad = maxFelicidad;
                }
            }
            switch (miEstado)
            {
                case estadoAnimal.feliz:
                    if (hambre < minHambre) { changeEstado(estadoAnimal.hambriento); }
                    if (felicidad < minFelicidad) { changeEstado(estadoAnimal.Idle); }
                    break;
                case estadoAnimal.Idle:
                    if (hambre < minHambre) { changeEstado(estadoAnimal.hambriento); }
                    if (felicidad < minTristeza) { changeEstado(estadoAnimal.triste); }
                    else if (felicidad > minFelicidad) { changeEstado(estadoAnimal.feliz); }
                    break;
                case estadoAnimal.triste:
                    if (felicidad < minIra) { changeEstado(estadoAnimal.enojado); }
                    else if (felicidad > minFelicidad) { changeEstado(estadoAnimal.Idle); }
                    break;
                case estadoAnimal.enojado:
                    if (hambre <= 0 || felicidad <= 0)
                    {
                        runAway();
                    }
                    else if (felicidad > minIra) { changeEstado(estadoAnimal.triste); }
                    break;
                case estadoAnimal.hambriento:
                    if (hambre <= 0 || felicidad<=0)
                    {
                        runAway();
                    }
                    if (hambre > minHambre) { changeEstado(estadoAnimal.Idle); }
                    if (felicidad < minIra) { changeEstado(estadoAnimal.enojado); }
                    break;
                default:
                    changeEstado(estadoAnimal.Idle);
                    break;
            }
            felicidadBar.fillAmount = felicidad / maxFelicidad;
            hambreBar.fillAmount = hambre / maxHambre;
        }
    }
    private void runAway()
    {
        FindObjectOfType<Stats>().desalojarCasa(gameObject);
        FindObjectOfType<Stats>().animalMenos(gameObject);
        //gameObject.SetActive(false);
    }
    private void changeEstado(estadoAnimal _estado)
    {
        miEstado = _estado;
        switch (_estado)
        {
            case estadoAnimal.feliz:
                audioManager.Play("CatMeow");
                renderer.sprite = feliz;
                break;
            case estadoAnimal.triste:
                audioManager.Play("CatMeow");
                renderer.sprite = triste;
                break;
            case estadoAnimal.hambriento:
                audioManager.Play("CatScream");
                renderer.sprite = hambriento;
                break;
            case estadoAnimal.enojado:
                audioManager.Play("CatScream");
                renderer.sprite = enojado;
                break;
            case estadoAnimal.Idle:
                audioManager.Play("CatMeow");
                renderer.sprite = idle;
                break;
            default:
                miEstado = estadoAnimal.Idle;
                audioManager.Play("CatMeow");
                renderer.sprite = idle;
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
        felicidad = maxFelicidad;
        hambre = maxHambre;
        changeEstado(estadoAnimal.feliz);
        transform.position = _posicion;
    }
    public void asignarCasa()
    {
        petHouse = true;
    }
    public void desAsignarCasa()
    {
        petHouse = false;
    }
    public void alimentar(float _cantidad)
    {
        hambre += _cantidad;
        if (hambre > maxHambre)
        {
            hambre = maxHambre;
        }
    }
    public void alimentarTiempo(float _tiempo)
    {
        StartCoroutine(alimentarCorutina(_tiempo));
    }
    public void recibirFelicidadTiempo(float _tiempo)
    {
        StartCoroutine(recibirFelicidadCorutina(_tiempo));
    }
    public void recibirFelicidad(float _cantidad)
    {
        felicidad += _cantidad;
        if (felicidad > maxFelicidad)
        {
            felicidad = maxFelicidad;
        }
    }
    private IEnumerator recibirFelicidadCorutina(float _tiempo)
    {
        float tiempoInicial = Time.time;
        do
        {
            felicidad += 1.5f;
            if (felicidad > maxFelicidad)
            {
                felicidad = maxFelicidad;
            }
            yield return new WaitForSeconds(0.04f);
        } while (Time.time - (tiempoInicial + _tiempo) < 0);
    }
    private IEnumerator alimentarCorutina(float _tiempo)
    {
        float tiempoInicial = Time.time;
        do
        {
            hambre += 1.5f;
            if (hambre > maxHambre)
            {
                hambre = maxHambre;
            }
            yield return new WaitForSeconds(0.04f);
        } while (Time.time - (tiempoInicial + _tiempo) < 0);
    }
}
