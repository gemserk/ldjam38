using UnityEngine;
using UnityEngine.Serialization;

public class SimpleMovementInput : MonoBehaviour {

	public string moveHorizontalAxis;
	public string moveForwardAxis;

	[FormerlySerializedAs("character")]
	public SimpleMovementCharacter simpleMovement;

	public float movementCooldown;

	float lastMovement;
	
	// Update is called once per frame
	void Update () {

		if (simpleMovement == null)
			return;

		if (simpleMovement.character.InAttackMode ())
			return;

		float moveHorizontal = Input.GetAxis (moveHorizontalAxis);
		float moveVertical = Input.GetAxis (moveForwardAxis);

		// if not moving, reset cooldown.
		if (Mathf.Abs (moveHorizontal) < 0.01f && Mathf.Abs (moveVertical) < 0.01f)
			lastMovement = 0;

		float elapsedTime = Time.realtimeSinceStartup - lastMovement;

		bool movementReady = elapsedTime > movementCooldown;

		if (!movementReady) {
			return;
		}

		if (moveHorizontal > 0) {
			simpleMovement.MoveLeft ();
			lastMovement = Time.realtimeSinceStartup;
		} else if (moveHorizontal < 0) {
			simpleMovement.MoveRight ();
			lastMovement = Time.realtimeSinceStartup;
		} else if (moveVertical > 0) {
			simpleMovement.MoveForward ();
			lastMovement = Time.realtimeSinceStartup;
		} else if (moveVertical < 0) {
			simpleMovement.MoveBackwards ();
			lastMovement = Time.realtimeSinceStartup;
		} 
		
	}
}
