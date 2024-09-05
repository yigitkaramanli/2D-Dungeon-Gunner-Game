using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class AnimatePlayer : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        //subscribe to events
        player.movementToPositionEvent.OnMovementToPosition += MovementToPositionEvent_OnMovementToPosition;
        player.movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
        player.idleEvent.OnIdle += IdleEvent_OnIdle;
        player.aimWeaponEvent.OnWeaponAim += AimWeaponEvent_OnWeaponAim;
    }

    private void OnDisable()
    {
        //Unsubscribe from the events
        player.movementToPositionEvent.OnMovementToPosition -= MovementToPositionEvent_OnMovementToPosition;
        player.movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
        player.idleEvent.OnIdle -= IdleEvent_OnIdle;
        player.aimWeaponEvent.OnWeaponAim -= AimWeaponEvent_OnWeaponAim;
    }

    private void MovementToPositionEvent_OnMovementToPosition(MovementToPositionEvent movementToPositionEvent, MovementToPositionEventArgs movementToPositionEventArgs)
    { 
       InitializeAimAnimationParameters();
       InitializeRollAnimationParameters();
       SetMovementToPositionAnimationParameters(movementToPositionEventArgs);
    }

    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent,
        MovementByVelocityEventArgs movementByVelocityEventArgs)
    {
        InitializeRollAnimationParameters();
        SetMovementAnimationParameters();
    }

    private void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        InitializeRollAnimationParameters();
        SetIdleAnimationParameters();
    }
    
    
     private void AimWeaponEvent_OnWeaponAim(AimWeaponEvent aimWeaponEvent, AimWeaponEventArgs aimWeaponEventArgs)
     {
         InitializeAimAnimationParameters();
         InitializeRollAnimationParameters();
         SetAimWeaponAnimationParameters(aimWeaponEventArgs.aimDirection);
     }

     private void InitializeAimAnimationParameters()
     {
         player.animator.SetBool(Settings.aimUp, false);
         player.animator.SetBool(Settings.aimDown, false);
         player.animator.SetBool(Settings.aimLeft, false);
         player.animator.SetBool(Settings.aimRight, false);
         player.animator.SetBool(Settings.aimUpLeft, false);
         player.animator.SetBool(Settings.aimUpRight, false);
     }

     private void InitializeRollAnimationParameters()
     {
         player.animator.SetBool(Settings.rollDown, false);
         player.animator.SetBool(Settings.rollLeft, false);
         player.animator.SetBool(Settings.rollRight, false);
         player.animator.SetBool(Settings.rollUp, false);

     }

     private void SetMovementAnimationParameters()
     {
         player.animator.SetBool(Settings.isMoving, true);
         player.animator.SetBool(Settings.isIdle, false);
     }

     private void SetMovementToPositionAnimationParameters(MovementToPositionEventArgs movementToPositionEventArgs)
     {
         if (movementToPositionEventArgs.isRolling)
         {
             if (movementToPositionEventArgs.moveDirection.x > 0f)
             {
                 player.animator.SetBool(Settings.rollRight, true);
             } 
             else if (movementToPositionEventArgs.moveDirection.x < 0f)
             {
                 player.animator.SetBool(Settings.rollLeft, true);
             }
             else if (movementToPositionEventArgs.moveDirection.y > 0f)
             {
                 player.animator.SetBool(Settings.rollUp, true);
             }
             else if (movementToPositionEventArgs.moveDirection.y < 0f)
             {
                 player.animator.SetBool(Settings.rollDown, true);
             }
         }
     }

     private void SetIdleAnimationParameters()
     {
         player.animator.SetBool(Settings.isMoving, false);
         player.animator.SetBool(Settings.isIdle, true);
         
     }

     private void SetAimWeaponAnimationParameters(AimDirection aimDirection)
     {
         switch (aimDirection)
         {
             case AimDirection.Up:
                 player.animator.SetBool(Settings.aimUp, true);
                 break;
             case AimDirection.UpRight:
                 player.animator.SetBool(Settings.aimUpRight, true);
                 break;
             case AimDirection.UpLeft:
                 player.animator.SetBool(Settings.aimUpLeft, true);
                 break;
             case AimDirection.Rigth:
                 player.animator.SetBool(Settings.aimRight, true);
                 break;
             case AimDirection.Left:
                 player.animator.SetBool(Settings.aimLeft, true);
                 break;
             case AimDirection.Down:
                 player.animator.SetBool(Settings.aimDown, true);
                 break;
         }
     }
}
