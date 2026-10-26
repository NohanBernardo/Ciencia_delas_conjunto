using UnityEngine;
using UnityEngine.EventSystems;



using UnityEngine;
using UnityEngine.EventSystems;

public class CliqueCarta : MonoBehaviour, IPointerClickHandler
{
    public SimuladorTurnos simulador;

    public int numeroJogador = 1;
    public int indiceCarta = 0;

    private bool estaSelecionada = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (simulador == null)
        {
            Debug.LogError("Simulador não conectado à carta!");
            return;
        }

        // Só permite selecionar durante o planejamento
        if (simulador.faseAtual != SimuladorTurnos.Fase.Planejamento)
            return;

        // Não permite alterar cartas após a confirmação
        if (numeroJogador == 1 && simulador.jogador1Confirmou)
            return;

        if (numeroJogador == 2 && simulador.jogador2Confirmou)
            return;

        if (numeroJogador != 1 && numeroJogador != 2)
        {
            Debug.LogWarning("Número do jogador inválido!");
            return;
        }

        if (!estaSelecionada)
        {
            // SELECIONAR CARTA
            if (numeroJogador == 1)
            {
                int quantidadeAntes = simulador.quantidadeCartasJogador1;

                simulador.SelecionarCartaJogador1(indiceCarta);

                estaSelecionada =
                    simulador.quantidadeCartasJogador1 > quantidadeAntes;
            }
            else
            {
                int quantidadeAntes = simulador.quantidadeCartasJogador2;

                simulador.SelecionarCartaJogador2(indiceCarta);

                estaSelecionada =
                    simulador.quantidadeCartasJogador2 > quantidadeAntes;
            }

            if (estaSelecionada)
                Debug.Log("Carta selecionada!");
        }
        else
        {
            // DESMARCAR CARTA
            if (numeroJogador == 1)
            {
                simulador.DesmarcarCartaJogador1(indiceCarta);
            }
            else
            {
                simulador.DesmarcarCartaJogador2(indiceCarta);
            }

            estaSelecionada = false;

            Debug.Log("Carta desmarcada!");
        }
    }
}


