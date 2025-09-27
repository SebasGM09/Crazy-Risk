using UnityEngine;
using UnityEngine.UI;

public class BotonColor : MonoBehaviour
{
    public Button boton;
    public bool derecho = false; // true = derecho, false = izquierdo
    public int color = 1; // 1 = rojo, 2 = verde, 3 = azul

    void Start()
    {
        boton.onClick.AddListener(OnClick);
    }

    void Update()
    {
        // Solo activo si es turno correcto y lado izquierdo (servidor = izquierdo)
        bool puedeElegir = (!derecho && GameManager.Instance.turno == 1) || (derecho && GameManager.Instance.turno == 2);
        boton.interactable = puedeElegir;
    }

    void OnClick()
    {
        // Solo procesa si lado izquierdo y turno correcto
        if ((!derecho && GameManager.Instance.turno == 1) || (derecho && GameManager.Instance.turno == 2))
        {
            if (!derecho) // izquierdo = servidor
            {
                GameManager.Instance.color1 = color;
                Debug.Log("Servidor seleccionó color: " + color);
            }
            else // derecho = cliente
            {
                GameManager.Instance.color2 = color;
                Debug.Log("Cliente seleccionó color: " + color);
            }
        }
    }
}

