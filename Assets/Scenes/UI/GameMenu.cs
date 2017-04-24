using UnityEngine;
using System;

public class GameMenu : MonoBehaviour {

	public CanvasGroup canvasGroup;

	bool open = false;

	public Action<GameMenu> openCallback;
	public Action<GameMenu> closeCallback;

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
