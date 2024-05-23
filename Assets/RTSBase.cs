using UnityEngine;

public class RTSBase : MonoBehaviour
{
    public GameObject Unidad; 
    public Transform spawnPoint; 
    public float spawnInterval = 5f; 
    public int maxUnits = 5; 
    public float baseHealth = 100;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            // Check if we haven't reached the maximum number of units
            if (GameObject.FindGameObjectsWithTag("Unit").Length < maxUnits)
            {
                SpawnUnit();
            }
        }
    }

    void SpawnUnit()
    {
        Instantiate(Unidad, spawnPoint.position, Quaternion.identity);
    }

    public void TakeDamage(float value){
        baseHealth -= value;
    }
    public void DestroyBase(){
        Destroy(gameObject);
    }
}
