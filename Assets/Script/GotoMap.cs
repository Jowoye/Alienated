using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GotoMap : MonoBehaviour {

    public Button returnButton;
	// Use this for initialization
	void Start () {
        returnButton.onClick.AddListener(GoToMap);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void GoToMap() {
       
        SceneManager.LoadScene("Map");
    }
}
