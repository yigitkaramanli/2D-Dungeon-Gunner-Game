using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SetActiveWeaponEvent))]
[DisallowMultipleComponent]
public class ActiveWeapon : MonoBehaviour
{
   #region Tooltip
   [Tooltip("Populate with the SpriteRenderer on the child Weapon gameobject")]
   #endregion
   [SerializeField]
   private SpriteRenderer weaponSpriteRenderer;

   #region Tooltip
   [Tooltip("Populate with the PolygonCollider2D on the child Weapon gameObject")]
   #endregion
   [SerializeField]
   private PolygonCollider2D weaponPolygonCollider2D;

   #region Tooltip
   [Tooltip("Populate with the Transform on the WeaponShootPosition gameobject")]
   #endregion
   [SerializeField]
   private Transform weaponShootPositionTransform;

   #region Tooltip
   [Tooltip("Populate with the Transform on the WeaponEffectPosition gameobject")]
   #endregion
   [SerializeField]
   private Transform weaponEffectPositionTransform;

   private SetActiveWeaponEvent setWeaponEvent;
   private Weapon currentWeapon;

   private void Awake()
   {
      setWeaponEvent = GetComponent<SetActiveWeaponEvent>();
   }

   private void OnEnable()
   {
      setWeaponEvent.OnSetActiveWeapon += SetWeaponEvent_OnSetActiveWeapon;
   }

   private void OnDisable()
   {
      setWeaponEvent.OnSetActiveWeapon -= SetWeaponEvent_OnSetActiveWeapon;
   }

   private void SetWeaponEvent_OnSetActiveWeapon(SetActiveWeaponEvent setActiveWeaponEvent,
      SetActiveWeaponEventArgs setActiveWeaponEventArgs)
   {
      SetWeapon(setActiveWeaponEventArgs.weapon);
   }

   private void SetWeapon(Weapon weapon)
   {
      currentWeapon = weapon;
      
      //set the current weapon sprite
      weaponSpriteRenderer.sprite = currentWeapon.weaponDetails.weaponSprite;
      
      //if the weapon has a polygon collider and a sprite then set it to the weapon sprite physics shape
      if (weaponPolygonCollider2D != null && weaponSpriteRenderer.sprite != null)
      {
         //Get the sprite physics shape
         List<Vector2> spritePhysicsShapePointList = new List<Vector2>();
         weaponSpriteRenderer.sprite.GetPhysicsShape(0, spritePhysicsShapePointList);

         //Set polygon collider on the weapon to pick up physics shape for sprite - set collider points to sprite physics shape points
         weaponPolygonCollider2D.points = spritePhysicsShapePointList.ToArray();
      }

      weaponShootPositionTransform.localPosition = currentWeapon.weaponDetails.weaponShootPosition;
   }

   public AmmoDetailsSO GetCurrentAmmo()
   {
      return currentWeapon.weaponDetails.weaponCurrentAmmo;
   }

   public Weapon GetCurrentWeapon()
   {
      return currentWeapon;
   }

   public Vector3 GetShootPosition()
   {
      return weaponShootPositionTransform.position;
   }

   public Vector3 GetShootEffectPosition()
   {
      return weaponEffectPositionTransform.position;
   }

   public void RemoveCurrentWeapon()
   {
      currentWeapon = null;
   }

   #region Validation
#if UNITY_EDITOR
   private void OnValidate()
   {
      HelperUtilities.ValidateCheckNullValue(this, nameof(weaponSpriteRenderer), weaponSpriteRenderer);
      HelperUtilities.ValidateCheckNullValue(this, nameof(weaponPolygonCollider2D), weaponPolygonCollider2D);
      HelperUtilities.ValidateCheckNullValue(this, nameof(weaponShootPositionTransform), weaponShootPositionTransform);
      HelperUtilities.ValidateCheckNullValue(this, nameof(weaponEffectPositionTransform),
         weaponEffectPositionTransform);
   }
#endif
   #endregion
}
