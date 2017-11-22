using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {
	public GameObject panel1;
	public GameObject panel2;
	public Grapher g;
	private bool flag = true;
	public float a;
	public float b;

	void Start () {
		panel1.SetActive (true);
		panel2.SetActive (false);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (flag) {
				panel1.SetActive (false);
				panel2.SetActive (true);
			} else {
				panel1.SetActive (true);
				panel2.SetActive (false);
			}

			flag = !flag;
		}
	}

	public void ChangePanel(List<Vector2> points){
		panel1.SetActive (false);
		panel2.SetActive (true);
		flag = !flag;
		g.GenerateGraph1 (points);
	}


}
