using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class CameraTransformRecorder : MonoBehaviour
{
    public Camera targetCamera;
    public float captureInterval = 0.1f;
    public string apiUrl = "http://127.0.0.1:8000/analyze/camera";

    private bool isRecording;
    private float timer;

    private List<CameraTransformSample> samples = new();

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.iKey.wasPressedThisFrame)
            StartRecording();

        if (Keyboard.current.oKey.wasPressedThisFrame)
            StopAndSend();

        if (!isRecording)
            return;

        timer += Time.deltaTime;

        if (timer >= captureInterval)
        {
            timer = 0f;
            CaptureTransform();
        }
    }

    void StartRecording()
    {
        samples.Clear();
        isRecording = true;
        timer = 0f;

        Debug.Log("Gravação iniciada");
    }

    void CaptureTransform()
    {
        Transform t = targetCamera.transform;

        samples.Add(new CameraTransformSample
        {
            px = t.position.x,
            py = t.position.y,
            pz = t.position.z,
            rx = t.rotation.x,
            ry = t.rotation.y,
            rz = t.rotation.z,
            rw = t.rotation.w,
            time = Time.time
        });
    }

    void StopAndSend()
    {
        isRecording = false;

        CameraTransformData data = new()
        {
            samples = samples
        };

        string json = JsonUtility.ToJson(data, true);
        StartCoroutine(SendJson(json));
    }

    IEnumerator SendJson(string json)
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Erro ao enviar JSON: {request.error}");
        }
        else
        {
            Debug.Log("JSON enviado com sucesso!");
            Debug.Log($"Resposta do servidor: {request.downloadHandler.text}");
        }
    }
}

[System.Serializable]
public class CameraTransformSample
{
    public float px, py, pz;
    public float rx, ry, rz, rw;
    public float time;
}

[System.Serializable]
public class CameraTransformData
{
    public List<CameraTransformSample> samples;
}
