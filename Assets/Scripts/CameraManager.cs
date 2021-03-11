using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField]
    [Range(0f,1f)]
    float trauma;

    float shake;
    //Transform originalTransform;
    /*  Camera Shake Implementation from GDC
     *  trauma -= Time.deltaTime; 
     *  shake = trauma^ or trauma^3
     *  angle = maxAngle * shake * Rand(-1,1)
     *  offsetX = maxOffset * shake * Rand(-1,1)
     *  offsetY = maxOffset * shake * Rand(-1,1)
     *  shakeCam.rot = originalRot + angle
     *  shakeCame.pos = originalPos + Vector2(offsetX,offsetY)
     */
    Transform originalTransform;
    float angle, offsetX, offsetY;
    public float maxAngle, maxOffset;
    GameObject player;

    Vector3 originPos;
    Quaternion originRot;
    private void Awake()
    {
        Random.InitState(1337);
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerScript.instance.gameObject;
        originalTransform = gameObject.transform;
        originPos = transform.position;
        originRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(ShakeCam(0.2f));
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(ShakeCam(0.4f));
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StartCoroutine(ShakeCam(0.5f));
        }
        trauma = Mathf.Clamp(trauma, 0f, 1f);
        if (trauma <0f)
        {
            trauma = 0f;
            transform.position = originPos;
            transform.rotation = originRot;
        }
        
        //if(trauma > 0)
        //{
        //    trauma -= Time.deltaTime;       //decreases linearly
        //}
    }

    
    IEnumerator ShakeCam(float _trauma)
    {
        trauma += _trauma;

        float offset = Random.value;
        offset = offset * 2f - 1;
        while (trauma>0)
        {
            trauma -= Time.deltaTime;
            shake = Mathf.Pow(trauma, 2f);
            //angle = maxAngle * shake * FakeNoise(trauma);
            //offsetX = maxOffset * shake * FakeNoise(trauma);
            //offsetY = maxOffset * shake * FakeNoise(trauma);
            angle = maxAngle * shake * GetPerlinNoise(trauma, offset);
            offsetX = maxOffset * shake * GetPerlinNoise(trauma, offset + 1f);
            offsetY = maxOffset * shake * GetPerlinNoise(trauma, offset + 2f);

            transform.rotation = originRot * Quaternion.Euler(new Vector3(0f, 0f, angle));
            transform.position = originPos + new Vector3(offsetX, offsetY, 0f);
            yield  return new WaitForSecondsRealtime(Time.deltaTime);
        }       
    }
    float FakeNoise(float time)
    {
        return Mathf.Lerp(Random.Range(-1f, 1f), Random.Range(-1f, 1f), time);        
    }
    float GetPerlinNoise(float time, float offset)
    {
        Debug.Log("Perlin noise = " + ((Mathf.PerlinNoise(time + offset, time + offset)*2f)-1f) +
            " denorm value = " + DeNomalise((Mathf.PerlinNoise(time + offset, time + offset)),-1f,1f));
        //return Mathf.Lerp(-1f, 1f, Mathf.PerlinNoise(time+offset, time+offset));
        return (DeNomalise(Mathf.PerlinNoise(time + offset, time + offset), -1f, 1f));
    }
    float DeNomalise(float value,float min, float max)
    {
        return value * (max - min) + min;
    }
}
