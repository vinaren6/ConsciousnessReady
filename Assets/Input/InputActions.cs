// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""60d409c3-7e20-487e-b005-ce12853134ad"",
            ""actions"": [
                {
                    ""name"": ""Movement X"",
                    ""type"": ""Value"",
                    ""id"": ""fb26beb1-de7b-4fff-b501-a8034c9a22a4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement Y"",
                    ""type"": ""Value"",
                    ""id"": ""ec5167fb-459b-4707-b425-b306198b0e9d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation X"",
                    ""type"": ""Value"",
                    ""id"": ""75cf06d7-a877-4bc1-974d-12b17efe0693"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation Y"",
                    ""type"": ""Value"",
                    ""id"": ""cf92b2f9-4b71-40f8-9510-32c4a9f1a139"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse Position X"",
                    ""type"": ""Value"",
                    ""id"": ""041254f6-32f4-4097-a243-e0a8407f3b11"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse Position Y"",
                    ""type"": ""Value"",
                    ""id"": ""1f4f8d8b-8864-4be8-928c-edf4952a665a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""974e511d-4188-46f0-8191-79fa423c12b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press,SlowTap(duration=0.5)""
                },
                {
                    ""name"": ""Boost"",
                    ""type"": ""Button"",
                    ""id"": ""52bd2e66-c08a-4e72-989e-ff66c8e0c788"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slow"",
                    ""type"": ""Button"",
                    ""id"": ""81e38f05-39b5-4f74-a705-3297e9d0b386"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hook"",
                    ""type"": ""Button"",
                    ""id"": ""a11ed6a6-3faa-48d9-8a03-f191647f9b8e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AnyButton"",
                    ""type"": ""Button"",
                    ""id"": ""39f5d985-a437-4302-9837-422c8a0659ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShowIngameOptions"",
                    ""type"": ""Button"",
                    ""id"": ""ae36829f-0ae8-4eba-8af4-6ef84121558e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b22517a6-215f-467e-9661-5af7d06f86b9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cae309fe-ef5e-454c-827e-b706c7c9d012"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7267470a-125a-4279-b299-2bb7a8bdcd2c"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9773fae4-e73c-4e10-9740-7c8476d774d3"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowIngameOptions"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a746d17d-c972-4459-8460-8ac83bc2e96f"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowIngameOptions"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3abbbaf-b426-4ae7-bd23-4c96b7216b16"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""536ea0a9-ad05-4cbb-98e0-22311551395d"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58147e39-3532-46b8-aaff-8d2ff5945f8b"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ff4418a-e36f-418a-9d17-a13cfd0cf7b5"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80faef2e-167f-41c2-8b50-b106075d745f"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6098a6d7-ab0c-4ae1-9b5e-4970f48b7271"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2484dbad-1d99-4e05-a7c0-36ed9e92a842"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33f6e259-557a-4ee2-8fad-a81406150014"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c953c228-b26c-4e1e-9c8c-ba4d825a1337"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b5b846e-3d95-4a0d-b2ca-cc22cbab8053"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ca95ae6-d2d2-4d6e-93da-c87eaf72f773"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97500621-1645-4546-b4d2-27e2d3f323f7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""036e2b08-bf4c-4558-a061-15e0cd55f164"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""52a67553-ff08-4f0e-9851-32208b7f3d18"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement X"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""634f6a4e-974d-4ea8-9c73-ad2163d94b72"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d075a017-28ac-43d9-ba1f-699b278cbd32"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""e74366ed-7c34-4d4e-955e-e28e478dd575"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement X"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0c3f7bad-79ed-4075-89ff-c4711c94e4f1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5d63d80e-9e01-4c11-ae16-a297dff96cd4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""9eb5ba7a-cd23-4484-9344-4f77a1dcca5b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Y"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0392f8ee-67e9-429a-84cb-1b292700c480"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""560139ea-a473-4823-a949-d5705679a7be"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""a630d210-2834-4731-92ad-0c4e0bdcfb1c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Y"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2203c7cc-aa41-425d-9f0d-be58a7e31cff"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0f732417-2ed4-4719-b85b-117087636221"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""28b59406-ba8b-4cb3-a204-ea65104f53e1"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation X"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""40b0b0df-6e2a-4b0f-8063-3b4150d10e9d"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ea19995b-d5b4-4031-a64c-354ba5d58f27"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""1c5bf670-6986-40b7-ab4a-1f1216a4b1e2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation Y"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""29be7ab0-5afe-4f99-aa21-8b6f8c078453"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""069ff332-6551-4d89-8963-8e787d1fd3c4"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""34a9751a-83f9-4979-a680-1dda2f07be20"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0f7ae0c-4084-448d-a0b8-b5a597da0ba6"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c66e5da-d542-41c4-aa03-fe075f5ef55c"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83e0017a-7dfd-4701-b803-5c90b19dbc4c"",
                    ""path"": ""<Mouse>/position/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Position Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4db00fac-b3bc-4381-8833-caa8f0045c9d"",
                    ""path"": ""<Mouse>/position/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Position X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_MovementX = m_Player.FindAction("Movement X", throwIfNotFound: true);
        m_Player_MovementY = m_Player.FindAction("Movement Y", throwIfNotFound: true);
        m_Player_RotationX = m_Player.FindAction("Rotation X", throwIfNotFound: true);
        m_Player_RotationY = m_Player.FindAction("Rotation Y", throwIfNotFound: true);
        m_Player_MousePositionX = m_Player.FindAction("Mouse Position X", throwIfNotFound: true);
        m_Player_MousePositionY = m_Player.FindAction("Mouse Position Y", throwIfNotFound: true);
        m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
        m_Player_Boost = m_Player.FindAction("Boost", throwIfNotFound: true);
        m_Player_Slow = m_Player.FindAction("Slow", throwIfNotFound: true);
        m_Player_Hook = m_Player.FindAction("Hook", throwIfNotFound: true);
        m_Player_AnyButton = m_Player.FindAction("AnyButton", throwIfNotFound: true);
        m_Player_ShowIngameOptions = m_Player.FindAction("ShowIngameOptions", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_MovementX;
    private readonly InputAction m_Player_MovementY;
    private readonly InputAction m_Player_RotationX;
    private readonly InputAction m_Player_RotationY;
    private readonly InputAction m_Player_MousePositionX;
    private readonly InputAction m_Player_MousePositionY;
    private readonly InputAction m_Player_Shoot;
    private readonly InputAction m_Player_Boost;
    private readonly InputAction m_Player_Slow;
    private readonly InputAction m_Player_Hook;
    private readonly InputAction m_Player_AnyButton;
    private readonly InputAction m_Player_ShowIngameOptions;
    public struct PlayerActions
    {
        private @InputActions m_Wrapper;
        public PlayerActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovementX => m_Wrapper.m_Player_MovementX;
        public InputAction @MovementY => m_Wrapper.m_Player_MovementY;
        public InputAction @RotationX => m_Wrapper.m_Player_RotationX;
        public InputAction @RotationY => m_Wrapper.m_Player_RotationY;
        public InputAction @MousePositionX => m_Wrapper.m_Player_MousePositionX;
        public InputAction @MousePositionY => m_Wrapper.m_Player_MousePositionY;
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputAction @Boost => m_Wrapper.m_Player_Boost;
        public InputAction @Slow => m_Wrapper.m_Player_Slow;
        public InputAction @Hook => m_Wrapper.m_Player_Hook;
        public InputAction @AnyButton => m_Wrapper.m_Player_AnyButton;
        public InputAction @ShowIngameOptions => m_Wrapper.m_Player_ShowIngameOptions;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @MovementX.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementX;
                @MovementX.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementX;
                @MovementX.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementX;
                @MovementY.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementY;
                @MovementY.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementY;
                @MovementY.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementY;
                @RotationX.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotationX;
                @RotationX.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotationX;
                @RotationX.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotationX;
                @RotationY.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotationY;
                @RotationY.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotationY;
                @RotationY.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotationY;
                @MousePositionX.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePositionX;
                @MousePositionX.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePositionX;
                @MousePositionX.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePositionX;
                @MousePositionY.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePositionY;
                @MousePositionY.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePositionY;
                @MousePositionY.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePositionY;
                @Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Boost.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBoost;
                @Boost.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBoost;
                @Boost.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBoost;
                @Slow.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlow;
                @Slow.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlow;
                @Slow.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlow;
                @Hook.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHook;
                @Hook.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHook;
                @Hook.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHook;
                @AnyButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAnyButton;
                @AnyButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAnyButton;
                @AnyButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAnyButton;
                @ShowIngameOptions.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShowIngameOptions;
                @ShowIngameOptions.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShowIngameOptions;
                @ShowIngameOptions.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShowIngameOptions;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MovementX.started += instance.OnMovementX;
                @MovementX.performed += instance.OnMovementX;
                @MovementX.canceled += instance.OnMovementX;
                @MovementY.started += instance.OnMovementY;
                @MovementY.performed += instance.OnMovementY;
                @MovementY.canceled += instance.OnMovementY;
                @RotationX.started += instance.OnRotationX;
                @RotationX.performed += instance.OnRotationX;
                @RotationX.canceled += instance.OnRotationX;
                @RotationY.started += instance.OnRotationY;
                @RotationY.performed += instance.OnRotationY;
                @RotationY.canceled += instance.OnRotationY;
                @MousePositionX.started += instance.OnMousePositionX;
                @MousePositionX.performed += instance.OnMousePositionX;
                @MousePositionX.canceled += instance.OnMousePositionX;
                @MousePositionY.started += instance.OnMousePositionY;
                @MousePositionY.performed += instance.OnMousePositionY;
                @MousePositionY.canceled += instance.OnMousePositionY;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Boost.started += instance.OnBoost;
                @Boost.performed += instance.OnBoost;
                @Boost.canceled += instance.OnBoost;
                @Slow.started += instance.OnSlow;
                @Slow.performed += instance.OnSlow;
                @Slow.canceled += instance.OnSlow;
                @Hook.started += instance.OnHook;
                @Hook.performed += instance.OnHook;
                @Hook.canceled += instance.OnHook;
                @AnyButton.started += instance.OnAnyButton;
                @AnyButton.performed += instance.OnAnyButton;
                @AnyButton.canceled += instance.OnAnyButton;
                @ShowIngameOptions.started += instance.OnShowIngameOptions;
                @ShowIngameOptions.performed += instance.OnShowIngameOptions;
                @ShowIngameOptions.canceled += instance.OnShowIngameOptions;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovementX(InputAction.CallbackContext context);
        void OnMovementY(InputAction.CallbackContext context);
        void OnRotationX(InputAction.CallbackContext context);
        void OnRotationY(InputAction.CallbackContext context);
        void OnMousePositionX(InputAction.CallbackContext context);
        void OnMousePositionY(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnBoost(InputAction.CallbackContext context);
        void OnSlow(InputAction.CallbackContext context);
        void OnHook(InputAction.CallbackContext context);
        void OnAnyButton(InputAction.CallbackContext context);
        void OnShowIngameOptions(InputAction.CallbackContext context);
    }
}
