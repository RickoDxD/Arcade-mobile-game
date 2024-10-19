using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestructableController))]
public class AsteroidController : MonoBehaviour
{
    [field: SerializeField]
    public AsteroidController OnDestroyedTemplate { get; private set; }
    [field: SerializeField]
    public float RotationSpeed { get; private set; }
    [field: SerializeField]
    public Vector2 Speed { get; private set; }
    public GameController GameController { get; private set; }

    public static AsteroidController Spawn(AsteroidController template, float rotationSpeed, Vector2 speed, GameController gameController)
    {
        AsteroidController newAsteroid = Instantiate(template);
        newAsteroid.GameController = gameController;
        newAsteroid.GetComponent<DestructableController>().GameController = gameController;
        newAsteroid.RotationSpeed = rotationSpeed;
        newAsteroid.Speed = speed;
        return newAsteroid;
    }

    // Update is called once per frame
    void Update()
    {
        RotateMeteor();
        MoveMeteor();
    }

    public void OnLaserHit(LaserController laser, DestructableController destructable)
    {
        if (OnDestroyedTemplate != null)
        {
            AsteroidController newObj = Spawn(OnDestroyedTemplate, RotationSpeed, Speed, GameController);
            newObj.transform.position = this.transform.position;
        }
        destructable.DefaultDestroy(laser);
    }

    private void RotateMeteor()
    {
        float newZ = transform.rotation.eulerAngles.z + (RotationSpeed * Time.deltaTime);
        Vector3 newR = new(0, 0, newZ);
        transform.rotation = Quaternion.Euler(newR);
    }

    public void MoveMeteor()
    {
        float newX = transform.position.x + (Speed.x * Time.deltaTime);
        float newY = transform.position.y + (Speed.y * Time.deltaTime);
        transform.position = new Vector2(newX, newY);
    }
}
