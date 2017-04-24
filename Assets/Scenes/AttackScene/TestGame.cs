using UnityEngine;
using Assets.Scripts.Game.Weapons;
using System.Collections;

public class TestGame : GameMode {

	public string switchCharacterButton;

	public Character[] characters;

	// TODO: the camera pos could be calculated dynamically based on character pos...
	public float[] cameraRailPositions = new float[] { 0.1f, 0.9f };

	public SimpleMovementCharacter currentMovement;
	public SimpleAttackInput currentAttack;

	int currentCharacter = 0;

	public GameCamera gameCamera;

	public Weapon weaponPrefab;

	public Hud hud;

	void Start()
	{
		for (int i = 0; i < characters.Length; i++) {
			var character = characters [i];
			character.Equip (GameObject.Instantiate (weaponPrefab));
			characters [i].EnterWalkMode ();
			characters [i].SetGameMode (this);
			characters [i].SetHud (hud);
		}

		currentMovement.character = characters [currentCharacter];
		currentAttack.character = characters [currentCharacter];
	}

	#region implemented abstract members of GameMode

	public override void OnCharacterFired (Character character)
	{
		if (character == characters [currentCharacter]) {
			StartCoroutine (SwitchPlayers ());
		}
	}

	public override void OnCharacterDeath (Character character)
	{
		
	}

	#endregion

	public float switchPlayersDelay = 0.25f;

	IEnumerator SwitchPlayers()
	{
		currentMovement.enabled = false;
		currentAttack.enabled = false;

		yield return new WaitForSeconds (switchPlayersDelay);

	    Bomb projectile;
	    while((projectile = GameObject.FindObjectOfType<Bomb>()) != null)
	    {
	        yield return null;
	    }

		NextPlayer ();

		gameCamera.SetRailPosition (cameraRailPositions [currentCharacter]);
//		gameCamera.CenterOn (cameraPositions [currentCharacter]);

		yield return new WaitWhile (gameCamera.IsTransitioning);

		currentMovement.enabled = true;
		currentAttack.enabled = true;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonUp (switchCharacterButton)) {
			NextPlayer();
			gameCamera.SetRailPosition (cameraRailPositions [currentCharacter]);
//			gameCamera.CenterOn (cameraPositions [currentCharacter]);
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
	}
}
