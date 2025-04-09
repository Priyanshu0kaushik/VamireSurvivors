using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static MyUpgradeEditor;

public class MyEnemyEditor : EditorWindow
{
    public GameObject myEnemyPrefab;
    public string EnemyPath = "Assets/UpgradesAndEnemies/Enemies/";
    public string myname;
    public int myMaxHealth;
    public int myRange;
    public int mySpeed;
    public int myDamage;
    public Movement myBehaviour;
    public enum Movement
    {
        None,
        AttackIfInRange,
        DirectAttack,
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Enter a Name for Enemy- ");
        myname = EditorGUILayout.TextField(myname);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Select the Prefab for Enemy- ");
        myEnemyPrefab = EditorGUILayout.ObjectField(myEnemyPrefab, typeof(GameObject), false) as GameObject;
        
        EditorGUILayout.EndHorizontal();
        if (myEnemyPrefab == null) return;
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Enter the MaxHealth for Enemy- ");
        myMaxHealth =  EditorGUILayout.IntField(myMaxHealth);
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Enter the Damage of Enemy- ");
        myDamage = EditorGUILayout.IntField(myDamage);
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Enter the Speed for Enemy- ");
        mySpeed = EditorGUILayout.IntField(mySpeed);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Select the Behaviour for Enemy- ");
        myBehaviour = (Movement)EditorGUILayout.EnumPopup(myBehaviour);

        EditorGUILayout.EndHorizontal();
        if (myBehaviour == Movement.None) return;

        if(myBehaviour == Movement.AttackIfInRange)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Enter the Circular Range Radius- ");
            myRange = EditorGUILayout.IntField(myRange);

            EditorGUILayout.EndHorizontal();
        }

        if(GUILayout.Button("Create Enemy")) CreateEnemy();

    }

    private void CreateEnemy()
    {
        EnemySO Enemy = new EnemySO();
        Enemy.EnemyPrefab = myEnemyPrefab;


        

        switch(myBehaviour){
            case Movement.DirectAttack:
                Enemy.EnemyPrefab.AddComponent<DirectAttackEnemy>();
                DirectAttackEnemy script = Enemy.EnemyPrefab.GetComponent<DirectAttackEnemy>();
                script.speed = mySpeed;
                script.MaxHealth = myMaxHealth;
                script.GivenDamage = myDamage;
                break;
            case Movement.AttackIfInRange:
                Enemy.EnemyPrefab.AddComponent<AttackRangeEnemy>();
                AttackRangeEnemy script_ = Enemy.EnemyPrefab.GetComponent<AttackRangeEnemy>();
                script_.speed = mySpeed;
                script_.MaxHealth = myMaxHealth;
                script_.GivenDamage = myDamage;
                script_.Range = myRange;
                break;
        }
        AssetDatabase.CreateAsset(Enemy, EnemyPath + myname + ".asset");
        ClearFields();
        AddInEnemyContainer();

    }


    [MenuItem("Tools/MyEnemyEditor")]
    public static void StartWindow()
    {
        MyEnemyEditor e = GetWindow<MyEnemyEditor>();
        e.Show();
        e.titleContent = new GUIContent("Enemy Editor", "Create Enemy");
    }

    void AddInEnemyContainer()
    {
        string[] allEnimies = AssetDatabase.FindAssets("t:EnemySO");
        EnemyContainer container = Resources.Load<EnemyContainer>("TheEnemyContainer");
        container.Enemies = new List<EnemySO>();
        foreach (string enemy in allEnimies)
        {
            EnemySO e = (EnemySO)AssetDatabase.LoadAssetAtPath<EnemySO>(AssetDatabase.GUIDToAssetPath(enemy));
            container.Enemies.Add(e);
        }
        EditorUtility.SetDirty(container);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    void ClearFields()
    {
        name = "";
        myEnemyPrefab = null;
        myBehaviour = Movement.None;
        mySpeed = 0;
        myDamage = 0;
        myMaxHealth = 0;
        myRange = 0;
    }
}
