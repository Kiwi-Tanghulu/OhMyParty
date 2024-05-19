namespace OMG.Datas
{
    [System.Serializable]
    public class UserData
    {
        public UserInteriorData InteriorData = null;
        public UserSkinData SkinData = null;

        public void CreateData()
        {
            InteriorData = new UserInteriorData();
            SkinData = new UserSkinData();
        }
    }
}
