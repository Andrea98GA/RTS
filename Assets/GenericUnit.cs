using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericUnit : MonoBehaviour
{

    void Start()
    {
        SelectUnit.Instance.allUnitsList.Add(gameObject);
    }

    private void OnDestroy()
    {
        SelectUnit.Instance.allUnitsList.Remove(gameObject);
    }
}
