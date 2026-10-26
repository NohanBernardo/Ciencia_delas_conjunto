using System.Collections;
using UnityEngine;

public class HandShake : MonoBehaviour
{
    public float anguloInicial = 0f;
    public int direcao = 1;
    public float anguloMax = 15f;
    public float velocidade = 10f;

    private float angulo;

    // aaaaa



    private void Start()
    {
        angulo = anguloInicial; 
    }

    private void Update()
    {
        // Definindo o angulo a cada frame
        angulo +=  direcao * velocidade * Time.deltaTime;

        if (angulo >= anguloMax)
        {
            direcao = -1;
        } if (angulo <= -anguloMax){
            direcao = 1;

        }
       
      
        // Movimentação
        transform.rotation = Quaternion.Euler(0,0,angulo);

    }
}
