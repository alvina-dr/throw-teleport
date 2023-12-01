using DG.Tweening;
using UnityEngine;

public class BlinkColor : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;

    public void Blink()
    {
        Renderer[] children = GetComponentsInChildren<Renderer>();
        foreach (Renderer childRenderer in children)
        {
            if(childRenderer.GetType() != typeof(ParticleSystemRenderer))
            {
                AddMaterial(childRenderer);
            }
        }
        DOVirtual.DelayedCall(.1f, () =>
        {
            foreach (Renderer rend in children)
            {
                if (rend.GetType() != typeof(ParticleSystemRenderer))
                    RemoveMaterial(rend);
            }
        });
    }

    public void AddMaterial(Renderer rend) 
    {
        Material[] newMaterials = new Material[rend.materials.Length + 1];
        for (int i = 0; i < rend.materials.Length; i++)
        {
            newMaterials[i] = rend.materials[i];
        }
        newMaterials[rend.materials.Length] = flashMaterial;
        rend.materials = newMaterials;
        if (rend.transform.childCount > 0)
        {
            //Renderer[] children = rend.transform.GetComponentsInChildren<Renderer>();
            //foreach (Renderer childRenderer in children)
            //{
            //    AddMaterial(childRenderer);
            //}
        }
    }

    public void RemoveMaterial(Renderer rend)
    {
        Material[] oldMaterials = new Material[rend.materials.Length - 1];
        for (int i = 0; i < oldMaterials.Length; i++)
        {
            oldMaterials[i] = rend.materials[i];
        }
        rend.materials = oldMaterials;
        if (rend.transform.childCount > 0)
        {
            //Renderer[] children = rend.transform.GetComponentsInChildren<Renderer>();
            //foreach (Renderer childRenderer in children)
            //{
            //    RemoveMaterial(childRenderer);
            //}
        }
    }
}
