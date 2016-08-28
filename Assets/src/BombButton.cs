using UnityEngine;
using System.Collections;
using System;

public class BombButton : MonoBehaviour
{
	public void OnClick(int name)
	{
		GameMediator.bombType = name;
	}
}