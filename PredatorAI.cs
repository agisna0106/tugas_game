using UnityEngine;

public class PredatorAI : MonoBehaviour
{
	public Transform player;
	public GameManager gameManager;
	private PlayerMovement playerScript;

	public float normalSpeed = 2f;
	public float chaseSpeed = 2.7f;
	public float recoverSpeed = 1.8f;

	public float recoverTime = 1.5f;
	private float recoverTimer;

	public float dissapearTime = 1f;
	private float dissapearTimer;

	public float detectionRange = 4f;
	public float attackRange = 1.2f;

	public float reactionTime = 0.4f;
	private float reactionTimer;

	private enum State {
		Patrol, Chase, Attack, Recover
	}

	private State currentState;

	void Start()
	{
		currentState = State.Patrol;
		if (player == null)
		{
			player = GameObject.FindWithTag("Player").transform;
		}

		playerScript = player.GetComponent<PlayerMovement>();
	}

	void Update()
	{
		AdjustDifficulty();

		float distance = Vector2.Distance(transform.position, player.position);

		switch (currentState)
		{
			case State.Patrol:
				Patrol();

				if (distance < detectionRange)
				{
					reactionTimer = reactionTime;
					currentState = State.Chase;
				}
				break;

			case State.Chase:
				reactionTimer -= Time.deltaTime;

				if (reactionTimer <= 0)
				{
					Chase();
				}

				if (distance < attackRange)
					currentState = State.Attack;
				break;

			case State.Attack:
				Attack();
				break;

			case State.Recover:
				Recover();

				recoverTimer -= Time.deltaTime;
				dissapearTimer -= Time.deltaTime;

				if (dissapearTimer <= 0)
				{
					Destroy(gameObject);
				}

				else if (recoverTimer <= 0)
				{
					currentState = State.Chase;
				}
				break;
		}
	}

	void AdjustDifficulty()
	{
		if (gameManager == null) return;

		if (gameManager.currentPhase == 2) 
		{
			chaseSpeed = 3f;
		} 
		else if (gameManager.currentPhase == 3)
		{
			chaseSpeed = 3.5f;
		}
	}

	void Patrol() 
	{
		transform.Translate(Vector2.left * normalSpeed * Time.deltaTime);
	}

	void Chase() 
	{
		float directionX = -1f;

		float directionY = player.position.y - transform.position.y;

		Vector2 direction = new Vector2(directionX, directionY * 0.4f);

		transform.Translate(direction.normalized * chaseSpeed * Time.deltaTime);
	}

	void Attack()
	{
		if (playerScript != null)
		{
			playerScript.health--;

			Debug.Log("Diserang predator! HP: " + playerScript.health);
		}

		recoverTimer = recoverTime;
		dissapearTimer = dissapearTime;

		currentState = State.Recover;
	}

	void Recover()
	{
		Vector2 direction = (transform.position - player.position).normalized;

		transform.Translate(direction * recoverSpeed * Time.deltaTime);
	}
}
