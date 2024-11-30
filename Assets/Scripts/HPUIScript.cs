
using UnityEngine;
using UnityEngine.Serialization;

public class HPUIScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] llby;
    void OnEnable() {
        Player_Stats.OnHPChanged += UpdateHP;
    }

    private void OnDisable() {
        Player_Stats.OnHPChanged -= UpdateHP;
    }

    // Update is called once per frame
    private void UpdateHP(int newHP) {
        for (int i = 0; i < llby.Length; i++) {
            if (i <= newHP) {
                llby[i].color = Color.white;
            }
            else {
                llby[i].color = new Color(30f / 255f, 30f / 255f, 30f / 255f);
            }
        }
    }
}
