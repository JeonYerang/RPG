using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipList : MonoBehaviour
{
    public struct Equipments
    {
        public Helmet helmet;
        public Armor armor;
        public Weapon weapon;
    }
    Equipments equipments;

    private void Awake()
    {
        equipments.helmet = null;
        equipments.armor = null;
        equipments.weapon = null;
    }

    public void PushEquipment(Helmet helmet)
    {
        equipments.helmet = helmet;
    }
    public void PushEquipment(Armor armor)
    {
        equipments.armor = armor;
    }
    public void PushEquipment(Weapon weapon)
    {
        equipments.weapon = weapon;
    }

    public void PopEquipment()
    {
        
    }
}
