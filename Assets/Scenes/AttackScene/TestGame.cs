using UnityEngine;

public class TestGame : MonoBehaviour {

	public string switchCharacterButton;

	public GameObject[] characters;

	// TODO: the camera pos could be calculated dynamically based on character pos...
	public Transform[] cameraPositions;

	public SimpleMovementCharacter currentMovement;

	int currentCharacter = 0;

	public GameCamera gameCamera;

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonUp (switchCharacterButton)) {
			currentCharacter = (currentCharacter + 1) % characters.Length;
			currentMovement.character = characters [currentCharacter];

			gameCamera.CenterOn (cameraPositions [currentCharacter]);
		}

	}
}
