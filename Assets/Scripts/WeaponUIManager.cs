using UnityEngine;
using TMPro;

public class WeaponUIManager : MonoBehaviour
{
    public TextMeshProUGUI[] weaponTexts;

    void Start()
    {
        // Hide all weapon texts at the start
        foreach (var weaponText in weaponTexts)
        {
            weaponText.gameObject.SetActive(false);
        }
    }

    public void UpdateWeaponUI(int selectedWeaponIndex, string[] weaponNames)
    {
        for (int i = 0; i < weaponTexts.Length; i++)
        {
            if (i == selectedWeaponIndex)
            {
                weaponTexts[i].text = weaponNames[i] + " (Selected)";
                weaponTexts[i].color = Color.yellow;
                weaponTexts[i].gameObject.SetActive(true); // Ensure selected weapon is visible
            }
            else
            {
                weaponTexts[i].gameObject.SetActive(false); // Hide non-selected weapons
            }
        }
    }
}
