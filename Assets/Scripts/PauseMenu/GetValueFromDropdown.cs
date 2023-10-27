using System.Collections.Generic;
//using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetValueFromDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

    public void GetDropdownValue()
    {
        int pickedEntryIndex = _dropdown.value;
        string selectedOption = _dropdown.options[pickedEntryIndex].text;
    }
}