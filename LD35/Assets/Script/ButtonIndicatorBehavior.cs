using UnityEngine;
using System.Collections;

public class ButtonIndicatorBehavior : MonoBehaviour {

	public void TriggerIndicator(int index)
    {
        transform.GetChild(index);
    }

}
