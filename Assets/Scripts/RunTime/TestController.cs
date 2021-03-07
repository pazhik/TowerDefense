using UnityEngine;

namespace RunTime
{
    public class TestController : IController
    {
        public void OnStart()
        {
            Debug.Log("Start");
        }

        public void OnStop()
        {
            Debug.Log("Stop");
        }

        public void Tick()
        {
            Debug.Log("Tick");
        }
    }
}