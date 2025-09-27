using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonHandler : MonoBehaviour
{
    [Header("Atributos del país")]
    public int dueño; // 1 = servidor, 2 = cliente
    public int ID;
    public List<int> adyacentes = new List<int>();
    public string nombre;
    public int tropas;
    public string continente;

    private Button boton;

    void Awake()
    {
        boton = GetComponent<Button>();
    }

    public void Configurar(int _dueño, int _ID, string _nombre, string _continente, int _tropasIniciales, List<int> _adyacentes)
    {
        dueño = _dueño;
        ID = _ID;
        nombre = _nombre;
        continente = _continente;
        tropas = _tropasIniciales;
        adyacentes = new List<int>(_adyacentes);
    }
}
