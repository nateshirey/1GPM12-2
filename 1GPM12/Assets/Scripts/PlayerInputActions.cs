//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.1.0
//     from Assets/Scripts/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PhotoControls"",
            ""id"": ""6c824771-bc9e-4285-9e57-dbf6b96306ca"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""fdca925f-43ab-47b0-aea8-282f49563e65"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CaptureImage"",
                    ""type"": ""Button"",
                    ""id"": ""4530174b-6931-4fbe-9c32-b9f05ca1c8f8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""dd175834-938d-4e24-8f6b-ce86fff3a9f7"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AddFriend"",
                    ""type"": ""Button"",
                    ""id"": ""7d2a560b-8400-4dfd-b337-5c14f8116fa9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""wasd"",
                    ""id"": ""2ea33457-ed26-4dd4-bd9a-dfafe2e3af23"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""15d066e2-ad8c-474d-8af4-d60654d1a927"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""82e0df61-833e-42d5-8171-e25eecb107de"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9c28f618-afd9-4b6c-89d2-0c05d69aabab"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5819eb15-7cdc-4b5c-b02e-d368110d6cc3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""a3226225-0f3a-49f2-be24-fd1a3a868080"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""cf425b51-9b9f-40e9-8691-ad97ccbe6508"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""de508362-aa04-47db-b892-557be70a7777"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3055984b-3320-4ce7-9a48-bc7eb68e3d37"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a304e5cd-1442-4a07-9d6c-450d9fd136ec"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bbda3d52-b0e7-4119-a0df-40786c07e91f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e253460-7dcf-4737-a2c0-dfc8b50e3d4a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CaptureImage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""268a59b7-05c6-4623-8c2c-1fab6b953fc7"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CaptureImage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""336b4d7a-470a-4bca-94f8-f649a2edb2f3"",
                    ""path"": ""<Gamepad>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f19ac6f9-aba0-44ff-a31f-aae2543258ae"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f532f164-e9b5-4bb5-a3f1-7b9871e30f87"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a9526a32-58f0-4d33-9263-80b1c337502b"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""651c0c1f-21a6-4423-8842-8c36996065ae"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AddFriend"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PhotoControls
        m_PhotoControls = asset.FindActionMap("PhotoControls", throwIfNotFound: true);
        m_PhotoControls_Look = m_PhotoControls.FindAction("Look", throwIfNotFound: true);
        m_PhotoControls_CaptureImage = m_PhotoControls.FindAction("CaptureImage", throwIfNotFound: true);
        m_PhotoControls_Zoom = m_PhotoControls.FindAction("Zoom", throwIfNotFound: true);
        m_PhotoControls_AddFriend = m_PhotoControls.FindAction("AddFriend", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PhotoControls
    private readonly InputActionMap m_PhotoControls;
    private IPhotoControlsActions m_PhotoControlsActionsCallbackInterface;
    private readonly InputAction m_PhotoControls_Look;
    private readonly InputAction m_PhotoControls_CaptureImage;
    private readonly InputAction m_PhotoControls_Zoom;
    private readonly InputAction m_PhotoControls_AddFriend;
    public struct PhotoControlsActions
    {
        private @PlayerInputActions m_Wrapper;
        public PhotoControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_PhotoControls_Look;
        public InputAction @CaptureImage => m_Wrapper.m_PhotoControls_CaptureImage;
        public InputAction @Zoom => m_Wrapper.m_PhotoControls_Zoom;
        public InputAction @AddFriend => m_Wrapper.m_PhotoControls_AddFriend;
        public InputActionMap Get() { return m_Wrapper.m_PhotoControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PhotoControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPhotoControlsActions instance)
        {
            if (m_Wrapper.m_PhotoControlsActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_PhotoControlsActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PhotoControlsActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PhotoControlsActionsCallbackInterface.OnLook;
                @CaptureImage.started -= m_Wrapper.m_PhotoControlsActionsCallbackInterface.OnCaptureImage;
                @CaptureImage.performed -= m_Wrapper.m_PhotoControlsActionsCallbackInterface.OnCaptureImage;
                @CaptureImage.canceled -= m_Wrapper.m_PhotoControlsActionsCallbackInterface.OnCaptureImage;
                @Zoom.started -= m_Wrapper.m_PhotoControlsActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_PhotoControlsActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_PhotoControlsActionsCallbackInterface.OnZoom;
                @AddFriend.started -= m_Wrapper.m_PhotoControlsActionsCallbackInterface.OnAddFriend;
                @AddFriend.performed -= m_Wrapper.m_PhotoControlsActionsCallbackInterface.OnAddFriend;
                @AddFriend.canceled -= m_Wrapper.m_PhotoControlsActionsCallbackInterface.OnAddFriend;
            }
            m_Wrapper.m_PhotoControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @CaptureImage.started += instance.OnCaptureImage;
                @CaptureImage.performed += instance.OnCaptureImage;
                @CaptureImage.canceled += instance.OnCaptureImage;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @AddFriend.started += instance.OnAddFriend;
                @AddFriend.performed += instance.OnAddFriend;
                @AddFriend.canceled += instance.OnAddFriend;
            }
        }
    }
    public PhotoControlsActions @PhotoControls => new PhotoControlsActions(this);
    public interface IPhotoControlsActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnCaptureImage(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnAddFriend(InputAction.CallbackContext context);
    }
}
