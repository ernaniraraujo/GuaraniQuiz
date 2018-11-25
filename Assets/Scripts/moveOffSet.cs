using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOffSet : MonoBehaviour {


    private Material materialAtual;
    public float velocidade;
    private float offSet;



	// Use this for initialization
	void Start () {

        // Obytem o material atual.
        materialAtual = GetComponent<Renderer>().material;


         


	}
	
	// Update is called once per frame
	void FixedUpdate () {

        offSet += 0.01f;
      //  print(offSet);
        materialAtual.SetTextureOffset("_MainTex", new Vector2 (offSet * velocidade,0));




	}
}
