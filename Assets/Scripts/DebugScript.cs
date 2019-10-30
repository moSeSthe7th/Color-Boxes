using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{

    public enum VibrationStyle
    {
        Pop,
        impactLight,
        impactMedium,
        impactHeavy,
        notificationSuccess,
        notificationWarning,
        notificationError
    }

    public Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        PopulateList();
    }


    public void Dropdown_Indexchanged(int index)
    {
        Debug.Log("Choosed : " + (VibrationStyle)index);
        LevelData.levelData.vibrationStyle = index;
    }

    void PopulateList()
    {
        string[] enumNames = Enum.GetNames(typeof(VibrationStyle));
        List<string> names = new List<string>(enumNames);

        dropdown.AddOptions(names);
        
    }
}
