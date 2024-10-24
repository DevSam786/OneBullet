using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTurrets : MonoBehaviour
{
    
    [SerializeField] GameObject turretGameobject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    public void TurretActive()
    {
        turretGameobject.SetActive(true);
    }
   
}
