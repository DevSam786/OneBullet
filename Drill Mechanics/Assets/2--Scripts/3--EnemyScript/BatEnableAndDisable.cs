using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnableAndDisable : MonoBehaviour
{
    public GameObject batCheckCollider;
    public void EnableBatCheck()
    {
        batCheckCollider.SetActive(true);
    }
    public void DisableBatCheck()
    {
        batCheckCollider.SetActive(false);
    }
}
