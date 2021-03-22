using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PolimorphismApp
{
    internal class CollisionManager
    {
        // Step #2: Define the event member
        public event EventHandler<NewCollisionEventArgs> NewCollision;

        // Step #3: Define a method responsible for raising the event
        // to notify registered objects that the event has occurred
        protected virtual void OnNewCollision(NewCollisionEventArgs e)
        {
            EventHandler<NewCollisionEventArgs> temp = Volatile.Read(ref NewCollision);
            if (temp != null)
            {
                temp(sender: this, e);
            }
        }

        // Step #4: Define a method that translates the
        // input into the desired event
        public void SimulateCollision(int X, int Y)
        {
            NewCollisionEventArgs e = new NewCollisionEventArgs(X, Y);
            OnNewCollision(e);
        }
    }
}
