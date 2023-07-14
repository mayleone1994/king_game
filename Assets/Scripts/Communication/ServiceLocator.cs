using System;
using System.Collections.Generic;

namespace KingGame
{
    public class King_ServiceLocator
    {
        private Dictionary<object, IController> _controllers;

        public King_ServiceLocator()
        {
            InitDict();
        }

        public void SetController(object controllerType, IController controller)
        {
            _controllers.Add(controllerType, controller);
        }

        public T GetController<T>() where T : IController
        {
            try
            {
                return (T)_controllers[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException("The controller was not registred");
            }
        }

        private void InitDict()
        {
            _controllers = new();
        }
    }
}