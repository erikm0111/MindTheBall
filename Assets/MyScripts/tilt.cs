using UnityEngine;
using System.Collections;

public class tilt : MonoBehaviour {

  public Vector3 currentRot;
  public GameObject spherePrefab;
  public Transform sphereSpawn;

	void Start () {

	}

	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Quaternion rot = Quaternion.Euler(vertical * 11f, 0f, horizontal * 11f);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 10f);
    }
}
