using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject[] menus;
    private int actualMenu=0;

    public void btn_Jugar()
    {
        menus[0].SetActive(false);
        menus[1].SetActive(true);
        actualMenu = 1;
    }
    public void btn_Creditos()
    {
        menus[0].SetActive(false);
        menus[2].SetActive(true);
        actualMenu = 2;
    }
    public void btn_Instructivo()
    {
        menus[0].SetActive(false);
        menus[3].SetActive(true);
        actualMenu = 3;
    }
    public void btn_Salir()
    {
        Application.Quit();
    }
    public void btn_JugarLvl1()
    {
        SceneManager.LoadScene(1);
    }
    public void btn_JugarLvl2()
    {
        SceneManager.LoadScene(2);
    }
    public void btn_Back()
    {
        menus[actualMenu].SetActive(false);
        menus[0].SetActive(true);
        actualMenu = 0;
    }
}
