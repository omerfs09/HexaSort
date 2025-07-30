using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;


    [SerializeField] private ParticleSystem levelEndVFX;

    

    public List<ParticleSystem> particles;
    private Dictionary<VFXEnums, List<ParticleSystem>> _pool = new();
    private Dictionary<VFXEnums, Transform> _particleParentList = new();
    [SerializeField] private int poolSize;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FillPoolAtStart();
    }

    

    public void LevelEndVFX()
    {
        levelEndVFX.Play();
    }

    
    public void FillPoolAtStart()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            var enumType = (VFXEnums)Enum.Parse(typeof(VFXEnums), particles[i].name);
            GameObject parent = new($"{particles[i].name}");
            parent.transform.SetParent(transform);
            _particleParentList.Add(enumType, parent.transform);

            var particleList = new List<ParticleSystem>();
            for (int j = 0; j < poolSize; j++)
            {
                var particle = Instantiate(particles[i], parent.transform);
                particleList.Add(particle);
                particle.gameObject.SetActive(false);
            }
            _pool.Add(enumType, particleList);
        }
    }

    public ParticleSystem SpawnParticle(VFXEnums vfxType)
    {
        var particle = _pool[vfxType].FirstOrDefault(p => !p.gameObject.activeInHierarchy);
        if (particle is not null)
        {
            return particle;
        }
        else
        {
            var particleName = vfxType.ToString();
            var particleToSpawn = particles.FirstOrDefault(p => p.gameObject.name == particleName);
            var particleParent = _particleParentList[vfxType];
            var spawnedParticle = Instantiate(particleToSpawn, particleParent);
            var list = _pool[vfxType];
            list.Add(spawnedParticle);
            return spawnedParticle;
        }
    }

    public ParticleSystem GetParticle(VFXEnums particleEnum, Vector3 spawnPos, bool isColorGoingToChange = false, Color newColor = default)
    {
        var particle = SpawnParticle(particleEnum);
        if (isColorGoingToChange)
        {
            var mainModule = particle.main;
            mainModule.startColor = newColor;
        }
        particle.gameObject.SetActive(true);
        particle.transform.position = spawnPos;
        return particle;
    }
}
public enum VFXEnums
{
    ClearSlotVFX,
}
