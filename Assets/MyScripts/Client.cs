using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour {

	public Transform ball;
  public Transform platform;

  public void NetworkUpdate(Vector3 position, Quaternion rotation) {
    ball.localPosition = position;
    platform.rotation = rotation;
  }
}
