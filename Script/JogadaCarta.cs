using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class JogadaCarta : MonoBehaviour
{
    //inventario
    public List<carta> cartas = new List<carta>();
    //Conhecimento
    public int conhecimentoIni = 3;
    public int conhecimento;
    public int conhecimentoReservado;
    public int conhecimentoDisponivel;

    //Seleçao
    public List<carta> selecionadas = new List<carta>();
    public List<carta> descarte = new List<carta>();
    //estado atual no turno
    public Estado estadoAtual = Estado.Planejando;
    
    //os estados 
    public enum Estado 
    {
        Planejando,
        Confirmada,
        Executada
    }


    void Start()
    {
        //3 de conhecimento ao iniciar o jogo
        conhecimento = conhecimentoIni;
        
        conhecimentoReservado = 0;
        conhecimentoDisponivel = conhecimento;
        
        selecionadas.Clear();
        descarte.Clear();
        //estado inicial
        estadoAtual = Estado.Planejando;
        AtualizarInterface();
    }

    void Update()
    {
        
    }

    public void SelecionarCarta(int indiceCarta)
    {

        //verificando se o estado atual é o "planejando", caso n for sla
        if (estadoAtual != Estado.Planejando) return;

        if (indiceCarta < 0 || indiceCarta >= cartas.Count) return;

        carta carta = cartas[indiceCarta];

        if (selecionadas.Contains (carta)) return;

        if (selecionadas.Count >= 3) return;

        if (carta.custo > conhecimentoDisponivel) return;
        //adiciona as cartas
        selecionadas.Add(carta);

        RecalcularReserva();

        AtualizarInterface();
    }


    public void DesmarcarCarta(int indiceCartaSelecionada)
    {
        if (estadoAtual != Estado.Planejando)
            return;

        if (indiceCartaSelecionada < 0 ||
            indiceCartaSelecionada >= selecionadas.Count)
            return;

        carta cartaRemovida = selecionadas[indiceCartaSelecionada];

        selecionadas.RemoveAt(indiceCartaSelecionada);

        RecalcularReserva();

        AtualizarInterface();
    }

    void AtualizarVisualDasCartas()
    {
        foreach (carta item in cartas)
        {
            item.AtualizarSelecaoVisual(
                selecionadas.Contains(item)
            );
        }
    }


    public void RecalcularReserva()
    {
        conhecimentoReservado = 0;

        foreach (carta carta in selecionadas)
        {
            conhecimentoReservado += carta.custo;
        }

        conhecimentoDisponivel = conhecimento - conhecimentoReservado;
    }

    public void ConfirmarJogada()
    {
        if (estadoAtual != Estado.Planejando)
            return;

        if (selecionadas.Count == 0)
            return;

        estadoAtual = Estado.Confirmada;

        AtualizarInterface();
    }



    public void ExecutarJogada()
    {
        if (estadoAtual != Estado.Confirmada)
            return;

        // Desconta o Conhecimento reservado
        conhecimento -= conhecimentoReservado;

        // Envia as cartas utilizadas para o descarte
        foreach (carta cartaSelecionada in selecionadas)
        {
            if (!descarte.Contains(cartaSelecionada))
                descarte.Add(cartaSelecionada);
        }

        // Limpa a seleção
        selecionadas.Clear();

        // Atualiza os valores de Conhecimento
        RecalcularReserva();

        // Marca a jogada como executada
        estadoAtual = Estado.Executada;

        AtualizarInterface();
    }

    public void CancelarJogada()
    {
        if (estadoAtual != Estado.Planejando &&
            estadoAtual != Estado.Confirmada)
            return;

        selecionadas.Clear();

        RecalcularReserva();

        estadoAtual = Estado.Planejando;

        AtualizarInterface();
    }



    public void AtualizarInterface()
    {
        Debug.Log("Estado atual: " + estadoAtual);
        Debug.Log("Conhecimento: " + conhecimento);
        Debug.Log("Conhecimento reservado: " + conhecimentoReservado);
        Debug.Log("Conhecimento disponível: " + conhecimentoDisponivel);
        Debug.Log("Cartas selecionadas: " + selecionadas.Count);
    }



    public void Reiniciar()
    {
        conhecimento = conhecimentoIni;
        selecionadas.Clear();
        descarte.Clear();
       

        RecalcularReserva();

        estadoAtual = Estado.Planejando;

        AtualizarInterface();
    }

}
