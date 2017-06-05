using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class tilt : NetworkBehaviour {

    public Vector3 currentRot;
    public GameObject spherePrefab;
    public Transform sphereSpawn;

	// Use this for initialization
	void Start () {
        GameObject sphere = (GameObject)Instantiate(spherePrefab, sphereSpawn.position, sphereSpawn.rotation);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        currentRot = GetComponent<Transform>().eulerAngles;

        if ((Input.GetAxis("Horizontal") > .2) && (currentRot.z <= 10 || currentRot.z >= 348))
        {
            transform.Rotate(0, 0, 1);
        }
        if ((Input.GetAxis("Horizontal") < -.2) && (currentRot.z >= 349 || currentRot.z <= 11))
        {
            transform.Rotate(0, 0, -1);
        }
        if ((Input.GetAxis("Vertical") > .2) && (currentRot.x <= 10 || currentRot.x >= 348))
        {
            transform.Rotate(1, 0, 0);
        }
        if ((Input.GetAxis("Vertical") < -.2) && (currentRot.x >= 349 || currentRot.x <= 11))
        {
            transform.Rotate(-1, 0, 0);
        }
    }


    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
