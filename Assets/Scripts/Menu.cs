using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public Button[] botones;
    public Text textoDinero;
    public Stats gameManager;
    public GameObject menuCompras;
    public Text fecha, comidaGato,comidaPerro;
    public bool paused;
    public GameObject[] menus;
    private void Start()
    {
        paused = false;
        botones[0].onClick.AddListener(() => btn_ComprarComida("Gato", 1000, 30));
        botones[1].onClick.AddListener(() => btn_ComprarComida("Perro", 1000, 30));
    }
    public void btn_ComprarCasa(float _precio)
    {
        if(gameManager.canBuy(_precio)){
            gameManager.comprarCasa(_precio);
            FindObjectOfType<audioManager>().Play("Dinero");
        }
    }
    public void btn_ComprarComida(string _tipo, int _cantidad, float _precio)
    {
        if (gameManager.canBuy(_precio))
        {
            gameManager.comprarComida(_tipo, _cantidad,_precio);
            FindObjectOfType<audioManager>().Play("Dinero");
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
    public void setTienda(bool _enabled)
    {
        menuCompras.SetActive(_enabled);
    }
    public void setComidaGato(int _cantidad)
    {
        if (_cantidad >= 1000)
        {
            comidaGato.text = _cantidad / 1000 + "Kg";
        }
        else
        {
            comidaGato.text = _cantidad + "g";
        }
    }
    public void setComidaPerro(int _cantidad)
    {
        if (_cantidad >= 1000)
        {
            comidaPerro.text = _cantidad / 1000 + "Kg";
        }
        else
        {
            comidaPerro.text = _cantidad + "g";
        }
    }
    public void gameOver()
    {
        menus[0].SetActive(false);
        menus[2].SetActive(true);
        Time.timeScale = 0;
    }
    public void pausar()
    {
        paused = !paused;
        if (paused)
        {
            menus[0].SetActive(false);
            menus[1].SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            menus[0].SetActive(true);
            menus[1].SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void btn_BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}