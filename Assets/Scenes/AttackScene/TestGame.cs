using UnityEngine;
using Assets.Scripts.Game.Weapons;

public class TestGame : MonoBehaviour {

	public string switchCharacterButton;

	public Character[] characters;

	// TODO: the camera pos could be calculated dynamically based on character pos...
	public Transform[] cameraPositions;

	public SimpleMovementCharacter currentMovement;
	public SimpleAttackInput currentAttack;

	int currentCharacter = 0;

	public GameCamera gameCamera;

	public Weapon weaponPrefab;

	void Start()
	{
		for (int i = 0; i < characters.Length; i++) {
			var character = characters [i];
			character.Equip (GameObject.Instantiate (weaponPrefab));
			characters [i].EnterWalkMode ();
		}

		currentMovement.character = characters [currentCharacter];
		currentAttack.character = characters [currentCharacter];
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonUp (switchCharacterButton)) {

			// reset characters to walk mode 

			for (int i = 0; i < characters.Length; i++) {
				characters [i].EnterWalkMode ();
			}

			currentCharacter = (currentCharacter + 1) % characters.Length;

			currentMovement.character = characters [currentCharacter];
			currentAttack.character = characters [currentCharacter];

			gameCamera.CenterOn (cameraPositions [currentCharacter]);
		}

	}
}
