using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlurControl : MonoBehaviour
{
    private PostProcessVolume _postProcessVolume;
    private DepthOfField _df;
    private ColorGrading _cg;
    public PlayerStats playerStats;

    private void Start()
    {
        _postProcessVolume = GetComponent<PostProcessVolume>();
        _postProcessVolume.profile.TryGetSettings<DepthOfField>(out _df);
        _postProcessVolume.profile.TryGetSettings<ColorGrading>(out _cg);
        _df.focalLength.value = 3f;
    }

    private void FixedUpdate()
    {
        //_df.focalLength.value = Mathf.PingPong(Time.time * 2, 300);
        _df.focalLength.value = playerStats.eyeHealth;
        _cg.temperature.value = playerStats.breathHealth;
    }

    public static int map(int value, int from1, int to1, int from2, int to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
