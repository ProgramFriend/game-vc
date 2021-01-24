using UnityEngine.UI;
using UnityEngine;
using System.Data;
using TMPro;
using System.Collections;

public class StatusIndicatorPlayer : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthBarRect;

    public Gradient gradient;
    public Image healthBarFill;

    public Slider slider; //exp slider
    public TMP_Text levelText;

    public TMP_Text goldText;
    [HideInInspector] public int curGold = 0;
    [HideInInspector] public int curXP;
    [HideInInspector] public int curHP = 0;
    [HideInInspector] public int maxHP = 0;


    public void SetHealth(int _curHealth, int _maxHealth)
    {
        maxHP = _maxHealth;
        if (curHP > _curHealth) StartCoroutine(MinusHealthByOne(_curHealth));
        else StartCoroutine(AddHealthByOne(_curHealth));
        curHP = _curHealth;
    }
    public void SetHealth(int _curHealth)
    {
        if (curHP > _curHealth) StartCoroutine(MinusHealthByOne(_curHealth));
        else StartCoroutine(AddHealthByOne(_curHealth));
        curHP = _curHealth;
    }

    public void SetExp(int XP, int maxXP, int curLevel)
    {
        StartCoroutine(AddExpByOne(XP, maxXP));
        curXP = XP;
        levelText.text = curLevel.ToString();
    }

    public void SetGold(int gold)
    {
        if (curGold < gold) StartCoroutine(AddGoldByOne(gold));
        else goldText.text = gold.ToString();
        curGold = gold;
    }

    public IEnumerator AddGoldByOne(int _gold)
    {
        float waitTime = 1f / _gold;
        for (int i = curGold; i <= _gold; i++)
        {
            goldText.text = i.ToString();
            yield return new WaitForSeconds(waitTime);
        }
    }

    public IEnumerator AddExpByOne(int _exp, int _max)
    {
        for (int i = curXP; i <= _exp; i++)
        {
            slider.value = (float)i / _max;
            if ((_exp - curXP) > 50) yield return new WaitForSeconds(0.1f / _exp);
            else yield return new WaitForSeconds(0.02f);
        }
    }

    public IEnumerator MinusHealthByOne(int _health)
    {
        for (int i = curHP; i >= _health; i--)
        {
            float _value = (float)i / maxHP;
            healthBarRect.localScale = new Vector2(_value, healthBarRect.localScale.y);
            healthBarFill.color = gradient.Evaluate(_value);
            //if ((curHP - _health) > 50) yield return new WaitForSeconds(0.1f / _health);
            /*else */
            yield return new WaitForSeconds(0.01f);
        }
    }
    public IEnumerator AddHealthByOne(int _health)
    {
        for (int i = curHP; i <= _health; i++)
        {
            float _value = (float)i / maxHP;
            healthBarRect.localScale = new Vector2(_value, healthBarRect.localScale.y);
            healthBarFill.color = gradient.Evaluate(_value);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
