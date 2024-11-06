using UnityEngine;

public class Downgrades : MonoBehaviour
{
    [SerializeField] bool isSpeed;
    [SerializeField] int speed;

    [SerializeField] float timer = 10;
    [SerializeField] float currentTime;
    [SerializeField] bool countDown = false;

    private void Update()
    {
        if (countDown)
        {
            currentTime -= Time.deltaTime;
        }

        if (currentTime <= 0)
        {
            countDown = false;
        }
    }

    public void SetDowngrade()
    {
        currentTime = timer;
        countDown = true;

        if (isSpeed)
        {
            WorldHolder.instance.SetSpeedMultiplier(-speed);
        }
        else
        {
            // Get enemies to speedup
        }
    }
}