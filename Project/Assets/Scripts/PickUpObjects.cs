using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects: MonoBehaviour {

    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp")
        {
            gameController.tiempoPartida += 10f;

            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
        }

        if (other.tag == "Arrival")
        {
            other.enabled = false;
            gameController.SetPickUpsActive();
            gameController.completedLaps += 1;
            StartCoroutine(ReactivateTrigger(other));
        }

    }

    IEnumerator ReactivateTrigger(Collider other)
    {
        yield return new WaitForSeconds(5f);
        other.enabled = true;
    }


}
