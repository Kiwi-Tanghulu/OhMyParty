using System.Threading.Tasks;
using UnityEngine;

namespace OMG.Networks
{
    public interface INetworkLobby
    {
        public int MemberCount { get; }

        public Task<bool> Join();
        public void Leave();

        public void Open();
        public void Close();

        public void SetData(string key, string value);
        public string GetData(string key);
    }
}
