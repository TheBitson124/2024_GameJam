using System.Collections;
using TMPro;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    [SerializeField] private float blinkInterval = 1.5f; 
    private bool _isVisible = true;
    private TextMeshProUGUI _text;
    void Start() 
    {
        _text = GetComponent<TextMeshProUGUI>();

        StartCoroutine(Blink());
    }
    
    private IEnumerator Blink() 
    {
        while (true) {
            _isVisible = !_isVisible;
            _text.enabled = _isVisible;

            if (_isVisible)
            {
                yield return new WaitForSeconds(blinkInterval); // Czas widoczności
            }
            else
            {
                yield return new WaitForSeconds(blinkInterval/3); // Czas niewidoczności
            }
        }
    }
}
