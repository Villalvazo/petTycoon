using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private Dictionary<string, float> comida;
    class casa
    {
        private Vector3 posicion;
        private bool ocupado;
        private bool construido;
        private Animal animalito;
        private Stats stats;
        public casa(Vector3 _posicion, Stats _stats)
        {
            posicion = _posicion;
            stats = _stats;
        }
        public Vector3 getPosicion()
        {
            return posicion;
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
        public void construirCasa()
        {
            construido = true;
        }
        public void alimentar()
        {
            animalito.alimentar(20);
            stats.restarComida(animalito.tag, 20);
        }
    }
    private void Start()
    {
        comida = new Dictionary<string, float>();
        comida.Add("Gato", 0);
        comida.Add("Perro", 0);
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

}
