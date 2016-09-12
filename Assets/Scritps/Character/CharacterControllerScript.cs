using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonUserControl))]
public class CharacterControllerScript : MonoBehaviour {

    #region Private Vars
    #region Inventory Vars | Private Variables for the inventory System |

    GameObject _inventory;
    GameObject _tooltip;
    GameObject _character;
    GameObject _dropBox;

    GameObject inventory;
    GameObject craftSystem;
    GameObject characterSystem;

    Camera firstPersonCamera;
    ThirdPersonUserControl CharacterMove;
    #endregion 
    #endregion


    #region Public Vars
    #region Inventory Vars | Public Variables for the inventory System |
    public bool showInventory = false;
    #endregion

    #endregion

    void Start()
    {
        firstPersonCamera = Camera.main.GetComponent<Camera>();
        CharacterMove = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonUserControl>();

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            PlayerInventory playerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
            if (playerInv.inventory != null)
                inventory = playerInv.inventory;
            if (playerInv.craftSystem != null)
                craftSystem = playerInv.craftSystem;
            if (playerInv.characterSystem != null)
                characterSystem = playerInv.characterSystem;
        }
    }

    void Update()
    {
        showInventory = lockMovement();
        if (showInventory)
        {
            CharacterMove.m_CanMove = false;
        }
        else
        {
            CharacterMove.m_CanMove = true;
        }
    }

    bool lockMovement()
    {
        if (inventory != null && inventory.activeSelf)
            return true;
        else if (characterSystem != null && characterSystem.activeSelf)
            return true;
        else if (craftSystem != null && craftSystem.activeSelf)
            return true;
        else
            return false;
    }
}
