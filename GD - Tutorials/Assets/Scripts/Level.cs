using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : MonoBehaviour
{
    [SerializeField] bool isUnlocked = false;
    [SerializeField] int level;
    [SerializeField] int levelPoints;

    [SerializeField] GameObject worldPrefab;

    // GETTERS
    public bool GetState() {  return isUnlocked; }

    public int GetLevel(){ return level; }

    public int GetPoints() { return levelPoints; }
    
    public GameObject GetWorldPrefab(){ return worldPrefab; }

    // SETTERS
    public void UnlockLevel() { isUnlocked = true; }

    public void SetPoints(int points) { levelPoints = points; }
}