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

	public Hud hud;

	public GameObject possiblePlatformsContainer;

	public WorldMovement worldMovement;

	void Start()
	{
		for (int i = 0; i < characters.Length; i++) {
			var character = characters [i];
			character.Equip (GameObject.Instantiate (weaponPrefab));
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

		NextPlayer ();

		gameCamera.SetRailPosition (cameraRailPositions [currentCharacter]);
//		gameCamera.CenterOn (cameraPositions [currentCharacter]);

		yield return new WaitWhile (gameCamera.IsTransitioning);

		if (currentMovement != null)
			currentMovement.enabled = true;
		currentAttack.enabled = true;

		if (currentMovementInput != null)
			currentMovementInput.enabled = true;
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

		if (currentMovement != null)
			currentMovement.character = characters [currentCharacter];
		
		currentAttack.character = characters [currentCharacter];

		if (currentMovementInput != null) {
			currentMovementInput.simpleMovement = characters [currentCharacter].GetComponent<SimpleMovementCharacter> ();
		}
	}
}
