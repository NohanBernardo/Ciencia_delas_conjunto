using UnityEngine;

public class CartaAcao : MonoBehaviour
{

   public enum TipoEfeito
    {
        RecuperarSaúde,
        GanharPesquisa,
        GanharInfraestrutura,

        
    }
    [Header("Efeito da Carta")]

    public TipoEfeito efeito;
    public int valor = 2;

    [Header("Custo da Carta")]
    public int custoConhecimento = 1;
    public void Executar(SimuladorTurnos simulador)
    {
        switch (efeito)
        {
            case TipoEfeito.RecuperarSaúde:
                simulador.saude += valor;
                break;

            case TipoEfeito.GanharPesquisa:
                simulador.pesquisa += valor;
                break;

            case TipoEfeito.GanharInfraestrutura:
                simulador.infraestrutura += valor;
                break;
        }

        Debug.Log("Carta executada! Efeito: " + efeito);
    }

  
}
