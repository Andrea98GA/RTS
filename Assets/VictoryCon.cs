using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryCon : MonoBehaviour
{
    public GameObject baseAmiga;
    public GameObject baseEnemiga;

    RTSBase amigo;
    RTSBase enemigo;

    void Start(){
        amigo = baseAmiga.GetComponent<RTSBase>();
        enemigo = baseEnemiga.GetComponent<RTSBase>();
    }

    void Update(){
        if(amigo.baseHealth <= 0){
            SceneManager.LoadScene(2);
        }
        if(enemigo.baseHealth <= 0){
            SceneManager.LoadScene(3);
        }
    }
}
