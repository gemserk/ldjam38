using UnityEngine;
using Assets.Scripts.Game.Weapons;
using System.Collections;
using UnityEngine.SceneManagement;

public static class ArrayExtensions
{
	public static T RandomItem<T>(this T[] array)
	{
		return array [UnityEngine.Random.Range (0, array.Length)];
	}
}

public static class TransformExtensions
{
	public static Transform RandomChild(this Transform t)
	{
		return t.GetChild(UnityEngine.Random.Range(0, t.childCount));
	}
}

public class TestGame : GameMode {

	public GameMenu gameMenu;

	public string switchCharacterButton;

	public Character[] characters;

	// TODO: the camera pos could be calculated dynamically based on character pos...
	public float[] cameraRailPositions = new float[] { 0.1f, 0.9f };

	public SimpleMovementInput currentMovementInput;

	public SimpleMovementCharacter currentMovement;
	public SimpleAttackInput currentAttack;

	int currentCharacter = 0;

	public GameCamera gameCamera;

	public Weapon weaponPrefab;

	public Weapon[] weaponPrefabsPerPlayer;

	public Hud hud;

	public GameObject possiblePlatformsContainer;

	public WorldMovement worldMovement;

	void Start()
	{
		for (int i = 0; i < characters.Length; i++) {
			var character = characters [i];

			var playerWeaponPrefab = weaponPrefab;

			if (weaponPrefabsPerPlayer != null)
				playerWeaponPrefab = weaponPrefabsPerPlayer [i];	

			character.Equip (GameObject.Instantiate (playerWeaponPrefab));
			characters [i].EnterWalkMode ();
			characters [i].SetGameMode (this);
			characters [i].SetHud (hud);

			var simpleMovement = characters [i].GetComponent<SimpleMovementCharacter> ();
			if (simpleMovement != null) {
				simpleMovement.worldMovement = worldMovement;
			}

			// create platform for player.

			if (possiblePlatformsContainer != null && possiblePlatformsContainer.transform.childCount > 0) {
				var platformSource = possiblePlatformsContainer.transform.RandomChild ();

//				var playerPlatformSource = possiblePlatforms.RandomItem ();
				var playerPlatform = GameObject.Instantiate (platformSource);
				playerPlatform.gameObject.SetActive (true);

				playerPlatform.transform.position = character.transform.position;
				playerPlatform.transform.localEulerAngles = new Vector3 (0, 90 * UnityEngine.Random.Range (0, 4), 0);
			}
		}

		if (currentMovement != null)
			currentMovement.character = characters [currentCharacter];
		
		currentAttack.character = characters [currentCharacter];

		if (currentMovementInput != null) {
			currentMovementInput.simpleMovement = characters [currentCharacter].GetComponent<SimpleMovementCharacter> ();
		}

		if (gameMenu != null) {
			gameMenu.Init(false);
		
			gameMenu.openCallback = delegate(GameMenu menu) {
				LeanTween.cancelAll(gameObject);
				LeanTween.value(gameObject, Time.timeScale, 0.0f, 2.0f).setUseEstimatedTime(true).setEase(LeanTweenType.easeOutQuad).setOnUpdate(delegate(float v) {
					Time.timeScale = v;
				});
			};

			gameMenu.closeCallback = delegate(GameMenu menu) {
				LeanTween.cancelAll(gameObject);
				LeanTween.value(gameObject, Time.timeScale, 1.0f, 0.25f).setUseEstimatedTime(true).setOnUpdate(delegate(float v) {
					Time.timeScale = v;
				});
			};

			gameMenu.restartCallback = delegate(GameMenu obj) {
				RestartGame();
			};
		}
	}

	void RestartGame()
	{
		LeanTween.cancelAll(gameObject);
		Time.timeScale = 1.0f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	#region implemented abstract members of GameMode

	Coroutine switchPlayersCoroutine;

	public override void OnCharacterFired (Character character)
	{
		if (character == characters [currentCharacter]) {
			switchPlayersCoroutine = StartCoroutine (SwitchPlayers ());
		}
	}

	public override void OnCharacterDeath (Character character)
	{
		StartCoroutine (PlayerLoseAnimation (character));
	}

	#endregion

	public float switchPlayersDelay = 0.25f;

	float showMenuDelay = 4.0f;

	IEnumerator PlayerLoseAnimation(Character character)
	{
		if (switchPlayersCoroutine != null)
			StopCoroutine (switchPlayersCoroutine);
		
		if (currentMovement != null)
			currentMovement.enabled = false;
		currentAttack.enabled = false;

		if (currentMovementInput != null)
			currentMovementInput.enabled = false;

//		yield return new WaitForSeconds (0.1f);

		gameCamera.CenterOn(character.transform.position);
//		gameCamera.

		yield return new WaitForSeconds (showMenuDelay);

		if (gameMenu != null) {
			gameMenu.Open ();
		}
	}

	IEnumerator SwitchPlayers()
	{
		if (currentMovement != null)
			currentMovement.enabled = false;
		currentAttack.enabled = false;

		if (currentMovementInput != null)
			currentMovementInput.enabled = false;

		yield return new WaitForSeconds (switchPlayersDelay);

	    Bomb projectile;
	    while((projectile = GameObject.FindObjectOfType<Bomb>()) != null)
	    {
	        yield return null;
	    }

		yield return new WaitForSeconds (0.1f);

//		var nextPlayer = GetNextPlayer ();
//		if (nextPlayer.IsDead)
//			yield break;

		NextPlayer ();

		//gameCamera.SetRailPosition (cameraRailPositions [currentCharacter]);
	    gameCamera.CenterOn(characters[currentCharacter].transform.position);
//		gameCamera.CenterOn (cameraPositions [currentCharacter]);

		yield return new WaitWhile (gameCamera.IsTransitioning);

		if (currentMovement != null)
			currentMovement.enabled = true;
		currentAttack.enabled = true;

		if (currentMovementInput != null)
			currentMovementInput.enabled = true;

		switchPlayersCoroutine = null;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonUp (switchCharacterButton)) {
			NextPlayer();
		    gameCamera.CenterOn(characters[currentCharacter].transform.position);
//			gameCamera.CenterOn (cameraPositions [currentCharacter]);
		}

	}

	Character GetNextPlayer()
	{
		int nextCharacter = (currentCharacter + 1) % characters.Length;
		return characters [nextCharacter];
	}

	void NextPlayer()
	{
		// reset characters to walk mode 

		for (int i = 0; i < characters.Length; i++) {
			characters [i].EnterWalkMode ();
		}

		currentCharacter = (currentCharacter + 1) % characters.Length;

		if (currentMovement != null)
			currentMovement.character = characters [currentCharacter];
		
		currentAttack.character = characters [currentCharacter];

		if (currentMovementInput != null) {
			currentMovementInput.simpleMovement = characters [currentCharacter].GetComponent<SimpleMovementCharacter> ();
		}
	}
}
