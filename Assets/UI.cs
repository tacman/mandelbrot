using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
	public Text framerate, zoom, iterations;
	public GameObject ui;
	public Mandelbrot mandelbrot;
	public float updateTime = 0.5f;

	void Start()
	{
		StartCoroutine(UpdateFPSCounter());
	}

	IEnumerator UpdateFPSCounter()
	{
		const string fpsPrefix = "Framerate: ";
		while (true)
		{
			int count = 0;
			float timeSinceLastUpdate = 0.0f;
			while (timeSinceLastUpdate < updateTime)
			{
				yield return new WaitForEndOfFrame();
				timeSinceLastUpdate += Time.unscaledDeltaTime;
				count++;
			}
			framerate.text = fpsPrefix + CalculateFPS(timeSinceLastUpdate, count);
		}
	}

	public void OnHelp()
	{
		// toggle help screen
		ui.SetActive(!ui.activeInHierarchy);
	}

	string CalculateFPS(float timeSinceLastUpdate, int count)
	{
		if (count != 0)
		{
			float averageTimePerFrame = timeSinceLastUpdate / count;
			int newFramerate = (int)(1.0f / averageTimePerFrame);
			return newFramerate.ToString();
		}
		else return "...";
	}

	void Update()
	{
		// if (Input.GetButtonDown("Help"))
		// {
		// 	if (ui.activeInHierarchy) ui.SetActive(false);
		// 	else ui.SetActive(true);
		// }
		zoom.text = "Zoom: " + (1 / mandelbrot.scale).ToString("F2") + "x";
		iterations.text = "Iterations: " + mandelbrot.maxIter;
	}
	
	public void OnQuit()
	{
		// save any game data here
#if UNITY_EDITOR
		// Application.Quit() does not work in the editor so
		// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
		UnityEditor.EditorApplication.isPlaying = false;
#else
             Application.Quit();
#endif
	}
}
