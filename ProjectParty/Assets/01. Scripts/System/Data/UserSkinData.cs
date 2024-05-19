namespace OMG.Datas
{
    [System.Serializable]
    public class UserSkinData
    {
        public SkinData CharacterSkin = null;
        public SkinData LobbySkin = null;

        public UserSkinData()
        {
            CharacterSkin = new SkinData();
            LobbySkin = new SkinData();
        }
    }
}
