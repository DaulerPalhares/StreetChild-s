using UnityEngine;
using System.Collections;


public class PlayerStats : MonoBehaviour {

    #region Protected Vars
    protected float maxHealth = 100;
    protected float maxStamina = 100;
    protected float maxDamage = 0;
    protected float maxArmor = 99;

    #endregion

    #region Public Vars
    public float currentHealth = 60;
    public float currentStamina = 100;
    public float currentDamage = 0;
    public float currentArmor = 0;
    #endregion


}
