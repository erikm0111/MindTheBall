using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadmenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("ChangeLevel", 3.0f);
    }

    void ChangeLevel()
    {
        SceneManager.LoadScene("menu");
    }

    // Update is called once per frame
    void Update () {

    }
}
