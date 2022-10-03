using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSlot : MonoBehaviour
{
    [SerializeField] Material basicMaterial;
    [SerializeField] Material plowdedMaterial;
    [SerializeField] Material plantedMaterial;

    public PlowButton plowderButton;
    public SeedButton seedButton;
    public HarvestButton harvestButton;

    [SerializeField] InventoryItem hoe;
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

        interactable = false;
        plowded = false;
        growed = false;
        gameObject.GetComponent<MeshRenderer>().material = basicMaterial;

        plowderButton = FindObjectOfType<PlowButton>(true);
        plowderButton.gameObject.SetActive(false);
        seedButton = FindObjectOfType<SeedButton>(true);
        seedButton.gameObject.SetActive(false);
        harvestButton = FindObjectOfType<HarvestButton>(true);
        harvestButton.gameObject.SetActive(false);

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
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactable = true;
            if (!plowded && ItemsManager.Instance.FindItemInInventory(hoe))
            {
                plowderButton.gameObject.SetActive(true);
                plowderButton.slot = this;
            }
            else if (plowded && !planted)
            {
                seedButton.gameObject.SetActive(true);
                seedButton.slot = this;
            }
            else if(plowded && planted && growed)
            {
                harvestButton.gameObject.SetActive(true);
                harvestButton.slot = this;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            plowderButton.gameObject.SetActive(false);
            seedButton.gameObject.SetActive(false);
            harvestButton.gameObject.SetActive(false);

            interactable = false;
            plowderButton.slot = null;
            seedButton.slot = null;
            harvestButton.slot = null;
        }
    }

    public void Plow()
    {
        gameObject.GetComponent<MeshRenderer>().material = plowdedMaterial;
        plowded = true;
        plowderButton.gameObject.SetActive(false);
    }
    public void Plant()
    {
        gameObject.GetComponent<MeshRenderer>().material = plantedMaterial;
        planted = true;
        seedButton.gameObject.SetActive(false);

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
        harvestButton.gameObject.SetActive(false);
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
