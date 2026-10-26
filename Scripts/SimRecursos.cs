using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;


public class SimRecursos : MonoBehaviour
{
    public static SimRecursos Instance { get; private set; }
    public bool JogoDerrotando { get; private set; }

    // Valores do recurso Saúde
    private int saudeIni = 10, saudeMin = 0, saudeMax = 30;
    public int saudeAtual;

    // Valores do recurso Infraestrutura
    private int infraIni = 5, infraMin = 0, infraMax = 30;
    public int infraAtual;

    // Valores do recurso Pesquisa
    private int pesquisaIni = 5, pesquisaMin = 0, pesquisaMax = 30;
    public int pesquisaAtual;

    // Valores do recurso Conhecimento do Jogador 1 e 2
    // AJUSTE: 'conhecimentoMin' foi retirado daqui para sumir com o aviso (warning CS0414) do Unity
    private int conhecimentoIni = 3, conhecimentoMax = 3;
    public int conhecimentoAtualJ1;
    public int conhecimentoAtualJ2;

    // Texto da cena que será atualizado com UpdateVisuals()
    public TextMeshProUGUI saude, infraestrutura, pesquisa, conhecimentoJ1, conhecimentoJ2;
    private int bandwidth;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        saudeAtual = saudeIni;
        infraAtual = infraIni;
        pesquisaAtual = pesquisaIni;

        conhecimentoAtualJ1 = conhecimentoIni;
        conhecimentoAtualJ2 = conhecimentoIni;

        AtualizarInterface();
    }

    public void GanharRecurso(SimRecursos recurso, int quantidade)
    {
        if (JogoDerrotando) return;

        if (quantidade <= 0) return;

        if (recurso == this)
        {
            // Agora vai ser usada os tres recursos que são Saude Conhecimento Pesquisa Infrastetura 

            saudeAtual = Mathf.Min(saudeAtual + quantidade, saudeMax);
            infraAtual = Mathf.Min(infraAtual + quantidade, infraMax);

            // TODO: Ler qual recurso e qual quantidade foram escolhidos na interface.

            // TODO: Validar a entrada, aplicar o ganho e respeitar o teto de 30.
            // TODO: Atualizar os valores exibidos.

            // AJUSTE: Mudado de '=' para '+' para somar o recurso corretamente em vez de substituir
            pesquisaAtual = Mathf.Min(pesquisaAtual + quantidade, pesquisaMax);

        }
        // Agora vai Atulizar interface do jogo 
        AtualizarInterface();


    }

    public void PerderRecurso()
    {
        if (JogoDerrotando) return;
        saudeAtual = Mathf.Max(saudeAtual - 1, saudeMin);
        pesquisaAtual = Mathf.Max(pesquisaAtual - 1, pesquisaMin);
        infraAtual = Mathf.Max(infraAtual - 1, infraMin);

        // Atulizar Interface de novo
        AtualizarInterface();
        VerificarDerrota();



        // TODO: Validar o recurso e a quantidade escolheram.
        // TODO: Aplicar a perda, impedir resultado negativo e verificar derrota.
        // TODO: Para este teste isolado, não aplicar perdas avulsas durante uma reserva
        // pendente; concluir ou cancelar a reserva primeiro para não criar conflito.
    }

    public void ReservarRecurso()
    {

        if (JogoDerrotando) return;

        Debug.Log("Recusto reservando para teste para jogando morrer");
        // TODO: Verificar a quantidade disponível considerando as reservas existentes.
        // TODO: Registrar a reserva com sua identificação sem descontar do saldo real.
        // TODO: Permitir reservar o saldo inteiro, mesmo deixando disponível zero.
        // TODO: Não declarar derrota apenas por não haver valor disponível.
    }

    public void CancelarReserva(int indiceReserva)
    {

        if (JogoDerrotando) return;

        AtualizarInterface();

        // TODO: Conferir se a reserva existe e remover somente a indicada.
        // TODO: Liberar o valor reservado sem acrescentá-lo ao saldo real.
        // TODO: Atualizar a interface; cancelar não gera recursos novos.
    }

    public void ExecutarReserva(int indiceReserva)
    {
        if (JogoDerrotando) return;

        //Quando jogador morre ele vai verificar se ele morreu 
        VerificarDerrota();

        // TODO: Cobrar efetivamente o valor e remover a reserva consumida.
        // TODO: Impedir executar a mesma reserva novamente.
        // TODO: Se o gasto zerar um recurso compartilhado, registrar derrota imediata.
        // O GDD permite um gasto fatal; não recusar só porque o resultado será zero.
    }

    public void GastarConhecimentoJogador1(int quantidade)
    {

        if (JogoDerrotando) return;


        // AJUSTE: Adicionado as chaves {} e a linha de código para descontar o conhecimento do Jogador 1
        if (quantidade > 0 && conhecimentoAtualJ1 >= quantidade)
        {
            conhecimentoAtualJ1 -= quantidade;
            AtualizarInterface();
        }

        // TODO: Validar a quantidade e o saldo do jogador 1 antes de gastar.
        // TODO: Não alterar o Conhecimento do jogador 2.
        // TODO: Conhecimento zero não causa derrota.
    }

    public void GastarConhecimentoJogador2(int quantidade)
    {
        if (JogoDerrotando) return;
        if (quantidade > 0 && conhecimentoAtualJ2 >= quantidade)
        {
            conhecimentoAtualJ2 -= bandwidth = quantidade;

            AtualizarInterface();
        }

    }

    public void NovaRodada()
    {

        if (JogoDerrotando) return;


        conhecimentoAtualJ1 = conhecimentoIni;
        conhecimentoAtualJ2 = conhecimentoIni;


        AtualizarInterface();

    }

    public void VerificarDerrota()
    {
        // Conferir os valores reais dos três recursos compartilhados.
        if (saudeAtual == 0 || infraAtual == 0 || pesquisaAtual == 0)
        {
            // Encerrar se algum for zero, informar o motivo e bloquear ações.
            JogoDerrotando = true;
            Debug.Log("Derrota detectada por recurso zerado.");
        }
    }


    private void AtualizarInterface()
    {
        saude.text = $"Saúde: {saudeAtual}/{saudeMax}";
        infraestrutura.text = $"Infraestrutura: {infraAtual}/{infraMax}";
        pesquisa.text = $"Pesquisa: {pesquisaAtual}/{pesquisaMax}";

        conhecimentoJ1.text = $"Conhecimento: {conhecimentoAtualJ1}/{conhecimentoMax}";
        conhecimentoJ2.text = $"Conhecimento: {conhecimentoAtualJ2}/{conhecimentoMax}";
    }

    public void Reiniciar()
    {
        JogoDerrotando = false; // AJUSTE: Adicionado para resetar a derrota quando o jogo recomeçar
        saudeAtual = saudeIni;
        infraAtual = infraIni;
        pesquisaAtual = pesquisaIni;

        conhecimentoAtualJ1 = conhecimentoIni;
        conhecimentoAtualJ2 = conhecimentoIni;

        AtualizarInterface();
        

    }

}
