using UnityEngine;

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    void Start()
    {
        // Enable the gyroscope
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        // Check if the device has a gyroscope
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            Debug.Log("Gyro is on!");
            return true;
        }
        Debug.Log("Gyro is off!");
        return false;
    }

    void Update()
    {
        if (gyroEnabled)
        {
            // Read the gyroscope rotation rate
            Vector3 rotationRate = gyro.rotationRateUnbiased;

            // Rotate the sprite around the Z axis based on the gyroscope's rotation rate
            Debug.Log("Rotation Rate: " + rotationRate);
            //transform.rotation = Input.gyro.attitude;
            transform.Rotate(0, 0, -rotationRate.y * Time.deltaTime * 100);
        }
    }
}