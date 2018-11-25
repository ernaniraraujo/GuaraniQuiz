using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class notaFinal : MonoBehaviour {

    public int idTema;

    public Text txtNota;
    public Text txtInfoTema;
    public GameObject estrela1;
    public GameObject estrela2;
    public GameObject estrela3;

    public int notaFinall;
    public int acertos;

    private soundController HinoSom;


    // Use this for initialization
    void Start () {

        idTema = PlayerPrefs.GetInt("idTema");
        notaFinall = PlayerPrefs.GetInt("notaFinalTemp" + idTema.ToString());
        acertos = PlayerPrefs.GetInt("acertosTemp" + idTema.ToString());
        HinoSom = FindObjectOfType(typeof(soundController)) as soundController;
        estrela1.SetActive(false); 
        estrela2.SetActive(false);
        estrela3.SetActive(false);



        //   txtNota.text = notaFinall.ToString();
        txtNota.text = acertos.ToString();
        txtInfoTema.text = "Você acertou " + acertos.ToString() + " de 10 perguntas";

        if(acertos == 10){
            estrela1.SetActive(true);
            estrela2.SetActive(true);
            estrela3.SetActive(true);
            HinoSom.playHino();
        }
        else if (acertos >= 7)
        {
            estrela1.SetActive(true);
            estrela2.SetActive(true);
            estrela3.SetActive(false);
        }
        else if (acertos >= 4)
        {
            estrela1.SetActive(true);
            estrela2.SetActive(false);
            estrela3.SetActive(false);
        }



    }
	
	// botão jogar novamente

    public void jogarNovamente(){

        SceneManager.LoadScene("T" + idTema.ToString());


    }


}
