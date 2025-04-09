using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateManager : MonoBehaviour
{
    public GameObject Player,weaponSpawn;
    int firstIndex, secondIndex;
    public Image Icon1, Icon2;
    public TextMeshProUGUI Name1, description1, Name2, description2;
    UpgradeSO first, second;
    List<UpgradeSO> list = new List<UpgradeSO>();
    public UpgradeContainer UpgradeContainer;
    public GameObject attachableParent;

    public void ChooseRandomUpgrade()
    {
        list.AddRange(UpgradeContainer.Upgrades);
        firstIndex = Random.Range(0, list.Count);
        first = list[firstIndex];
        Debug.Log(list.Count+" "+list[firstIndex].name);
        list.RemoveAt(firstIndex);

        secondIndex = Random.Range(0,list.Count);
        second = list[secondIndex];
        ShowCards();

        list.Clear();
    }

    void ShowCards()
    {
        Name1.text = first.name;
        description1.text = first.Description;
        Icon1.sprite = first.Icon;
        Name2.text = second.name;
        description2.text = second.Description;
        Icon2.sprite = second.Icon;

        
    }

    public void Option1()
    {
        ActivateUpgrade(first);
        
    }
    public void Option2()
    {
        ActivateUpgrade(second);
    }

    void ActivateUpgrade(UpgradeSO upgrade)
    {
        if (upgrade.PrefabToAttach == null) AddStats(upgrade);
        else Attach(upgrade);
        GameManager.Instance.SwitchState<PlayingState>();
    }

    void Attach(UpgradeSO upgrade)
    {
        GameObject AttachObject = Instantiate(upgrade.PrefabToAttach, attachableParent.transform);
        AttachObject.GetComponent<OrbitMovement>().target = Player;
        Player.GetComponent<Player>().AttachedUpgrades.Add(AttachObject);
        
    }

    void AddStats(UpgradeSO upgrade)
    {
        Player player = Player.GetComponent<Player>();
        AddHealth(upgrade.AddHealth);
        MoreMaxHealth(upgrade.AddmaxHealth);
        player.MoveSpeed += upgrade.AddSpeed;
        if (upgrade.AddWeaponSize != 0) BiggerWeapon(upgrade.AddWeaponSize);
    }

 




    // bigger sword
    void BiggerWeapon(float size)
    {
        weaponSpawn.GetComponent<WeaponSpawn>().BiggerWeapon(new Vector3(size, size, size));
    }

    // Add 10 more health to MaxHealth
    void MoreMaxHealth(int h)
    {
        Player.GetComponent<Player>().MaxHealth += h;
        Player.GetComponent<Player>().TakeDamage(0);
    }

    void AddHealth(int health)
    {
        if (Player.GetComponent<Player>().Health + health > Player.GetComponent<Player>().MaxHealth)
        {
            Player.GetComponent<Player>().Health = Player.GetComponent<Player>().MaxHealth;
            
        }
        else
        {
            Player.GetComponent<Player>().Health += health;
        }
        Player.GetComponent<Player>().TakeDamage(0);
    }

}
