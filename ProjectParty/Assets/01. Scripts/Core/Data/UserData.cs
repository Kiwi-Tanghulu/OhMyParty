namespace OMG.Datas
{
    [System.Serializable]
    public class UserData
    {
        public UserInteriorData InteriorData = null;

        public void CreateData()
        {
            InteriorData = new UserInteriorData();
        }
    }
}
