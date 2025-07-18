using UnityEngine;

public class PanelPositioner : MonoBehaviour
{
    [SerializeField] private float _distanceFromCamera = 0.8f;
    [SerializeField] private Vector3 _displayOffset = new Vector3(0, 0, 0);
    [SerializeField] private bool _faceCamera = true;
    private Quaternion _initialRotation;


    private Camera _mainCamera;

    void Awake()
    {
        _mainCamera = Camera.main;

        if (_mainCamera == null)
        {
            Debug.LogError("PanelPositioner: Keine Hauptkamera mit dem Tag 'MainCamera' gefunden! Panel kann nicht korrekt positioniert werden.", this);
        }

        _initialRotation = transform.rotation;
    }

// public void PositionPanelInFrontOfCamera()
// {
//     if (_mainCamera == null)
//     {
//         Debug.LogError("PanelPositioner: _mainCamera ist null. Panel kann nicht positioniert werden.", this);
//         return;
//     }

//     // 1) Position berechnen
//     Vector3 newPosition = _mainCamera.transform.position 
//                         + _mainCamera.transform.forward * _distanceFromCamera
//                         + _mainCamera.transform.right * _displayOffset.x
//                         + _mainCamera.transform.up    * _displayOffset.y
//                         + _mainCamera.transform.forward * _displayOffset.z;

//     transform.position = newPosition;

//     // 2) Rotation zurücksetzen auf die gespeicherte Ausgangs-Rotation...
//     transform.rotation = _initialRotation;

//     // 3) ...und bei Bedarf neu ausrichten, sodass das Panel zur Kamera zeigt
//     if (_faceCamera)
//     {
//         // Variante A: LookAt + korrekte Up-Achse
//         transform.LookAt(_mainCamera.transform.position, Vector3.up);
//         // Optional: Quaternion.LookRotation sorgt nochmal für exakt gerade Ausrichtung
//         transform.rotation = Quaternion.LookRotation(
//             transform.position - _mainCamera.transform.position,
//             Vector3.up
//         );
//     }

//     Debug.Log($"PanelPositioner: '{gameObject.name}' wurde vor der Kamera positioniert.");
// }

    public void PositionPanelInFrontOfCamera()
    {
        if (_mainCamera == null)
        {
            Debug.LogError("PanelPositioner: _mainCamera ist null. Panel kann nicht positioniert werden.", this);
            return;
        }

        Vector3 newPosition = _mainCamera.transform.position + _mainCamera.transform.forward * _distanceFromCamera;

        newPosition += _mainCamera.transform.right * _displayOffset.x;
        newPosition += _mainCamera.transform.up * _displayOffset.y;
        newPosition += _mainCamera.transform.forward * _displayOffset.z;

        transform.position = newPosition;

        if (_faceCamera)
        {
            transform.LookAt(_mainCamera.transform.position);
            transform.rotation = Quaternion.LookRotation(transform.position - _mainCamera.transform.position, Vector3.up);
        }

        Debug.Log($"PanelPositioner: '{gameObject.name}' wurde vor der Kamera positioniert.");
    }
}