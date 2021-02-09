using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ {

	// I'm not convinced yet that this couldn't just be done via shader
	public class BillboardRotator : MonoBehaviour {
		void OnBecameInvisible() {
			enabled = false;
		}
		void OnBecameVisible() {
			enabled = true;
		}
		void OnWillRenderObject() {
			Vector3 disp = Camera.current.transform.position - transform.position;
			Vector3 up = Vector3.up - Vector3.Project(Vector3.up, disp.normalized);
			transform.rotation = Quaternion.LookRotation(disp, up);
		}
		
	}

}
