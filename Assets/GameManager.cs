using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Alias y colores (menú)")]
    public string jugador1; // alias del servidor
    public string jugador2; // alias del cliente
    public int color1 = 0;  // 1=rojo, 2=verde, 3=azul
    public int color2 = 0;// 1=rojo, 2=verde, 3=azul

    [Header("Turno y rol local")]
    [Tooltip("1 = turno del servidor, 2 = turno del cliente")]
    public int turno = 1;

    [Tooltip("true = esta instancia es servidor, false = es cliente")]
    public bool esServidor = true;

    [Header("Fase actual del juego")]
    [Tooltip("1 = colocar tropas, 2 = atacar, 3 = reforzar")]
    public int fase = 1;

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

    /// <summary>
    /// Cambia el turno local (y si hay un NetworkManager lo notifica).
    /// </summary>
    public void CambiarTurno()
    {
        turno = (turno == 1) ? 2 : 1;

        // Si tienes un NetworkManager con método EnviarTurno, lo notificamos.
        // Cambia "MyNetworkManager" por el nombre real si lo tienes distinto.
        if (MyNetworkManager.Instance != null)
        {
            MyNetworkManager.Instance.EnviarTurno(turno);
        }

        Debug.Log("Turno cambiado. Ahora turno = " + turno + " (esServidor local = " + esServidor + ")");
    }
}
