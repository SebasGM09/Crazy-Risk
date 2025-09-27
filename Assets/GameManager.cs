using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Alias y colores (menú)")]
    public string jugador1; // alias del servidor
    public string jugador2; // alias del cliente
    public int color1 = 0;  // 1=rojo, 2=verde, 3=azul
    public int color2 = 0;  // 1=rojo, 2=verde, 3=azul

    [Header("Turno y rol local")]
    public int turno = 1;
    public bool esServidor = true;

    [Header("Fase actual del juego")]
    public int fase = 1;

    public int tropasReforzar1 = 26; // servidor
    public int tropasReforzar2 = 26; // cliente

    [Header("Grupos de países conectados con el mismo dueño")]
    public List<List<int>> gruposConectados = new List<List<int>>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CambiarTurno()
    {
        turno = (turno == 1) ? 2 : 1;
        Debug.Log("Turno cambiado. Ahora turno = " + turno);
    }

    /// <summary>
    /// Distribuye los 42 territorios entre servidor (1), cliente (2) y neutro (3).
    /// </summary>
    public void DistribuirTerritorios()
    {
        List<int> ids = Enumerable.Range(1, 42).ToList();
        System.Random rng = new System.Random();
        ids = ids.OrderBy(x => rng.Next()).ToList();

        BotonHandler[] paises = FindObjectsOfType<BotonHandler>();

        // Asignar dueño 1 (servidor)
        for (int i = 0; i < 14; i++)
        {
            var pais = paises.FirstOrDefault(p => p.ID == ids[i]);
            if (pais != null)
            {
                pais.dueño = 1;
                pais.tropas = 1;
            }
        }

        // Asignar dueño 2 (cliente)
        for (int i = 14; i < 28; i++)
        {
            var pais = paises.FirstOrDefault(p => p.ID == ids[i]);
            if (pais != null)
            {
                pais.dueño = 2;
                pais.tropas = 1;
            }
        }

        // Asignar dueño 3 (neutro)
        List<BotonHandler> neutros = new List<BotonHandler>();
        for (int i = 28; i < 42; i++)
        {
            var pais = paises.FirstOrDefault(p => p.ID == ids[i]);
            if (pais != null)
            {
                pais.dueño = 3;
                pais.tropas = 1; // mínimo 1 tropa
                neutros.Add(pais);
            }
        }

        // Repartir tropas restantes del ejército neutro (40 - 14 = 26)
        int tropasRestantes = 40 - neutros.Count;
        while (tropasRestantes > 0)
        {
            var paisAleatorio = neutros[rng.Next(neutros.Count)];
            paisAleatorio.tropas++;
            tropasRestantes--;
        }

        Debug.Log("Territorios distribuidos correctamente, incluyendo ejército neutro");
    }
}
