using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

	public CanvasGroup canvasGroup;

	bool open = false;

	public Action<GameMenu> openCallback;
	public Action<GameMenu> closeCallback;

	public Action<GameMenu> restartCallback;

	public Button resumeButton;
	public Button restartButton;

	void Start()
	{
		resumeButton.onClick.AddListener(delegate() {
			Close();	
		});

		restartButton.onClick.AddListener(delegate() {
			if (restartCallback != null)
				restartCallback(this);
		});
	}

	public void Init(bool open)
	{
		this.open = open;
		if (open)
			Open();
		else
			Close();
	}

	public void Open()
	{
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;
		canvasGroup.alpha = 1;
		open = true;

		if (openCallback != null)
			openCallback(this);
	}

	public void Close()
	{
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
		canvasGroup.alpha = 0;
		open = false;

		if (closeCallback != null) {
			closeCallback(this);
		}
	}

	public bool IsOpen()
	{
		return open;
	}


}
