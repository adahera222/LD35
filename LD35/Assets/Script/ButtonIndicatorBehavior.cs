using UnityEngine;
using System.Collections;

public class ButtonIndicatorBehavior : MonoBehaviour {

    public Material[] materials;

	public void TriggerIndicator(int index)
    {
        transform.GetChild(index).GetComponent<MeshRenderer>().material = materials[1];
    }

}
