using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionArea : MonoBehaviour
{
    Collider2D collision;

    // Start is called before the first frame update
    void Start()
    {
        collision = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E))
        {

            Collider2D[] itemColliderList = Physics2D.OverlapBoxAll(transform.position, GetComponent<Collider2D>().bounds.size, 0f);

            // Se encontrou um item, pega o componente WorldItem e o adiciona ao inventário
            foreach (Collider2D itemCollider in itemColliderList)
            {
                if (itemCollider != null)
                {
                    WorldItem worldItem = itemCollider.gameObject.GetComponent<WorldItem>();
                    if (worldItem != null)
                    {
                        InventoryManager.Instance.AddItem(worldItem.Item);  // Adiciona o item ao inventário
                        Destroy(itemCollider.gameObject);  // Remove o item do mundo
                    }
                }
            }

        }
    }

}
