using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonListo : MonoBehaviour
{
    public Button boton;
    public bool derecho = false; // true = derecho (cliente), false = izquierdo (servidor)

    void Start()
    {
        boton.onClick.AddListener(OnClick);
    }

    void Update()
    {
        // Solo activo si es turno correcto y es el rol correcto
        bool puedeActivar = (!derecho && GameManager.Instance.turno == 1 && GameManager.Instance.esServidor) ||
                            (derecho && GameManager.Instance.turno == 2 && !GameManager.Instance.esServidor);
        boton.interactable = puedeActivar;
    }

    void OnClick()
    {
        // Validaciones b�sicas y verificaci�n de turno/rol
        bool aliasCorrecto = derecho ? GameManager.Instance.jugador2 != null : GameManager.Instance.jugador1 != null;
        bool colorCorrecto = derecho ? GameManager.Instance.color2 != 0 : GameManager.Instance.color1 != 0;

        bool rolCorrecto = (!derecho && GameManager.Instance.turno == 1 && GameManager.Instance.esServidor) ||
                            (derecho && GameManager.Instance.turno == 2 && !GameManager.Instance.esServidor);

        if (!aliasCorrecto || !colorCorrecto || !rolCorrecto) return; // No hacer nada si falta info o rol incorrecto

        if (!derecho) // izquierdo = servidor
        {
            GameManager.Instance.CambiarTurno();
            Debug.Log("Listo lado izquierdo: turno cambiado");
        }
        else // derecho = cliente
        {
            // Verificar que jugador2 y color2 no coincidan con jugador1 y color1
            if (GameManager.Instance.jugador2 == GameManager.Instance.jugador1 ||
                GameManager.Instance.color2 == GameManager.Instance.color1)
            {
                Debug.Log("Alias o color coinciden, no se puede iniciar");
                return;
            }

            GameManager.Instance.CambiarTurno();
            Debug.Log("Listo lado derecho: abriendo escena del juego");

            SceneManager.LoadScene("GameScene");
            GameManager.Instance.DistribuirTerritorios();
        }
    }
}

