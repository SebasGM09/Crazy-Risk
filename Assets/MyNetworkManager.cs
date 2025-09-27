using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

public class MyNetworkManager : MonoBehaviour
{
    public static MyNetworkManager Instance;

    private TcpListener server;
    private TcpClient client;
    private StreamReader reader;
    private StreamWriter writer;

    // IP y puerto por defecto
    public string serverIP = "127.0.0.1";
    public int port = 5000;

    // Saber si esta máquina es servidor
    public bool esServidor = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistir entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ---------------- SERVIDOR ----------------
    public void IniciarServidor()
    {
        server = new TcpListener(IPAddress.Any, port);
        server.Start();
        Debug.Log("Servidor iniciado en puerto " + port);

        // Aceptar cliente de forma asíncrona
        server.AcceptTcpClientAsync().ContinueWith(t =>
        {
            client = t.Result;
            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
            EscucharMensajes();
        });
    }

    // ---------------- CLIENTE ----------------
    public void ConectarCliente(string ip)
    {
        client = new TcpClient(ip, port);
        reader = new StreamReader(client.GetStream());
        writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
        EscucharMensajes();
    }

    // ---------------- ESCUCHAR MENSAJES ----------------
    private void EscucharMensajes()
    {
        new Thread(() =>
        {
            while (true)
            {
                try
                {
                    string mensaje = reader.ReadLine();
                    if (mensaje != null && mensaje.StartsWith("TURN"))
                    {
                        int nuevoTurno = int.Parse(mensaje.Split(':')[1]);
                        GameManager.Instance.turno = nuevoTurno;
                        Debug.Log("Recibido turno: " + nuevoTurno);
                    }
                }
                catch
                {
                    Debug.Log("Conexión cerrada.");
                    break;
                }
            }
        }).Start();
    }

    // ---------------- ENVIAR TURNO ----------------
    public void EnviarTurno(int turno)
    {
        if (writer != null)
        {
            writer.WriteLine("TURN:" + turno);
            Debug.Log("Enviado turno: " + turno);
        }
    }
}
