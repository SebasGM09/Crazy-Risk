using UnityEngine;
using UnityEngine.UI;

public class InputAlias : MonoBehaviour
{
    public InputField input;
    public bool derecho = false; // true = derecho, false = izquierdo

    void Start()
    {
        input.onEndEdit.AddListener(OnEndEdit);
    }

    void Update()
    {
        // Solo editable si turno correcto
        bool editable = (!derecho && GameManager.Instance.turno == 1) || (derecho && GameManager.Instance.turno == 2);
        input.interactable = editable;
    }

    void OnEndEdit(string texto)
    {
        if ((!derecho && GameManager.Instance.turno == 1) || (derecho && GameManager.Instance.turno == 2))
        {
            if (!derecho) GameManager.Instance.jugador1 = texto;
            else GameManager.Instance.jugador2 = texto;

            Debug.Log((!derecho ? "Servidor" : "Cliente") + " ingresó alias: " + texto);
        }
    }
}
