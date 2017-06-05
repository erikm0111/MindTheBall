using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontBounce : MonoBehaviour {
  private Rigidbody rigid;
  
  void Start () {
    rigid = GetComponent<Rigidbody>();
	}

	void Update () {
    if (rigid.velocity.y > 0) {
      rigid.velocity = new Vector2(rigid.velocity.x, 0f);
    }
	}
}
