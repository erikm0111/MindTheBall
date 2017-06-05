using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ball : MonoBehaviour {

  void Start () {
	}

	void Update () {
	}

  void OnTriggerEnter(Collider other)
  {
      if(other.name == "Goal")
      {
          Destroy(gameObject, .2f);
          Debug.Log("You win!");
      }
  }

}
