using UnityEngine;

public class GameMenuInput : MonoBehaviour {

	public GameMenu gameMenu;

	public string optionsMenuActionName;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp(optionsMenuActionName)) {
			if (gameMenu.IsOpen()) {
				gameMenu.Close();
			} else {
				gameMenu.Open();
			}
		}
	}
}
