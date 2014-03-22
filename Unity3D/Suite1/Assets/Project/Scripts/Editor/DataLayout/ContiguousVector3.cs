using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;


using System; 

namespace DataLayout
{
	/*
		Test whether an array of Unity3D's Vector3 (which is a struct/value type) has a difference in performance
        if we assign new values with the Vector3 constructor to the entries or not.

        Expectation: performance should be the same if new Vector3() doesn't acutally perform random access allocations
        Test results: random access is marginally slower than contiguous (37 vs 34 seconds)
        Conclusion: struct does seem to keep memory layout contiguous, but still a slight performance overhead that I cannot explain here

		See also StructVsClass test
 	*/
	[TestFixture]
	public class ContiguousVector3 : LugusTestBase
	{

		public override void ExpectationAverageImplementation()
		{
			return;
		}



		public Vector3[] contiguous = null;
		public Vector3[] randomAccess = null;

		public void PopulateArrays()
		{
			if( contiguous != null )
				return;

			contiguous = new Vector3[1000];

			randomAccess = new Vector3[1000];
			for( int i = 0; i < 1000; ++i )
			{
				randomAccess[i] = new Vector3();
			}
		}
		
		public override void SingleRun(out double lowestNumber, out double highestNumber)
		{
			PopulateArrays();


			int count = 10000;//0000;
			
			LugusStopwatch.Start();
			
			zContiguous(count);
			
			lowestNumber = LugusStopwatch.Stop();
			
			
			LugusStopwatch.Start();
			
			zRandomAccess(count);
			
			highestNumber = LugusStopwatch.Stop();
		}
		
		
		[Test]
		public void zContiguous([Values (10000)] int amount)
		{
			PopulateArrays();

			for( int i = 0; i < amount; ++i )
			{
				for( int j = 0; j < contiguous.Length; ++j )
				{
					float x = contiguous[j].x; 
				}
			}
		}
		
		[Test]
		public void zRandomAccess([Values (10000)] int amount)
		{
			PopulateArrays();

			for( int i = 0; i < amount; ++i )
			{
				for( int j = 0; j < randomAccess.Length; ++j )
				{
					float x = randomAccess[j].x;
				}
			}
		}
	}
}
