using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MyObjects/UpgradeContainer")]
public class UpgradeContainer : ScriptableObject
{
    public List<UpgradeSO> Upgrades = new List<UpgradeSO>();
}
