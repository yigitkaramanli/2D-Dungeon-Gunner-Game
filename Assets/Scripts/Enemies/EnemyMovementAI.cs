using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[DisallowMultipleComponent]
public class EnemyMovementAI : MonoBehaviour
{
    #region Tooltip
    [Tooltip("MovementDetailsSO scriptable object containing movement details")]
    #endregion
    [SerializeField]
    private MovementDetailsSO movementDetails;

    private Enemy enemy;
    private Stack<Vector3> movementSteps = new Stack<Vector3>();
    private Vector3 playerReferencePosition;
    private Coroutine moveEnemyRoutine;
    private float currentEnemyPathRebouldCooldown;
    private WaitForFixedUpdate waitForFixedUpdate;
    [HideInInspector] public float moveSpeed;
    private bool chasePlayer = false;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        moveSpeed = movementDetails.GetMoveSpeed();
    }

    private void Start()
    {
        waitForFixedUpdate = new WaitForFixedUpdate();
        playerReferencePosition = GameManager.Instance.GetPlayer().GetPlayerPosition();
    }

    private void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        currentEnemyPathRebouldCooldown -= Time.deltaTime;

        if (!chasePlayer && Vector3.Distance(transform.position, GameManager.Instance.GetPlayer().GetPlayerPosition()) <
            enemy.enemyDetails.chaseDistance)
        {
            chasePlayer = true;
        }

        if (!chasePlayer)
        {
            return;
        }

        if (currentEnemyPathRebouldCooldown <= 0f ||
            (Vector3.Distance(playerReferencePosition, GameManager.Instance.GetPlayer().GetPlayerPosition()) >
             Settings.playerMoveDistanceToRebuildPath))
        {
            currentEnemyPathRebouldCooldown = Settings.enemyPathRebuildCooldown;

            playerReferencePosition = GameManager.Instance.GetPlayer().GetPlayerPosition();

            CreatePath();

            if (movementSteps != null)
            {
                if (moveEnemyRoutine != null)
                {
                    enemy.idleEvent.CallIdleEvent();
                    StopCoroutine(moveEnemyRoutine);
                }

                moveEnemyRoutine = StartCoroutine(MoveEnemyRoutine(movementSteps));
            }
        }
    }

    private IEnumerator MoveEnemyRoutine(Stack<Vector3> movementSteps)
    {
        while (movementSteps.Count > 0)
        {
            Vector3 nextPosition = movementSteps.Pop();

            while (Vector3.Distance(nextPosition, transform.position) > 0.2f)
            {
                enemy.movementToPositionEvent.CallMovementToPositionEvent(nextPosition, transform.position, moveSpeed,
                    (nextPosition - transform.position).normalized);

                yield return waitForFixedUpdate;
            }

            yield return waitForFixedUpdate;
        }

        enemy.idleEvent.CallIdleEvent();
    }

    private void CreatePath()
    {
        Room currentRoom = GameManager.Instance.GetCurrentRoom();
        Grid grid = currentRoom.instantiatedRoom.grid;

        Vector3Int playerGridPosition = GetNearestNonObstaclePlayerPosition(currentRoom);

        Vector3Int enemyGridPosition = grid.WorldToCell(transform.position);
        movementSteps = AStar.BuildPath(currentRoom, enemyGridPosition, playerGridPosition);

        if (movementSteps != null)
        {
            movementSteps.Pop();
        }
        else
        {
            enemy.idleEvent.CallIdleEvent();
        }
    }

    private Vector3Int GetNearestNonObstaclePlayerPosition(Room currentRoom)
    {
        Vector3 playerPosition = GameManager.Instance.GetPlayer().GetPlayerPosition();
        Vector3Int playerCellPosition = currentRoom.instantiatedRoom.grid.WorldToCell(playerPosition);

        Vector2Int adjustedPlayerCellPosition =
            new Vector2Int(playerCellPosition.x - currentRoom.roomTemplateLowerBounds.x,
                playerCellPosition.y - currentRoom.roomTemplateLowerBounds.y);

        //find out if the player is on a half obstacle tile or not 0 if it is an obstacle tile
        int obstacle =
            currentRoom.instantiatedRoom.aStarMovementPenalty[adjustedPlayerCellPosition.x,
                adjustedPlayerCellPosition.y];

        if (obstacle != 0)
        {
            return playerCellPosition;
        }
        else // get the nearest surrounding cell that is not an obstacle 
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (j == 0 && i == 0)
                        continue;
                    try
                    {
                        obstacle = currentRoom.instantiatedRoom.aStarMovementPenalty[adjustedPlayerCellPosition.x + i,
                            adjustedPlayerCellPosition.y + j];
                        if (obstacle != 0)
                        {
                            return new Vector3Int(playerCellPosition.x + i, playerCellPosition.y + j, 0);
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            return playerCellPosition;
        }
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(movementDetails), movementDetails);
    }
#endif
    #endregion
}