using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;


using System; 

namespace SimpleTests
{
	/*
		Test if for is faster than foreach in going through an array

        Expectation: for is faster, as it skips the IEnumerable (or similar) interface function calls
        Test results: for is about 20% faster (for = 50, foreach = 60)
 	*/
	[TestFixture]
	public class Foreach : LugusTestBase
	{

		public override void ExpectationAverageImplementation()
		{
			return;
		}

		
		public override void SingleRun(out double lowestNumber, out double highestNumber)
		{
			int count = 10000000;
			
			LugusStopwatch.Start();
			
			For(count);
			
			lowestNumber = LugusStopwatch.Stop();
			
			
			LugusStopwatch.Start();
			
			Foreach_(count);
			
			highestNumber = LugusStopwatch.Stop();
		}
		
		
		[Test]
		public void For([Values (10000000)] int amount)
		{
			float[] arr = new float[amount];

			float count = 0;
			for( int i = 0; i < arr.Length; ++i )
			{
				count += arr[i];
			}
		}
		
		[Test]
		public void Foreach_([Values (10000000)] int amount)
		{
			float[] arr = new float[amount];
			
			float count = 0;
			foreach( float val in arr )
			{
				count += val;
			}
		}
	}
}
