using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SimuladorCompraDescarte : MonoBehaviour
{
    public int totalCartas = 15;
    public int maoInicial = 4;
    public int limiteMao = 6;

    private List<int> baralho = new List<int>();
    private List<int> mao = new List<int>();
    private List<int> cemiterio = new List<int>();

    private int indexCartaSelecionada = -1;
    private int i;

    public void Start()
    {
        Reiniciar();
    }

    public void NovaRodada()
    {
        if (mao.Count >= limiteMao)
        {
            Debug.Log("Mão está no limite");
            return;
        }

        ComprarCarta();
        AtualizarInterface();
    }

    public void ExecutarCartaSelecionada()
    {
        if (indexCartaSelecionada >= 0 && indexCartaSelecionada < mao.Count)
        {
            int cartaUsada = mao[indexCartaSelecionada];

            mao.RemoveAt(indexCartaSelecionada);
            cemiterio.Add(cartaUsada);

            indexCartaSelecionada = -1;
            AtualizarInterface();

        }
        else
        {
            Debug.Log("Nenhuma carta vai ser executada");
        }
    }

    public void SelecionarCarta(int indiceCarta)
    {
        if (indiceCarta >= 0 && indiceCarta < mao.Count)
        {
            indexCartaSelecionada = indiceCarta;
            Debug.Log("Carta selecionda: " + mao[indiceCarta]);
            AtualizarInterface();

        }
    }

    public void ComprarCarta()
    {
        if (baralho.Count  == 0)
        {
            ReporBaralho();
        }

        if (baralho.Count == 0)
        {
            Debug.Log("Nao ha cartas disponiveis");
            return;
        }
        mao.Add(baralho[0]);
        baralho.RemoveAt(0);
    }

    public void ReporBaralho()
    {
        if (cemiterio.Count > 0)
        {
            baralho.AddRange(cemiterio);
            cemiterio.Clear();

            EmbaralharLista(baralho);

            Debug.Log("Baralho criado");
        } else
        {
            Debug.Log("Não tem carta no cemitério para repor o baralho");
        }
    }

    public void EmbaralharLista(List<int> lista)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            int temp = lista[i];
            int randomIndex = Random.Range(i, lista.Count);
            lista[i] = lista[randomIndex];
            lista[randomIndex] = temp;
        }
    }

    public void AtualizarInterface()
    {

    }

    public void Reiniciar()
    {
        baralho.Clear();
        mao.Clear();
        cemiterio.Clear();
        indexCartaSelecionada = -1;

        for (int i = 1; i <= totalCartas; i++) ;
        {
            baralho.Add(i);
        }

        EmbaralharLista(baralho);

        for (int i = 0; i < maoInicial; i++)
        {
            if (baralho.Count > 0 )
            {
                mao.Add(baralho[0]);
                baralho.RemoveAt(0);
            }

            AtualizarInterface();
        }
    }

}
