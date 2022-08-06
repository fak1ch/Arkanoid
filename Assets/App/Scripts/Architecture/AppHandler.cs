using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture
{
    public class AppHandler
    {
        private List<CustomBehaviour> _behaviours;

        public AppHandler()
        {
            _behaviours = new List<CustomBehaviour>();
        }

        public void AddBehaviour(CustomBehaviour customBehaviour)
        {
            _behaviours.Add(customBehaviour);
        }

        public void Initialize()  
        {
            for (int i = 0; i < _behaviours.Count; i++)
            {
                _behaviours[i].Initialize();
            }
        }

        public void Tick()
        {
            for (int i = 0; i < _behaviours.Count; i++)
            {
                _behaviours[i].Tick();
            }
        }

        public void FixedTick()
        {
            for (int i = 0; i < _behaviours.Count; i++)
            {
                _behaviours[i].FixedTick();
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < _behaviours.Count; i++)
            {
                _behaviours[i].Dispose();
            }
        }
    }
}
