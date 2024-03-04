﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Enviro 
{
    [Serializable]
    public class EnviroEffectTypes
    {
        public ParticleSystem mySystem; 
        public string name;
        public GameObject prefab;
        public Vector3 localPositionOffset;
        public Vector3 localRotationOffset;
        public float emissionRate = 0f;
        public float maxEmission; 
    }

    [Serializable]
    public class EnviroEffects
    {
        public List<EnviroEffectTypes> effectTypes = new List<EnviroEffectTypes>();
    }
 
    [Serializable]
    [ExecuteInEditMode]
    public class EnviroEffectsModule : EnviroModule
    {  
        public Enviro.EnviroEffects Settings;
        public EnviroEffectsModule preset;

        //Inspector
        public bool showSetupControls;
        public bool showEmissionControls;
        public override void Enable ()
        { 
            if(EnviroManager.instance == null)
               return;
               
            Setup();
        } 

        public override void Disable ()
        { 
            if(EnviroManager.instance == null)
               return;

            Cleanup();
        }

        private void Setup()
        {
            CreateEffects();
        }  
 
        private void Cleanup()
        {
            if(EnviroManager.instance.Objects.effects != null)
               DestroyImmediate(EnviroManager.instance.Objects.effects);
        }

        public override void UpdateModule ()
        { 
            UpdateEffects();
        }

        public void CreateEffects() 
        {
            if(EnviroManager.instance.Objects.effects != null)
               DestroyImmediate(EnviroManager.instance.Objects.effects);

            if(EnviroManager.instance.Objects.effects == null)
            {
                EnviroManager.instance.Objects.effects = new GameObject();
                EnviroManager.instance.Objects.effects.name = "Effects";
                EnviroManager.instance.Objects.effects.transform.SetParent(EnviroManager.instance.transform);
                EnviroManager.instance.Objects.effects.transform.localPosition = Vector3.zero;
            }

            for(int i = 0; i < Settings.effectTypes.Count; i++)
            {
                if(Settings.effectTypes[i].mySystem != null)
                    DestroyImmediate(Settings.effectTypes[i].mySystem.gameObject);

                GameObject sys;
                  
                if(Settings.effectTypes[i].prefab != null)
                {
                   sys = Instantiate(Settings.effectTypes[i].prefab,Settings.effectTypes[i].localPositionOffset,Quaternion.identity);
                   sys.transform.SetParent(EnviroManager.instance.Objects.effects.transform);
                   sys.name = Settings.effectTypes[i].name;
                   sys.transform.localPosition = Settings.effectTypes[i].localPositionOffset;
                   sys.transform.localEulerAngles = Settings.effectTypes[i].localRotationOffset;
                   Settings.effectTypes[i].mySystem = sys.GetComponent<ParticleSystem>();
                }
            }
        }

        public float GetEmissionRate(ParticleSystem system)
        {
            return system.emission.rateOverTime.constantMax;
        }


        public void SetEmissionRate(ParticleSystem sys, float emissionRate)
        {
            var emission = sys.emission;
            var rate = emission.rateOverTime;
            rate.constantMax = emissionRate;
            emission.rateOverTime = rate;
        }

        private void UpdateEffects()
        {
            Shader.SetGlobalFloat("_EnviroLightIntensity", EnviroManager.instance.solarTime);

            for(int i = 0; i < Settings.effectTypes.Count; i++)
            {
                if(Settings.effectTypes[i].mySystem != null)
                {
                    float currentEmission = Settings.effectTypes[i].maxEmission * Settings.effectTypes[i].emissionRate;

                    SetEmissionRate(Settings.effectTypes[i].mySystem,currentEmission);

                    if(currentEmission > 0f && !Settings.effectTypes[i].mySystem.isPlaying)
                       Settings.effectTypes[i].mySystem.Play();
                }
            }
        }

        //Save and Load
        public void LoadModuleValues ()
        {
            if(preset != null)
            {
                Settings = JsonUtility.FromJson<Enviro.EnviroEffects>(JsonUtility.ToJson(preset.Settings));
            } 
            else
            {
                Debug.Log("Please assign a saved module to load from!");
            }
        }

        public void SaveModuleValues ()
        {
#if UNITY_EDITOR
        EnviroEffectsModule t =  ScriptableObject.CreateInstance<EnviroEffectsModule>();
        t.name = "Effects Module";
        t.Settings = JsonUtility.FromJson<Enviro.EnviroEffects>(JsonUtility.ToJson(Settings));
 
        string assetPathAndName = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(EnviroHelper.assetPath + "/New " + t.name + ".asset");
        UnityEditor.AssetDatabase.CreateAsset(t, assetPathAndName);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
#endif
        }

        public void SaveModuleValues (EnviroEffectsModule module)
        {
            module.Settings = JsonUtility.FromJson<Enviro.EnviroEffects>(JsonUtility.ToJson(Settings));
            #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(module);
            UnityEditor.AssetDatabase.SaveAssets();
            #endif
        }
    }
}