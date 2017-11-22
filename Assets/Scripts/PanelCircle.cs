using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PanelCircle : MonoBehaviour {

	public CanvasManager cm;

	private List<GameObject> images;
	private List<Vector2> points;

	private float D;
	private float W;
	private int sessionCount = 0;
	private int progression = 0;
	private int n = 9;
	private float t;
	private int i;
	private List<float> lD;
	private List<float> lW;

	void Start () {
		t = 0;

		lD = new List<float> ();
		lW = new List<float> ();
		points = new List<Vector2> ();
		lD.Add (180);
		lD.Add (280);
		lD.Add (380);
		lD.Add (180);
		lD.Add (280);
		lD.Add (380);

		lW.Add (1.6f);
		lW.Add (0.3f);
		lW.Add (1.6f);
		lW.Add (0.5f);
		lW.Add (1.6f);
		lW.Add (0.5f);

		GenerateNew ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		t += Time.deltaTime;
		if (progression == n + 1) {
			//cm.ChangePanel (points1);
			if (sessionCount < 6)
				GenerateNew ();
			else{
				WriteOut ();
				Application.Quit ();
			}
				
		}
	}

	public void MouseClick(){
		if (progression != 0) {
			//Debug.Log ("D: " + D + "\nW: " + W);
			float ID = Mathf.Log ((2 * D / (100 * W / 2) + 1), 2);
			Vector2 newPoint = new Vector2 (ID, t);
			points.Add (newPoint);
			Debug.Log ("t = " + t + "\n ID = " + ID);
		}
		//Debug.Log (newPoint);
		progression++;
		t = 0;
	
		images [progression - 1].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("greyCircle");
		images [progression - 1].GetComponent<Button> ().enabled = false;

		if (progression <= n) {
			images [progression].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("blueCircle");
			images [progression].GetComponent<Button> ().enabled = true;
		}
	}

	public void GenerateNew(){
		progression = 0;
		//D = Random.Range (120, 280);
		//W = Random.Range (0.5f, 2f);

		D = lD [sessionCount];
		W = lW [sessionCount];
		Debug.Log ("SESSION "+sessionCount);
		sessionCount++;
		//points1 = new List<Vector2> ();
		images = new List<GameObject> ();

		List<GameObject> tmp = new List<GameObject> ();

		for (i = 0; i < n; i++) {
			tmp.Add(transform.GetChild(i).gameObject);
			tmp[i].GetComponent<RectTransform>().localPosition= new Vector3(D * Mathf.Cos(2 * Mathf.PI * i / n), D * Mathf.Sin(2 * Mathf.PI * i / n),0);
			tmp [i].GetComponent<RectTransform> ().localScale = new Vector3 (W, W, 1);
		}

		images.Add(tmp[0]);
		images.Add(tmp[5]);
		images.Add(tmp[1]);
		images.Add(tmp[6]);
		images.Add(tmp[2]);
		images.Add(tmp[7]);
		images.Add(tmp[3]);
		images.Add(tmp[8]);
		images.Add(tmp[4]);
		images.Add(tmp[0]);

		images [0].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("blueCircle");
		images [0].GetComponent<Button> ().enabled = true;
	}

	public void WriteOut(){
		string stuff = "";
		float actualID = points[0].x;
		float lastID = 0;
		foreach (Vector2 p in points) {
			if (p.x != actualID) {
				stuff += actualID.ToString() + "\n" + (p.y *1000).ToString () + ",";
				actualID = p.x;
			} else {
				stuff += (p.y *1000).ToString ()+",";
			}
			lastID = p.x;
		}
		stuff += lastID.ToString(); 

		Debug.Log (stuff);
		File.WriteAllText(Application.dataPath +"/data.txt",stuff);
	}
}
