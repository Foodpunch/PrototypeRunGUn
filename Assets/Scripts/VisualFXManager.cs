using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFXManager : MonoBehaviour
{
    public static VisualFXManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
[System.Serializable]
public class Effects            //effects class, effects data
{
    public string name;
    public GameObject gameObject;
    Vector3 position;
    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }
    Quaternion rotation;
    public Quaternion Rotation { get => rotation; set => rotation = value; }
    public enum EffectType
    {
        GENERIC,
        BULLET,
        GIBLETS,
        EXPLOSION
    }
    public EffectType Type;
    public Vector3 OriginDirection;
    public AudioClip audioClip;

    public Effects(Effects e, ContactPoint2D contact)
    {
        name = e.name;
        gameObject = e.gameObject;
        position = contact.point;
        Rotation = Quaternion.FromToRotation(e.OriginDirection, contact.normal);
        audioClip = e.audioClip;
    }
}
[CreateAssetMenu]
public class EffectsData : ScriptableObject     //scriptable obj holding all effects
{
    //static EffectsData _instance;               //singleton reference
    //public static EffectsData Instance
    //{
    //    get
    //    {
    //        if (_instance == null)

    //            _instance = new EffectsData();

    //        return Instance;
    //    }
    //}

    public List<Effects> EffectsList;           

    public void SpawnAllFX(ContactPoint2D contact)
    {
        for(int i =0; i<EffectsList.Count; i++)
        {
            Effects FXClone = new Effects(EffectsList[i], contact);
            Instantiate(FXClone.gameObject, FXClone.Position, FXClone.Rotation);

        }
    }

   
}