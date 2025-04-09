using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyObjects/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public GameObject PrefabToAttach;
    public Sprite Icon;
    public int AddmaxHealth;
    public int AddHealth;
    public float AddWeaponSize;
    public int AddSpeed;

    public string Description;

}
