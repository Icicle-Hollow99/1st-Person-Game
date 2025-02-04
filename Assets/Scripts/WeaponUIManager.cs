using UnityEngine;
using TMPro;

public class WeaponUIManager : MonoBehaviour
{
    public TextMeshProUGUI[] weaponTexts;

    public void UpdateWeaponUI(int selectedWeaponIndex, string[] weaponNames)
    {
        for (int i = 0; i < weaponTexts.Length; i++)
        {
            if (i == selectedWeaponIndex)
            {
                weaponTexts[i].text = weaponNames[i] + " (Selected)";
                weaponTexts[i].color = Color.yellow;
            }
            else
            {
                weaponTexts[i].text = weaponNames[i];
                weaponTexts[i].color = Color.white;
            }
        }
    }
}