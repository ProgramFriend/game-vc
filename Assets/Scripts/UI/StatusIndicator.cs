using UnityEngine.UI;
using UnityEngine;
using System.Data;
using TMPro;
using System.Collections;

public class StatusIndicator : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthBarRect;

    public Gradient gradient;
    public Image healthBarFill;

    [HideInInspector] public int curHP = 0;
    [HideInInspector] public int maxHP = 0;


    public void SetHealth(int _curHealth, int _maxHealth)
    {
        maxHP = _maxHealth;
        if (curHP > _curHealth) StartCoroutine(MinusHealthByOne(_curHealth));
        else
        {
            float _value = (float)_curHealth / maxHP;
            healthBarRect.localScale = new Vector2(_value, healthBarRect.localScale.y);
            healthBarFill.color = gradient.Evaluate(_value);
        }
        curHP = _curHealth;
    }

    public IEnumerator MinusHealthByOne(int _health)
    {
        float x = 0.8f / (curHP - _health);
        for (int i = curHP; i >= _health; i--)
        {
            float _value = (float)i / maxHP;
            healthBarRect.localScale = new Vector2(_value, healthBarRect.localScale.y);
            healthBarFill.color = gradient.Evaluate(_value);
            yield return new WaitForSeconds(x);
        }
    }
}
