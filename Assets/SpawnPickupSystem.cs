using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnWeaponPickupSystem : MonoBehaviour
{
    [SerializeField] private DroppedWeapon[] availableDroppedWeapons;
    [SerializeField] private Transform spawnCentreTransform;
    [SerializeField] private int weaponPickupsToSpawn = 15;

    [SerializeField] private float spawnAreaRadius;
    [SerializeField] private float spawnPositionHeight;

    List<DroppedWeapon> spawnedDroppedWeaponsList = new List<DroppedWeapon>();

    public void SpawnDroppedPickups()
    {
        for(int i = 0; i < weaponPickupsToSpawn; i++)
        {
            Vector2 randomPointInCircleOfSpawnAreaRadius = Random.insideUnitCircle * spawnAreaRadius;

            Vector3 spawnPoint = new Vector3(spawnCentreTransform.position.x + randomPointInCircleOfSpawnAreaRadius.x, spawnPositionHeight, spawnCentreTransform.position.z + randomPointInCircleOfSpawnAreaRadius.y);

            int randomDroppedWeaponTypeIndex = Random.Range(0, availableDroppedWeapons.Length);

            spawnedDroppedWeaponsList.Add(Instantiate(availableDroppedWeapons[randomDroppedWeaponTypeIndex], spawnPoint, availableDroppedWeapons[randomDroppedWeaponTypeIndex].transform.rotation));
        }        
    }

    public void ClearDroppedWeapons()
    {
        for(int i = 0; i < spawnedDroppedWeaponsList.Count; i++)
        {
            if (spawnedDroppedWeaponsList[i] != null)
            {
                Destroy(spawnedDroppedWeaponsList[i].gameObject);
            }
        }

        spawnedDroppedWeaponsList.Clear();
    }
}
