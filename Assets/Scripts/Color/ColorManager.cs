using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core;

public class ColorManager : Singleton<ColorManager>
{
    public List<Material> materials;
    [NonReorderable]
    public List<ColorSetup> colorSetups;

    public void ChangeColorByType(ArtManager.ArtType artType)
    {
        var setup = colorSetups.Find(a => a.artType == artType);

        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetColor("_Color", setup.colors[i]);
        }
    }
}

[System.Serializable]
public class ColorSetup
{
    public ArtManager.ArtType artType;
    public List<Color> colors;
}
