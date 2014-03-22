using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ILugusStopwatch
{
	void Start();
	double Stop(); // returns time in milliseconds

	double Milliseconds();
}

public class LugusStopwatch
{
	private static ILugusStopwatch _instance = null;
	
	public static ILugusStopwatch use 
	{ 
		get 
		{
			if ( _instance == null )
			{
				_instance = new LugusStopwatchImplementation();
			}
			
			
			return _instance; 
		}
	}

	public static void Start()
	{
		use.Start();
	}
	
	public static  double Stop()
	{
		return use.Stop();
	}

	public static  double Milliseconds()
	{
		return use.Milliseconds();
	}
}

public class LugusStopwatchImplementation : ILugusStopwatch
{
	public System.Diagnostics.Stopwatch stopwatch = null;

	public void Start()
	{
		if( stopwatch == null )
		{
			stopwatch = new System.Diagnostics.Stopwatch(); 
		}

		stopwatch.Reset();
		stopwatch.Start();
	}

	public double Stop()
	{
		if( stopwatch == null )
			return 0;

		stopwatch.Stop();
		return stopwatch.Elapsed.Milliseconds;
	}

	public double Milliseconds()
	{
		if( stopwatch == null )
			return 0;
		
		return stopwatch.Elapsed.Milliseconds;
	}
}
