using UnityEngine;

public class Powerups : MonoBehaviour
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

    public void SetPowerup()
    {
        currentTime = timer;
        countDown = true;

        if (isSpeed)
        {
            Player.instance.SetSpeedMultiplier(speed);
        }
        else
        {
            // Get enemies to leave and catch them
        }
    }
}