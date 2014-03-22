using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;


using System; 

namespace SimpleTests
{
	/*
		Test whether it's faster to but a bool-test before calling a function,
		or directly inside of the function and return; if the test fails.

        Expectation: outside the function is faster, as it prevents the context switch
        Test results: outside is 2x faster than inside (outside = 35, inside = 70)
 	*/
	[TestFixture]
	public class BoolTestLocation : LugusTestBase
	{
		public override void ExpectationAverageImplementation()
		{
			return;
		}

		public override void SingleRun(out double lowestNumber, out double highestNumber)
		{
			int count = 10000000;
			
			LugusStopwatch.Start();
			
			BoolLocationOutside(count);
			
			lowestNumber = LugusStopwatch.Stop();
			
			
			LugusStopwatch.Start();
			
			BoolLocationInside(count);
			
			highestNumber = LugusStopwatch.Stop();
		}


		public void EmptyFunction()
		{

		}
		
		public bool val = false;

		public void BoolFunction()
		{
			if( !val )
				return; 
		}


		[Test]
		public void BoolLocationOutside([Values (10000000)] int amount)
		{
			for( int i = 0; i < amount; ++i )
			{
				if( val )
				{
					EmptyFunction();
				}
			}
		}

		[Test]
		public void BoolLocationInside([Values (10000000)] int amount)
		{
			for( int i = 0; i < amount; ++i )
			{
				BoolFunction();
			}
		}
	}
}
