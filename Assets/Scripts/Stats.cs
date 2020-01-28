using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public GameObject[] slotsCasas;
    private Dictionary<string, int> comida;
    private float dinero;
    private List<casa> listaCasas;
    public Menu menu;
    private DateTime fecha;
    public GameObject[] animales;
    class casa
    {
        private int slot;
        private bool ocupado;
        private GameObject animalito;
        private Stats stats;
        public casa(int _slot, Stats _stats)
        {
            slot = _slot;
            stats = _stats;
            ocupado = false;
        }
        public int getSlot()
        {
            return slot;
        }
        public bool getOcupado()
        {
            return ocupado;
        }
        public void asignar(GameObject _animalito)
        {
            ocupado = true;
            animalito = _animalito;
            animalito.GetComponent<Animal>().asignarCasa();
        }
        public void desAsignar()
        {
            ocupado = false;
        }
        public void alimentar(float _cantidad)
        {
            animalito.GetComponent<Animal>().alimentar(_cantidad);
        }
        public bool compararAnimal(GameObject _animal)
        {
            if (animalito == null) { return false; }
            if (animalito == _animal)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    private void Start()
    {
        comida = new Dictionary<string, int>();
        comida.Add("Gato", 0);
        comida.Add("Perro", 0);
        dinero = 20.0f;
        listaCasas = new List<casa>();
        fecha = DateTime.Now;
    }

    public void agregarTipoDeComida(string _tipo, int _cantidad)
    {
        comida.Add(_tipo, _cantidad);
    }
    public void agregarComida(string _tipo, int _cantidad)
    {
        try
        {
            comida[_tipo] += _cantidad;
            if (_tipo == "Gato")
            {
                menu.setComidaGato(comida[_tipo]);
            }
            else
            {
                menu.setComidaPerro(comida[_tipo]);
            }
        }
        catch (Exception e)
        {

        }
    }
    public bool checkifEnoughComida(string _tipo, float _cantidad)
    {
        try
        {
            if(comida[_tipo] - _cantidad > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception");
            return false;
        }
    }
    public void restarComida(string _tipo, int _cantidad)
    {
        try
        {
            comida[_tipo] -= _cantidad;
            if (_tipo == "Gato")
            {
                menu.setComidaGato(comida[_tipo]);
            }
            else
            {
                menu.setComidaPerro(comida[_tipo]);
            }
        }
        catch (Exception e)
        {

        }
    }

    public void comprarCasa(float _precio)
    {
        if (listaCasas.Count < slotsCasas.Length)
        {
            tryToBuy(_precio);
            slotsCasas[listaCasas.Count].SetActive(true);
            listaCasas.Add(new casa(listaCasas.Count,this));            
        }
    }
    public void comprarComida(string _tipo, int _cantidad, float _precio)
    {
        tryToBuy(_precio);
        agregarComida(_tipo, _cantidad);
    }
    public bool canBuy(float _precio)
    {
        if (dinero >= _precio)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void tryToBuy(float _precio)
    {
        if (dinero >= _precio)
        {
            dinero -= _precio;
            menu.setDinero(dinero);
        }
    }
    public void pasarDia()
    {
        Debug.Log("Fecha antes: " + fecha);
        fecha=fecha.AddDays(1);
        Debug.Log("Fecha despues: " + fecha);
        menu.cambiarFecha(fecha.Day+"/"+fecha.Month+"/"+fecha.Year);
    }
    public void obtenerDinero(float _cantidad)
    {
        dinero += _cantidad;
        menu.setDinero(dinero);
    }
    public bool checkifAvailableCasa()
    {
        for(int i=0;i<listaCasas.Count;i++)
        {
            if (!listaCasas[i].getOcupado())
            {
                return true;
            }
        }
        return false;
    }
    public Vector3 adoptarMascota(GameObject _animal)
    {
        for (int i = 0; i < listaCasas.Count; i++)
        {
            if (!listaCasas[i].getOcupado())
            {
                listaCasas[i].asignar(_animal);
                return slotsCasas[i].transform.position+Vector3.right*0.3f;
            }
        }
        return Vector3.zero;
    }
    public void desalojarCasa(GameObject _animal)
    {
        for (int i = 0; i < listaCasas.Count; i++)
        {
            if (listaCasas[i].compararAnimal(_animal))
            {
                listaCasas[i].desAsignar();
                return;
            }
        }
    }
    public void animalMenos(GameObject _animal)
    {
        _animal.SetActive(false);
        bool bandera = false;
        for(int i=0;i<animales.Length;i++)
        {
            if (animales[i].activeSelf)
            {
                bandera = true;
            }
        }
        if (!bandera)
        {
            menu.gameOver();
        }
    }
}
