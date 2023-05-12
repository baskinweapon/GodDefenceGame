using System.Collections;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    public TextMeshProUGUI text;

    private void OnEnable() {
        text.text = "Use your God power";
        StartCoroutine(TutorProcess());
    }

    IEnumerator TutorProcess() {
        yield return new WaitForSeconds(4f);
        text.text = "Tap on the enemy to kill him";
        yield return new WaitForSeconds(5f);
        text.text = "Upgrade Your Pyramid";
        yield return new WaitForSeconds(2f);
        text.text = "Save Pharaon";
    }
}
