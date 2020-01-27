using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public Button[] botones;
    public Text textoDinero;
    public Stats gameManager;
    public GameObject menuCompras;
    public Text fecha;

    private void Start()
    {
        botones[0].onClick.AddListener(() => btn_ComprarComida("Gato", 1000, 30));
        botones[1].onClick.AddListener(() => btn_ComprarComida("Perro", 1000, 30));
    }
    public void btn_ComprarCasa(float _precio)
    {
        if(gameManager.canBuy(_precio)){
            gameManager.comprarCasa(_precio);
        }
    }
    public void btn_ComprarComida(string _tipo, int _cantidad, float _precio)
    {
        if (gameManager.canBuy(_precio))
        {
            gameManager.comprarComida(_tipo, _cantidad,_precio);
        }
    }
    public void setDinero(float _cantidad)
    {
        textoDinero.text = _cantidad.ToString();
    }
    public void cambiarFecha(string _string)
    {
        fecha.text = _string;
    }
}
