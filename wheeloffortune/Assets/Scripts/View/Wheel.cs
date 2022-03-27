using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Wheel : MonoBehaviour
{

    public int numberOfSectors = 16;
    public float rotationTime=5f;
    private const float FullCircle = 360.0f;
    public Transform sectorParent;
    private bool isSpinning;

    public Transform wheelCircle;
    public Transform stickTransform;
    public GameObject sectorPrefab;


    public List<int> defValues;
    private int layerMask;

    public AudioSource audioSource ;
    public AudioClip sfxWheel ;

    private UnityEvent onSpinStartEvent=new UnityEvent();
    private UnityEvent onSpinEndEvent=new UnityEvent();
    public static IntEvent RewardEvent=new IntEvent();
    
    
    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("sectors");
        SetPosition();
        SetDefValues();
        SetSectorValues();
    }

    
    
    private void SetDefValues()
    {
        defValues = Enumerable.Range(1, 1000)
                                .Select(n => n*1000)
                                .ToList();
    }

    private void SetSectorValues()
    {
        Sector[] sectors = GetComponentsInChildren<Sector>();
        List<int> used=new List<int>();
        foreach (var VARIABLE in sectors)
        {
            int sectorValue = GetNextSectorvalue(used);
            VARIABLE.ScoreValue = sectorValue;
        }
    }

    private int GetNextSectorvalue(List<int> used)
    {
        while (true)
        {
            int tekint = defValues[Random.Range(0, defValues.Count)];
            if (!used.Contains(tekint))
            {
                return tekint;
            }
        }
    }


    private void SetPosition()
    {
        for (int i = 0; i < numberOfSectors; i++)
        {
            
            GameObject sectorOb = Instantiate(sectorPrefab,sectorParent);
            sectorOb.transform.RotateAround(sectorParent.position,Vector3.forward,-FullCircle/(float)numberOfSectors*i);
            Sector sector = sectorOb.GetComponent<Sector>();
            sector.Init(i);
        }
    }


    private void Awake()
    {
        onSpinEndEvent.AddListener(DetectResult);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = this.sfxWheel;
    }

    public void Spin()
    {
        if (!isSpinning)
        {
            isSpinning = true;
            onSpinStartEvent.Invoke();
            
            float rightOffset = 0;
            float leftOffset = FullCircle;
            
            float randomAngle = Random.Range(leftOffset, rightOffset);
            Vector3 targetRotation = Vector3.back * (randomAngle +  360 * rotationTime);
            
            float angle1, angle2;

            angle1=angle2=wheelCircle.eulerAngles.z ;
            
            float pieceAngle = 2*FullCircle / numberOfSectors;

            wheelCircle
                .DORotate(targetRotation, rotationTime)
                .SetEase(Ease.InOutCirc)
                .OnUpdate(() =>
                {
                    float diff = Mathf.Abs (angle1 - angle2) ;
                    if (diff >= pieceAngle) {
                        audioSource.PlayOneShot (audioSource.clip) ;
                        angle1 = angle2 ;
                    }
                    angle2 = wheelCircle.eulerAngles.z ;
                })
                .OnComplete(() =>
                {
                    isSpinning = false;
                    onSpinEndEvent.Invoke();
                });

        }
    }

    public void DetectResult()
    {    
        RaycastHit2D hit = Physics2D.Raycast(stickTransform.position, Vector2.zero,-10,layerMask);
             
        Debug.DrawRay(stickTransform.position,  Vector2.right, Color.green, 5f);
        if (hit.transform != null)
        {
            Sector sector = hit.transform.gameObject.GetComponent<Sector>();
            if (sector != null)
            {
                //Debug.Log(sector.ScoreValue.ToString());    
                RewardEvent.Invoke(sector.ScoreValue);
            }
        }
        
    }
    
    
}
