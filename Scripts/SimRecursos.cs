using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SimRecursos : MonoBehaviour
{
    public static SimRecursos Instance { get; private set; }

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
    private int conhecimentoIni = 3, conhecimentoMin = 0, conhecimentoMax = 3;
    public int conhecimentoAtualJ1;
    public int conhecimentoAtualJ2;

    // Texto da cena que será atualizado com UpdateVisuals()
    public TextMeshProUGUI saude, infraestrutura, pesquisa, conhecimentoJ1, conhecimentoJ2;

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

    public void GanharRecurso()
    {

        // TODO: Ler qual recurso e qual quantidade foram escolhidos na interface.
        // TODO: Validar a entrada, aplicar o ganho e respeitar o teto de 30.
        // TODO: Atualizar os valores exibidos.
    }

    public void PerderRecurso()
    {
        // TODO: Validar o recurso e a quantidade escolhidos.
        // TODO: Aplicar a perda, impedir resultado negativo e verificar derrota.
        // TODO: Para este teste isolado, não aplicar perdas avulsas durante uma reserva
        // pendente; concluir ou cancelar a reserva primeiro para não criar conflito.
    }

    public void ReservarRecurso()
    {
        // TODO: Verificar a quantidade disponível considerando as reservas existentes.
        // TODO: Registrar a reserva com sua identificação sem descontar do saldo real.
        // TODO: Permitir reservar o saldo inteiro, mesmo deixando disponível zero.
        // TODO: Não declarar derrota apenas por não haver valor disponível.
    }

    public void CancelarReserva(int indiceReserva)
    {
        // TODO: Conferir se a reserva existe e remover somente a indicada.
        // TODO: Liberar o valor reservado sem acrescentá-lo ao saldo real.
        // TODO: Atualizar a interface; cancelar não gera recursos novos.
    }

    public void ExecutarReserva(int indiceReserva)
    {
        // TODO: Cobrar efetivamente o valor e remover a reserva consumida.
        // TODO: Impedir executar a mesma reserva novamente.
        // TODO: Se o gasto zerar um recurso compartilhado, registrar derrota imediata.
        // O GDD permite um gasto fatal; não recusar só porque o resultado será zero.
    }

    public void GastarConhecimentoJogador1(int quantidade)
    {
        // TODO: Validar a quantidade e o saldo do jogador 1 antes de gastar.
        // TODO: Não alterar o Conhecimento do jogador 2.
        // TODO: Conhecimento zero não causa derrota.
    }

    public void GastarConhecimentoJogador2(int quantidade)
    {
        // TODO: Aplicar as mesmas verificações, usando o saldo do jogador 2.
        // TODO: Manter os dois saldos independentes e atualizar a tela.
    }

    public void NovaRodada()
    {
        // TODO: Restaurar ambos os Conhecimentos para 3, sem somar sobre o saldo anterior.
        // TODO: Não restaurar automaticamente Saúde, Pesquisa ou Infraestrutura.
        // TODO: Exigir reservas resolvidas ou canceladas antes de abrir outra rodada.
    }

    public void VerificarDerrota()
    {
        // TODO: Conferir os valores reais dos três recursos compartilhados.
        if (saudeAtual == 0 && infraAtual == 0 && pesquisaAtual == 0)
        {
            
        }
        // TODO: Encerrar se algum for zero, informar o motivo e bloquear ações.

        // TODO: Reiniciar deve continuar disponível após a derrota.
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
        saudeAtual = saudeIni;
        infraAtual = infraIni;
        pesquisaAtual = pesquisaIni;

        conhecimentoAtualJ1 = conhecimentoIni;
        conhecimentoAtualJ2 = conhecimentoIni;

        AtualizarInterface();
        // e remover todas as reservas.

        // TODO: Limpar o resultado anterior e liberar as ações de teste.
        
    }

}