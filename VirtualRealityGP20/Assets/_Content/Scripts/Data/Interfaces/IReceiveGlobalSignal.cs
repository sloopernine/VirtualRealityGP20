using System;
using _Content.Scripts.Data.Containers.GlobalSignal;
using Data.Enums;

namespace Data.Interfaces
{
    public interface IReceiveGlobalSignal
    {
        public void ReceiveGlobal(Enum eventState, GlobalSignalBaseData globalSignalData = null);
    }
}