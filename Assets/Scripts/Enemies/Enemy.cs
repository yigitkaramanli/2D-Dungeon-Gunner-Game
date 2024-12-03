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
[RequireComponent(typeof(EnemyMovementAI))]
[DisallowMultipleComponent]
#endregion
public class Enemy : MonoBehaviour
{
    public EnemyDetailsSO enemyDetails;
    private EnemyMovementAI enemyMovementAI;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    private CircleCollider2D circleCollider2D;
    private PolygonCollider2D polygonCollider2D;
    private Animator animator;
    [HideInInspector] public SpriteRenderer[] spriteRendererArray;

    private void Awake()
    {
        enemyMovementAI = GetComponent<EnemyMovementAI>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        idleEvent = GetComponent<IdleEvent>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        spriteRendererArray = GetComponentsInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
}