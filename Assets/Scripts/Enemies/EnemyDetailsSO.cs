using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDetails_", menuName = "Scriptable Objects/Enemy/EnemyDetails")]
public class EnemyDetailsSO : ScriptableObject
{
    #region Header BASE ENEMY DETAILS
    [Space(10)]
    [Header("BASE ENEMY DETAILS")]
    #endregion
    #region Tooltip
    [Tooltip("The name of the enemy")]
    #endregion
    public string enemyName;

    #region Tooltip
    [Tooltip("The prefab for the enemy")]
    #endregion
    public GameObject enemyPrefab;

    #region Tooltip
    [Tooltip("Distance to the player before starting to chase")]
    #endregion
    public float chaseDistance = 50f;

    #region Header ENEMY MATERIAL
    [Space(10)]
    [Header("ENEMY MATERIAL")]
    #endregion
    #region Tooltip
    [Tooltip("This is the standard lit shader material for the enemy (that will be used after the enemy materializes")]
    #endregion
    public Material enemyStandardMaterial;

    #region Header ENEMY MATERIALIZE SETTINGS
    [Space(10)]
    [Header("ENEMY MATERIALIZE SETTINGS")]
    #endregion
    #region Tooltip
    [Tooltip("The time that it takes the enemy to materialize (In seconds)")]
    #endregion
    public float enemyMaterializeTime;

    #region Tooltip
    [Tooltip("The shader to be used when the enemy is being materialized")]
    #endregion
    public Shader enemyMaterializeShader;

    [ColorUsage(true, true)]
    #region Tooltip
    [Tooltip(
        "The color to use when the enemy is being materialized. HDR color so intensity can be set to cause glowing/Bloom")]
    #endregion
    public Color enemyMaterializeColor;

    #region Header ENEMY WEAPON SETTINGS
    [Space(10)]
    [Header("ENEMY WEAPON SETTINGS")]
    #endregion
    #region Tooltip
    [Tooltip("The weapon that the enemy uses - none if the enemy does not use a weapon.")]
    #endregion
    public WeaponDetailsSO enemyWeapon;

    #region Tooltip
    [Tooltip(
        "The minimum time delay in seconds between bursts of enemy fire. This value should be greater than 0. A random value will be selected between the minimum and maximum values.")]
    #endregion
    public float firingIntervalMin = 0.1f;

    #region Tooltip
    [Tooltip(
        "The maximum time delay in seconds between bursts of enemy fire. A random value will be selected between the minimum and maximum values.")]
    #endregion
    public float firingIntervalMax = 1f;

    #region Tooltip
    [Tooltip(
        "The minimum firing duration that the enemy shoots for during a burst. This value should be greater than 0. A random value will be selected between the minimum and maximum values.")]
    #endregion
    public float firingDurationMin = 1f;

    #region Tooltip
    [Tooltip(
        "The maximum firing duration that the enemy shoots for during a burst. A random value will be selected between the minimum and maximum values.")]
    #endregion
    public float firingDurationMax = 2f;

    #region Tooltip
    [Tooltip(
        "Select if this enemy requires line of sight of the player to fire. If this is not selected the enemy will fire regardless of the obstacles between if the player is 'in range'.")]
    #endregion
    public bool firingLineOfSightRequired;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(enemyName), enemyName);
        HelperUtilities.ValidateCheckNullValue(this, nameof(enemyPrefab), enemyPrefab);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(chaseDistance), chaseDistance, false);
        HelperUtilities.ValidateCheckNullValue(this, nameof(enemyStandardMaterial), enemyStandardMaterial);
        HelperUtilities.ValidateCheckNullValue(this, nameof(enemyMaterializeShader), enemyMaterializeShader);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(enemyMaterializeTime), enemyMaterializeTime, true);
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(firingIntervalMin), firingIntervalMin,
            nameof(firingIntervalMax), firingIntervalMax, false);
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(firingDurationMin), firingDurationMin,
            nameof(firingDurationMax), firingDurationMax, false);
    }
#endif
    #endregion
}