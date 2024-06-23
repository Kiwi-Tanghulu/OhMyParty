using UnityEngine;

namespace OMG.Test
{
    public class TInheritedType : MonoBehaviour
    {
        private void Awake()
        {
            Parent child = new Child();
            Debug.Log(child.GetType().ToString());
        }

        public class Constructor<T>
        {
            static Constructor()
            {
                Debug.Log(typeof(T));
            }
        }

        public class Parent
        {
            
        }

        public class Child : Parent
        {
            public class Constructor : Constructor<Child> 
            {
                static Constructor()
                {
                    
                }
            }
        }
    }
}
