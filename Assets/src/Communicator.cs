using UnityEngine;
using System.Collections;

public static class Communicator {

	static int _score;
	static int _killcnt;
	static int _time;

	static public void setScore(int value)
	{
		_score = value;
	}

	static public int getScore()
	{
		return _score;
	}

	static public void setKillCnt(int value)
	{
		_killcnt = value;
	}

	static public int getKillCnt()
	{
		return _killcnt;
	}

	static public void setTime(int value)
	{
		_time = value;
	}

	static public int getTime()
	{
		return _time;
	}

}
