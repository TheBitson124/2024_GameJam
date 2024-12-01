using System.Collections;
using TMPro;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    [SerializeField] private float blinkInterval = 1.5f; 
    private bool _isVisible = true;
    private TextMeshProUGUI _text;
    private RestartScript _rs;
    private bool _isMenu;
    void Start() 
    {
        _text = GetComponent<TextMeshProUGUI>();
        _rs = GetComponent<RestartScript>();

        if (_rs is null) {
            _isMenu = true;
        }
        
        StartCoroutine(Blink());
    }
    
    private IEnumerator Blink() 
    {
        while (true) 
        {
            
            if ((_rs != null && _rs.isDead) || _isMenu)
            {
                _isVisible = !_isVisible;
                _text.enabled = _isVisible;

                if (_isVisible)
                {
                    yield return new WaitForSeconds(blinkInterval); // Czas widoczności
                }
                else
                {
                    yield return new WaitForSeconds(blinkInterval / 3); // Czas niewidoczności
                }
            }
            else
            {
               
                _text.enabled = false;
                yield return null; 
            }
        }
    }
}
