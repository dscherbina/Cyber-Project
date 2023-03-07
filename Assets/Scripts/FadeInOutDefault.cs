using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOutDefault : MonoBehaviour
{

	public static bool sceneEndDefault;
	public float fadeSpeedDefault;
	public int nextLevelDefault;
	private Image _imageDefault;
	public static bool sceneStartingDefault;

	void Awake()
	{
		_imageDefault = GetComponent<Image>();
		_imageDefault.enabled = true;
		sceneStartingDefault = true;
		sceneEndDefault = false;

	}

	void Update()
	{
		if (sceneStartingDefault) StartScene();
		if (sceneEndDefault) EndScene();
	}

	void StartScene()
	{
		_imageDefault.color = Color.Lerp(_imageDefault.color, Color.clear, fadeSpeedDefault * Time.deltaTime);

		if (_imageDefault.color.a <= 0.01f)
		{
			_imageDefault.color = Color.clear;
			_imageDefault.enabled = false;
			sceneStartingDefault = false;

		}
	}

	void EndScene()
	{
		_imageDefault.enabled = true;
		_imageDefault.color = Color.Lerp(_imageDefault.color, Color.black, fadeSpeedDefault * Time.deltaTime);

		if (_imageDefault.color.a >= 0.95f)
		{

			_imageDefault.color = Color.black;
			SceneManager.LoadScene(2);
		}
	}
}

