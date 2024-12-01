using System;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneInitializer : MonoBehaviour, IGameInitializer
{
    private readonly Dictionary<Type, object> _registeredTypes = new();

    [SerializeField] private PopupsManager _popupsManager;
    
    private void Awake()
    {
        RegisterTypes();
        Initialize();
    }

    public T Resolve<T>()
    {
        return (T)_registeredTypes[typeof(T)];
    }
        
    public virtual void RegisterInstance<T>(T instance)
    {
        _registeredTypes.Add(typeof(T), instance);
    }
        
    private void RegisterTypes()
    {
        RegisterInstance(_popupsManager);
    }

    private void Initialize()
    {
        _popupsManager.Init();
    }
}
