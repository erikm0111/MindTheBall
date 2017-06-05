using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ball : NetworkBehaviour {

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Goal")
        {
            Destroy(gameObject, .2f);
            Debug.Log("You win!");
            //SceneManager.LoadScene("level2");
            //NetworkManager.singleton.ServerChangeScene("level2");
            NetworkManager.Shutdown();
            NetworkManager.singleton.ServerChangeScene("mainmenu");
        }
    }

}
