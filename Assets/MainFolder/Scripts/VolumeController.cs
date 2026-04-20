using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _bgmSlider; // インスペクターでBGMスライダーを登録
    [SerializeField] private Slider _seSlider;  // インスペクターでSEスライダーを登録

    private const string BGM_PARAM = "BGMVolume";
    private const string SE_PARAM = "SEVolume";

    void Start()
    {
        //保存されている値を読み込む（なければデフォルトの1.0f）
        float bgmVol = PlayerPrefs.GetFloat("BGM_SAVE_KEY", 1.0f);
        float seVol = PlayerPrefs.GetFloat("SE_SAVE_KEY", 1.0f);

        //スライダーの見た目に反映させる（これでバーの位置が戻るのを防ぐ）
        if (_bgmSlider != null)
        {
            _bgmSlider.value = bgmVol;
        }

        if (_seSlider != null)
        {
            _seSlider.value = seVol;
        }

        //実際の音量に反映させる
        SetBGMVolume(bgmVol);
        SetSEVolume(seVol);
    }

    public void SetBGMVolume(float sliderValue)
    {
        float dB = ConvertToDecibel(sliderValue);
        _audioMixer.SetFloat(BGM_PARAM, dB);

        //値を保存する
        PlayerPrefs.SetFloat("BGM_SAVE_KEY", sliderValue);
    }

    public void SetSEVolume(float sliderValue)
    {
        float dB = ConvertToDecibel(sliderValue);
        _audioMixer.SetFloat(SE_PARAM, dB);

        // 値を保存する
        PlayerPrefs.SetFloat("SE_SAVE_KEY", sliderValue);
    }

    private float ConvertToDecibel(float value)
    {
        return Mathf.Log10(Mathf.Max(0.0001f, value)) * 20f;
    }
}