using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioSourceGetSpectrumDataExample : MonoBehaviour
{

    public float[] spectrum;

    public GameObject AmpLow;
    public GameObject AmpLowMid;
    public GameObject AmpMid;
    public GameObject AmpHighMid;
    public GameObject AmpHigh;

    void FixedUpdate()
    {
         spectrum = new float[256];

        float smoothTime = 0.9f;
        float yVelocity = 0.1f;

        AudioListener.GetOutputData(spectrum, 0);//spectrum, 0, FFTWindow.Rectangular);

        //foreach (float f in spectrum)
        //Debug.Log(f);

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);

            
            float newScale = Mathf.SmoothDamp(AmpLow.transform.localScale.y, getMidValue(spectrum, 0, 50), ref yVelocity, smoothTime);
            AmpLow.transform.localScale = new Vector3(0.5f, newScale ,0.5f);

            AmpLowMid.transform.localScale = new Vector3(0.5f, getMidValue(spectrum, 50, 50), 0.5f);
            AmpMid.transform.localScale = new Vector3(0.5f, getMidValue(spectrum, 100, 50), 0.5f);
            AmpHighMid.transform.localScale = new Vector3(0.5f, getMidValue(spectrum, 150,50), 0.5f);
            AmpHigh.transform.localScale = new Vector3(0.5f, getMidValue(spectrum, 200, 50), 0.5f);
        }
    }

    float getMidValue(float[] array, int startNr, int count)
    {
        float sum = 0;

        for (int i = 0; i < count; i++)
            sum += array[i+startNr];

        sum = (sum / (float)count) * 10f;

        return sum;
    }
}