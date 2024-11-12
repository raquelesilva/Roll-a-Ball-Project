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
    [SerializeField] int singleStars;
    [SerializeField] int coopStars;

    [SerializeField] GameObject worldPrefab;

    // GETTERS
    public bool GetIndividualState() {  return isUnlocked; }
    public bool GetCoopState() {  return isCoopUnlocked; }
    public bool GetStoryState() {  return isStoryUnlocked; }
    public int GetLevel(){ return level; }
    public int GetSingleStars() { return singleStars; }
    public int GetCoopStars() { return coopStars; }
    public GameObject GetWorldPrefab(){ return worldPrefab; }

    // SETTERS
    public void UnlockIndividualLevel() { isUnlocked = true; }
    public void UnlockCoopLevel() { isCoopUnlocked = true; }
    public void StoryLevels(bool unlock) { isStoryUnlocked = unlock; }
    public void SetSingleStars(int stars) { singleStars = stars; }
    public void SetCoopStars(int stars) { coopStars = stars; }
}