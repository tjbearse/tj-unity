using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

	
namespace TJ.Measurements {

	public class Ruler : MonoBehaviour {
		#if UNITY_EDITOR
		public bool components = true;

		void OnDrawGizmos() {

			Handles.matrix = transform.localToWorldMatrix;
			Gizmos.matrix = transform.localToWorldMatrix;

			Handles.Label(
				Vector3.zero,
				$"scale: {transform.localScale}\nrotation:{transform.rotation.eulerAngles}"
			);

			int n = transform.childCount;
			for(int i = 0; i < n; i++) {
				Transform t = transform.GetChild(i);
				if (t == null || !t.gameObject.activeInHierarchy) continue;
				Vector3 tLocal = transform.InverseTransformPoint(t.transform.position);

				// hypot
				Gizmos.color = Color.magenta;
				float distance = tLocal.magnitude;

				if (!components) {
					// TODO camera scale this
					float radius = 3 / Mathf.Max(transform.localScale.y, transform.localScale.y, transform.localScale.z);
					Gizmos.DrawSphere(tLocal, radius);
					Handles.Label(tLocal * 1.1f, $"[{t.gameObject.name}] d={distance}");
				} else {
					Handles.Label(tLocal * 1.1f, $"[{t.gameObject.name}]");
					Handles.Label(tLocal * .5f, $"dist: {distance}");
					Gizmos.DrawLine(Vector3.zero, tLocal);

					// x
					Gizmos.color = Color.red;
					Vector3 end = Vector3.right * tLocal.x;
					Gizmos.DrawLine(Vector3.zero, end);
					distance = tLocal.x;
					Handles.Label(end * .75f, $"x: {distance}");

					// y
					Gizmos.color = Color.green;
					end = tLocal;
					end.y = 0;
					Gizmos.DrawLine(tLocal, end);
					distance = tLocal.y;
					Handles.Label(Vector3.Lerp(tLocal, end, .5f), $"y: {distance}");

					// z
					Gizmos.color = Color.blue;
					end = tLocal;
					end.z = 0;
					Gizmos.DrawLine(end, tLocal);
					distance = tLocal.z;
					Handles.Label(Vector3.Lerp(end, tLocal, .75f), $"z: {distance}");
				}
			}
		}
		
		#endif
	}

}
