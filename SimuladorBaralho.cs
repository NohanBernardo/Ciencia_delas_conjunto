
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SimuladorBaralho : MonoBehaviour
{
    [System.Serializable]
    public class Carta
    {
        public string nome;
        public string tipo;
        public int custo;

        public Carta(string nome, string tipo, int custo)
        {
            this.nome = nome;
            this.tipo = tipo;
            this.custo = custo;
        }
    }

    [Header("Áreas das cartas")]
    public Transform cartasMarie;
    public Transform cartasJaqueline;
    public Transform cartasAmeacas;

    [Header("Textos de Conhecimento")]
    public TMP_Text textoConhecimentoMarie;
    public TMP_Text textoConhecimentoJaqueline;

    [Header("Interface dos turnos")]
    public TMP_Text textoTurno;
    public Button botaoComprarMarie;
    public Button botaoComprarJaqueline;
    public Button botaoPassarTurno;
    public Button botaoReiniciar;

    private const int TAMANHO_BARALHO = 15;
    private const int MAO_INICIAL = 3;
    private const int MAO_MAXIMA = 5;
    private const int CONHECIMENTO_INICIAL = 3;
    private const int CONHECIMENTO_MAXIMO = 3;

    private int conhecimentoMarie;
    private int conhecimentoJaqueline;

    private bool turnoMarie = true;

    private List<Carta> baralhoMarie = new List<Carta>();
    private List<Carta> baralhoJaqueline = new List<Carta>();

    private List<Carta> descarteMarie = new List<Carta>();
    private List<Carta> descarteJaqueline = new List<Carta>();

    private List<Carta> maoMarie = new List<Carta>();
    private List<Carta> maoJaqueline = new List<Carta>();

    private void Start()
    {
        ConfigurarOrganizacao(cartasMarie);
        ConfigurarOrganizacao(cartasJaqueline);
        ConfigurarOrganizacao(cartasAmeacas);

        if (botaoComprarMarie != null)
            botaoComprarMarie.onClick.AddListener(ComprarMarie);

        if (botaoComprarJaqueline != null)
            botaoComprarJaqueline.onClick.AddListener(ComprarJaqueline);

        if (botaoPassarTurno != null)
            botaoPassarTurno.onClick.AddListener(PassarTurno);

        if (botaoReiniciar != null)
            botaoReiniciar.onClick.AddListener(ReiniciarJogo);

        ReiniciarJogo();
    }

    private void ConfigurarOrganizacao(Transform area)
    {
        if (area == null)
            return;

        HorizontalLayoutGroup layout =
            area.GetComponent<HorizontalLayoutGroup>();

        if (layout == null)
            layout = area.gameObject.AddComponent<HorizontalLayoutGroup>();

        layout.spacing = 10;
        layout.padding = new RectOffset(10, 10, 10, 10);
        layout.childAlignment = TextAnchor.MiddleLeft;
        layout.childControlWidth = false;
        layout.childControlHeight = false;
        layout.childForceExpandWidth = false;
        layout.childForceExpandHeight = false;
    }

    private List<Carta> CriarBaralhoMarie()
    {
        return new List<Carta>
        {
            new Carta("Pesquisa Científica", "Normal", 1),
            new Carta("Coleta de Dados", "Normal", 1),
            new Carta("Análise de Amostras", "Normal", 1),
            new Carta("Estudo Experimental", "Normal", 1),
            new Carta("Relatório Científico", "Normal", 1),
            new Carta("Observação Laboratorial", "Normal", 1),
            new Carta("Teste de Hipótese", "Normal", 1),
            new Carta("Investigação", "Normal", 1),
            new Carta("Revisão de Dados", "Normal", 1),
            new Carta("Descoberta", "Especial", 2),
            new Carta("Equipamento Avançado", "Especial", 2),
            new Carta("Pesquisa Intensiva", "Especial", 2),
            new Carta("Experimento Especial", "Especial", 2),
            new Carta("Grande Avanço", "Especial", 3),
            new Carta("Inovação Científica", "Especial", 3)
        };
    }

    private List<Carta> CriarBaralhoJaqueline()
    {
        return new List<Carta>
        {
            new Carta("Análise Viral", "Normal", 1),
            new Carta("Coleta Epidemiológica", "Normal", 1),
            new Carta("Estudo do Surto", "Normal", 1),
            new Carta("Investigação Clínica", "Normal", 1),
            new Carta("Registro de Casos", "Normal", 1),
            new Carta("Monitoramento", "Normal", 1),
            new Carta("Pesquisa de Campo", "Normal", 1),
            new Carta("Avaliação de Riscos", "Normal", 1),
            new Carta("Relatório Epidemiológico", "Normal", 1),
            new Carta("Resposta Imediata", "Especial", 3),
            new Carta("Diagnóstico Rápido", "Especial", 2),
            new Carta("Mapeamento Viral", "Especial", 2),
            new Carta("Controle Epidemiológico", "Especial", 2),
            new Carta("Sequenciamento Genético", "Especial", 3),
            new Carta("Plano de Contingência", "Especial", 2)
        };
    }

    public void ReiniciarJogo()
    {
        conhecimentoMarie = CONHECIMENTO_INICIAL;
        conhecimentoJaqueline = CONHECIMENTO_INICIAL;

        turnoMarie = true;

        maoMarie.Clear();
        maoJaqueline.Clear();

        descarteMarie.Clear();
        descarteJaqueline.Clear();

        baralhoMarie = CriarBaralhoMarie();
        baralhoJaqueline = CriarBaralhoJaqueline();

        Embaralhar(baralhoMarie);
        Embaralhar(baralhoJaqueline);

        LimparArea(cartasMarie);
        LimparArea(cartasJaqueline);
        LimparArea(cartasAmeacas);

        for (int i = 0; i < MAO_INICIAL; i++)
        {
            ComprarCarta(true);
            ComprarCarta(false);
        }

        CriarAmeacas();

        AtualizarConhecimento();
        AtualizarInterfaceTurno();
        AtualizarBotoes();

        Debug.Log("Jogo reiniciado. Turno da Marie.");
    }

    private void ComprarMarie()
    {
        if (!turnoMarie)
            return;

        ComprarCarta(true);
    }

    private void ComprarJaqueline()
    {
        if (turnoMarie)
            return;

        ComprarCarta(false);
    }

    private void ComprarCarta(bool pertenceMarie)
    {
        List<Carta> baralho =
            pertenceMarie ? baralhoMarie : baralhoJaqueline;

        List<Carta> mao =
            pertenceMarie ? maoMarie : maoJaqueline;

        List<Carta> descarte =
            pertenceMarie ? descarteMarie : descarteJaqueline;

        if (mao.Count >= MAO_MAXIMA)
        {
            Debug.Log("A mão já possui 5 cartas.");
            AtualizarBotoes();
            return;
        }

        if (baralho.Count == 0)
        {
            if (descarte.Count == TAMANHO_BARALHO)
            {
                baralho.AddRange(descarte);
                descarte.Clear();
                Embaralhar(baralho);
            }
            else
            {
                Debug.Log("Não há cartas disponíveis para comprar.");
                AtualizarBotoes();
                return;
            }
        }

        Carta carta = baralho[0];
        baralho.RemoveAt(0);
        mao.Add(carta);

        Transform area =
            pertenceMarie ? cartasMarie : cartasJaqueline;

        CriarCartaVisual(carta, area, pertenceMarie);
        AtualizarBotoes();
    }

    private void JogarCarta(
        GameObject objeto,
        Carta carta,
        bool pertenceMarie)
    {
        if (pertenceMarie != turnoMarie)
        {
            Debug.Log("Não é o turno deste cientista!");
            return;
        }

        if (pertenceMarie)
        {
            if (conhecimentoMarie < carta.custo)
            {
                Debug.Log("Marie não tem Conhecimento suficiente!");
                return;
            }

            conhecimentoMarie -= carta.custo;
            maoMarie.Remove(carta);
            descarteMarie.Add(carta);
        }
        else
        {
            if (conhecimentoJaqueline < carta.custo)
            {
                Debug.Log("Jaqueline não tem Conhecimento suficiente!");
                return;
            }

            conhecimentoJaqueline -= carta.custo;
            maoJaqueline.Remove(carta);
            descarteJaqueline.Add(carta);
        }

        Destroy(objeto);

        AtualizarConhecimento();
        AtualizarBotoes();

        OrganizarCartas(cartasMarie);
        OrganizarCartas(cartasJaqueline);

        Debug.Log("Carta jogada: " + carta.nome);
    }

    private void CriarCartaVisual(
        Carta carta,
        Transform area,
        bool pertenceMarie)
    {
        if (area == null)
        {
            Debug.LogError("Área de cartas não conectada!");
            return;
        }

        GameObject objeto = new GameObject(carta.nome);
        objeto.transform.SetParent(area, false);

        RectTransform rect = objeto.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(150, 100);

        Image imagem = objeto.AddComponent<Image>();
        Button botao = objeto.AddComponent<Button>();

        imagem.color = carta.tipo == "Especial"
            ? Color.yellow
            : Color.gray;

        GameObject textoObjeto = new GameObject("Texto");
        textoObjeto.transform.SetParent(objeto.transform, false);

        RectTransform textoRect =
            textoObjeto.AddComponent<RectTransform>();

        textoRect.anchorMin = Vector2.zero;
        textoRect.anchorMax = Vector2.one;
        textoRect.offsetMin = Vector2.zero;
        textoRect.offsetMax = Vector2.zero;

        TextMeshProUGUI texto =
            textoObjeto.AddComponent<TextMeshProUGUI>();

        texto.text = carta.nome + "\nCusto: " + carta.custo;
        texto.alignment = TextAlignmentOptions.Center;
        texto.color = Color.black;
        texto.fontSize = 14;
        texto.raycastTarget = false;

        botao.onClick.AddListener(() =>
        {
            JogarCarta(objeto, carta, pertenceMarie);
        });

        OrganizarCartas(area);
    }

    private void PassarTurno()
    {
        turnoMarie = !turnoMarie;

        if (turnoMarie)
            RecuperarConhecimentoMarie();
        else
            RecuperarConhecimentoJaqueline();

        AtualizarConhecimento();
        AtualizarInterfaceTurno();
        AtualizarBotoes();

        Debug.Log(
            turnoMarie
                ? "Turno da Marie."
                : "Turno da Jaqueline."
        );
    }

    private void RecuperarConhecimentoMarie()
    {
        if (conhecimentoMarie < CONHECIMENTO_MAXIMO)
            conhecimentoMarie++;
    }

    private void RecuperarConhecimentoJaqueline()
    {
        if (conhecimentoJaqueline < CONHECIMENTO_MAXIMO)
            conhecimentoJaqueline++;
    }

    private void AtualizarInterfaceTurno()
    {
        if (textoTurno != null)
        {
            textoTurno.text = turnoMarie
                ? "Turno: Marie Curie"
                : "Turno: Jaqueline Goes de Jesus";
        }
    }

    private void AtualizarBotoes()
    {
        if (botaoComprarMarie != null)
        {
            botaoComprarMarie.interactable =
                turnoMarie && maoMarie.Count < MAO_MAXIMA;
        }

        if (botaoComprarJaqueline != null)
        {
            botaoComprarJaqueline.interactable =
                !turnoMarie && maoJaqueline.Count < MAO_MAXIMA;
        }

        if (botaoPassarTurno != null)
            botaoPassarTurno.interactable = true;
    }

    private void AtualizarConhecimento()
    {
        if (textoConhecimentoMarie != null)
        {
            textoConhecimentoMarie.text =
                "Conhecimento Marie: " + conhecimentoMarie;
        }

        if (textoConhecimentoJaqueline != null)
        {
            textoConhecimentoJaqueline.text =
                "Conhecimento Jaqueline: " + conhecimentoJaqueline;
        }
    }

    private void CriarAmeacas()
    {
        CriarAmeaca("Crise Viral");
        CriarAmeaca("Surto Epidemiológico");
    }

    private void CriarAmeaca(string nome)
    {
        if (cartasAmeacas == null)
            return;

        GameObject objeto = new GameObject(nome);
        objeto.transform.SetParent(cartasAmeacas, false);

        RectTransform rect = objeto.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(150, 100);

        Image imagem = objeto.AddComponent<Image>();
        imagem.color = Color.red;

        GameObject textoObjeto = new GameObject("Texto");
        textoObjeto.transform.SetParent(objeto.transform, false);

        RectTransform textoRect =
            textoObjeto.AddComponent<RectTransform>();

        textoRect.anchorMin = Vector2.zero;
        textoRect.anchorMax = Vector2.one;
        textoRect.offsetMin = Vector2.zero;
        textoRect.offsetMax = Vector2.zero;

        TextMeshProUGUI texto =
            textoObjeto.AddComponent<TextMeshProUGUI>();

        texto.text = nome;
        texto.alignment = TextAlignmentOptions.Center;
        texto.color = Color.black;
        texto.fontSize = 14;
        texto.raycastTarget = false;

        OrganizarCartas(cartasAmeacas);
    }

    private void OrganizarCartas(Transform area)
    {
        if (area == null)
            return;

        HorizontalLayoutGroup layout =
            area.GetComponent<HorizontalLayoutGroup>();

        RectTransform rect = area as RectTransform;

        if (layout != null && rect != null)
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
    }

    private void LimparArea(Transform area)
    {
        if (area == null)
            return;

        for (int i = area.childCount - 1; i >= 0; i--)
        {
            GameObject filho = area.GetChild(i).gameObject;
            filho.SetActive(false);
            Destroy(filho);
        }
    }

    private void Embaralhar(List<Carta> cartas)
    {
        for (int i = cartas.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            Carta temporaria = cartas[i];
            cartas[i] = cartas[j];
            cartas[j] = temporaria;
        }
    }
}



