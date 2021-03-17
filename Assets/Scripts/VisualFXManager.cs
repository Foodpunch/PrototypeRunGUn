using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFXManager : MonoBehaviour
{
    public static VisualFXManager i;
    public List<Effects> effectsList;


    // Start is called before the first frame update
    void Awake()
    {
        i = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnALLFX(ContactPoint2D contact)      //testing
    {
        SpawnFX(effectsList, contact);
    }
    public void SpawnFXType(Effects.EffectType _type, ContactPoint2D contact)
    {
        SpawnFX(GetEffectsByType(_type), contact);
    }
    //public void SpawnBulletFX(ContactPoint2D contact)
    //{
    //    SpawnFX(GetEffectsByType(Effects.EffectType.BULLET), contact);
    //    //for(int i=0;i<effectsList.Count; i++)
    //    //{
    //    //    if(effectsList[i].Type == Effects.EffectType.BULLET)
    //    //    {
    //    //        Effects FXClone = new Effects(effectsList[i], contact);
    //    //        Instantiate(FXClone.gameObject, FXClone.Position, FXClone.Rotation);
    //    //    }
    //    //}
    //}
    List<Effects> GetEffectsByType(Effects.EffectType _type)
    {
        List<Effects> listToReturn = new List<Effects>();
        for(int i=0; i<effectsList.Count; i++)
        {
            if(effectsList[i].Type == _type)
            {
                listToReturn.Add(effectsList[i]);
            }
        }
        return listToReturn;
    }
    void SpawnFX(List<Effects> _list,ContactPoint2D contact)
    {
        for(int i=0; i<_list.Count; i++)
        {
            Effects _fx = new Effects(_list[i], contact);
            GameObject FXClone = Instantiate(_fx.gameObject, _fx.Position, _fx.Rotation);
            FXClone.transform.SetParent(gameObject.transform);
        }
    }

}
[System.Serializable]
public class Effects            //effects class, effects data
{
    //public string name;
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
 //   public AudioClip audioClip; audio manager will handle this

    public Effects(Effects e, ContactPoint2D contact)
    {
       // name = e.name;
        gameObject = e.gameObject;
        position = contact.point;
        Rotation = Quaternion.FromToRotation(e.OriginDirection, contact.normal);
     //   audioClip = e.audioClip;
    }
}
