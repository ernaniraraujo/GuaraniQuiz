using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class temaInfo : MonoBehaviour {

    private temaJogo temaJogo;

    [Header ("Configuração do Tema")]
    public int idTema;
    public string nomeDoTema;
    public Color corTema;
    public int Vidas;
    public Text moedas;

    [Header("Configuraçnao do Botão")]
    public Text idTemaTxt;
    public bool requerNotaMinima;
    public int  notaMinimaNecessaria;

    private Button btnTema;

    public GameObject[ ] estrela;

    //public GameObject estrela1;
    //public GameObject estrela2;
    //public GameObject estrela3;

    private int notaFinal;

    // Use this for initialization
    void Start () {

        // recebe o botão

        btnTema = GetComponent<Button>();


        // busca o script temaJogo.
         temaJogo = FindObjectOfType(typeof(temaJogo)) as temaJogo;


        // Configura o numero do botão 
        idTemaTxt.text = idTema.ToString();

        // COloca todas as estrelas como falso.
        foreach (GameObject e in estrela ){
            e.SetActive(false);}

        idTema = PlayerPrefs.GetInt("idTema");
        Vidas = PlayerPrefs.GetInt("vidas");
        int notaFinall = PlayerPrefs.GetInt("notaFinal" + idTema.ToString());
        //notaFinal = 10;
        // Configura as estrelas do botão
        moedas.text = Vidas.ToString();
        estrelas();

        // temaJogo.txtNomeTema.text = "Oi Eu sou um teste";
        print("Iniciou o temaInfo");


    }

    void verificaNotaMinima(){

        btnTema.interactable = false;

        if(requerNotaMinima == true){

            int notafinal = PlayerPrefs.GetInt("notaFinal" + ( idTema - 1).ToString());

            if (notaFinal >= notaMinimaNecessaria ){

                btnTema.interactable = true;

            }else{



            }

        } 
        else
        {
            btnTema.interactable = true;
        }

    }

    public  void selecionarTema(){
    
        temaJogo.txtNomeTema.text = nomeDoTema;
        temaJogo.txtNomeTema.color = corTema;
        print("Passou por aqui :"+ nomeDoTema + "<--");
        estrelas();
    //    verificaNotaMinima();



    } 


    public void estrelas(){

        //Coloca todas as estrelas como false.
        foreach (GameObject e in estrela){

            e.SetActive(false);
        }

        //Zerou o numero de estrelas que tem de acender
        int nEstrelas = 0;

        //testa a nota, 


        //   idTemaTxt.text = idTema.ToString();
        //   estrela1.SetActive(false);
        //   estrela2.SetActive(false);
        //   estrela3.SetActive(false);

        idTema = PlayerPrefs.GetInt("idTema");
        int notaFinall = PlayerPrefs.GetInt("notaFinal" + idTema.ToString());
        print("Nota final no botão pequeno =" + notaFinall.ToString());
        if (notaFinall == 10)
        {
            nEstrelas = 3;
        }
        else if (notaFinall >= 7)
        {
            nEstrelas = 2;
        }
        else if (notaFinall >= 5)
        {
            nEstrelas = 1;
        }
        print("Numero de estrelas =" + nEstrelas.ToString());
        for (int i = 0; i < nEstrelas ; i++){

            estrela[i].SetActive(true);
        }



    }

}
