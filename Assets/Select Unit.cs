using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnit : MonoBehaviour
{
    public static SelectUnit Instance { get; set; }

    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> unitSelect = new List<GameObject>();

    public LayerMask clickable;
    public LayerMask ground;
    public GameObject groundMarker;

    private Camera cammy;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        cammy = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cammy.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift)) 
                {
                    MultiSelect(hit.collider.gameObject);
                }

                else
                {
                    SelectByClicking(hit.collider.gameObject);
                }

            }

            else
            {
                if(!Input.GetKey(KeyCode.LeftShift))
                {
                 DeselectAll();
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && unitSelect.Count > 0)
        {
            RaycastHit hit;
            Ray ray = cammy.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position =hit.point;

                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            }
        }
    }

     private void MultiSelect(GameObject unit)
    {
        if (!unitSelect.Contains(unit))
        {
            unitSelect.Add(unit);
            SelectUnits(unit, true);
        }

        else 
        {
            SelectUnits(unit, false);
            unitSelect.Remove(unit);
        }
    }

    private void DeselectAll()
    {
        foreach (var unit in unitSelect)
        {
            SelectUnits(unit, false);
        }

        groundMarker.SetActive(false);
        unitSelect.Clear();
    }

    internal void DragSelect(GameObject unit)
    {
        if (!unitSelect.Contains(unit))
        {
            unitSelect.Add(unit);
            SelectUnits(unit, true);
        }
        else
        {
            SelectUnits(unit, false);
            unitSelect.Remove(unit);
        }
    }

    private void SelectByClicking(GameObject unit)
    {
        DeselectAll();

        unitSelect.Add(unit);
        SelectUnits(unit, true);
    }

    private void SelectUnits(GameObject unit, bool isSelected)
    {
        TriggerSelection(unit, isSelected);
        EnableUnitMovement(unit, isSelected);
    }

    private void EnableUnitMovement(GameObject unit, bool Move)
    {
        unit.GetComponent<MCMovement>().enabled = Move;
    }

    private void TriggerSelection(GameObject unit, bool isVisible)
    {
        unit.transform.GetChild(0).gameObject.SetActive(isVisible);
    }

}
