using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wind") && LevelData.levelData.isBlowActive && !LevelData.levelData.isBlown)
        {
            Time.timeScale = 5f;
            LevelData.levelData.isBlown = true;
            //LevelData.levelData.ThrowableCubes;
            //GameObject[] thrownObjects = GameObject.FindGameObjectsWithTag("ThrownObject");
            LevelData.levelData.CustomVibration(DebugScript.VibrationStyle.impactHeavy);
            foreach (GameObject thrownObject in LevelData.levelData.throwableCubes)
            {
                thrownObject.GetComponent<Collider>().enabled = true;
                thrownObject.GetComponent<Rigidbody>().isKinematic = false;
                thrownObject.GetComponent<Rigidbody>().AddExplosionForce(42000f, other.transform.position, 100000f, 4000f);
            }
        }
    }
}
