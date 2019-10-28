﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class VibrationHandler
{
#if UNITY_IPHONE
    const string dll = "__Internal";
#elif UNITY_ANDROID
    const string dll = "VibrationDLL";
#elif UNITY_EDITOR
    const string dll = "";
#endif


#if UNITY_EDITOR

    public void vibrate()
    {
        //Debug.Log("Vibrated");
    }




#elif UNITY_ANDROID
    public void vibrate()
    {
       // Debug.Log ("Vibrated");
    }

#elif UNITY_IPHONE

     [DllImport(dll)]
     private static extern void generateFeedback();

     public void vibrate()
     {
         generateFeedback();
     }


    
#endif

}

