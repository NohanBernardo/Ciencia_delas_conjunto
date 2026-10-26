using TMPro;
using UnityEngine;

public class SimuladorEfeitos : MonoBehaviour
{
    [Header("Recursos")]
    public int saude = 10;
    public int pesquisa = 5;
    public int infraestrutura = 5;

    [Header("Rodada")]
    public int rodada = 1;
    public string fase = "Rodada aberta";

    [Header("Interface")]
    public TMP_Text textoRecursos;
    public TMP_Text textoRodada;
    public TMP_Text textoEfeitos;
    public TMP_Text textoHistorico;

    private bool rodadaAberta = true;
    private bool derrota = false;

    private bool experimentoAtivo = false;
    private int duracaoExperimento = 0;

    private bool legadoAtivo = false;
    private int duracaoLegado = 0;

    private bool ameacaAnulada = false;

    private string ameaca = "Ameaça de teste";
    private string historico = "";

    void Start()
    {
        Reiniciar();
    }

    int Limitar(int valor)
    {
        return Mathf.Clamp(valor, 0, 30);
    }

    void Registrar(string mensagem)
    {
        historico = mensagem + "\n" + historico;
        if (textoHistorico != null)
            textoHistorico.text = historico;
    }

    public void IntervencaoEmergencial()
    {
        if (derrota) return;
        saude = Limitar(saude + 2);

        Registrar("Intervenção Emergencial: Saúde +2.");
        AtualizarInterface();
    }

    public void EnergiaNuclear()
    {
        if (derrota) return;
        pesquisa = Limitar(pesquisa + 2);
        infraestrutura = Limitar(infraestrutura + 1);

        Registrar("Energia Nuclear: Pesquisa +2 e Infraestrutura +1.");
        AtualizarInterface();
    }

    public void ExperimentoAvancado()
    {
        if (derrota || experimentoAtivo) return;
        experimentoAtivo = true;
        duracaoExperimento = 3;

        Registrar("Experimento Avançado ativado: Pesquisa + 1 no inicio das próximas 2 rodadas.");
        AtualizarInterface();
    }

    public void LegadoCientifico()
    {
        if (derrota || legadoAtivo) return;
        legadoAtivo = true;
        duracaoLegado = 2;

        Registrar("Legado Científico ativado: Pesquisa + 1 no inicio das próximas 2 rodadas.");
        AtualizarInterface();
    }

    public void SequenciamentoGenetico()
    {
        if (derrota) return;
        ameacaAnulada = true;

        Registrar("Sequenciamento Genético: Ameaça anulada nesta rodada..");
        AtualizarInterface();
    }

    public void EncerrarRodada()
    {
        if (derrota || !rodadaAberta) return;
        fase = "Encerrando rodada";

        if (experimentoAtivo && duracaoExperimento > 0) ;
        {
            saude = Limitar(saude + 1);
            pesquisa = Limitar(pesquisa + 1);
            infraestrutura = Limitar(infraestrutura + 1);

            duracaoExperimento--;

            Registrar("Experimento Avançado: Saúde, Pesquisa e Infraestrutura +1.");

            if (duracaoExperimento == 0)
            {
                experimentoAtivo = false;
                Registrar("Experimento Avançado expirou.");
            }
        }

        ResolverPenalidadeDaAmeaca();

        rodadaAberta = false;

        if (derrota)
        {
            fase = "Derrota";

        }
        else
        {
            fase = "Rodada encerrada";
        }
        AtualizarInterface();
    }

    public void IniciarRodada()
    {
        if (derrota || rodadaAberta) return;

        rodada++;
        ameacaAnulada = false;
        rodadaAberta = true;
        fase = "Rodada aberta";

        if (legadoAtivo && duracaoLegado > 0)
        {
            pesquisa = Limitar(pesquisa + 1);
            duracaoLegado--;

            Registrar("Legado Científico: Pesquisa +1.");

            if (duracaoLegado == 0)
            {
                legadoAtivo = false;
                Registrar("Legado Científico expirou.");
            }
        }
        Registrar("Rodada " + rodada + "iniciada.");
        AtualizarInterface();
    }

    public void ResolverPenalidadeDaAmeaca ()
    {
        if (ameacaAnulada)
        {
            Registrar(ameaca + "foi bloqueada pelo Sequenciamento Genético.");

        }
        else
        {
            saude = Limitar(saude - 2);
            Registrar(ameaca + "penalidade aplicada. Saúde -2.");
        }

        ameacaAnulada = false;
        if (saude <= 0 || pesquisa <= 0 || infraestrutura <= 0)
        {
            derrota = true;
            fase = "Derrota";

            Registrar("Derrota! Um dos recursos chegou a zero.");
        }
    }

    public void AtualizarInterface()
    {
        if (textoRecursos != null)
        {
            textoRecursos.text = "SAÚDE: " + saude + "\nPESQUISA: " + pesquisa + "\nINFRAESTRUTURA: " + infraestrutura;
        }
        if(textoRodada != null)
        {
            textoRodada.text = "Rodada: " + rodada + "\nFase: " + fase;
        }
        if (textoEfeitos != null)
        {
            textoEfeitos.text = "EFEITOS ATIVOS: \n";
            if (experimentoAtivo)
                textoEfeitos.text += "Experimento Avançado: " + duracaoExperimento + " aplicaçÂo(ões) restante(s)\n";
            if (legadoAtivo)
                textoEfeitos.text += "Legado Científico: " + duracaoLegado + " aplicaçÂo(ões) restante(s)\n";
            if (!experimentoAtivo && !legadoAtivo)
                textoEfeitos.text += "Nenhum efeito ativo.";
            if (ameacaAnulada)
                textoEfeitos.text += "\nAmeaça anulada nesta rodada.";
        }
        if (textoHistorico != null)
            textoHistorico.text = historico;
    }

    public void Reiniciar ()
    {
        saude = 10;
        pesquisa = 5;
        infraestrutura = 5;

        rodada = 1;
        fase = "Rodada aberta";

        rodadaAberta = true;
        derrota = false;

        experimentoAtivo = false;
        duracaoExperimento = 0;

        legadoAtivo = false;
        duracaoLegado = 0;

        ameacaAnulada = false;
        ameaca = "Ameaça de teste";
        historico = "";

        Registrar("Simulador reiniciado. Rodada 1 iniciada.");
        AtualizarInterface();
    }
}
