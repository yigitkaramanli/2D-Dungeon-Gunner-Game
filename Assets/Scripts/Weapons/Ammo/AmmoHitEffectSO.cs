using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoHitEffect_", menuName = "Scriptable Objects/Weapons/Ammo Hit Effect")]
public class AmmoHitEffectSO : ScriptableObject
{
    #region Header AMMO HIT EFFECT DETAILS
    [Space(10)]
    [Header("AMMO HIT EFFECT DETAILS")]
    #endregion
    #region Tooltip
    [Tooltip(
        "The color gradient for the hit effect. This gradient show the color of particles during their lifetime - from left to right.")]
    #endregion
    public Gradient colorGradient;
    
    #region Tooltip
    [Tooltip("The length of time the particle system is emitting particles")]
    #endregion
    public float duration = 0.50f;
    
    #region Tooltip
    [Tooltip("The start particle size for the particle effect")]
    #endregion
    public float startParticleSize = 0.25f;
    
    #region Tooltip
    [Tooltip("The start particle speed for the particle effect")]
    #endregion
    public float startParticleSpeed = 3f;
    
    #region Tooltip
    [Tooltip("The particle lifetime for the particle effect")]
    #endregion
    public float startLifetime = 0.5f;
    
    #region Tooltip
    [Tooltip("The maximum number of particles to be emitted")]
    #endregion
    public int maxParticleNumber = 100;
    
    #region Tooltip
    [Tooltip("The number of particles emitted per second. If zero it will just be a burst")]
    #endregion
    public int emissionRate = 100;
    
    #region Tooltip
    [Tooltip("How many particles should be emitted in the particle effect burst")]
    #endregion
    public int burstParticleNumber = 20;
    
    #region Tooltip
    [Tooltip("The gravity on the particles - a small negative number will make them float up")]
    #endregion
    public float effectGravity = -0.01f;
    
    #region Tooltip
    [Tooltip("The sprite for the particle effect. If none is specified then the default particle sprite will be used")]
    #endregion
    public Sprite sprite;

    #region Tooltip
    [Tooltip(
        "The min velocity of the particle over its lifetime. A random value will be picked between the min and the max values")]
    #endregion
    public Vector3 velocityOverLifetimeMin;
    
    #region Tooltip
    [Tooltip(
        "The max velocity of the particle over its lifetime. A random value will be picked between the min and the max values")]
    #endregion
    public Vector3 velocityOverLifetimeMax;
    
    #region Tooltip
    [Tooltip(
        "ammoHitEffectPrefab contains the particle system for the shoot effect - and is configured by the ammoHitEffectPrefab SO")]
    #endregion
    public GameObject ammoHitEffectPrefab;


    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(duration), duration, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(startParticleSize), startParticleSize, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(startParticleSpeed), startParticleSpeed, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(startLifetime), startLifetime, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(maxParticleNumber), maxParticleNumber, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(emissionRate), emissionRate, true);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(burstParticleNumber), burstParticleNumber, true);
        HelperUtilities.ValidateCheckNullValue(this, nameof(ammoHitEffectPrefab), ammoHitEffectPrefab);
    }
#endif
    #endregion
}
