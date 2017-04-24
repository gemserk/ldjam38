using UnityEngine;

public class GameMenu : MonoBehaviour {

	public CanvasGroup canvasGroup;

	bool open = false;

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
	}

	public void Close()
	{
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
		canvasGroup.alpha = 0;
		open = false;
	}

	public bool IsOpen()
	{
		return open;
	}


}
