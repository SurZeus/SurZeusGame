using UnityEngine;
using System;
using System.Collections.Generic;

//[ExecuteInEditMode]
public class SkinnedLOD : MonoBehaviour
{
    public Mesh LOD0;
    public Mesh LOD1;
    public Mesh LOD2;
    public Mesh LOD3;

    public List<Material> LOD_0_Materials;
    public List<Material> LOD_1_Materials;
    public List<Material> LOD_2_Materials;
    public List<Material> LOD_3_Materials;

    public float lod0Dist = 10f;
    public float lod1Dist = 20f;
    public float lod2Dist = 30f;
    public float lod3Dist = 50f;
    public float lodTimer = 0.3f;

    public Camera camera1;
    private bool foundCamera;
    public int currLOD;
    private float currTimer;


    public void Start()
    {
        GetCamera();
        currLOD = 0;
    }

    void Update()
    {
        if (foundCamera == false)
            GetCamera();

        //Update LODs Timer
        currTimer -= Time.deltaTime;
        if (currTimer <= 0.0f)
        {
            UpdateMeshLOD();

            currTimer = lodTimer; //Reset Timer
        }
    }

    public void UpdateMeshLOD()
    {
        if (camera1 == null) return;

        Vector3 camPos1 = camera1.transform.position;

        if ((transform.position - camPos1).sqrMagnitude < (lod0Dist * QualitySettings.lodBias) * (lod0Dist * QualitySettings.lodBias)) //LOD 0
        {
            if (currLOD != 0)
            {
                SetLowestLODMesh(QualitySettings.maximumLODLevel);
                SetLODMaterials(QualitySettings.maximumLODLevel);
                currLOD = 0;
                return;
            }
        }
        else if ((transform.position - camPos1).sqrMagnitude < (lod1Dist * QualitySettings.lodBias) * (lod1Dist * QualitySettings.lodBias)) //LOD 1
        {
            if (currLOD != 1)
            {
                SetLowestLODMesh(QualitySettings.maximumLODLevel + 1);
                SetLODMaterials(GetLowestLODMats(QualitySettings.maximumLODLevel + 1));
                currLOD = 1;
                return;
            }
        }
        else if ((transform.position - camPos1).sqrMagnitude < (lod2Dist * QualitySettings.lodBias) * (lod2Dist * QualitySettings.lodBias)) //LOD 2
        {
            if (currLOD != 2)
            {
                SetLowestLODMesh(QualitySettings.maximumLODLevel + 2);
                SetLODMaterials(GetLowestLODMats(QualitySettings.maximumLODLevel + 2));
                currLOD = 2;
                return;
            }
        }
        else if ((transform.position - camPos1).sqrMagnitude < (lod3Dist * QualitySettings.lodBias) * (lod3Dist * QualitySettings.lodBias)) //LOD 3
        {
            if (currLOD != 3)
            {
                SetLowestLODMesh(QualitySettings.maximumLODLevel + 3);
                SetLODMaterials(GetLowestLODMats(QualitySettings.maximumLODLevel + 3));
                currLOD = 3;
                return;
            }
        }
    }

    public void SetLODMaterials(int lod)
    {
        Material[] currMats;
        bool wasSuccess = false;

        switch (lod)
        {
            case 0: //LOD 0
                if (LOD_0_Materials.Count > 0)
                {
                    int existingMatsCount = 0;
                    currMats = new Material[LOD_0_Materials.Count];
                    for (var x = 0; x < LOD_0_Materials.Count; x++)
                    {
                        currMats[x] = LOD_0_Materials[x];
                        if (LOD_0_Materials[x] != null)
                            existingMatsCount++;
                    }
                    if (existingMatsCount / LOD_0_Materials.Count > 0.5f) //Atleast 50% of materials exist
                    {
                        GetComponent<Renderer>().sharedMaterials = currMats;
                        wasSuccess = true;
                    }
                }
                break;
            case 1:
                if (LOD_1_Materials.Count > 0)
                {
                    int existingMatsCount = 0;
                    currMats = new Material[LOD_1_Materials.Count];
                    for (var x = 0; x < LOD_1_Materials.Count; x++)
                    {
                        currMats[x] = LOD_1_Materials[x];
                        if (LOD_1_Materials[x] != null)
                            existingMatsCount++;
                    }
                    if (existingMatsCount / LOD_1_Materials.Count > 0.5f) //Atleast 50% of materials exist
                    {
                        GetComponent<Renderer>().sharedMaterials = currMats;
                        wasSuccess = true;
                    }
                }
                break;
            case 2:
                if (LOD_2_Materials.Count > 0)
                {
                    int existingMatsCount = 0;
                    currMats = new Material[LOD_2_Materials.Count];
                    for (var x = 0; x < LOD_2_Materials.Count; x++)
                    {
                        currMats[x] = LOD_2_Materials[x];
                        if (LOD_2_Materials[x] != null)
                            existingMatsCount++;
                    }
                    if (existingMatsCount / LOD_2_Materials.Count > 0.5f) //Atleast 50% of materials exist
                    {
                        GetComponent<Renderer>().sharedMaterials = currMats;
                        wasSuccess = true;
                    }
                }
                break;
            case 3:
                if (LOD_3_Materials.Count > 0)
                {
                    int existingMatsCount = 0;
                    currMats = new Material[LOD_3_Materials.Count];
                    for (var x = 0; x < LOD_3_Materials.Count; x++)
                    {
                        currMats[x] = LOD_3_Materials[x];
                        if (LOD_3_Materials[x] != null)
                            existingMatsCount++;
                    }
                    if (existingMatsCount / LOD_3_Materials.Count > 0.5f) //Atleast 50% of materials exist
                    {
                        GetComponent<Renderer>().sharedMaterials = currMats;
                        wasSuccess = true;
                    }
                }
                break;
        }

        if (wasSuccess)
            Debug.Log("[CharacterLOD] " + this.transform.root.name + " Swapped LOD Mats to: " + lod);
    }

    public int GetLowestLODMats(int desired)
    {
        if (desired >= 3)
        {
            if (LOD_3_Materials.Count > 0)
                return 3;
            if (LOD_2_Materials.Count > 0)
                return 2;
            if (LOD_1_Materials.Count > 0)
                return 1;
            if (LOD_0_Materials.Count > 0)
                return 0;
        }
        if (desired == 2)
        {
            if (LOD_2_Materials.Count > 0)
                return 2;
            if (LOD_1_Materials.Count > 0)
                return 1;
            if (LOD_0_Materials.Count > 0)
                return 0;
        }
        if (desired == 1)
        {
            if (LOD_1_Materials.Count > 0)
                return 1;
            if (LOD_0_Materials.Count > 0)
                return 0;
        }
        if (desired == 0)
        {
            if (LOD_0_Materials.Count > 0)
                return 0;
        }
        return 0;
    }

    public void SetLowestLODMesh(int desired)
    {
        SkinnedMeshRenderer mf1 = GetComponent<SkinnedMeshRenderer>();
        if (desired >= 3)
        {
            if (LOD3 != null)
                mf1.sharedMesh = LOD3;
            if (LOD2 != null)
                mf1.sharedMesh = LOD2;
            if (LOD1 != null)
                mf1.sharedMesh = LOD1;
            if (LOD0 != null)
                mf1.sharedMesh = LOD0;
        }
        if (desired == 2)
        {
            if (LOD2 != null)
                mf1.sharedMesh = LOD2;
            if (LOD1 != null)
                mf1.sharedMesh = LOD1;
            if (LOD0 != null)
                mf1.sharedMesh = LOD0;
        }
        if (desired == 1)
        {
            if (LOD1 != null)
                mf1.sharedMesh = LOD1;
            if (LOD0 != null)
                mf1.sharedMesh = LOD0;
        }
        if (desired == 0)
            if (LOD0 != null)
                mf1.sharedMesh = LOD0;
    }

    public void GetCamera()
    {
        try
        {
            camera1 = Camera.main;
            foundCamera = true;
        }
        catch (Exception e)
        {
            Debug.Log("[CharacterLOD] Couldn't find Main Camera: " + e.Message);
        }
    }
}