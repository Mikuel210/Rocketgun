using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{

  [SerializeField] private List<WeaponSO> weapons;
  [SerializeField] private WeaponParent weaponParent;

  void Update()
  {
    for (int i = 0; i < 10; i++)
    {
      KeyCode key = KeyCode.Alpha1 + i;
      if (i == 9) key = KeyCode.Alpha0;
      if (!Input.GetKeyDown(key) || i >= weapons.Count) continue;
  
      weaponParent.Equip(weapons[i]);
    }
  }

}

