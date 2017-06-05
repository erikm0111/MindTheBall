using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClickPlay()
    {
        //Application.LoadLevel("Play");
        SceneManager.LoadScene("level1");
    }
}
