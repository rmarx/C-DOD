#define CALCULATE_AVERAGE

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;



public class LugusTestBase 
{
	[Test]
	public virtual void Expectation()
	{
		double lowestNumber, highestNumber;
		
		SingleRun( out lowestNumber, out highestNumber );
		TestExpectation( lowestNumber, highestNumber );
	}

#if CALCULATE_AVERAGE
	[Test]
	public virtual void ExpectationAverage()
	{
		ExpectationAverageImplementation();
	}
#endif 

	public virtual void ExpectationAverageImplementation()
	{
		int n = 100;
		
		double lowestTotal = 0.0;
		double highestTotal = 0.0;
		
		double lowest = 0.0;
		double highest = 0.0;
		
		for ( int i = 0; i < n; ++i )
		{
			SingleRun( out lowest, out highest );
			
			lowestTotal += lowest;
			highestTotal += highest;
		}
		
		TestExpectation( lowestTotal / (double) n, highestTotal / (double) n );
	}

	public virtual void SingleRun(out double lowestNumber, out double highestNumber)
	{
		lowestNumber = 0.0f;
		highestNumber = .0f;
	}

	public void TestExpectation(double lowestNumber, double higherNumber, string message = "")
	{
		if( higherNumber > lowestNumber )
			Assert.Inconclusive ( message + " " + higherNumber + " > " + lowestNumber );
		else
			Assert.Fail(  message + " " + higherNumber + " < " + lowestNumber );
	}
}
