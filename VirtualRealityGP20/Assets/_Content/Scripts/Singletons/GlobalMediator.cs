using System;
using System.Collections.Generic;
using _Content.Scripts.Data.Containers.GlobalSignal;
using Data.Interfaces;
using UnityEngine;

namespace _Content.Scripts.Singletons
{
    [DefaultExecutionOrder(-9)]
    public class GlobalMediator : MonoBehaviour, IReceiveGlobalSignal, ISendGlobalSignal
    {
        public static GlobalMediator Instance;
        
        private List<IReceiveGlobalSignal> subscribers = new List<IReceiveGlobalSignal>();
        
        public int totalSubscribers;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
        
        public void ReceiveGlobal(Enum eventState, GlobalSignalBaseData globalSignalData = null)
        {
            SendGlobal(eventState, globalSignalData);
        }

        public void SendGlobal(Enum eventState, GlobalSignalBaseData signalData)
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.ReceiveGlobal(eventState, signalData);
            }
        }

        public void Subscribe(IReceiveGlobalSignal subscriber)
        {
            subscribers.Add(subscriber);
            totalSubscribers++;
        }

        public void UnSubscribe(IReceiveGlobalSignal subscriber)
        {
            subscribers.Remove(subscriber);
            totalSubscribers--;
        }
    }
}
