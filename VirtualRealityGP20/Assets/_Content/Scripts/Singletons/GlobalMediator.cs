using System;
using _Content.Scripts.Data.Containers.GlobalSignal;
using Data.Interfaces;
using UnityEngine;

namespace _Content.Scripts.Singletons
{
    [DefaultExecutionOrder(-9)]
    public class GlobalMediator : MonoBehaviour, IReceiveGlobalSignal, ISendGlobalSignal
    {
        public void ReceiveGlobal(Enum eventState, GlobalSignalBaseData globalSignalData = null)
        {
            throw new NotImplementedException();
        }

        public void SendGlobal(Enum eventState, GlobalSignalBaseData globalSignalData = null)
        {
            throw new NotImplementedException();
        }
    }
}
