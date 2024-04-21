using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "MovementDetails_", menuName = "Scriptable Objects/Movement/MovementDetails")]
public class MovementDetailsSO : ScriptableObject
{
    #region Header MOVEMENT DETAILS
    [Space(10)]
    [Header("MOVEMENT DETAILS")]
    #endregion

    #region Tooltip
    [Tooltip(
        "The minimum move speed. The GetMoveSpeed method calculates a random value between the minimum and maximum.")]
    #endregion
    public float minMoveSpeed = 8f;

    #region Tooltip
    [Tooltip(
        "The maximum move speed. The GetMoveSpeed method calculates a random value between the minimum and maximum.")]
    #endregion
    public float maxMoveSpeed = 8f;

    #region Tooltip
    [Tooltip("If there is roll movement, this is the roll speed.")]
    #endregion
    public float rollSpeed;

    #region Tooltip
    [Tooltip("If there is roll movement, Roll distance of the player.")]
    #endregion
    public float rollDistance;

    #region Tooltip
    [Tooltip("If there is roll movement, this is the cooldown time in seconds between roll actions.")]
    #endregion
    public float rollCoolDownTime;


    public float GetMoveSpeed()
    {
        if (minMoveSpeed == maxMoveSpeed)
        {
            return minMoveSpeed;
        }
        else
        {
            return Random.Range(minMoveSpeed, maxMoveSpeed);
        }
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(minMoveSpeed), minMoveSpeed, nameof(maxMoveSpeed),
            maxMoveSpeed, false);
        if (rollDistance != 0f || rollSpeed != 0f  || rollCoolDownTime != 0f)
        {
            HelperUtilities.ValidateCheckPositiveValue(this, nameof(rollDistance), rollDistance, false);
            HelperUtilities.ValidateCheckPositiveValue(this, nameof(rollSpeed), rollSpeed, false);
            HelperUtilities.ValidateCheckPositiveValue(this, nameof(rollCoolDownTime), rollCoolDownTime, false);
        }
    }
#endif
    #endregion
}
