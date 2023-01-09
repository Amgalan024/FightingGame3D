using UnityEngine;
using VContainer;

namespace MVC_Pattern.Scripts.Services
{
    public  abstract class BaseServiceBuilder : MonoBehaviour
    {
        public abstract void Build(IContainerBuilder builder);
    }
}