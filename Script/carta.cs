
using UnityEngine;
using UnityEngine.EventSystems;

public class carta : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    [Header("Informações da carta")]
    public string nome;
    public int custo;

    [Header("Animação Idle")]
    public float alturaIdle = 0.08f;
    public float velocidadeIdle = 2f;
    public float rotacaoIdle = 2f;

    [Header("Seleção da carta")]
    public float alturaSelecao = 0.6f;
    public float rotacaoSelecao = 5f;
    public float velocidadeAnimacao = 8f;

    [Header("Hover")]
    public float alturaHover = 0.2f;

    [Header("Simulador")]
    public JogadaCarta simulador;

    private Vector3 posicaoInicial;
    private Quaternion rotacaoInicial;

    private bool selecionada = false;
    private bool mouseSobreCarta = false;
    private float tempo;

    void Start()
    {
        posicaoInicial = transform.localPosition;
        rotacaoInicial = transform.localRotation;

        if (simulador == null)
            simulador = FindObjectOfType<JogadaCarta>();
    }

    void Update()
    {
        tempo += Time.deltaTime * velocidadeIdle;

        float movimentoY = Mathf.Sin(tempo) * alturaIdle;
        float movimentoZ =
            Mathf.Sin(tempo * 0.7f) * rotacaoIdle;

        Vector3 posicaoDesejada =
            posicaoInicial + Vector3.up * movimentoY;

        Quaternion rotacaoDesejada =
            rotacaoInicial *
            Quaternion.Euler(0, 0, movimentoZ);

        if (selecionada)
        {
            posicaoDesejada += Vector3.up * alturaSelecao;

            rotacaoDesejada = rotacaoInicial *
                Quaternion.Euler(0, 0, -rotacaoSelecao);
        }
        else if (mouseSobreCarta)
        {
            posicaoDesejada += Vector3.up * alturaHover;
        }

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            posicaoDesejada,
            Time.deltaTime * velocidadeAnimacao
        );

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            rotacaoDesejada,
            Time.deltaTime * velocidadeAnimacao
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseSobreCarta = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseSobreCarta = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (simulador == null)
            return;

        if (simulador.selecionadas.Contains(this))
        {
            int indice = simulador.selecionadas.IndexOf(this);
            simulador.DesmarcarCarta(indice);
        }
        else
        {
            int indice = simulador.cartas.IndexOf(this);

            if (indice == -1)
                return;

            simulador.SelecionarCarta(indice);
        }

        AtualizarSelecaoVisual(
            simulador.selecionadas.Contains(this)
        );
    }

    public void AtualizarSelecaoVisual(bool valor)
    {
        selecionada = valor;
    }
}
