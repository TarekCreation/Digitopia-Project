using UnityEngine;
using UnityEditor;
using System.IO;

public class AudioBakerWindow : EditorWindow
{
    public AudioClip[] clipsToBake;
    public float step = 0.1f;
    public int smoothingWindow = 1;
    public float silenceThreshold = 0.05f;

    [MenuItem("Tools/Audio Baker")]
    static void ShowWindow() => GetWindow<AudioBakerWindow>("Audio Baker");

    void OnGUI()
    {
        SerializedObject so = new SerializedObject(this);
        EditorGUILayout.PropertyField(so.FindProperty("clipsToBake"), true);
        so.ApplyModifiedProperties();

        step = EditorGUILayout.FloatField("Step (s)", step);
        smoothingWindow = EditorGUILayout.IntField("Smoothing window (frames)", smoothingWindow);
        silenceThreshold = EditorGUILayout.Slider("Silence Threshold", silenceThreshold, 0f, 1f);

        if (GUILayout.Button("Bake Selected Clips"))
        {
            BakeAll();
        }
    }

    void BakeAll()
    {
        string outFolder = "Assets/Resources/BakedAudio";
        if (!Directory.Exists(outFolder)) Directory.CreateDirectory(outFolder);

        foreach (var clip in clipsToBake)
        {
            if (clip == null) continue;
            var values = BakeClip(clip, step, smoothingWindow, silenceThreshold);
            var wrapper = new BakedWrapper { values = values, step = step, sampleRate = clip.frequency };

            string outPath = Path.Combine(outFolder, clip.name + "_baked.json");
            File.WriteAllText(outPath, JsonUtility.ToJson(wrapper));
            Debug.Log($"Baked {clip.name} -> {outPath} (frames: {values.Length})");
        }

        AssetDatabase.Refresh();
    }

    float[] BakeClip(AudioClip clip, float stepSeconds, int smoothWindow, float threshold)
    {
        clip.LoadAudioData();
        int channels = clip.channels;
        int sampleRate = clip.frequency;
        int samplesTotal = clip.samples;
        float[] interleaved = new float[samplesTotal * channels];
        clip.GetData(interleaved, 0);

        int samplesPerStep = Mathf.Max(1, Mathf.FloorToInt(stepSeconds * sampleRate));
        int frames = Mathf.CeilToInt((float)samplesTotal / samplesPerStep);
        float[] amplitudes = new float[frames];

        for (int f = 0; f < frames; f++)
        {
            int startFrame = f * samplesPerStep;
            int endFrame = Mathf.Min(samplesTotal, startFrame + samplesPerStep);

            double sumSq = 0.0;
            int count = 0;

            for (int frame = startFrame; frame < endFrame; frame++)
            {
                for (int ch = 0; ch < channels; ch++)
                {
                    float v = interleaved[frame * channels + ch];
                    sumSq += (double)v * (double)v;
                    count++;
                }
            }

            float rms = (count > 0) ? Mathf.Sqrt((float)(sumSq / count)) : 0f;
            amplitudes[f] = rms;
        }

        float max = 0f;
        for (int i = 0; i < amplitudes.Length; i++) if (amplitudes[i] > max) max = amplitudes[i];
        if (max > 0f)
        {
            for (int i = 0; i < amplitudes.Length; i++) amplitudes[i] /= max;
        }

        for (int i = 0; i < amplitudes.Length; i++)
        {
            if (amplitudes[i] < threshold) amplitudes[i] = 0f;
        }

        if (smoothWindow > 1)
        {
            float[] smooth = new float[amplitudes.Length];
            int w = smoothWindow;
            for (int i = 0; i < amplitudes.Length; i++)
            {
                float sum = 0f; int cnt = 0;
                for (int j = -w; j <= w; j++)
                {
                    int idx = i + j;
                    if (idx >= 0 && idx < amplitudes.Length) { sum += amplitudes[idx]; cnt++; }
                }
                smooth[i] = (cnt > 0) ? sum / cnt : amplitudes[i];
            }
            amplitudes = smooth;
        }

        return amplitudes;
    }

    [System.Serializable]
    class BakedWrapper { public float[] values; public float step; public int sampleRate; }
}
