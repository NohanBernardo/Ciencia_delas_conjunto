using UnityEngine;

public class CartaAçao : MonoBehaviour
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
