using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : MonoBehaviour
{
    [SerializeField] bool isUnlocked = false;
    [SerializeField] bool isCoopUnlocked = false;
    [SerializeField] bool isStoryUnlocked = false;
    [SerializeField] int level;
    [SerializeField] int levelPoints;

    [SerializeField] GameObject worldPrefab;

    // GETTERS
    public bool GetIndividualState() {  return isUnlocked; }
    public bool GetCoopState() {  return isCoopUnlocked; }
    public bool GetStoryState() {  return isStoryUnlocked; }

    public int GetLevel(){ return level; }

    public int GetPoints() { return levelPoints; }
    
    public GameObject GetWorldPrefab(){ return worldPrefab; }

    // SETTERS
    public void UnlockIndividualLevel() { isUnlocked = true; }
    public void UnlockCoopLevel() { isCoopUnlocked = true; }
    public void StoryLevels(bool unlock) { isStoryUnlocked = unlock; }

    public void SetPoints(int points) { levelPoints = points; }
}