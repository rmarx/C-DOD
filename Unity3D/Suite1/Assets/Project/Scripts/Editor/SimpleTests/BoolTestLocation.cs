using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

/*
 * 
 * perf test:
- if( !bool ) function(){…} (if-test in caller) (this one should be much faster, since no context-switch)
vs
- function(){ if(!bool) return; … } (if-test in callee)
 */
using System; 

namespace SimpleTests
{
	[TestFixture]
	public class BoolTestLocation : MonoBehaviour 
	{
		public bool val = false;

		[Test]
		public void Expectation()
		{
			int count = 10000000;
			System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();


			BoolLocationOutside(count);
			
			stopwatch.Stop();
			double outsideS = stopwatch.Elapsed.Milliseconds;

			
			stopwatch.Start();

			BoolLocationInside(count);
			
			stopwatch.Stop();
			double insideS = stopwatch.Elapsed.Milliseconds;

			Console.WriteLine( "TESTING!" );
			if( insideS > outsideS )
				Assert.Inconclusive ( " " + insideS + " > " + outsideS );
			else
				Assert.Fail(  " " + insideS + " < " + outsideS );
		}

		public void EmptyFunction()
		{

		}

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


		public void SetupLocal()
		{
			// assign variables that have to do with this class only
			//System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
			//stopwatch.Start();

			//stopwatch.Stop();
			//stopwatch.Elapsed.Seconds;
		}
		
		public void SetupGlobal()
		{
			// lookup references to objects / scripts outside of this script
		}
		
		protected void Awake()
		{
			SetupLocal();
		}

		protected void Start () 
		{
			SetupGlobal();
		}
		
		protected void Update () 
		{
		
		}
	}
}
