using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundController : MonoBehaviour {


    private AudioSource AudioS;
    public AudioClip somAcerto, somErro, hinoTime;



    void Awake(){

        DontDestroyOnLoad(this.gameObject);

    }


	// Use this for initialization
	void Start () {

        AudioS = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playAcerto(){

        AudioS.PlayOneShot(somAcerto);
    }

    public void playErro(){

        AudioS.PlayOneShot(somErro);

    }
    public void playHino()
    {

        AudioS.PlayOneShot(hinoTime);

    }
}
