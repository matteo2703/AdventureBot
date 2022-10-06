using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSlot : MonoBehaviour
{
    [SerializeField] Material basicMaterial;
    [SerializeField] Material plowdedMaterial;
    [SerializeField] Material plantedMaterial;

    [SerializeField] InventoryItem plantFruit;

    public float growingTimeInSeconds;

    public bool interactable;
    public bool plowded;
    public bool planted;
    public bool growed;

    [SerializeField] List<GameObject> prefabsPlantGrowing;
    GameObject plant;
    public int actualState;

    Input input;

    private void Awake()
    {
        actualState = 0;

        plowded = false;
        growed = false;
        gameObject.GetComponent<MeshRenderer>().material = basicMaterial;

        input = new Input();
        input.General.Enable();

        input.General.Interact.started += i =>
        {
            if (interactable && !plowded)
                Plow();
            else if (interactable && plowded && !growed)
                Plant();
            else if (interactable && growed)
                Harvest();
        };
    }
    public void SetState()
    {
        if (planted)
            Plant();
        else if (plowded)
            Plow();
    }

    public void Plow()
    {
        gameObject.GetComponent<MeshRenderer>().material = plowdedMaterial;
        plowded = true;
    }
    public void Plant()
    {
        gameObject.GetComponent<MeshRenderer>().material = plantedMaterial;
        planted = true;

        plant = Instantiate(prefabsPlantGrowing[actualState], gameObject.transform);
        StartCoroutine(Growing());
    }

    public IEnumerator Growing()
    {
        while (actualState < prefabsPlantGrowing.Count - 1)
        {
            yield return new WaitForSeconds(growingTimeInSeconds / prefabsPlantGrowing.Count);
            Destroy(plant);
            actualState++;
            plant = Instantiate(prefabsPlantGrowing[actualState], gameObject.transform);
        }
        growed = true;
    }
    public void Harvest()
    {
        Destroy(plant);
        gameObject.GetComponent<MeshRenderer>().material = basicMaterial;
        ItemsManager.Instance.AddPlayerObject(plantFruit);

        GrowQuest quest = FindObjectOfType<GrowQuest>();
        if (quest != null)
            quest.plantsGrowed++;

        plowded = false;
        planted = false;
        growed = false;
        actualState = 0;
    }
}
