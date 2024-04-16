using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
   public int startingHealth;
   public int currentHealth;

   public void SetStartingHealth(int startingHealth)
   {
      this.startingHealth = startingHealth;
      currentHealth = startingHealth;
   }

   public int GetStartingHealth()
   {
      return startingHealth;
   }
}
