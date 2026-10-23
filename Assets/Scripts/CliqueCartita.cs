using UnityEngine;
using UnityEngine.EventSystems;



public class CliqueCarta : MonoBehaviour, IPointerClickHandler
{
    public SimuladorTurnos simulador;

    public int numeroJogador = 1;
    public int indiceCarta = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Cartita selecionadita");

        if (simulador == null)
        {
            Debug.LogError("cartita não selecionadita!");
            return;
        }

        if (numeroJogador == 1)
        {
            simulador.SelecionarCartaJogador1(indiceCarta);
        }
        else if (numeroJogador == 2)
        {
            simulador.SelecionarCartaJogador2(indiceCarta);
        }
    }
}

