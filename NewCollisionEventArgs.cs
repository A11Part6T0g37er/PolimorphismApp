using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolimorphismApp
{
	// Step #1: Define a type that will hold any additional information that
	// should be sent to receivers of the event notification
	class NewCollisionEventArgs
    {

		private readonly int X, Y;
		public NewCollisionEventArgs(int X, int Y)
		{
			this.X = X;
			this.Y = Y;
		}
		public int X_Collision { get { return this.X; } }
		public int Y_Collision { get { return this.Y; } }

	}
}
