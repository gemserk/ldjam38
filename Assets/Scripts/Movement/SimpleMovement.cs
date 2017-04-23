using UnityEngine;

public class SimpleMovement : MonoBehaviour {

	public string moveHorizontalAxis;
	public string moveForwardAxis;

	public SimpleMovementCharacter character;

	public float movementCooldown;

	float lastMovement;
	
	// Update is called once per frame
	void Update () {

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
			character.MoveLeft ();
			lastMovement = Time.realtimeSinceStartup;
		} else if (moveHorizontal < 0) {
			character.MoveRight ();
			lastMovement = Time.realtimeSinceStartup;
		} else if (moveVertical > 0) {
			character.MoveForward ();
			lastMovement = Time.realtimeSinceStartup;
		} else if (moveVertical < 0) {
			character.MoveBackwards ();
			lastMovement = Time.realtimeSinceStartup;
		} 
		
	}
}
