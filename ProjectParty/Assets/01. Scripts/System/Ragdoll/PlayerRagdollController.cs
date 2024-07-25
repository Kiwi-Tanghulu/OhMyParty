using OMG.NetworkEvents;
using OMG.Ragdoll;

namespace OMG.Player
{
    public class PlayerRagdollController : RagdollController
    {
        public override void Init(CharacterController controller)
        {
            //onInitActive = true;

            base.Init(controller);

            //SetActive(false);
        }

        protected override void SetActive(BoolParams value)
        {
            base.SetActive(value);

            //if (value)
            //{
                

            //    OnActiveEvent?.Invoke();
            //}
            //else
            //{
            //    OnDeactiveEvent?.Invoke();
            //}

            //active = value;
        }
    }
}
