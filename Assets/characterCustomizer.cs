using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class characterCustomizer : MonoBehaviour
{
    [SerializeField] private Transform displayTransform;

    [SerializeField] private Material[] _materials;
    [SerializeField] private RawImage[] _lockedColours;

    [SerializeField] private Slider _metallicSlider;
    [SerializeField] private Slider _smoothnessSlider;
    [SerializeField] private Slider _transformScaleSliderX;
    [SerializeField] private Slider _transformScaleSliderY;

    [SerializeField] private string _materialName;

    [SerializeField] private TMP_Text _materialText;
    [SerializeField] private TMP_Text _MetallicValueNumber;
    [SerializeField] private TMP_Text _SmoothnessValueNumber;
    [SerializeField] private TMP_Text _transformScaleValueNumberX;
    [SerializeField] private TMP_Text _transformScaleValueNumberY;
    [SerializeField] private TMP_Text _MaterialColorNameR;
    [SerializeField] private TMP_Text _MaterialColorNameG;
    [SerializeField] private TMP_Text _MaterialColorNameB;
    [SerializeField] private TMP_Text _MaterialColorNameA;
    [SerializeField] private TMP_Text _CashAmount;

    [SerializeField] private TMP_Text[] _lockedText;

    [SerializeField] private string _Metallic = "_Metallic";
    [SerializeField] private string _Glossiness = "_Glossiness";
    [SerializeField] private string _Color = "_Color";

    [SerializeField] private float xScale, yScale;
    [SerializeField] private float cash;

    [SerializeField] private bool[] _lockedColoursB;
    void Awake()
    {
        _lockedColoursB = new bool[_lockedColours.Length];

        for (int i = 0; i < _lockedColoursB.Length; i++)
        {
            _lockedColoursB[i] = true;

            _lockedText[i].enabled = true;
            _lockedText[i].text = "Locked - 1000";
        }

        xScale = PlayerPrefs.GetFloat("xScale");
        yScale = PlayerPrefs.GetFloat("yScale");
        displayTransform.localScale = new Vector3(xScale, yScale, displayTransform.localScale.z);
        _transformScaleSliderX.value = xScale;
        _transformScaleValueNumberX.text = _transformScaleSliderX.value.ToString("f1");
        _transformScaleSliderY.value = yScale;
        _transformScaleValueNumberY.text = _transformScaleSliderY.value.ToString("f1");
        _CashAmount.text = cash.ToString("f1");
    }
    void LateUpdate()
    {
        if (_materialName != "")
        {
            _materialText.text = _materialName;

            _metallicSlider.interactable = true;
            _smoothnessSlider.interactable = true;
        }
        else
        {
            _materialText.text = "Select a Material";
        }
    }
    public void getAllDataFromMaterials()
    {
        foreach (Material item in _materials)
        {
            if (item)
            {
                if (_materialName.Contains(item.name.ToString()))
                {
                    _metallicSlider.value = item.GetFloat(_Metallic);
                    _smoothnessSlider.value = item.GetFloat(_Glossiness);
                    _MetallicValueNumber.text = _metallicSlider.value.ToString("f1");
                    _SmoothnessValueNumber.text = _smoothnessSlider.value.ToString("f1");

                    _MaterialColorNameR.text = "R " + item.GetColor(_Color).r.ToString("f1");
                    _MaterialColorNameG.text = "G " + item.GetColor(_Color).g.ToString("f1");
                    _MaterialColorNameB.text = "B " + item.GetColor(_Color).b.ToString("f1");
                    _MaterialColorNameA.text = "A " + item.GetColor(_Color).a.ToString("f1");
                }
            }
        }
    }
    public void MetallicChange(float num)
    {
        foreach (Material item in _materials)
        {
            if (item)
            {
                if (_materialName.Contains(item.name.ToString()))
                {
                    _metallicSlider.value = num;
                    item.SetFloat(_Metallic, num);
                }
            }
        }
    }
    public void SmoothnessChange(float num)
    {
        foreach (Material item in _materials)
        {
            if (item)
            {
                if (_materialName.Contains(item.name.ToString()))
                {
                    _smoothnessSlider.value = num;
                    item.SetFloat(_Glossiness, num);
                }
            }
        }
    }
    public void transformSizeX(float num)
    {
        displayTransform.localScale = new Vector3(num, displayTransform.localScale.y, displayTransform.localScale.z);
        _transformScaleValueNumberX.text = _transformScaleSliderX.value.ToString("f1");
        PlayerPrefs.SetFloat("xScale", num);
        Debug.Log("Saving to playerprefs...");
    }
    public void transformSizeY(float num)
    {
        displayTransform.localScale = new Vector3(xScale, num, displayTransform.localScale.z);
        _transformScaleValueNumberY.text = _transformScaleSliderY.value.ToString("f1");
        PlayerPrefs.SetFloat("yScale", num);
        Debug.Log("Saving to playerprefs...");
    }
    public void SetMaterialName(string name)
    {
        _materialName = name;
        getAllDataFromMaterials();
    }
    public void resetMaterialProperties()
    {
        foreach (Material item in _materials)
        {
            if (_materialName.Contains(item.name.ToString()))
            {
                if (item)
                {
                    if (item.GetFloat(_Metallic) > 0)
                    {
                        item.SetFloat(_Metallic, 0);

                        Debug.Log(item.name + " metallic was reset");
                    }

                    if (item.GetFloat(_Glossiness) > 0)
                    {
                        item.SetFloat(_Glossiness, 0);
                        Debug.Log(item.name + " glossiness was reset");
                    }

                    if (item.GetColor(_Color) != Color.white)
                    {
                        item.SetColor(_Color, Color.white);
                        Debug.Log(item.name + " glossiness was reset");
                    }
                }
            }
        }
    }
    public void randomMaterialProperties()
    {
        foreach (Material item in _materials)
        {
            if (_materialName.Contains(item.name.ToString()))
            {
                if (item)
                {
                    item.SetFloat(_Metallic, Random.Range(.1f, 1f));
                    item.SetFloat(_Glossiness, Random.Range(.1f, 1f));
                    Debug.Log("Finishing randomizing your selected material");
                }
            }
        }
    }
    public void resetAllMaterialProperties()
    {
        foreach (Material item in _materials)
        {
            if (item)
            {
                item.SetFloat(_Metallic, 0);
                item.SetFloat(_Glossiness, 0);
                item.SetColor(_Color, Color.white);

                Debug.Log("Reset All materials to NORMAL");
            }
        }
    }
    public void setMaterialColour(RawImage rawImage)
    {
        foreach (Material item in _materials)
        {
            if (_materialName.Contains(item.name.ToString()))
            {
                if (item)
                {
                    item.SetColor(_Color, rawImage.color);
                }
            }
        }
    }
    public void unlockColours(int index)
    {
        if (cash >= 1000)
        {
            if (_lockedColoursB[index])
            {
                _lockedColoursB[index] = false;
                _lockedText[index].enabled = false;

                Debug.Log(index + " This color has been unlocked " + _lockedColours[index].color.ToString());
            }
        }

        foreach (Material item in _materials)
        {
            if (!_lockedColoursB[index])
            {
                if (_materialName.Contains(item.name.ToString()))
                {
                    if (item)
                    {
                        setMaterialColour(_lockedColours[index]);
                    }
                }
            }
        }
    }
}
