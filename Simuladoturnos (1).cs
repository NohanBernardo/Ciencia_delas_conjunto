using UnityEngine;

using System.Collections.Generic;

public class SimuladorTurnos : MonoBehaviour
{
    [Header("Cartas dos Jogadores")]
    public GameObject[] cartasJogador1 = new GameObject[4];
    public GameObject[] cartasJogador2 = new GameObject[4];

    [Header("Controle de Rodadas")]
    public int rodadaAtual = 1;

    public enum Fase
    {
        Inicio,
        Planejamento,
        Execucao,
        Ameacas,
        Fim
    }

    public Fase faseAtual = Fase.Inicio;

    [Header("Recursos")]
    public int saude = 10;
    public int pesquisa = 5;
    public int infraestrutura = 5;

    [Header("Conhecimento")]
    public int conhecimentoJogador1 = 3;
    public int conhecimentoJogador2 = 3;

    [Header("Confirmacoes")]
    public bool jogador1Confirmou = false;
    public bool jogador2Confirmou = false;

    [Header("Estado da Partida")]
    public bool partidaTerminou = false;

    [Header("Cartas Selecionadas")]
    public int quantidadeCartasJogador1 = 0;
    public int quantidadeCartasJogador2 = 0;

    private bool[] selecionadasJogador1;
    private bool[] selecionadasJogador2;

    private void Start()
    {
        PrepararPartida();
        IniciarRodada();
    }

    // PREPARAR PARTIDA
    public void PrepararPartida()
    {
        rodadaAtual = 1;

        saude = 10;
        pesquisa = 5;
        infraestrutura = 5;

        conhecimentoJogador1 = 3;
        conhecimentoJogador2 = 3;

        jogador1Confirmou = false;
        jogador2Confirmou = false;

        partidaTerminou = false;

        quantidadeCartasJogador1 = 0;
        quantidadeCartasJogador2 = 0;

        selecionadasJogador1 = new bool[cartasJogador1.Length];
        selecionadasJogador2 = new bool[cartasJogador2.Length];

        faseAtual = Fase.Inicio;

        Debug.Log("Partida preparada!");
    }

    // INICIAR RODADA
    public void IniciarRodada()
    {
        if (partidaTerminou || faseAtual != Fase.Inicio)
            return;

        conhecimentoJogador1 = 3;
        conhecimentoJogador2 = 3;

        jogador1Confirmou = false;
        jogador2Confirmou = false;

        quantidadeCartasJogador1 = 0;
        quantidadeCartasJogador2 = 0;

        selecionadasJogador1 = new bool[cartasJogador1.Length];
        selecionadasJogador2 = new bool[cartasJogador2.Length];

        faseAtual = Fase.Planejamento;

        Debug.Log("Rodada " + rodadaAtual + " iniciada!");
        Debug.Log("Fase atual: " + faseAtual);

        AtualizarInterface();
    }

    // SELECIONAR CARTA JOGADOR 1
    public void SelecionarCartaJogador1(int indiceCarta)
    {
        if (partidaTerminou || faseAtual != Fase.Planejamento)
            return;

        if (jogador1Confirmou)
            return;

        if (indiceCarta < 0 || indiceCarta >= cartasJogador1.Length)
            return;

        GameObject carta = cartasJogador1[indiceCarta];

        if (carta == null)
            return;

        if (selecionadasJogador1[indiceCarta])
            return;

        if (quantidadeCartasJogador1 >= 3)
        {
            Debug.Log("Jogador 1 já selecionou 3 cartas!");
            return;
        }

        selecionadasJogador1[indiceCarta] = true;
        quantidadeCartasJogador1++;

        Debug.Log("Jogador 1 selecionou: " + carta.name);
    }

    // SELECIONAR CARTA JOGADOR 2
    public void SelecionarCartaJogador2(int indiceCarta)
    {
        if (partidaTerminou || faseAtual != Fase.Planejamento)
            return;

        if (jogador2Confirmou)
            return;

        if (indiceCarta < 0 || indiceCarta >= cartasJogador2.Length)
            return;

        GameObject carta = cartasJogador2[indiceCarta];

        if (carta == null)
            return;

        if (selecionadasJogador2[indiceCarta])
            return;

        if (quantidadeCartasJogador2 >= 3)
        {
            Debug.Log("Jogador 2 já selecionou 3 cartas!");
            return;
        }

        selecionadasJogador2[indiceCarta] = true;
        quantidadeCartasJogador2++;

        Debug.Log("Jogador 2 selecionou: " + carta.name);
    }

    // DESMARCAR CARTA JOGADOR 1
    public void DesmarcarCartaJogador1(int indiceCarta)
    {
        if (partidaTerminou || faseAtual != Fase.Planejamento)
            return;

        if (jogador1Confirmou)
            return;

        if (indiceCarta < 0 || indiceCarta >= selecionadasJogador1.Length)
            return;

        if (!selecionadasJogador1[indiceCarta])
            return;

        selecionadasJogador1[indiceCarta] = false;
        quantidadeCartasJogador1--;

        Debug.Log("Jogador 1 desmarcou a carta " + indiceCarta);
    }

    // DESMARCAR CARTA JOGADOR 2
    public void DesmarcarCartaJogador2(int indiceCarta)
    {
        if (partidaTerminou || faseAtual != Fase.Planejamento)
            return;

        if (jogador2Confirmou)
            return;

        if (indiceCarta < 0 || indiceCarta >= selecionadasJogador2.Length)
            return;

        if (!selecionadasJogador2[indiceCarta])
            return;

        selecionadasJogador2[indiceCarta] = false;
        quantidadeCartasJogador2--;

        Debug.Log("Jogador 2 desmarcou a carta " + indiceCarta);
    }

    // BOTÃO CONFIRMAR JOGADOR 1
    public void ConfirmarJogador1()
    {
        if (partidaTerminou || faseAtual != Fase.Planejamento)
            return;

        if (jogador1Confirmou)
            return;

        jogador1Confirmou = true;

        Debug.Log("Jogador 1 confirmou!");

        VerificarConfirmacoes();
    }

    // BOTÃO CONFIRMAR JOGADOR 2
    public void ConfirmarJogador2()
    {
        if (partidaTerminou || faseAtual != Fase.Planejamento)
            return;

        if (jogador2Confirmou)
            return;

        jogador2Confirmou = true;

        Debug.Log("Jogador 2 confirmou!");

        VerificarConfirmacoes();
    }

    // VERIFICAR CONFIRMAÇÕES
    private void VerificarConfirmacoes()
    {
        if (jogador1Confirmou && jogador2Confirmou)
        {
            Debug.Log("Ambos confirmaram! Iniciando execução.");

            ExecutarJogadas();
        }
    }

    // EXECUTAR JOGADAS
    public void ExecutarJogadas()
    {
        if (partidaTerminou || faseAtual != Fase.Planejamento)
            return;

        if (!jogador1Confirmou || !jogador2Confirmou)
        {
            Debug.Log("Aguardando os dois jogadores!");
            return;
        }

        faseAtual = Fase.Execucao;

        Debug.Log("Iniciando execução das cartas!");

        // EXECUTAR CARTAS DO JOGADOR 1
        for (int i = 0; i < cartasJogador1.Length; i++)
        {
            if (!selecionadasJogador1[i])
                continue;

            GameObject carta = cartasJogador1[i];

            if (carta == null)
                continue;

            CartaAcao acao = carta.GetComponent<CartaAcao>();

            if (acao != null)
            {
                Debug.Log("Jogador 1 executou: " + carta.name);

                acao.Executar(this);

                VerificarDerrota();

                if (partidaTerminou)
                {
                    AtualizarInterface();
                    return;
                }
            }
            else
            {
                Debug.LogWarning(
                    "A carta " + carta.name +
                    " não possui o componente CartaAcao!"
                );
            }
        }

        // EXECUTAR CARTAS DO JOGADOR 2
        for (int i = 0; i < cartasJogador2.Length; i++)
        {
            if (!selecionadasJogador2[i])
                continue;

            GameObject carta = cartasJogador2[i];

            if (carta == null)
                continue;

            CartaAcao acao = carta.GetComponent<CartaAcao>();

            if (acao != null)
            {
                Debug.Log("Jogador 2 executou: " + carta.name);

                acao.Executar(this);

                VerificarDerrota();

                if (partidaTerminou)
                {
                    AtualizarInterface();
                    return;
                }
            }
            else
            {
                Debug.LogWarning(
                    "A carta " + carta.name +
                    " não possui o componente CartaAcao!"
                );
            }
        }

        Debug.Log("Execução das cartas finalizada!");

        Debug.Log("Saúde: " + saude);
        Debug.Log("Pesquisa: " + pesquisa);
        Debug.Log("Infraestrutura: " + infraestrutura);

        faseAtual = Fase.Ameacas;

        Debug.Log("Fase atual: " + faseAtual);

        AtualizarInterface();
    }

    // RESOLVER AMEAÇAS
    public void ResolverAmeacas()
    {
        if (partidaTerminou || faseAtual != Fase.Ameacas)
            return;

        Debug.Log("Resolvendo ameaças!");

        // TODO: Implementar ameaças.
    }

    // FINALIZAR RODADA
    public void FinalizarRodada()
    {
        if (partidaTerminou || faseAtual != Fase.Ameacas)
            return;

        VerificarDerrota();

        if (partidaTerminou)
            return;

        // TODO: Verificar se as ameaças foram resolvidas.

        rodadaAtual++;

        faseAtual = Fase.Inicio;

        IniciarRodada();
    }

    // VERIFICAR DERROTA
    public void VerificarDerrota()
    {
        if (partidaTerminou)
            return;

        if (saude <= 0 || pesquisa <= 0 || infraestrutura <= 0)
        {
            partidaTerminou = true;
            faseAtual = Fase.Fim;

            Debug.Log("DERROTA! Um recurso chegou a zero!");

            AtualizarInterface();
        }
    }

    // ATUALIZAR INTERFACE
    public void AtualizarInterface()
    {
        // TODO: Atualizar textos e botões da Unity.
    }

    // REINICIAR PARTIDA
    public void Reiniciar()
    {
        PrepararPartida();
        IniciarRodada();

        Debug.Log("Partida reiniciada!");
    }

    // TESTAR CARTAS PELO INSPECTOR
    [ContextMenu("Testar Carta 1 do Jogador 1")]
    public void TestarCartaJogador1()
    {
        SelecionarCartaJogador1(0);
    }

    [ContextMenu("Testar Carta 1 do Jogador 2")]
    public void TestarCartaJogador2()
    {
        SelecionarCartaJogador2(0);
    }
}