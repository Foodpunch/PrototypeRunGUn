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
    public void SpawnFXType(Effects.EffectType effectType,Vector3 pos)
    {
        SpawnFX(GetEffectsByType(effectType), pos, Quaternion.identity);
    }
    public void SpawnFXName(string name,ContactPoint2D contact)
    {
        Effects _fx = GetEffectByName(name);
        //SpawnFX(_fx, contact);
    }
    public void SpawnFXName(string name,Vector3 pos)
    {
        Effects _fx = GetEffectByName(name);
        SpawnFX(_fx, pos);
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
    Effects GetEffectByName(string name)
    {
        for(int i=0; i<effectsList.Count; i++)
        {
            if(Equals(name, effectsList[i].gameObject.name))
            {
                return effectsList[i];
            }
        }
        throw new System.Exception("No such effect name");
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
    void SpawnFX(List<Effects> _list,Vector3 pos,Quaternion quat)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            Effects _fx = new Effects(_list[i],new ContactPoint2D());
            GameObject FXClone = Instantiate(_fx.gameObject, pos, quat);
            FXClone.transform.SetParent(gameObject.transform);
        }
    }
    void SpawnFX(Effects fx, Vector3 pos)
    {
        Effects _fx = new Effects(fx,new ContactPoint2D());
        GameObject FXClone = Instantiate(_fx.gameObject, pos, Quaternion.identity);
        FXClone.transform.SetParent(gameObject.transform);
    }
}
[System.Serializable]
public class Effects            //effects class, effects data
{
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
        gameObject = e.gameObject;
        position = contact.point;
        Rotation = Quaternion.FromToRotation(e.OriginDirection, contact.normal);
     //   audioClip = e.audioClip;
    }
}
