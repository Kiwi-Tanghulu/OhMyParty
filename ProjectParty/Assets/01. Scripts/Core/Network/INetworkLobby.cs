using System.Threading.Tasks;
using UnityEngine;

namespace OMG.Networks
{
    public interface INetworkLobby
    {
        public Task<bool> Join();
        public void Leave();

        public void Open();
        public void Close();
    }
}
