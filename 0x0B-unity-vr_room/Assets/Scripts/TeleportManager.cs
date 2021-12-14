using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private TeleportationProvider provider;
    InputAction _teleportActivate;
    bool _isActive;

    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.enabled = false;

        _teleportActivate = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        _teleportActivate.performed += OnTeleportActivate;
        _teleportActivate.canceled += OnTeleportRelease;

        _teleportActivate.Enable();
    }

    void OnTeleportActivate(InputAction.CallbackContext contex)
	{
        rayInteractor.enabled = true;
	}

    void OnTeleportRelease(InputAction.CallbackContext contex)
	{
        rayInteractor.TryGetHitInfo(out Vector3 ReticlePos, out Vector3 _, out var _, out var isValidTarget);
        if (isValidTarget)
        {
            TeleportRequest request = new TeleportRequest()
            {
                destinationPosition = ReticlePos,
            };

            provider.QueueTeleportRequest(request);
        }
        rayInteractor.enabled = false;
    }
}
