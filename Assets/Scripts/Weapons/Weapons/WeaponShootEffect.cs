using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class WeaponShootEffect : MonoBehaviour
{
    private ParticleSystem shootEffectParticleSystem;

    private void Awake()
    {
        shootEffectParticleSystem = GetComponent<ParticleSystem>();
    }

    public void SetShootEffect(WeaponShootEffectSO shootEffect, float aimAngle)
    {
        SetShootEffectColorGradient(shootEffect.colorGradient);

        SetShootEffectParticleStartingValues(shootEffect.duration, shootEffect.startParticleSize,
            shootEffect.startParticleSpeed, shootEffect.startLifetime, shootEffect.effectGravity,
            shootEffect.maxParticleNumber);

        SetShootEffectParticleEmission(shootEffect.emissionRate, shootEffect.burstParticleNumber);

        SetEmitterRotation(aimAngle);
        
        SetShootEffectParticleSprite(shootEffect.sprite);
        
        SetShootEffectVelocityOverLifetime(shootEffect.velocityOverLifetimeMin,shootEffect.velocityOverLifetimeMax);
    }
    
    private void SetShootEffectColorGradient(Gradient gradient)
    {
        ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = shootEffectParticleSystem.colorOverLifetime;

        colorOverLifetimeModule.color = gradient;
    }

    private void SetShootEffectParticleStartingValues(float shootEffectDuration, float shootEffectStartParticleSize, float shootEffectStartParticleSpeed, float shootEffectStartLifetime, float shootEffectEffectGravity, int shootEffectMaxParticleNumber)
    {
        ParticleSystem.MainModule mainModule= shootEffectParticleSystem.main;

        mainModule.duration = shootEffectDuration;
        mainModule.startLifetime = shootEffectStartLifetime;
        mainModule.gravityModifier = shootEffectEffectGravity;
        mainModule.startSize = shootEffectStartParticleSize;
        mainModule.startSpeed = shootEffectStartParticleSpeed;
        mainModule.maxParticles = shootEffectMaxParticleNumber;
    }

    private void SetShootEffectParticleEmission(int emissionRate, int burstParticleNumber)
    {
        ParticleSystem.EmissionModule emissionModule = shootEffectParticleSystem.emission;

        ParticleSystem.Burst burst = new ParticleSystem.Burst(0f, burstParticleNumber);
        emissionModule.SetBurst(0,burst);

        emissionModule.rateOverTime = emissionRate;
    }

    private void SetShootEffectParticleSprite(Sprite sprite)
    {
        ParticleSystem.TextureSheetAnimationModule textureSheetAnimationModule =
            shootEffectParticleSystem.textureSheetAnimation;

        textureSheetAnimationModule.SetSprite(0,sprite);
    }


    private void SetEmitterRotation(float angle)
    {
        transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    private void SetShootEffectVelocityOverLifetime(Vector3 velocityOverLifetimeMin, Vector3 velocityOverLifetimeMax)
    {
        ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule =
            shootEffectParticleSystem.velocityOverLifetime;

        ParticleSystem.MinMaxCurve minMaxCurveX = new ParticleSystem.MinMaxCurve();
        minMaxCurveX.mode = ParticleSystemCurveMode.TwoConstants;
        minMaxCurveX.constantMin = velocityOverLifetimeMin.x;
        minMaxCurveX.constantMax = velocityOverLifetimeMax.x;
        velocityOverLifetimeModule.x = minMaxCurveX;

        ParticleSystem.MinMaxCurve minMaxCurveY = new ParticleSystem.MinMaxCurve();
        minMaxCurveY.mode = ParticleSystemCurveMode.TwoConstants;
        minMaxCurveY.constantMin = velocityOverLifetimeMin.y;
        minMaxCurveY.constantMax = velocityOverLifetimeMax.y;
        velocityOverLifetimeModule.y = minMaxCurveY;
        
        ParticleSystem.MinMaxCurve minMaxCurveZ = new ParticleSystem.MinMaxCurve();
        minMaxCurveZ.mode = ParticleSystemCurveMode.TwoConstants;
        minMaxCurveZ.constantMin = velocityOverLifetimeMin.y;
        minMaxCurveZ.constantMax = velocityOverLifetimeMax.y;
        velocityOverLifetimeModule.z = minMaxCurveZ;
    }
}
