using UnityEngine;
using Assets.Scripts.Game.Weapons;

public class TestGame : GameMode {

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
			characters [i].SetGameMode (this);
		}

		currentMovement.character = characters [currentCharacter];
		currentAttack.character = characters [currentCharacter];
	}

	#region implemented abstract members of GameMode

	public override void OnCharacterFired (Character character)
	{
		if (character == characters [currentCharacter]) {
			NextPlayer ();
		}
	}

	#endregion

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonUp (switchCharacterButton)) {
			NextPlayer();
		}

	}

	void NextPlayer()
	{
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
