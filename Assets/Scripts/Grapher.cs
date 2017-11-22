using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class Grapher : MonoBehaviour {
	public GameObject point;
	public GameObject line;
	public CanvasManager cm;

	private GameObject p;
	private GameObject l;
	private float offsetX = -350;
	private float offsetY = -250;
	private List<Vector2> allPoints;
	private float a;
	private float b;

	void Start(){
		allPoints = new List<Vector2> ();
	}

	void Update(){
	}

	public void GenerateGraph1(List<Vector2> points){
		int i = 0;
		for (i = 0; i < points.Count; i++) {
			allPoints.Add (points [i]);
			p = Instantiate (point, transform);
			p.transform.localPosition = new Vector3 (offsetX + (points[i].x), offsetY + (points[i].y), 0);
			//p.transform.localPosition = Vector3.zero;
		}
	}
		

	public void DrawRegression(){
		WriteOut ();
		//LinearRegression (allPoints);
		//l = Instantiate (line, transform);
		//Debug.Log (Mathf.Atan (b));
		//l.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan(b));
		//l.transform.localPosition = new Vector3 (offsetX, a+offsetY, 0);
	}

	/// <summary>
	/// Fits a line to a collection of (x,y) points.
	/// </summary>
	/// <param name="xVals">The x-axis values.</param>
	/// <param name="yVals">The y-axis values.</param>
	/// <param name="inclusiveStart">The inclusive inclusiveStart index.</param>
	/// <param name="exclusiveEnd">The exclusive exclusiveEnd index.</param>
	/// <param name="rsquared">The r^2 value of the line.</param>
	/// <param name="yintercept">The y-intercept value of the line (i.e. y = ax + b, yintercept is b).</param>
	/// <param name="slope">The slop of the line (i.e. y = ax + b, slope is a).</param>
	public void LinearRegression(List<Vector2> points1){
		float sumOfX = 0;
		float sumOfY = 0;
		float sumOfXSq = 0;
		float sumOfYSq = 0;
		float ssX = 0;
		float ssY = 0;
		float sumCodeviates = 0;
		float sCo = 0;

		int inclusiveStart = 0;
		int exclusiveEnd = allPoints.Count;
		float count = exclusiveEnd - inclusiveStart;
		for (int ctr = inclusiveStart; ctr < exclusiveEnd; ctr++)
		{
			float x = points1[ctr].x;
			float y = points1[ctr].y;
			sumCodeviates += x * y;
			sumOfX += x;
			sumOfY += y;
			sumOfXSq += x * x;
			sumOfYSq += y * y;
		}
		ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
		ssY = sumOfYSq - ((sumOfY * sumOfY) / count);
		float RNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
		float RDenom = (count * sumOfXSq - (sumOfX * sumOfX))
			* (count * sumOfYSq - (sumOfY * sumOfY));
		sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

		float meanX = sumOfX / count;
		float meanY = sumOfY / count;
		//double dblR = RNumerator / Mathf.Sqrt(RDenom);
		//rsquared = dblR * dblR;
		a =  meanY - ((sCo / ssX) * meanX);
		b = sCo / ssX;
		Debug.Log ("final");
		Debug.Log ("a :"+a);
		Debug.Log ("b :"+b);
	}



	public void WriteOut(){
		string stuff = "";
		float actualID = allPoints[0].x;
		float lastID = 0;
		foreach (Vector2 p in allPoints) {
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
























	/*[Range(10, 100)]
	public int resolution = 10;

	private int currentResolution;
	private ParticleSystem.Particle[] points;
	private ParticleSystem ps;

	private void CreatePoints () {
		currentResolution = resolution;
		ps = GetComponent<ParticleSystem> ();
		points = new ParticleSystem.Particle[resolution];
		float increment = 1f / (resolution - 1);
		for(int i = 0; i < resolution; i++){
			float x = i * increment;
			points[i].position = new Vector3(x, 0f, 0f);
			points[i].startColor = new Color(x, 0f, 0f);
			points[i].startSize = 0.1f;
		}
	}

	void Update () {
		if (currentResolution != resolution || points == null) {
			CreatePoints();
		}
		for (int i = 0; i < resolution; i++) {
			Vector3 p = points[i].position;
			p.y = p.x;
			points[i].position = p;
			Color c = points [i].GetCurrentColor (ps);
			c.g = p.y;
			points[i].startColor = c;
		}
		ps.SetParticles(points, points.Length);
	}
	*/
}