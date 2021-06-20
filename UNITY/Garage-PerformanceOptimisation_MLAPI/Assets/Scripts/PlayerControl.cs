// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerActionControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActionControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""Vehicle"",
            ""id"": ""253078ff-8606-498c-bd2a-7cea393e0c96"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""120376b7-9a30-4132-a8ca-b0bac1892c2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Wheelie"",
                    ""type"": ""Button"",
                    ""id"": ""f40032fd-b209-456e-8f7a-394870a7ce76"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Draw"",
                    ""type"": ""Button"",
                    ""id"": ""ca58bb4f-a498-40b2-8980-f8cf5adc49ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Strike"",
                    ""type"": ""Button"",
                    ""id"": ""3da61253-622b-4d9a-b8cb-a8293f1bd441"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""keyboard"",
                    ""id"": ""f755cc00-b282-4aad-a70f-0f34f5011bad"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e8c329a9-b44b-4e65-95fc-05f062cbd4a9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""912a489d-fe24-4617-9b78-8796f89e135a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""eb1d8b67-1bd6-4541-a815-6a4084a314b6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d577a41f-b257-480b-8b84-e3975d5b8b10"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8c14bba8-4ec4-4b99-9537-483dc25d31b8"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Wheelie"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d6e05ecb-936c-4083-ac5b-8bef9ef874f4"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Draw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb735371-d9a9-449d-b626-c1d0ca761ac4"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strike"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Vehicle
        m_Vehicle = asset.FindActionMap("Vehicle", throwIfNotFound: true);
        m_Vehicle_Move = m_Vehicle.FindAction("Move", throwIfNotFound: true);
        m_Vehicle_Wheelie = m_Vehicle.FindAction("Wheelie", throwIfNotFound: true);
        m_Vehicle_Draw = m_Vehicle.FindAction("Draw", throwIfNotFound: true);
        m_Vehicle_Strike = m_Vehicle.FindAction("Strike", throwIfNotFound: true);
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

    // Vehicle
    private readonly InputActionMap m_Vehicle;
    private IVehicleActions m_VehicleActionsCallbackInterface;
    private readonly InputAction m_Vehicle_Move;
    private readonly InputAction m_Vehicle_Wheelie;
    private readonly InputAction m_Vehicle_Draw;
    private readonly InputAction m_Vehicle_Strike;
    public struct VehicleActions
    {
        private @PlayerActionControls m_Wrapper;
        public VehicleActions(@PlayerActionControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Vehicle_Move;
        public InputAction @Wheelie => m_Wrapper.m_Vehicle_Wheelie;
        public InputAction @Draw => m_Wrapper.m_Vehicle_Draw;
        public InputAction @Strike => m_Wrapper.m_Vehicle_Strike;
        public InputActionMap Get() { return m_Wrapper.m_Vehicle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(VehicleActions set) { return set.Get(); }
        public void SetCallbacks(IVehicleActions instance)
        {
            if (m_Wrapper.m_VehicleActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_VehicleActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_VehicleActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_VehicleActionsCallbackInterface.OnMove;
                @Wheelie.started -= m_Wrapper.m_VehicleActionsCallbackInterface.OnWheelie;
                @Wheelie.performed -= m_Wrapper.m_VehicleActionsCallbackInterface.OnWheelie;
                @Wheelie.canceled -= m_Wrapper.m_VehicleActionsCallbackInterface.OnWheelie;
                @Draw.started -= m_Wrapper.m_VehicleActionsCallbackInterface.OnDraw;
                @Draw.performed -= m_Wrapper.m_VehicleActionsCallbackInterface.OnDraw;
                @Draw.canceled -= m_Wrapper.m_VehicleActionsCallbackInterface.OnDraw;
                @Strike.started -= m_Wrapper.m_VehicleActionsCallbackInterface.OnStrike;
                @Strike.performed -= m_Wrapper.m_VehicleActionsCallbackInterface.OnStrike;
                @Strike.canceled -= m_Wrapper.m_VehicleActionsCallbackInterface.OnStrike;
            }
            m_Wrapper.m_VehicleActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Wheelie.started += instance.OnWheelie;
                @Wheelie.performed += instance.OnWheelie;
                @Wheelie.canceled += instance.OnWheelie;
                @Draw.started += instance.OnDraw;
                @Draw.performed += instance.OnDraw;
                @Draw.canceled += instance.OnDraw;
                @Strike.started += instance.OnStrike;
                @Strike.performed += instance.OnStrike;
                @Strike.canceled += instance.OnStrike;
            }
        }
    }
    public VehicleActions @Vehicle => new VehicleActions(this);
    public interface IVehicleActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnWheelie(InputAction.CallbackContext context);
        void OnDraw(InputAction.CallbackContext context);
        void OnStrike(InputAction.CallbackContext context);
    }
}
