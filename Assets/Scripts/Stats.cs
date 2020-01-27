using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public GameObject[] slotsCasas;
    private Dictionary<string, float> comida;
    private float dinero;
    private List<casa> listaCasas;
    public Menu menu;
    private DateTime fecha;
    class casa
    {
        private int slot;
        private bool ocupado;
        private Animal animalito;
        private Stats stats;
        public casa(int _slot, Stats _stats)
        {
            slot = _slot;
            stats = _stats;
        }
        public int getSlot()
        {
            return slot;
        }
        public bool getOcupado()
        {
            return ocupado;
        }
        public void asignar(Animal _animalito)
        {
            ocupado = true;
            animalito = _animalito;
            animalito.asignarCasa();
        }
        public void desAsignar()
        {
            ocupado = false;
        }
        public void alimentar(float _cantidad)
        {
            animalito.alimentar(_cantidad);
        }
    }
    private void Start()
    {
        comida = new Dictionary<string, float>();
        comida.Add("Gato", 0);
        comida.Add("Perro", 0);
        dinero = 0.0f;
        listaCasas = new List<casa>();
        fecha = DateTime.Now;
    }

    public void agregarTipoDeComida(string _tipo, float _cantidad)
    {
        comida.Add(_tipo, _cantidad);
    }
    public void agregarComida(string _tipo, float _cantidad)
    {
        try
        {
            comida[_tipo] += _cantidad;
        }
        catch (Exception e)
        {

        }
    }
    public void restarComida(string _tipo, float _cantidad)
    {
        try
        {
            comida[_tipo] -= _cantidad;
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
}
