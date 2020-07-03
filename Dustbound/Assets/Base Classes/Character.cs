using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStates state;
    public string charName;
    public string charDesc;
    public int baseHealth;
    public int baseDmg;
    public int baseArmour;

    public virtual void Attack()
    {
        //Overide this method for specific characters
    }

    public enum CharacterStates { idle, attacking, walking }
}
