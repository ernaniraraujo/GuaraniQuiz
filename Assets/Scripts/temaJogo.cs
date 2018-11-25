using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // Habilitar os componentes de UI no sistema.
using UnityEngine.SceneManagement;
using UnityEngine;


public class temaJogo : MonoBehaviour {

    //Declaração de variáveis



    public Button       btnPlay;
    public Text         txtNomeTema;
    public GameObject   infoTema;
    public Text         txtInfoTema;
    public GameObject   estrela1;
    public GameObject   estrela2;
    public GameObject   estrela3;
  // public GameObject[] estrelas;



    //Banco de Dados fake 
    public string[]     nomeTema;
    public int          numeroQuestoes;


    public int         idTema;
    public int Vidas;

    public bool requerNotaMinima;
    public int  notaMinimaNecessaria;

    private Button btnTema;


    private soundController soundController;


    // Use this for initialization
    void Start () {


        // recebe o botão
        btnTema = GetComponent<Button>();

       // btnPlay.interactable = false;
        infoTema.SetActive(false);
        estrela1.SetActive(false);
        estrela2.SetActive(false);
        estrela3.SetActive(false);
        txtInfoTema.text = "";
       
        soundController = FindObjectOfType(typeof(soundController)) as soundController;

    }

    private bool verificaNotaMinima()
    {

      //  btnTema.interactable = false;

        if (requerNotaMinima == true)
        {

            int notaFinal = PlayerPrefs.GetInt("notaFinal" + (idTema - 1).ToString());

            if (notaFinal >= notaMinimaNecessaria)
            {

        //        btnTema.interactable = true;
                return true;

            }
            else
            {
                print("Nao tem nota minima");
                return false;


            }

        }
        else
        {
          //  btnTema.interactable = true;
            return true;
        }
    }

        public void SelecioneTema(int i){

        idTema = i;


        Vidas = PlayerPrefs.GetInt("vidas");
        print("Vidas =" + Vidas.ToString());
//       print("Selecionou o tema =" + nomeTema[(idTema - 1)].ToString());

      /*  if (Vidas <= 0)
        {

            txtNomeTema.text = "Você não tem moedas o suficiente para jogar";


        }
        else
        {
*/
            // verificaNotaMinima nota minima para jogar.
            print("nota minima ta marcado? " + requerNotaMinima.ToString());

            if (requerNotaMinima == true)
            {

                bool verificaMinima = verificaNotaMinima();
                print("Retornou minimo ?" + verificaMinima.ToString());

                if (verificaMinima == false)
                {

                    txtNomeTema.text = "Você Tem de obter pelo menos 2 estrelas na fase anterior para poder jogar";
                }
                else{
                    AgoraRoda();
                }
            }

            else

            {
                AgoraRoda();
            }


    }

    private void AgoraRoda(){

        // PODE SELECONAR
        int notaFinal = PlayerPrefs.GetInt("notaFinal" + idTema.ToString());
        int acertos = PlayerPrefs.GetInt("acertos" + idTema.ToString());
        infoTema.SetActive(false);
        estrela1.SetActive(false);
        estrela2.SetActive(false);
        estrela3.SetActive(false);

        int idNomeTema = idTema - 1;
        txtNomeTema.text = nomeTema[idNomeTema];
        txtInfoTema.text = "Você acertou  " + acertos.ToString() + " de " + numeroQuestoes.ToString() + " questões";
        print("Funcionou as selação");
        if (notaFinal == 10)
        {
            estrela1.SetActive(true);
            estrela2.SetActive(true);
            estrela3.SetActive(true);
        }
        else if (notaFinal >= 7)
        {
            estrela1.SetActive(true);
            estrela2.SetActive(true);
            estrela3.SetActive(false);
        }
        else if (notaFinal >= 4)
        {
            estrela1.SetActive(true);
            estrela2.SetActive(false);
            estrela3.SetActive(false);
        }


        PlayerPrefs.SetInt("idTema", idTema);

        infoTema.SetActive(true);
        btnPlay.interactable = true;
        print("Botão Play Liberado  =" + idTema.ToString());


    }
    public void Jogar(){

        int idTemaJogar = PlayerPrefs.GetInt("idTema", idTema);

        print("Tema ao selecionar JPGAR =" + idTemaJogar.ToString());
        if(idTemaJogar > 0){
        SceneManager.LoadScene( "T"+ idTemaJogar.ToString());
        }


    }

    public void VaiParaLoja()
    {

            SceneManager.LoadScene("loja");


    }


    // Update is called once per frame
    void Update () {
		
	}
}
