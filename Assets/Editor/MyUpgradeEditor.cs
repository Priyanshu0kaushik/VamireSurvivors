using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyUpgradeEditor : EditorWindow
{

    public Sprite Icon;
    public new string name;
    public string description;
    public string UpgradePath = "Assets/UpgradesAndEnemies/Upgrades/";
    public UpgradeType upgradetype;
    public Movements movement;
    public GameObject AttachObject;
    //orbitVars
    public float myOrbitRadius, myMovementSpeed;

    //stats
    public int myAddmaxHealth, myAddHealth, myAddSpeed;
    public float myAddWeaponSize;

    public enum UpgradeType
    {
        None,
        Attach,
        Stats,
    }

    public enum Movements
    {
        None,
        Orbit,
    }


    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        // Name and Icon
        EditorGUILayout.LabelField("Enter the name for your Upgrade: ");
        name = EditorGUILayout.TextField(name);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        Icon = (Sprite)EditorGUILayout.ObjectField(Icon, typeof(Sprite), false);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Enter Description for Upgrade: ");
        description = EditorGUILayout.TextField(description);

        EditorGUILayout.EndHorizontal();


        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description)) return;

        // UpgradeType
        upgradetype = (UpgradeType)EditorGUILayout.EnumPopup("Type of Upgrade: ", upgradetype);

        switch (upgradetype)
        {
            case UpgradeType.Attach:
                AttachUI();
                break;
            case UpgradeType.Stats:
                StatsUI();
                break;
            case UpgradeType.None:
                break;
        }

    }
    void AttachUI()
    {
        EditorGUILayout.LabelField("Select your GameObject you want to Attach: ");
        AttachObject = EditorGUILayout.ObjectField(AttachObject, typeof(GameObject), false) as GameObject;

        movement = (Movements)EditorGUILayout.EnumPopup("Movement", movement);
        if (movement == Movements.Orbit)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("How Far you want Object to revolve: ");
            myOrbitRadius = EditorGUILayout.FloatField(myOrbitRadius);

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("How Fast you want Object to revolve: ");
            myMovementSpeed = EditorGUILayout.FloatField(myMovementSpeed);

            EditorGUILayout.EndHorizontal();
        }


        if(AttachObject!=null) if (GUILayout.Button("Create Upgrade")) CreateAttachAsset();

    }

    void CreateAttachAsset()
    {
        Debug.Log("Entered");
        UpgradeSO upgrade = CreateInstance<UpgradeSO>();

        upgrade.name = name;
        upgrade.Description = description;
        upgrade.Icon = Icon;
        upgrade.PrefabToAttach = AttachObject;

        if (upgrade.PrefabToAttach != null)
        {
            
            upgrade.PrefabToAttach.AddComponent<Weapon>();
            upgrade.PrefabToAttach.AddComponent<OrbitMovement>();
            upgrade.PrefabToAttach.GetComponent<OrbitMovement>().orbitRadius = myOrbitRadius;
            upgrade.PrefabToAttach.GetComponent<OrbitMovement>().movementSpeed = myMovementSpeed;

            AssetDatabase.CreateAsset(upgrade, UpgradePath + upgrade.name + ".asset");
            ClearFields();
            AddInUpdateContainer();
        }
    }

    void StatsUI()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Enter Value to add to Player's MaxHealth:");
        myAddmaxHealth = EditorGUILayout.IntField(myAddmaxHealth);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Enter Value to add to Player's Health:");
        myAddHealth = EditorGUILayout.IntField(myAddHealth);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Enter Value to add to Player's Move Speed:");
        myAddSpeed = EditorGUILayout.IntField(myAddSpeed);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Enter Value to add to Player's Weapon's Size:");
        myAddWeaponSize = EditorGUILayout.FloatField(myAddWeaponSize);

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create Upgrade")) CreateStatAsset();

    }

    public void CreateStatAsset()
    {
        UpgradeSO upgrade = CreateInstance<UpgradeSO>();

        //set values
        upgrade.name = name;
        upgrade.Description = description;
        upgrade.Icon = Icon;


        upgrade.AddmaxHealth = myAddmaxHealth;
        upgrade.AddHealth = myAddHealth;
        upgrade.AddSpeed = myAddSpeed;
        upgrade.AddWeaponSize = myAddWeaponSize;




        AssetDatabase.CreateAsset(upgrade, UpgradePath + upgrade.name + ".asset");
        ClearFields();
        AddInUpdateContainer();
    }

    [MenuItem("Tools/MyUpgradeEditor")]
    public static void StartWindow()
    {
        MyUpgradeEditor e = GetWindow<MyUpgradeEditor>();
        e.Show();
        e.titleContent = new GUIContent("Upgrade Editor", "Create Upgrade");
    }


    void ClearFields()
    {
        name = "";
        description = "";
        Icon = null;
        AttachObject = null;
        upgradetype = UpgradeType.None;
        myAddmaxHealth = 0;
        myAddHealth = 0;
        myAddSpeed = 0;
        myAddWeaponSize = 0;
    }

    void AddInUpdateContainer()
    {
        string[] allUpgrades = AssetDatabase.FindAssets("t:UpgradeSO");
        UpgradeContainer upgradeContainer = Resources.Load<UpgradeContainer>("TheUpgradeContainer");
        upgradeContainer.Upgrades = new List<UpgradeSO>();
        foreach (string upgrade in allUpgrades)
        {
            UpgradeSO u = (UpgradeSO)AssetDatabase.LoadAssetAtPath<UpgradeSO>(AssetDatabase.GUIDToAssetPath(upgrade));
            upgradeContainer.Upgrades.Add(u);
        }
        EditorUtility.SetDirty(upgradeContainer);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

}
