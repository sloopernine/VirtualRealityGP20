using System.Collections.Generic;
using _Content.Scripts.Data.Containers.GlobalSignal;
using Data.Enums;
using Data.Interfaces;
using UnityEngine;

namespace Singletons
{
    [DefaultExecutionOrder(-9)]
    public class GlobalMediator : MonoBehaviour, IReceiveGlobalSignal, ISendGlobalSignal
    {
        public static GlobalMediator INSTANCE;

        private List<IReceiveGlobalSignal> subscribers = new List<IReceiveGlobalSignal>();
        
        public int totalSubscribers;
        
        private void Awake()
        {
            if (INSTANCE == null)
                INSTANCE = this;
            else
                Destroy(gameObject);
        }
        
        public void ReceiveGlobal(GlobalEvent eventState, GlobalSignalBaseData signalData)
        {
            SendGlobal(eventState, signalData);
        }

        public void SendGlobal(GlobalEvent eventState, GlobalSignalBaseData signalData)
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
