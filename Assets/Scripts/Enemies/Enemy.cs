using System;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine;

#region REQUIRE COMPONENTS
[RequireComponent(typeof(SortingGroup))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MovementToPositionEvent))]
[RequireComponent(typeof(MovementToPosition))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(AnimateEnemy))]
[RequireComponent(typeof(MaterializeEffect))]
[RequireComponent(typeof(EnemyMovementAI))]
[DisallowMultipleComponent]
#endregion
public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemyDetailsSO enemyDetails;
    private EnemyMovementAI enemyMovementAI;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    private MaterializeEffect materializeEffect;
    private CircleCollider2D circleCollider2D;
    private PolygonCollider2D polygonCollider2D;
    public Animator animator;
    [HideInInspector] public SpriteRenderer[] spriteRendererArray;

    private void Awake()
    {
        enemyMovementAI = GetComponent<EnemyMovementAI>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        materializeEffect = GetComponent<MaterializeEffect>();
        idleEvent = GetComponent<IdleEvent>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        spriteRendererArray = GetComponentsInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void EnemyInitialization(EnemyDetailsSO enemDetails, int enemySpawnNumber, DungeonLevelSO dungeonLevel)
    {
        this.enemyDetails = enemDetails;

        SetEnemyMovementUpdateFrame(enemySpawnNumber);
        SetEnemyAnimationSpeed();

        StartCoroutine(MaterializeEnemy());
    }

    private void SetEnemyMovementUpdateFrame(int enemySpawnNumber)
    {
        enemyMovementAI.SetUpdatedFrameNumber(enemySpawnNumber % Settings.targetFrameRateToSpreadPathfindingOver);
    }

    private void SetEnemyAnimationSpeed()
    {
        animator.speed = enemyMovementAI.moveSpeed / Settings.baseSpeedForEnemyAnimations;
    }

    private IEnumerator MaterializeEnemy()
    {
        EnemyEnable(false);

        yield return StartCoroutine(materializeEffect.MaterializeCoroutine(enemyDetails.enemyMaterializeShader,
            enemyDetails.enemyMaterializeColor, enemyDetails.enemyMaterializeTime, spriteRendererArray,
            enemyDetails.enemyStandardMaterial));

        EnemyEnable(true);
    }

    private void EnemyEnable(bool isEnabled)
    {
        circleCollider2D.enabled = isEnabled;
        polygonCollider2D.enabled = isEnabled;
        enemyMovementAI.enabled = isEnabled;
    }
}