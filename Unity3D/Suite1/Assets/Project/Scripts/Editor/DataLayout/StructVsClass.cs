using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;


using System; 

namespace DataLayout
{
	/*
		Test whether an array of structs is faster than an array of classes.

        Expectation: classes are much slower, since they can be in random locations in the memory
        Test results: classes are only very marginally slower (49s class vs 45s struct)
        Conclusion: this is weird... further investigation is needed TODO

		See Also:
		http://nprogramming.wordpress.com/2010/06/01/c-performance-class-vs-struct/

 	*/
	[TestFixture]
	public class StructVsClass : LugusTestBase
	{
		/*
		public override void ExpectationAverageImplementation()
		{
			return;
		}
		*/
		
		
		
		public CustomClass[] classArray = null;
		public CustomStruct[] structArray = null;
		
		public void PopulateArrays()
		{
			if( classArray != null )
				return;

			// extra array to try and aleviate the memory allocations
			// if not, the allocator might still just allocate the new CustomClass() instances after eachother in memory
			//CustomClass[] array2 = new CustomClass[1000];

			classArray = new CustomClass[1000];
			for( int i = 0; i < 1000; ++i )
			{
				classArray[i] = new CustomClass();
				//array2[i] = new CustomClass();
			}
			
			structArray = new CustomStruct[1000];
			for( int i = 0; i < 1000; ++i )
			{
				structArray[i] = new CustomStruct();
			}
		}
		
		public override void SingleRun(out double lowestNumber, out double highestNumber)
		{
			PopulateArrays();
			
			
			int count = 10000;//0000;
			
			LugusStopwatch.Start();
			
			zStruct(count);
			
			lowestNumber = LugusStopwatch.Stop();
			
			
			LugusStopwatch.Start();
			
			zClass(count);
			
			highestNumber = LugusStopwatch.Stop();
		}
		
		
		[Test]
		public void zClass([Values (10000)] int amount)
		{
			PopulateArrays();
			
			for( int i = 0; i < amount; ++i )
			{
				for( int j = 0; j < classArray.Length; ++j )
				{
					float x = classArray[j].x; 
					x = x * x;
				}
			}
		}
		
		[Test]
		public void zStruct([Values (10000)] int amount)
		{
			PopulateArrays();
			
			for( int i = 0; i < amount; ++i )
			{
				for( int j = 0; j < structArray.Length; ++j )
				{
					float x = structArray[j].x;
					x = x * x;
				}
			}
		}
	}

	public class CustomClass
	{
		public float x = 0.0f;
		public float y = 0.0f;
		public float z = 0.0f;
	}
	
	
	public struct CustomStruct
	{
		public float x;
		public float y;
		public float z;
	}
}
