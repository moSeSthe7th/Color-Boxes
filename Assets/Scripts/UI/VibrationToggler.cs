using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationToggler : MonoBehaviour
{
    public Sprite vibrationOnSprite;
    public Sprite vibrationOffSprite;

    public void ToggleVibration()
    {
        //Open vibration
        if(LevelData.levelData.isVibrationActive == 0)
        {
            //sprite change yap
            LevelData.levelData.isVibrationActive = 1;
            GetComponent<Button>().image.sprite = vibrationOnSprite;
            
        }
        //Close vibration
        else
        {
            LevelData.levelData.isVibrationActive = 0;
            GetComponent<Button>().image.sprite = vibrationOffSprite;
            
        }

        PlayerPrefs.SetInt("Vibration", LevelData.levelData.isVibrationActive);
    }
}
