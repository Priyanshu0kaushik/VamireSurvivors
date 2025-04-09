using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyObjects/EnemyContainer")]
public class EnemyContainer : ScriptableObject
{
    public List<EnemySO> Enemies = new List<EnemySO>();
}
